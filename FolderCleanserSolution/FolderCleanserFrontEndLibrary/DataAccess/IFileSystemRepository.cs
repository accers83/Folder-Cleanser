

namespace FolderCleanserFrontEndLibrary.DataAccess;

public interface IFileSystemRepository
{
    void DeleteFile(string path);
    List<string> GetAllFiles(string path);
    DateTime GetFileLastWriteTime(string path);
    double GetFileSizeMB(string path);
}