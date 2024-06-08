using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Models;

public interface IStaffRepository
{
    Task<Staff> GetStaffByEmailAsync(string email);
    Task<bool> ValidatePasswordAsync(Staff staff, string password);
    Task<Staff> GetStaffByIdAsync(string staffId);
    Task<IEnumerable<Staff>> GetAllStaffAsync();
    Task AddStaffAsync(StaffDto staffDto);
    Task UpdateStaffAsync(Staff staff);
    Task DeleteStaffAsync(string staffId);
}
