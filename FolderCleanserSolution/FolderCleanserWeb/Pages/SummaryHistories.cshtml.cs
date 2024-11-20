using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FolderCleanserWeb.Pages;

public class SummaryHistoriesModel : PageModel
{
    private readonly IFolderCleanserApiRepository _data;

    [BindProperty]
    public List<SummaryHistoryModel> SummaryHistories { get; set; }

    public SummaryHistoriesModel(IFolderCleanserApiRepository data)
    {
        _data = data;
    }

    public async Task OnGet()
    {
        SummaryHistories = await _data.GetSummaryHistoriesAsync();
    }
}
