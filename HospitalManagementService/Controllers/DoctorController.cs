using HospitalManagementService.DTO;
using HospitalManagementService.Entity;
using HospitalManagementService.Interface;
using HospitalManagementService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Xml.Linq;

namespace HospitalManagementService.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public readonly IDoctor service;
        public  DoctorController(IDoctor _service)
        {
           this.service = _service;
        }
        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(DoctorRequest request)
        {
            try
            {
                var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await service.CreateDoctor(request,doctorId);
                if (result)
                {
                    var response = new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Doctor Created Successfully",
                        Data = result
                    };
                    return Ok(response);
                }
                return BadRequest("invalid input");
            }
            catch (Exception ex)
            {
                return Ok($"An error occurred: {ex.Message}");
            }
        }
       // [Authorize(Roles = "Admin")]
        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(int doctorId)
        {
            try
            {
                var result = await service.GetDoctorById(doctorId);
                if (result!=null)
                {
                    return Ok(result);
                }
                return BadRequest("invalid input");
            }
            catch (Exception ex)
            {
                return Ok($"An error occurred : {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var result = await service.GetAllDoctors();
                if (result != null)
                {
                    var response = new ResponseModel<IEnumerable<DoctorEntity>>
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
                return Ok($"An error occurred : {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctor(int doctorId, DoctorRequest request)
        {
            try
            {
                var result = await service.UpdateDoctor(doctorId, request);
                if (result)
                {
                    var response = new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Updated Successfully",
                        Data = result
                    };
                    return Ok(response);
                }
                return BadRequest("invalid input");
            }
            catch (Exception ex)
            {
                return Ok($"An error occurred : {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> DeleteDoctor(int doctorId)
        {
            try
            {
                var result = await service.DeleteDoctor(doctorId);
                if (result)
                {
                    var response = new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Deleted Successfully",
                        Data = result
                    };
                    return Ok(response);
                }
                return BadRequest("invalid input");
            }
            catch (Exception ex)
            {
                return Ok($"An error occurred : {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("specialization/{specialization}")]
        public async Task<IActionResult> GetDoctorsBySpecialization(string specialization)
        {
            try
            {
                var result = await service.GetDoctorsBySpecialization(specialization);
                if (result != null)
                {
                    var response = new ResponseModel<IEnumerable<DoctorEntity>>
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
                return Ok($"An error occurred : {ex.Message}");
            }
        }

    }
}
