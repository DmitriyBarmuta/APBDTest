using Test.Model.Appointment;

namespace Tutorial9.Repository;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken);
}