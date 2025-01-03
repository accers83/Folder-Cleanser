using FolderCleanserFrontEndLibrary.Models;

namespace FolderCleanserConsole.Processors
{
    public interface IFolderCleanserProcessor
    {
        SummaryHistoryModel ProcessPath(PathModel path);
    }
}