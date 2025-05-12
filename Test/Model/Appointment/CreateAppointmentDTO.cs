using Test.Model.AppointmentService;

namespace Test.Model.Appointment;

public class CreateAppointmentDTO
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string Pwz { get; set; }
    public List<AppointmentServiceDTO> Services { get; set; }
}