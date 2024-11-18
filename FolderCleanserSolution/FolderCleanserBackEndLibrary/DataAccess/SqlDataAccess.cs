using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FolderCleanserBackEndLibrary.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public List<T> LoadData<T, U>(string sqlStatement,
                                 U parameters,
                                 string connectionStringName,
                                 bool isStoredProcedure = false)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);
        CommandType commandType = CommandType.Text;

        if (isStoredProcedure)
        {
            commandType = CommandType.StoredProcedure;
        }

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(sqlStatement, parameters, commandType: commandType).ToList();
            return rows;
        }
    }

    public void SaveData<T>(string sqlStatement,
                        T parameters,
                        string connectionStringName,
                        bool isStoredProcedure = false)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);
        CommandType commandType = CommandType.Text;

        if (isStoredProcedure)
        {
            commandType = CommandType.StoredProcedure;
        }

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(sqlStatement, parameters, commandType: commandType);
        }
    }
}
