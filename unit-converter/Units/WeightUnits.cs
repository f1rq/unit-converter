using System.Collections.Generic;

namespace unit_converter.Units;

public class WeightUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "g", 0.001 },
        { "kg", 1 },
        { "lb", 0.453592 },
        { "oz", 0.0283495 }
    };
}