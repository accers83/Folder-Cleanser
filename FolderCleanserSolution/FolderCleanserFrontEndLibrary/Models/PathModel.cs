using System.ComponentModel.DataAnnotations;

namespace FolderCleanserFrontEndLibrary.Models;

public class PathModel
{
    public int Id { get; set; }

    [Required]
    public string Path { get; set; }

    [Required]
    [Range(minimum: 0, maximum: 365)] 
    public int RetentionDays { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }
}