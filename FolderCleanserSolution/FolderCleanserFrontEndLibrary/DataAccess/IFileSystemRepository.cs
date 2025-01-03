

namespace FolderCleanserFrontEndLibrary.DataAccess;

public interface IFileSystemRepository
{
    List<string> GetAllFiles(string path);
    DateTime GetFileLastWriteTime(string path);
    double GetFileSizeMB(string path);
}