using System.Collections.Generic;

namespace unit_converter.Units;

public class TimeUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "ms", 0.001 },
        { "s", 1 },
        { "min", 60 },
        { "h", 3600 },
        { "day", 86400 },
        { "week", 604800 },
        { "month", 2592000 }, // avg 30 days
        { "year", 31536000 }, // 365 days
        { "μs", 1e-6 },
        { "ns", 1e-9 }
    };
}