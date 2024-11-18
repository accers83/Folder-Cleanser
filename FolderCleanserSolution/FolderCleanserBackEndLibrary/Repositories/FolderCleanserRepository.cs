using FolderCleanserBackEndLibrary.DataAccess;
using FolderCleanserBackEndLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace FolderCleanserBackEndLibrary.Repositories;

public class FolderCleanserRepository
{
    private readonly IConfiguration _config;
    private readonly ISqlDataAccess _db;

    public FolderCleanserRepository(IConfiguration config, ISqlDataAccess db)
    {
        _config = config;
        _db = db;
    }

    public List<PathModel> GetPaths()
    {
        var output = _db.LoadData<PathModel, dynamic>("select * from dbo.[Path] where Deleted is not null",
                                                      new { },
                                                      _config.GetConnectionString("FolderCleanserDB"));

        return output;
    }

    public void AddPath(PathModel path)
    {
        _db.SaveData<dynamic>("dbo.spPath_Insert",
                              path,
                              _config.GetConnectionString("FolderCleanserDB"),
                              true);
    }

    public void DeletePath(int id)
    {
        _db.SaveData<dynamic>("dbo.spPath_DeleteById",
                              new { Id = id },
                              _config.GetConnectionString("FolderCleanserDB"),
                              true);
    }

    public List<SummaryHistoryModel> GetSummaryHistory(int pathId = 0)
    {
        List<SummaryHistoryModel> output = new();

        if (pathId == 0)
        {
            output = _db.LoadData<SummaryHistoryModel, dynamic>("dbo.spSummaryHistory_GetByPathId",
                                                                          new { PathId = pathId },
                                                                          _config.GetConnectionString("FolderCleanserDB"),
                                                                          true);
        }
        else
        {
            output = _db.LoadData<SummaryHistoryModel, dynamic>("select * from dbo.SummaryHistory",
                                                              new { },
                                                              _config.GetConnectionString("FolderCleanserDB"));
        }

        return output;
    }

    public void AddSummaryHistory(SummaryHistoryModel summaryHistory)
    {
        _db.SaveData<dynamic>("dbo.SummaryHistory_Insert",
                              summaryHistory,
                              _config.GetConnectionString("FolderCleanserDB"),
                              true);
    }
}
