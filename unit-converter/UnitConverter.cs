using System;
using System.Collections.Generic;
using unit_converter.Units;

namespace unit_converter;

public class UnitConverter
{
    // Sample units by category
    private readonly Dictionary<string, List<string>> _unitsByCategory =
        new()
        {
            { "Length", new() { "cm", "m", "km", "mi" } },
            { "Weight", new() { "g", "kg", "lb", "oz" } },
            { "Area", new() { "m²", "km²", "mi²", "acres", "hectares" } },
            { "Currency", new() { "USD", "EUR", "GBP", "PLN", "JPY" } }
        };
    
    public IEnumerable<string> GetCategories()
        => _unitsByCategory.Keys;
    
    public IEnumerable<string> GetUnits(string category)
        => _unitsByCategory[category];

    public double Convert(string category, string from, string to, double value)
    {
        if (category == "Currency")
        {
            double valueInUsd = value / CurrencyRates.Rates[from];
            return valueInUsd * CurrencyRates.Rates[to];
        }
        
        var factors = category switch
        {
            "Length" => LengthUnits.Factors,
            "Weight" => WeightUnits.Factors,
            "Area" => AreaUnits.Factors,
            "Currency" => CurrencyRates.Rates,
            _ => throw new ArgumentException("Unknown category")
        };
        
        double baseValue = value * factors[from];
        return baseValue / factors[to];
    }
}