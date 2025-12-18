using System.Collections.Generic;

namespace unit_converter.Units;

public class VolumeUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "ml", 0.001 },
        { "L", 1 },
        { "m³", 1000 },
        { "cm³", 0.001 },
        { "gal", 3.78541 },
        { "qt", 0.946353 },
        { "pt", 0.473176 },
        { "cup", 0.24 },
        { "fl oz", 0.0295735 }
    };
}