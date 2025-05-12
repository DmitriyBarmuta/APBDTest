using Test.Model.Doctor;

namespace Tutorial9.Repository;

public interface IDoctorRepository
{
    Task<Doctor> GetByIdAsync(int id, CancellationToken cancellationToken);
}