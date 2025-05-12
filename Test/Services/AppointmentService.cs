using Test.Model.Appointment;
using Test.Services;
using Tutorial9.Repository;

namespace Tutorial9.Services;

public class AppointmentService : IAppointmentService
{
    private readonly ITestRepository _testRepository;

    public AppointmentService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }


    public Task<AppointmentInformationDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}