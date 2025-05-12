using Test.Infrastructure;

namespace Tutorial9.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public AppointmentRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    /*
     * ───────────────────────────────────────────────────────────────
     *      ADO.NET DbCommand / SqlCommand Execution Options
     * ───────────────────────────────────────────────────────────────
     *
     * 1. ExecuteNonQuery()
     *    → Use for: INSERT, UPDATE, DELETE, or DDL (CREATE TABLE, etc.)
     *    → Returns: int (number of rows affected, or -1 for DDL)
     *
     * 2. ExecuteScalar()
     *    → Use for: returning a single value (e.g., SELECT COUNT(*))
     *    → Returns: object (first column of first row only)
     *
     * 3. ExecuteReader()
     *    → Use for: reading tabular data (multiple rows/columns)
     *    → Returns: DbDataReader (loop with .Read())
     *
     * 4. ExecuteXmlReader()
     *    → Use for: queries that return XML (e.g., SELECT ... FOR XML)
     *    → Returns: XmlReader
     *
     * ───────────────────────────────────────────────────────────────
     *      Async Variants (for non-blocking I/O)
     * ───────────────────────────────────────────────────────────────
     *
     * ExecuteNonQueryAsync(CancellationToken)
     * ExecuteScalarAsync(CancellationToken)
     * ExecuteReaderAsync(CancellationToken)
     * ExecuteXmlReaderAsync()   // No CancellationToken overload
     *
     * ───────────────────────────────────────────────────────────────
     *      Other Command Configuration
     * ───────────────────────────────────────────────────────────────
     *
     * CommandType.Text               → Default for raw SQL
     * CommandType.StoredProcedure   → Use when executing a stored proc
     *
     * cmd.Parameters.AddWithValue("@param", value); → Always prefer parameters for security & type safety
     */
}