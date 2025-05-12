using System.Reflection;
using Test.Infrastructure;

namespace Test.Config;

public class DatabaseInitializer
{

    private readonly ISqlConnectionFactory _connectionFactory;
    
    public DatabaseInitializer(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        await using var connection = _connectionFactory.GetConnection();
        await connection.OpenAsync();

        var dropSql = ReadEmbeddedSql("drop1A.sql");
        await using var dropCmd = connection.CreateCommand();
        dropCmd.CommandText = dropSql;
        await dropCmd.ExecuteNonQueryAsync();
        
        var createSql = ReadEmbeddedSql("create1A.sql");
        await using var createCmd = connection.CreateCommand();
        createCmd.CommandText = createSql;
        await createCmd.ExecuteNonQueryAsync();
    }

    private static string ReadEmbeddedSql(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly
            .GetManifestResourceNames()
            .FirstOrDefault(x => x.Contains(fileName));

        if (resourceName == null)
            throw new FileNotFoundException($"Couldn't find embedded resource: {fileName}");

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}