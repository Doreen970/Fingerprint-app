using Backend.Data;
using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;

        public DeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            return await _context.Devices.ToListAsync();
        }

        public async Task<IEnumerable<Device>> GetDevicesByStaffIdAsync(string staffId)
        {
            return await _context.Devices
                                 .Where(d => d.StaffId == staffId)
                                 .ToListAsync();
        }

        public async Task AddDeviceAsync(DeviceDto deviceDto) 
        {
            var device = new Device
            {
                DeviceName = deviceDto.DeviceName,
                SerialNumber= deviceDto.SerialNumber,
                DateAdded = deviceDto.DateAdded,
                StaffId = deviceDto.StaffId
            };
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(int deviceId)
        {
            var device = await _context.Devices.FindAsync(deviceId);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignDeviceToStaffAsync(int deviceId, string staffId) 
        {
            var device = await _context.Devices.FindAsync(deviceId);
            if (device != null)
            {
                device.StaffId = staffId;
                _context.Devices.Update(device);
                await _context.SaveChangesAsync();
            }
        }
    }
}
