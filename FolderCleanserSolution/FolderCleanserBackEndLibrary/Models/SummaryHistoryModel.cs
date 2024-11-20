namespace FolderCleanserBackEndLibrary.Models;

public class SummaryHistoryModel
{
    public int Id { get; set; }
    public int PathId { get; set; }
    public DateTime ProcessingStartDateTime { get; set; }
    public DateTime ProcessingEndDateTime { get; set; }
    public int ProcessingDurationMins { get; set; }
    public int FilesDeletedCount { get; set; }
    public double FileSizeDeletedMB { get; set; }
}
