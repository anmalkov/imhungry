using ImHungry.Api.Helpers;
using ImHungry.Api.Models;
using System.Globalization;

namespace ImHungry.Api.Repositories;

public class MobileFoodPointsRepository: IMobileFoodPointsRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MobileFoodPointsRepository> _logger;
    private readonly string? _csvDataUrl;

    public MobileFoodPointsRepository(IHttpClientFactory clientFactory, ILogger<MobileFoodPointsRepository> logger, IConfiguration configuration)
    {
        _httpClient = clientFactory.CreateClient();
        _logger = logger;

        _csvDataUrl = configuration["IMHUNGRY_CSV_DATA_URL"];
        if (_csvDataUrl is null)
        {
            _logger.LogError("Environment variable IMHUNGRY_CSV_DATA_URL is NOT set");
        } else
        {
            _logger.LogDebug($"Environment variable IMHUNGRY_CSV_DATA_URL = {_csvDataUrl}");
        }
    }

    public async Task<IEnumerable<MobileFoodPoint>> GetAllAsync()
    {
        var csvData = await GetCsvDataAsync(_csvDataUrl);

        var mobileFoodPoints = CsvHelper.MapCsvData(csvData, _logger);

        return mobileFoodPoints;
    }

    private async Task<string?> GetCsvDataAsync(string url)
    {

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Request to get CSV data was unsuccessfull. Status code: {response.StatusCode}, Content: {await response.Content.ReadAsStringAsync()}");
            return null;
        }

        var csvData = await response.Content.ReadAsStringAsync();
        if (csvData is null)
        {
            _logger.LogError($"Respose to get CSV data returned NO data");
            return null;
        }

        return csvData;
    }
}
