using HospitalManagementService.DTO;
using HospitalManagementService.Entity;

namespace HospitalManagementService.Interface
{
    public interface IDoctor
    {
        Task<bool> CreateDoctor(DoctorRequest request,int doctorId);
        Task<DoctorEntity?> GetDoctorById(int doctorId);
        Task<IEnumerable<DoctorEntity?>> GetAllDoctors();
        Task<bool> UpdateDoctor(int doctorId, DoctorRequest request);
        Task<bool> DeleteDoctor(int doctorId);
        public Task<IEnumerable<DoctorEntity?>> GetDoctorsBySpecialization(string specialization);
    }
}
