using System.Collections.Generic;

namespace unit_converter.Units;

public class LengthUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "mm", 0.001 },
        { "cm", 0.01 },
        { "m", 1 },
        { "km", 1000 },
        { "in", 0.0254 },
        { "ft", 0.3048 },
        { "yd", 0.9144 },
        { "mi", 1609.34 },
        { "nm", 1e-9 },
        { "µm", 1e-6 }
    };
}