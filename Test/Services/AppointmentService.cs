using Test.Exceptions;
using Test.Model.Appointment;
using Test.Model.AppointmentService;
using Test.Model.Doctor;
using Test.Model.Patient;
using Test.Repository;
using Tutorial9.Repository;

namespace Test.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IServiceRepository _serviceRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
        IDoctorRepository doctorRepository, IServiceRepository serviceRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _serviceRepository = serviceRepository;
    }


    public async Task<AppointmentInformationDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0) throw new InvalidAppointmentIdException("Id of an appointment should be positive integer.");

        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (appointment == null) throw new InvalidAppointmentIdException($"The appointment with ID {id} could not be found.");

        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);
        if (patient == null) throw new NoSuchPatientException($"There's no patient with ID {id} assigned to this appointment.");
        
        var doctor = await _doctorRepository.GetByIdAsync(id, cancellationToken);

        var services = await _serviceRepository.GetAssociatedServices(id, cancellationToken);

        var patientDto = new PatientDTO
        {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
        };

        var doctorDto = new DoctorDTO
        {
            DoctorId = doctor.Id,
            Pwz = doctor.Pwz,
        };
        
        var appointmentServiceDtoList = services.Select((service) => new AppointmentServiceDTO
        {
            Name = service.Name,
            ServiceFee = service.ServiceFee
        }).ToList();

        return new AppointmentInformationDTO
        {
            Date = appointment.Date,
            Patient = patientDto,
            Doctor = doctorDto,
            AppointmentServices = appointmentServiceDtoList
        };
    }

    public async Task CreateNewAppointment(CreateAppointmentDTO createAppointmentDto, CancellationToken cancellationToken)
    {
        return await _appointmentRepository.CreateNewAppointment(cancellationToken);
    }
}