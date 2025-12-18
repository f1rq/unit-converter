using System.Collections.Generic;

namespace unit_converter.Units;

public class WeightUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "mg", 1e-6 },
        { "g", 0.001 },
        { "kg", 1 },
        { "t", 1000},
        { "lb", 0.453592 },
        { "oz", 0.0283495 }
    };
}