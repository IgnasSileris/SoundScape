namespace SoundScapeApp.Services;

public class CircularBuffer
{
    private const int Capacity = 10;
    private readonly byte[] _buffer = new byte[Capacity];
    private int _writeIndex = 0;
    private int _readIndex = 0;

    public bool Write(byte byteToWrite)
    {
        if ((_writeIndex + 1) % Capacity == _readIndex)
        {
            // buffer is full, skip
            return false;
        }

        _buffer[_writeIndex] = byteToWrite;
        _writeIndex = (_writeIndex + 1) % Capacity;
        return true;
    }

    public bool Read(ref byte byteToReadTo)
    {
        if (_readIndex == _writeIndex)
        {
            // buffer is empty
            return false;
        }

        byteToReadTo = _buffer[_readIndex];
        _readIndex = (_readIndex + 1) % Capacity;
        return true;
    }
}