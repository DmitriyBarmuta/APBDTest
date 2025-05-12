using Test.Model.Appointment;

namespace Test.Repository;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task CreateNewAppointment(Appointment appointment, CancellationToken cancellationToken);
    Task<bool> DoesAppointmentExist(int appointmentId, CancellationToken cancellationToken);
}