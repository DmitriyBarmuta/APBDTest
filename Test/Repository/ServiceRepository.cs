using Test.Infrastructure;
using Test.Model.Service;

namespace Tutorial9.Repository;

public class ServiceRepository : IServiceRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public ServiceRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<List<Service>> GetAssociatedServices(int appointmentId, CancellationToken cancellationToken)
    {
        const string sql = @"
                            SELECT * FROM Appointment a
                            JOIN Appointment_Service aserv ON a.Appointment_Id = aserv.Appointment_Id
                            JOIN Service s ON s.Service_Id = aserv.Service_Id
                            WHERE a.Appointment_Id = @AppointmentId
                            ";
        
        await using var conn = _connectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

        await conn.OpenAsync(cancellationToken);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        
        var resultList = new List<Service>();

        while (await reader.ReadAsync(cancellationToken))
        {
            resultList.Add(new Service
            {
                ServiceId = reader.GetInt32(reader.GetOrdinal("Service_Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                BaseFee = reader.GetDecimal(reader.GetOrdinal("Base_Fee")),
                ServiceFee = reader.GetDecimal(reader.GetOrdinal("Service_Fee"))
            });
        }

        return resultList;
    }
}