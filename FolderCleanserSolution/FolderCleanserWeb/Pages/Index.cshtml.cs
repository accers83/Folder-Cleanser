using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FolderCleanserWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IFolderCleanserApiRepository _data;
    public List<PathModel> Paths { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IFolderCleanserApiRepository data)
    {
        _logger = logger;
        _data = data;
    }

    public async Task OnGetAsync()
    {
        Paths = await _data.GetPathsAsync();
    }
}
