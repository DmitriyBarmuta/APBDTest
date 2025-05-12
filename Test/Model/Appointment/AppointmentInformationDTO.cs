using Test.Model.AppointmentService;
using Test.Model.Doctor;
using Test.Model.Patient;

namespace Test.Model.Appointment;

public class AppointmentInformationDTO
{
    public DateTime Date { get; set; }
    public PatientDTO Patient { get; set; }
    public DoctorDTO Doctor { get; set; }
    public List<AppointmentServiceDTO> AppointmentServices { get; set; }
}