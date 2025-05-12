using Test.Model.Patient;

namespace Tutorial9.Repository;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(int id, CancellationToken cancellationToken);
}