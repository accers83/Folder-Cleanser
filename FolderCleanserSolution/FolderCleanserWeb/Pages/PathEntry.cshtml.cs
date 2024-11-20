using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FolderCleanserWeb.Pages;

public class PathEntryModel : PageModel
{
    private readonly IFolderCleanserApiRepository _data;

    [BindProperty]
    public PathModel Path { get; set; }

    public PathEntryModel(IFolderCleanserApiRepository data)
    {
        _data = data;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        await _data.AddPathAsync(Path);

        return RedirectToPage("./Index");
    }
}
