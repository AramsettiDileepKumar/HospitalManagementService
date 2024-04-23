using Dapper;
using HospitalManagementService.Context;
using HospitalManagementService.DTO;
using HospitalManagementService.Entity;
using HospitalManagementService.Interface;
using System.Data;

namespace HospitalManagementService.Service
{
    public class DepartmentService:IDepartment
    {
        private readonly DapperContext context;
        public DepartmentService(DapperContext context) 
        {
            this.context = context;
        }
        public async Task<bool> CreateDept(DepartmentRequest deptRequest)
        {
            try
            {
                var query = "INSERT INTO Department(DeptName) VALUES (@DeptName);";
                DepartmentEntity e = MapToEntity(deptRequest);
                var parameters = new DynamicParameters();
                parameters.Add("@DeptName", deptRequest.DeptName);
                using (var connection = context.getConnection())
                {
                    var result = await connection.ExecuteAsync(query, parameters);
                    return result>0;
                }
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<DepartmentEntity?> getByDeptId(int id)
        {
            try
            {
                var query = "SELECT * FROM Department WHERE DeptId = @DeptId;";
                var parameters = new DynamicParameters();
                parameters.Add("@DeptId", id);
                using (var connection = context.getConnection())
                {
                    var result = await connection.QueryFirstOrDefaultAsync<DepartmentEntity>(query, parameters);
                    return result;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<DepartmentEntity?> getByDeptName(string name)
        {
            try
            {
                string query = "Select * from Department where DeptName=@dname";
                var result= await context.getConnection().QueryFirstOrDefaultAsync<DepartmentEntity>(query, new { dname = name });
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> updateDepartment(DepartmentRequest request,int deptid)
        {
            try
            {
                string query = "update Department set DeptName=@deptname where DeptId=@Deptid";
                var result = await context.getConnection().ExecuteAsync(query, new {deptname=request.DeptName,Deptid=deptid});
                return result > 0;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> deleteDepartment(int deptid)
        {
            try { return await context.getConnection().ExecuteAsync("Delete from Department where DeptId=@Deptid;", new { Deptid = deptid }) > 0; }
            catch(Exception ex) { throw new Exception(ex.Message);}
        }
        private DepartmentEntity MapToEntity(DepartmentRequest request) => new DepartmentEntity { DeptName = request.DeptName };


    }
}
