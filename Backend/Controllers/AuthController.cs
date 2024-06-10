using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Interfaces;
using System.ComponentModel.DataAnnotations;
using Backend.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Staff> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Staff> _signInManager;

        public AuthController(UserManager<Staff> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, SignInManager<Staff> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var staff = new Staff
                {
                    UserName = registerDto.UserName,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email
                };

                var result = await _userManager.CreateAsync(staff, registerDto.Password);

                if (result.Succeeded)
                {
                    // Check if the "Staff" role exists
                    if (!await _roleManager.RoleExistsAsync("Staff"))
                    {
                        // If the role doesn't exist, create it
                        await _roleManager.CreateAsync(new IdentityRole("Staff"));
                    }

                    // Assign the "Staff" role to the user
                    await _userManager.AddToRoleAsync(staff, "Staff");

                    var roles = await _userManager.GetRolesAsync(staff);

                    return Ok(new NewUserDto
                    {
                        UserName = staff.UserName,
                        Email = staff.Email,
                        Role = roles.FirstOrDefault(),
                        Token = _tokenService.CreateToken(staff)
                    });
                }
                else
                {
                    return StatusCode(500, result.Errors);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }

                return StatusCode(500, "An error occurred while saving changes to the database.");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized("Invalid Email!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid Email or Password");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault(), 
                Token = _tokenService.CreateToken(user)
            });
        }

    }
}
