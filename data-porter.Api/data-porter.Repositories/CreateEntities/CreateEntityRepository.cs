using core.Models.AppSettings;
using data_porter.Models.Models.Upload.CreateEntities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Text;

namespace data_porter.Repositories.CreateEntities;

/// <summary>
/// Create Entity Repository
/// </summary>
public class CreateEntityRepository : CreateEntityDecorator
{
    private readonly string _connectionString;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="connectionStringSettings">Connection String Settings. Implements <see cref="IOptions{ConnectionStringSettings}"/></param>
    public CreateEntityRepository(IOptions<ConnectionStringSettings> connectionStringSettings)
    {
        _connectionString = connectionStringSettings.Value.PostgresConnectionString;
    }

    /// <inheritdoc/>
    public override async Task<CreateEntityResponse> CreateEntityAsync(string fileName, JArray jsonArray)
    {
        using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        var tableName = fileName.Replace('-', '_');
        // Drop the table if it exists
        //using NpgsqlCommand dropTableCommand = new NpgsqlCommand($"DROP TABLE IF EXISTS {tableName};", connection);
        //await dropTableCommand.ExecuteNonQueryAsync();

        // Create the table dynamically
        JObject firstObject = (jsonArray[0] as JObject)!;
        StringBuilder createTableQuery = new StringBuilder($"CREATE TABLE {tableName} (uuid uuid not null constraint pk_{tableName}_uuid PRIMARY KEY");

        foreach (var property in firstObject.Properties())
        {
            createTableQuery.Append($", {property.Name}");

            string columnType = GetColumnType(property.Value.Type);
            createTableQuery.Append($" {columnType} NOT NULL");
        }
        createTableQuery.Append(");");

        using NpgsqlCommand createTableCommand = new NpgsqlCommand(createTableQuery.ToString(), connection);
        await createTableCommand.ExecuteNonQueryAsync();

        // Insert the data from the dynamic list into the table
        foreach (JObject item in jsonArray)
        {
            StringBuilder insertQuery = new StringBuilder($"INSERT INTO {tableName} (");
            StringBuilder columnNames = new StringBuilder();
            StringBuilder columnValues = new StringBuilder();

            foreach (var property in item.Properties())
            {
                columnNames.Append($"{property.Name}, ");
                columnValues.Append($"@{property.Name}, ");
            }
            columnNames.Length -= 2; // Remove the last comma and space
            columnValues.Length -= 2; // Remove the last comma and space

            insertQuery.Append($"{columnNames}) VALUES ({columnValues});");

            using NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery.ToString(), connection);
            foreach (var property in item.Properties())
            {
                insertCommand.Parameters.AddWithValue(property.Name, Convert.ToString(property.Value)!);
            }

            await insertCommand.ExecuteNonQueryAsync();
        }

        return new CreateEntityResponse(tableName);
    }

    private static string GetColumnType(JTokenType tokenType)
    {
        switch (tokenType)
        {
            case JTokenType.Integer:
                return "INTEGER";
            case JTokenType.Float:
                return "DOUBLE PRECISION";
            case JTokenType.String:
                return "VARCHAR(255)";
            case JTokenType.Boolean:
                return "BOOLEAN";
            case JTokenType.Date:
                return "TIMESTAMP";
            default:
                throw new ArgumentException("Unsupported data type.");
        }
    }
}
