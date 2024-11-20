﻿using FolderCleanserFrontEndLibrary.Models;

namespace FolderCleanserFrontEndLibrary.DataAccess;

public interface IFolderCleanserApiRepository
{
    Task AddPathAsync(PathModel path);
    Task<List<PathModel>> GetPathsAsync();
    Task<List<SummaryHistoryModel>> GetSummaryHistoriesAsync(int pathId = 0);
}