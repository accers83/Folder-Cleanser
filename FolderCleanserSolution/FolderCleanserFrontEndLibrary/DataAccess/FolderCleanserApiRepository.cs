using FolderCleanserFrontEndLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace FolderCleanserFrontEndLibrary.DataAccess;

public class FolderCleanserApiRepository : IFolderCleanserApiRepository
{
    private readonly ILogger<FolderCleanserApiRepository> _logger;
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseApiUrl;

    public FolderCleanserApiRepository(ILogger<FolderCleanserApiRepository> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
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

    public async Task AddPathAsync(PathModel path)
    {
        var requestUri = _baseApiUrl + "/api/Path/";
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsync(requestUri,
                                              new StringContent(JsonSerializer.Serialize(path), encoding: Encoding.UTF8, "application/json"));
    }
}
