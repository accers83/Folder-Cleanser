using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FolderCleanserWeb.Pages;

public class SummaryHistoriesModel : PageModel
{
    private readonly IFolderCleanserApiRepository _data;

    [BindProperty(SupportsGet = true)]
    public int PathId { get; set; } = 0;

    [BindProperty]
    public List<SummaryHistoryModel> SummaryHistories { get; set; }

    public SummaryHistoriesModel(IFolderCleanserApiRepository data)
    {
        _data = data;
    }

    public async Task OnGet()
    {
        if (PathId > 0)
        {
            SummaryHistories = await _data.GetSummaryHistoriesAsync(PathId);
        }
        else
        {
            SummaryHistories = await _data.GetSummaryHistoriesAsync();
        }
    }
}
