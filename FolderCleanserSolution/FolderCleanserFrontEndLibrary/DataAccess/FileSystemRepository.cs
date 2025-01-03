﻿namespace FolderCleanserFrontEndLibrary.DataAccess;

public class FileSystemRepository : IFileSystemRepository
{
    public List<string> GetAllFiles(string path)
    {
        List<string> output = new();

        if (Directory.Exists(path) == false)
        {
            throw new ArgumentException("Path does not exist, or an occured when trying to determine if it exists.");
        }

        output = Directory.GetFiles(path, "*.*", searchOption: SearchOption.AllDirectories).ToList();
        return output;
    }

    public DateTime GetFileLastWriteTime(string path)
    {
        DateTime output = new();
        output = File.GetLastWriteTime(path);
        return output;
    }

    public double GetFileSizeMB(string path)
    {
        double output = 0;

        var fileSizeBytes = new FileInfo(path).Length;
        output = ((double)fileSizeBytes / 1024) / 1024;

        return output;
    }
}