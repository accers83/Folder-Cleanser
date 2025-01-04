using FolderCleanserBackEndLibrary.Models;

namespace FolderCleanserBackEndLibrary.Repositories
{
    public interface IFolderCleanserRepository
    {
        void AddPath(PathModel path);
        void AddSummaryHistory(SummaryHistoryModel summaryHistory);
        void DeletePath(int id);
        PathModel GetPath(int id);
        PathModel GetPath(string path);
        List<PathModel> GetPaths();
        List<SummaryHistoryModel> GetSummaryHistories(int pathId = 0);
    }
}