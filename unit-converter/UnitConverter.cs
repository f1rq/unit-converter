using System;
using System.Collections.Generic;
using System.Linq;
using unit_converter.Units;

namespace unit_converter;

public class UnitConverter
{
    // Sample units by category
    private readonly Dictionary<string, Func<IEnumerable<string>>> _unitsByCategory =
        new()
        {
            { "Length", () => LengthUnits.Factors.Keys },
            { "Weight", () => WeightUnits.Factors.Keys },
            { "Area", () => AreaUnits.Factors.Keys },
            { "Volume", () => VolumeUnits.Factors.Keys },
            { "Currency", () => CurrencyRates.GetAvailableCurrencies() },
            { "Data", () => DataUnits.Units.Keys },
            { "Time", () => TimeUnits.Factors.Keys },
        };
    
    public IEnumerable<string> GetCategories()
        => _unitsByCategory.Keys;
    
    public IEnumerable<string> GetUnits(string category)
        => _unitsByCategory[category]();

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
            "Volume" => VolumeUnits.Factors,
            "Currency" => CurrencyRates.Rates,
            "Data" => DataUnits.Units.ToDictionary(
                u => u.Key,
                u => u.Value.Factor
            ),
            "Time" => TimeUnits.Factors,
            _ => throw new ArgumentException("Unknown category")
        };
        
        double baseValue = value * factors[from];
        return baseValue / factors[to];
    }
}