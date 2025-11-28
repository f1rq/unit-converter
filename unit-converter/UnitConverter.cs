using System;
using System.Collections.Generic;

namespace unit_converter;

public class UnitConverter
{
    // Sample units by category
    private readonly Dictionary<string, List<string>> _unitsByCategory =
        new()
        {
            { "Length", new() { "cm", "m", "km", "mi" } },
            { "Weight", new() { "g", "kg", "lb", "oz" } },
            { "Area", new() { "m²", "km²", "mi²", "acres", "hectares" } }, // TODO: Implement Area conversion
            { "Currency", new() { "USD", "EUR", "GBP", "PLN", "JPY" } } // TODO: Implement Currency conversion
        };
    
    public IEnumerable<string> GetCategories()
        => _unitsByCategory.Keys;
    
    public IEnumerable<string> GetUnits(string category)
        => _unitsByCategory[category];

    public double Convert(string category, string from, string to, double value)
    {
        var factors = category switch
        {
            "Length" => new Dictionary<string, double>
            {
                { "cm", 0.01 },
                { "m", 1 },
                { "km", 1000 },
                { "mi", 1609.34 }
            },
            "Weight" => new Dictionary<string, double>
            {
                { "g", 0.001 },
                { "kg", 1 },
                { "lb", 0.453592 },
                { "oz", 0.0283495 }
            },
            "Area" => new Dictionary<string, double>
            {
                { "m²" , 1 },
                { "km²", 1000000 },
                { "mi²", 2589988.11 },
                { "acres", 4046.86 },
                { "hectares", 10000 }
            },
            _ => throw new ArgumentException("Unknown category")
        };
        
        double valueInBase = value * factors[from];
        double converted = valueInBase / factors[to];
        return converted;
    }
}