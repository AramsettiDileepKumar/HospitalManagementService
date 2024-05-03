using HospitalManagementService.DTO;
using HospitalManagementService.Entity;
using HospitalManagementService.Interface;
using HospitalManagementService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment dept;
        public DepartmentController(IDepartment dept)
        {
            this.dept = dept;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentRequest deptRequest)
        {
            try
            {
                var result=await dept.CreateDept(deptRequest);
                if (result)
                {
                    var response = new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Department Created",
                        Data = result
                    };
                    return Ok(response);
                }
                return BadRequest("invalid input");
            }
            catch (Exception ex)
            {
                return Ok($"An error occurred while adding the Department: {ex.Message}");
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("deptid")]
        public async Task<IActionResult> UpdateDepartment(DepartmentRequest request,int deptid)
        {
            try 
            {
                var result = await dept.updateDepartment(request,deptid);
                if (result)
                {
                    var response = new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Department Updated",
                        Data = result
                    };
                    return Ok(response);
                }
                return BadRequest("invalid input");

            }
            catch(Exception ex) 
            {
                return Ok(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("deptid")]
        public async Task<IActionResult> DeleteDepartment(int deptid)
        {
            try
            {
                var result = await dept.deleteDepartment(deptid);
                if (result)
                {
                    var response = new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Department Deleted",
                        Data = result
                    };
                    return Ok(response);
                }
                return BadRequest("invalid input");

            }
            catch (Exception ex)
            {
                return Ok( ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("deptid")]
        public async Task<IActionResult> getByDeptId(int DeptId)
        {
            try
            {
                var result = await dept.getByDeptId(DeptId);
                if (result!=null)
                {
                    var response = new ResponseModel<DepartmentEntity>
                    {
                        Success = true,
                        Message = "Details Fetched Successfully",
                        Data = result
                    };
                    return Ok(response);
                }
                return BadRequest("invalid input");
            }
            catch (Exception ex)
            {
                return Ok( $"An error occurred : {ex.Message}");
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("byname")]
        public async Task<IActionResult> getByDeptName(string name)
        {
            
            try
            {
                var result = await dept.getByDeptName(name);
                if (result != null)
                {
                    var response = new ResponseModel<DepartmentEntity>
                    {
                        Success = true,
                        Message = "Details Fetched Successfully",
                        Data = result
                    };
                    return Ok(response);
                }
                return BadRequest("invalid input");
            }
            catch (Exception ex)
            {
                return Ok( $"An error occurred : {ex.Message}");
            }
        }

    }
}
