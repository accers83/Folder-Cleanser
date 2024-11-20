using FolderCleanserFrontEndLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FolderCleanserFrontEndLibrary.DataAccess;

public class DataCleanserApiRepository : IDataCleanserApiRepository
{
    private readonly ILogger<DataCleanserApiRepository> _logger;
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseApiUrl;

    public DataCleanserApiRepository(ILogger<DataCleanserApiRepository> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _config = config;
        _httpClientFactory = httpClientFactory;
        _baseApiUrl = _config.GetValue<string>("FolderCleanserApiBaseUrl");
    }

    public async Task<List<PathModel>> GetPathsAsync()
    {
        var requestUri = _baseApiUrl + "/api/Path/";
        List<PathModel> output = new();

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
        {
            var responseText = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            output = JsonSerializer.Deserialize<List<PathModel>>(responseText, options);
        }

        return output;
    }
}
