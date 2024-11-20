using FolderCleanserBackEndLibrary.Models;
using FolderCleanserBackEndLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FolderCleanserAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SummaryHistoryController : ControllerBase
{
    private readonly IFolderCleanserRepository _folderCleanserRepository;
    private readonly ILogger<SummaryHistoryController> _logger;

    public SummaryHistoryController(IFolderCleanserRepository folderCleanserRepository, ILogger<SummaryHistoryController> logger)
    {
        _folderCleanserRepository = folderCleanserRepository;
        _logger = logger;
    }

    // GET: api/<SummaryHistoryController>
    [HttpGet]
    public List<SummaryHistoryModel> Get()
    {
        var summaryHistory = _folderCleanserRepository.GetSummaryHistory();
        return summaryHistory;
    }

    // GET: api/<SummaryHistoryController>/5
    [HttpGet("{pathId}")]
    public List<SummaryHistoryModel> Get(string pathId)
    {

        bool isValidBool = int.TryParse(pathId, out int value);

        if (string.IsNullOrWhiteSpace(pathId))
        {
            _logger.LogError("Invalid request, no pathId provided");
            throw new BadHttpRequestException("Invalid request, no pathId provided", 400);
        }
        else if (isValidBool == false)
        {
            _logger.LogError("Invalid request, invalid pathId provided");
            throw new BadHttpRequestException("Invalid request, invalid pathId provided", 400);
        }
        else
        {
            var path = _folderCleanserRepository.GetPath(value);

            if (path is null)
            {
                _logger.LogError("Invalid request, pathId not found");
                throw new BadHttpRequestException("Invalid request, pathId not found", 400);
            }
            else if (path.Deleted is not null)
            {
                _logger.LogError("Invalid request, path is deleted");
                throw new BadHttpRequestException("Invalid request, path is deleted", 400);
            }

            var summaryHistory = _folderCleanserRepository.GetSummaryHistory(value);
            return summaryHistory;
        }


    }

    // POST api/<SummaryHistoryController>
    [HttpPost]
    public void Post([FromBody] SummaryHistoryModel data)
    {
        if (ModelState.IsValid == false)
        {
            _logger.LogError("Invalid request, model state is not valid");
            throw new BadHttpRequestException("Invalid request, model state is not valid", 400);
        }

        var path = _folderCleanserRepository.GetPath(data.PathId);

        if (path is null)
        {
            _logger.LogError("Invalid request, pathId not found");
            throw new BadHttpRequestException("Invalid request, pathId not found", 400);
        }

        _logger.LogInformation("Adding new summary history: {summaryHistory}", data);
        _folderCleanserRepository.AddSummaryHistory(data);
    }
}
