using System.Collections.Generic;

namespace unit_converter.Units;

public class AreaUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "cm²", 0.0001 },
        { "m²" , 1 },
        { "km²", 1000000 },
        { "mi²", 2589988.11 },
        { "acres", 4046.86 },
        { "hectares", 10000 },
        { "in²", 0.00064516 },
        { "ft²", 0.092903 },
        { "yd²", 0.836127 }
    };
}