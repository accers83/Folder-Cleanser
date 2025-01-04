using FolderCleanserBackEndLibrary.DataAccess;
using FolderCleanserBackEndLibrary.Models;

namespace FolderCleanserBackEndLibrary.Repositories;

public class FolderCleanserRepository : IFolderCleanserRepository
{
    private readonly ISqlDataAccess _db;

    public FolderCleanserRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public List<PathModel> GetPaths()
    {
        var output = _db.LoadData<PathModel, dynamic>("select * from dbo.[Path] where Deleted is null",
                                                      new { },
                                                      "FolderCleanserDB");

        return output;
    }

    public PathModel GetPath(int id)
    {
        return GetPath("", id);
    }

    public PathModel GetPath(string path)
    {
        return GetPath(path, 0);
    }

    private PathModel GetPath(string path, int id)
    {
        string sqlStatement = string.Empty;
        var connectionStringName = "FolderCleanserDB";
        PathModel output;

        if (string.IsNullOrWhiteSpace(path) == false)
        {
            sqlStatement = "select * from dbo.[Path] where [Path] = @Path and Deleted is null";
            output = _db.LoadData<PathModel, dynamic>(sqlStatement,
                                                      new { Path = path },
                                                      connectionStringName).FirstOrDefault();
        }
        else if (id != 0)
        {
            sqlStatement = "select * from dbo.[Path] where Id = @Id and Deleted is null";
            output = _db.LoadData<PathModel, dynamic>(sqlStatement,
                                                      new { Id = id },
                                                      connectionStringName).FirstOrDefault();
        }
        else
        {
            throw new ArgumentException("No valid Id or Path provided");
        }

        return output;
    }

    public void AddPath(PathModel path)
    {
        _db.SaveData<dynamic>("dbo.spPath_Insert",
                              new { path.Path, path.RetentionDays },
                              "FolderCleanserDB",
                              true);
    }

    public void DeletePath(int id)
    {
        _db.SaveData<dynamic>("dbo.spPath_DeleteById",
                              new { Id = id },
                              "FolderCleanserDB",
                              true);
    }

    public List<SummaryHistoryModel> GetSummaryHistories(int pathId = 0)
    {
        List<SummaryHistoryModel> output = new();
        var connectionStringName = "FolderCleanserDB";

        if (pathId == 0)
        {
            output = _db.LoadData<SummaryHistoryModel, dynamic>("dbo.spSummaryHistory_Get",
                                                              new { },
                                                              connectionStringName,
                                                              true);
        }
        else
        {
            output = _db.LoadData<SummaryHistoryModel, dynamic>("dbo.spSummaryHistory_GetByPathId",
                                                                          new { PathId = pathId },
                                                                          connectionStringName,
                                                                          true);
        }

        return output;
    }

    public void AddSummaryHistory(SummaryHistoryModel summaryHistory)
    {
        _db.SaveData<dynamic>("dbo.spSummaryHistory_Insert",
                              new
                              {
                                  summaryHistory.PathId,
                                  summaryHistory.ProcessingStartDateTime,
                                  summaryHistory.ProcessingEndDateTime,
                                  summaryHistory.ProcessingDurationMins,
                                  summaryHistory.FilesDeletedCount,
                                  summaryHistory.FileSizeDeletedMB
                              },
                              "FolderCleanserDB",
                              true);
    }
}
