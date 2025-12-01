using System.Collections.Generic;

namespace unit_converter.Units;

public class AreaUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "m²" , 1 },
        { "km²", 1000000 },
        { "mi²", 2589988.11 },
        { "acres", 4046.86 },
        { "hectares", 10000 }
    };
}