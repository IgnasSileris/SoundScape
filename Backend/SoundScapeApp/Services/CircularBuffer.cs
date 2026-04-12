using System;

namespace SoundScapeApp.Services;

/// <summary>
/// Circular Buffer implementation with overwrite.
/// </summary>
public class CircularBuffer<T>(int _capacity)
{
    private readonly int capacity = _capacity;
    private readonly T[] buffer = new T[_capacity];
    private int writeIndex = 0;
    private int readIndex = 0;
    private int count = 0;


    public bool Write(T dataToWrite)
    {
        if (count == capacity)
        {
            // buffer full (fully unread)
            readIndex = (readIndex + 1) % capacity;
        }
        else
        {
            count++;
        }

        buffer[writeIndex] = dataToWrite;
        writeIndex = (writeIndex + 1) % capacity;

        return true;
    }

    public int BulkWrite(ReadOnlySpan<T> bulkDataToWrite)
    {
        int startIndex = 0;
        int toWrite = bulkDataToWrite.Length;
        if (toWrite > capacity)
        {
            // only take the latest data
            toWrite = capacity;
            startIndex = bulkDataToWrite.Length - capacity;
        }

        // write up to the end of the buffer
        int firstPart = Math.Min(toWrite, capacity - writeIndex);

        bulkDataToWrite[startIndex..(startIndex + firstPart)].CopyTo(buffer.AsSpan(writeIndex, firstPart));

        if (firstPart < toWrite)
        {
            int secondPart = toWrite - firstPart;
            bulkDataToWrite[(startIndex + firstPart)..(startIndex + firstPart + secondPart)].CopyTo(buffer.AsSpan(0, secondPart));
        }

        int overwrites = Math.Max(0, count + toWrite - capacity);
        readIndex = (readIndex + overwrites) % capacity;

        writeIndex = (writeIndex + toWrite) % capacity;
        count = Math.Min(count + toWrite, capacity);

        return toWrite;
    }

    public bool Read(out T dataToReadTo)
    {
        if (count == 0)
        {
            // buffer is empty
            dataToReadTo = default!;
            return false;
        }

        dataToReadTo = buffer[readIndex];
        readIndex = (readIndex + 1) % capacity;
        count--;

        return true;
    }

    public int BulkRead(Span<T> bulkDataToWriteTo)
    {
        int toRead = Math.Min(bulkDataToWriteTo.Length, count);

        int firstPart = Math.Min(capacity - readIndex, toRead);

        buffer.AsSpan(readIndex, firstPart).CopyTo(bulkDataToWriteTo[..firstPart]);

        if (firstPart < toRead)
        {
            int secondPart = toRead - firstPart;
            buffer.AsSpan(firstPart, secondPart).CopyTo(bulkDataToWriteTo[firstPart..secondPart]);
        }

        readIndex = (readIndex + toRead) % capacity;
        count = Math.Max(0, count - toRead);

        return toRead;
    }
}