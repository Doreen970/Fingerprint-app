using Backend.Dtos;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetDevicesByStaffIdAsync(string staffId);
        Task AddDeviceAsync(DeviceDto deviceDto);
        Task DeleteDeviceAsync(int deviceId);
        Task AssignDeviceToStaffAsync(int deviceId, string staffId);
        Task<List<Device>> GetAllDevicesAsync();
    }
}
