using System.Collections.Generic;

namespace unit_converter.Units;

public class PressureUnits
{
    public static readonly Dictionary<string, double> Factors = new()
    {
        { "Pa", 1},
        { "hPa", 100},
        { "bar", 100000},
        { "psi", 6894.76},
        { "kPa", 1000},
        { "MPa", 1e6},
        { "atm", 101_325},
        { "Torr", 133.322},
    };
}