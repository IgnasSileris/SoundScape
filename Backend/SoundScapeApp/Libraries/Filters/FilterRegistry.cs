using System;
using System.Collections.Generic;

namespace SoundScapeApp.Libraries.Filters;

public class FilterRegistry
{
    private readonly Dictionary<string, string> Filters = [];
    public string? GetFilter(string filterId)
    {
        if (!Filters.TryGetValue(filterId, out string? value))
        {
            return null;
        }

        return value;
    }
}