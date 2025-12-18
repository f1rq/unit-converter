using System.Collections.Generic;

namespace unit_converter.Units;

public class DataUnit {
    public string Key { get; init; }     // "MB"
    public string Label { get; init; }   // "MB = megabyte"
    public double Factor { get; init; }

    public override string ToString() => Label;
    
    public DataUnit(string key, string label, double factor)
    {
        Key = key;
        Label = label;
        Factor = factor;
    }
}

public class DataUnits
{
    public static readonly IReadOnlyDictionary<string, DataUnit> Units =
        new Dictionary<string, DataUnit>
        {
            ["B"] = new("B", "B - byte", 1),
            ["KB"] = new("KB", "KB - kilobyte", 1024),
            ["MB"] = new("MB", "MB - megabyte", 1024 * 1024),
            ["GB"] = new("GB", "GB - gigabyte", 1024 * 1024 * 1024),
            ["TB"] = new("TB", "TB - terabyte", 1024L * 1024 * 1024 * 1024),
            ["PB"] = new("PB", "PB - petabyte", 1024L * 1024 * 1024 * 1024 * 1024),

            ["b"] = new("b", "b - bit", 1.0 / 8),
            ["Kb"] = new("Kb", "Kb - kilobit", 1024.0 / 8),
            ["Mb"] = new("Mb", "Mb - megabit", (1024.0 * 1024) / 8),
            ["Gb"] = new("Gb", "Gb - gigabit", (1024.0 * 1024 * 1024) / 8),
            ["Tb"] = new("Tb", "Tb - terabit", (1024.0 * 1024 * 1024 * 1024) / 8),
            ["Pb"] = new("Pb", "Pb - petabit", (1024.0 * 1024 * 1024 * 1024 * 1024) / 8),

            ["KiB"] = new("KiB", "KiB - kibibyte", 1024),
            ["MiB"] = new("MiB", "MiB - mebibyte", 1024 * 1024),
            ["GiB"] = new("GiB", "GiB - gibibyte", 1024 * 1024 * 1024),
            ["TiB"] = new("TiB", "TiB - tebibyte", 1024L * 1024 * 1024 * 1024),
            ["PiB"] = new("PiB", "PiB - pebibyte", 1024L * 1024 * 1024 * 1024 * 1024),
        };
}