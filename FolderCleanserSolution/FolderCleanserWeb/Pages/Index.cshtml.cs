using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FolderCleanserWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDataCleanserApiRepository _data;
    public List<PathModel> Paths { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IDataCleanserApiRepository data)
    {
        _logger = logger;
        _data = data;
    }

    public async Task OnGetAsync()
    {
        Paths = await _data.GetPathsAsync();
    }
}
