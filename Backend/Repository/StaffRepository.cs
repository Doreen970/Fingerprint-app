using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Dtos;

public class StaffRepository : IStaffRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Staff> _userManager;

    public StaffRepository(ApplicationDbContext context, UserManager<Staff> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Staff> GetStaffByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> ValidatePasswordAsync(Staff staff, string password)
    {
        return await _userManager.CheckPasswordAsync(staff, password);
    }

    public async Task<Staff> GetStaffByIdAsync(string staffId)
    {
        return await _context.Staffs.FindAsync(staffId);
    }

    public async Task<IEnumerable<Staff>> GetAllStaffAsync()
    {
        return await _context.Staffs
            .Include(s => s.Device)
            .Include(s => s.Transactions)
            .ToListAsync();
    }


    public async Task AddStaffAsync(StaffDto staffDto)  
    {
        var staff = new Staff
        {
            FirstName = staffDto.FirstName,
            LastName = staffDto.LastName,  
            RegistrationNo = staffDto.RegistrationNo,
            DateOfBirth = staffDto.DateOfBirth, 
            PassportID = staffDto.PassportID,       
        };
        await _context.Staffs.AddAsync(staff);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStaffAsync(Staff staff)
    {
        _context.Staffs.Update(staff);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStaffAsync(string staffId)
    {
        var staff = await _context.Staffs.FindAsync(staffId);
        if (staff != null)
        {
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
        }
    }
}
