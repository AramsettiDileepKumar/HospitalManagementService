using HospitalManagementService.DTO;
using HospitalManagementService.Entity;

namespace HospitalManagementService.Interface
{
    public interface IDepartment
    {
        Task<bool> CreateDept(DepartmentRequest deptRequest);
        Task<bool> updateDepartment(DepartmentRequest request,int deptid);
        Task<bool> deleteDepartment(int deptid);
        Task<DepartmentEntity?> getByDeptId(int id);
        Task<DepartmentEntity?> getByDeptName(string name);
    }
}
