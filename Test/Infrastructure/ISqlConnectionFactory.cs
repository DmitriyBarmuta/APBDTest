using Microsoft.Data.SqlClient;

namespace Test.Infrastructure;

public interface ISqlConnectionFactory
{
    SqlConnection GetConnection();
}