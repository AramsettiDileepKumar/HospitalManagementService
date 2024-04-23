using Dapper;
using HospitalManagementService.Context;
using HospitalManagementService.DTO;
using HospitalManagementService.Entity;
using HospitalManagementService.Interface;
using System.Data;

namespace HospitalManagementService.Service
{
    public class DoctorService :IDoctor
    {
        private readonly DapperContext context;
        public DoctorService( DapperContext context)
        {
            this.context = context;
          
        }
            public async Task<bool> CreateDoctor(DoctorRequest request)
            {
            try
            {
                var query = "INSERT INTO Doctor (DoctorId, DeptId, DoctorName, DoctorAge, DoctorAvailable, Specialization, Qualifications) VALUES (@DoctorId, @DeptId, @DoctorName, @DoctorAge, @DoctorAvailable, @Specialization, @Qualifications);";
                DoctorEntity e = MapToEntity(request);
                //getUserById(request.DoctorId));
                var parameters = new DynamicParameters();
                parameters.Add("@DoctorId", e.DoctorId);
                parameters.Add("@DeptId", e.DeptId);
                parameters.Add("@DoctorName", e.DoctorName);
                parameters.Add("@DoctorAge", e.DoctorAge);
                parameters.Add("@DoctorAvailable", e.DoctorAvailable);
                parameters.Add("@Specialization", e.Specialization);
                parameters.Add("@Qualifications", e.Qualifications);
                using (var connection = context.getConnection())
                {
                    var result = await connection.ExecuteAsync(query, parameters);
                    return result > 0;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            }
            private DoctorEntity MapToEntity(DoctorRequest request)
            {
                return new DoctorEntity
                {
                    DoctorId = request.DoctorId,
                    DeptId = request.DeptId,
                    DoctorName = request.DoctorName,
                    DoctorAge = request.DoctorAge,
                    DoctorAvailable = request.DoctorAvailable,
                    Specialization = request.Specialization,
                    Qualifications = request.Qualifications
                };
            }
            public async Task<DoctorEntity?> GetDoctorById(int doctorId)
            {
                try
                {
                    var query = " SELECT * FROM Doctor WHERE DoctorId = @DoctorId;";
                    var parameters = new DynamicParameters();
                    parameters.Add("@DoctorId", doctorId);
                    using (var connection = context.getConnection())
                    {
                        var result =await connection.QueryFirstOrDefaultAsync<DoctorEntity>(query, parameters);
                        return result;
                    }
                }
                catch(Exception ex)
                {
                throw new Exception(ex.Message);
                }
            }
            public async Task<IEnumerable<DoctorEntity?>> GetAllDoctors()
            {
                    try
                    {
                        var connection = context.getConnection();
                        string query = "SELECT * FROM Doctor;";
                        return await connection.QueryAsync<DoctorEntity>(query);
                    }
                    catch(Exception ex) 
                    {
                       throw new Exception(ex.Message);
                     }
            }
              public async Task<bool> UpdateDoctor(int doctorId, DoctorRequest request)
              {
                    try
                    {
                        DoctorEntity? existingDoctor = await GetDoctorById(doctorId);
                        if (existingDoctor == null)
                            return false; // Doctor not found
                        existingDoctor.DeptId = request.DeptId;
                        existingDoctor.Specialization = request.Specialization;
                        existingDoctor.Qualifications = request.Qualifications;
                        existingDoctor.DoctorAge = request.DoctorAge;
                        existingDoctor.DoctorAvailable = request.DoctorAvailable;
                        string query = "update Doctor set Specialization=@Specialization,Qualifications=@Qualifications,DoctorName=@Doctorname,DoctorAge=@doctorAge where DoctorId=@DoctorId and DeptId=@DeptId";
                        var parameters = new DynamicParameters();
                        parameters.Add("@DoctorId", doctorId);
                        parameters.Add("@DeptId", request.DeptId);
                        parameters.Add("@Doctorname", request.DoctorName);
                        parameters.Add("@doctorAge", request.DoctorAge);
                        parameters.Add("@Specialization", request.Specialization);
                        parameters.Add("@Qualifications", request.Qualifications);
                        var connection = context.getConnection();
                        var rowsAffected = await connection.ExecuteAsync(query, parameters);
                        return rowsAffected>0;
                    }
                    catch(Exception ex) 
                    {
                     throw new Exception(ex.Message);
                    }
             }
            public async Task<bool> DeleteDoctor(int doctorId)
            {
                    try
                    {
                        string query = "DELETE FROM Doctor WHERE  DoctorId = @DoctorId;";
                        var result= await context.getConnection().ExecuteAsync(query, new { DoctorId = doctorId });
                        return result > 0;
                    }
                    catch(Exception e) 
                    {
                        throw new Exception(e.Message);
                    }
            }
            public async Task<IEnumerable<DoctorEntity?>> GetDoctorsBySpecialization(string specialization)
            {
                    try
                    {
                        string query = "SELECT * FROM Doctor WHERE Specialization = @Specialization;";
                        return await context.getConnection().QueryAsync<DoctorEntity>(query, new { Specialization = specialization });
                    } catch(Exception e) 
                    {
                        throw new Exception(e.Message);
                    }
             }

        }
    }
