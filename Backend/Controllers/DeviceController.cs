using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDevices()
        {
            var devices = await _deviceRepository.GetAllDevicesAsync();
            return Ok(devices);
        }

        [HttpGet("staff/{staffId}")]
        public async Task<IActionResult> GetDevicesByStaffId(string staffId)
        {
            var devices = await _deviceRepository.GetDevicesByStaffIdAsync(staffId);
            return Ok(devices);
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice(DeviceDto deviceDto)
        {
            await _deviceRepository.AddDeviceAsync(deviceDto);

            return Ok(new { message = "Device successfully added." });
        }


        [HttpDelete("{deviceId}")]
        public async Task<IActionResult> DeleteDevice(int deviceId)
        {
            await _deviceRepository.DeleteDeviceAsync(deviceId);
            return NoContent();
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignDeviceToStaff([FromBody] AssignDeviceDto model) // New endpoint for assigning device
        {
            await _deviceRepository.AssignDeviceToStaffAsync(model.DeviceId, model.StaffId);
            return NoContent();
        }
    }
}
