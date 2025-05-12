using Test.Model.Appointment;

namespace Test.Services;

public interface IAppointmentService
{
    Task<AppointmentInformationDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateNewAppointment(CreateAppointmentDTO createAppointmentDto, CancellationToken cancellationToken);
}