using System.Collections.Generic;

namespace unit_converter.Units;

public class LengthUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "cm", 0.01 },
        { "m", 1 },
        { "km", 1000 },
        { "mi", 1609.34 }
    };
}