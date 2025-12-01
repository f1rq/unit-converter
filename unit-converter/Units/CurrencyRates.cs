using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace unit_converter.Units;

public class CurrencyRates
{
    public static Dictionary<string, double> Rates { get; private set; } = new();

    public static IEnumerable<string> GetAvailableCurrencies()
        => Rates.Keys;
    
    private static readonly HttpClient _http = new();

    public static async Task UpdateRatesAsync()
    {
        try
        {
            string url = "https://open.er-api.com/v6/latest/USD";
            var json = await _http.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            
            var data = JsonSerializer.Deserialize<ExchangeApiResponse>(json, options);

            if (data?.Rates != null)
                Rates = data.Rates;
        }
        catch
        {
            // In case of any error, keep existing rates
        }
    }
}

public class ExchangeApiResponse
{
    public Dictionary<string, double>? Rates { get; set; }
}