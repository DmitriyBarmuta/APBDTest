using Test.Infrastructure;
using Test.Model.Appointment;

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
    
    public async Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        const string sql  = "SELECT * FROM Appointment WHERE Appointment_Id = @AppointmentId;";

        await using var conn = _connectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@AppointmentId", id);

        await conn.OpenAsync(cancellationToken);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken)) return null;
        
        return new Appointment
        {
            AppointmentId = reader.GetInt32(reader.GetOrdinal("Appointment_Id")),
            PatientId = reader.GetInt32(reader.GetOrdinal("Patient_Id")),
            DoctorId = reader.GetInt32(reader.GetOrdinal("Doctor_Id")),
            Date = reader.GetDateTime(reader.GetOrdinal("Date"))
        };
    }
}