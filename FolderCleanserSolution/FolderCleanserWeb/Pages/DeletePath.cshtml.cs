using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FolderCleanserWeb.Pages;

public class DeletePathModel : PageModel
{
    private readonly IFolderCleanserApiRepository _data;

    [Required]
    [BindProperty(SupportsGet = true)]
    public int PathId { get; set; }

    [BindProperty]
    public bool ConfirmDelete { get; set; } = false;

    [BindProperty]
    public PathModel Path { get; set; }

    public DeletePathModel(IFolderCleanserApiRepository data)
    {
        _data = data;
    }

    public async Task OnGetAsync()
    {
        Path = await _data.GetPathAsync(PathId);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ConfirmDelete == false)
        {
            return RedirectToPage("/DeletePath", new { PathId });
        }

        await _data.DeletePathAsync(PathId);
        return RedirectToPage("./Index");
    }
}
