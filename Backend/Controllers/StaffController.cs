using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Interfaces;
using Backend.Dtos;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IDeviceRepository _deviceRepository;

        public StaffController(IStaffRepository staffRepository, IDeviceRepository deviceRepository)
        {
            _staffRepository = staffRepository;
            _deviceRepository = deviceRepository;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetStaffByEmail(string email)
        {
            var staff = await _staffRepository.GetStaffByEmailAsync(email);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var staff = await _staffRepository.GetAllStaffAsync();
            return Ok(staff);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(string id)
        {
            var staff = await _staffRepository.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(StaffDto staffDto)  
        {
            await _staffRepository.AddStaffAsync(staffDto);
            return CreatedAtAction(nameof(GetStaffById), staffDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(string id, Staff staff)
        {
            if (id != staff.Id)
            {
                return BadRequest();
            }
            await _staffRepository.UpdateStaffAsync(staff);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            await _staffRepository.DeleteStaffAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/devices")]
        public async Task<IActionResult> GetDevicesByStaffId(string id)
        {
            var devices = await _deviceRepository.GetDevicesByStaffIdAsync(id);
            return Ok(devices);
        }

        [HttpPost("add-device")]
        public async Task<IActionResult> AddDevice(DeviceDto deviceDto)  
        {
            await _deviceRepository.AddDeviceAsync(deviceDto);
            return CreatedAtAction(nameof(GetDevicesByStaffId), deviceDto);
        }

        [HttpDelete("delete-device/{deviceId}")]
        public async Task<IActionResult> DeleteDevice(int deviceId)
        {
            await _deviceRepository.DeleteDeviceAsync(deviceId);
            return NoContent();
        }
    }
}
