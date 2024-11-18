namespace FolderCleanserBackEndLibrary.Models;

public class PathModel
{
    public int Id { get; set; }
    public string Path { get; set; }
    public int RetentionDays { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }
}
