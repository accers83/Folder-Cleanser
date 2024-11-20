using FolderCleanserFrontEndLibrary.Models;

namespace FolderCleanserFrontEndLibrary.DataAccess
{
    public interface IDataCleanserApiRepository
    {
        Task<List<PathModel>> GetPathsAsync();
    }
}