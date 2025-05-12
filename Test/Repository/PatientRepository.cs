using Test.Infrastructure;
using Test.Model.Appointment;
using Test.Model.Patient;
using Tutorial9.Repository;

namespace Test.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public PatientRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<Patient?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        const string sql  = "SELECT * FROM Patient WHERE Patient_Id = @PatientId;";

        await using var conn = _connectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@PatientId", id);

        await conn.OpenAsync(cancellationToken);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken)) return null;
        
        return new Patient
        {
            Id = reader.GetInt32(reader.GetOrdinal("Patient_Id")),
            FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
            LastName = reader.GetString(reader.GetOrdinal("Last_Name")),
            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("Date_Of_Birth"))
        };
    }
}