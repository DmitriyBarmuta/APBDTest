using Test.Model.Service;

namespace Tutorial9.Repository;

public interface IServiceRepository
{
    Task<List<Service>> GetAssociatedServices(int appointmentId, CancellationToken cancellationToken);
}