using FolderCleanserFrontEndLibrary.Models;

namespace FolderCleanserFrontEndLibrary.DataAccess;

public interface IFolderCleanserApiRepository
{
    Task AddPathAsync(PathModel path);
    Task DeletePathAsync(int id);
    Task<PathModel> GetPathAsync(int id);
    Task<List<PathModel>> GetPathsAsync();
    Task<List<SummaryHistoryModel>> GetSummaryHistoriesAsync(int pathId = 0);
}