using FolderCleanserBackEndLibrary.Models;
using FolderCleanserBackEndLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FolderCleanserAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PathController : ControllerBase
{
    private readonly IFolderCleanserRepository _folderCleanserRepository;
    private readonly ILogger<PathController> _logger;

    public PathController(IFolderCleanserRepository folderCleanserRepository, ILogger<PathController> logger)
    {
        _folderCleanserRepository = folderCleanserRepository;
        _logger = logger;
    }

    // GET: api/<PathController>
    [HttpGet]
    public List<PathModel> Get()
    {
        var paths = _folderCleanserRepository.GetPaths();
        return paths;
    }

    // POST api/<PathController>
    [HttpPost]
    public void Post([FromBody] PathModel data)
    {
        if (ModelState.IsValid == false)
        {
            _logger.LogError("Invalid request, model state is not valid");
            throw new BadHttpRequestException("Invalid request, model state is not valid", 400);
        }
        else
        {
            var path = _folderCleanserRepository.GetPath(data.Path);

            if (path is not null)
            {
                _logger.LogError("Invalid request, path already exists");
                throw new BadHttpRequestException("Invalid request, path already exists", 400);
            }

            _logger.LogInformation("Adding new path: {path}", data);
            _folderCleanserRepository.AddPath(data);
        }
    }

    // DELETE api/<PathController>/5
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        bool isValidBool = int.TryParse(id, out int value);

        if (string.IsNullOrWhiteSpace(id))
        {
            _logger.LogError("Invalid request, no Id provided");
            throw new BadHttpRequestException("Invalid request, no Id provided", 400);
        }
        else if (isValidBool == false)
        {
            _logger.LogError("Invalid request, invalid Id provided");
            throw new BadHttpRequestException("Invalid request, invalid Id provided", 400);
        }
        else
        {
            var path = _folderCleanserRepository.GetPath(value);

            if (path is null)
            {
                _logger.LogError("Invalid request, id not found");
                throw new BadHttpRequestException("Invalid request, id not found", 400);
            }
            else if (path.Deleted is not null)
            {
                _logger.LogError("Invalid request, path already deleted");
                throw new BadHttpRequestException("Invalid request, path already deleted", 400);
            }

            _folderCleanserRepository.DeletePath(value);
        }
    }
}
