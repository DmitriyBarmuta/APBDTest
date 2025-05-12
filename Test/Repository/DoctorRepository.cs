using Test.Infrastructure;
using Test.Model.Doctor;
using Tutorial9.Repository;

namespace Test.Repository;

public class DoctorRepository : IDoctorRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public DoctorRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<Doctor?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        const string sql  = "SELECT * FROM Doctor WHERE Doctor_Id = @DoctorId";

        await using var conn = _connectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@DoctorId", id);

        await conn.OpenAsync(cancellationToken);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken)) return null;
        
        return new Doctor
        {
            Id = reader.GetInt32(reader.GetOrdinal("Doctor_Id")),
            FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
            LastName = reader.GetString(reader.GetOrdinal("Last_Name")),
            Pwz = reader.GetString(reader.GetOrdinal("Pwz"))
        };
    }

    public async Task<Doctor?> GetByPwz(string pwz, CancellationToken cancellationToken)
    {
        const string sql  = "SELECT * FROM Doctor WHERE Pwz = @DoctorPwz";

        await using var conn = _connectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@DoctorPwz", pwz);

        await conn.OpenAsync(cancellationToken);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken)) return null;
        
        return new Doctor
        {
            Id = reader.GetInt32(reader.GetOrdinal("Doctor_Id")),
            FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
            LastName = reader.GetString(reader.GetOrdinal("Last_Name")),
            Pwz = reader.GetString(reader.GetOrdinal("Pwz"))
        };
    }
}