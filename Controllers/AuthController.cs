using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Hospital_Clinic_Appointment_System.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
public class AuthController : ControllerBase 
{
    private readonly IAuthRepository authRepository;
    private readonly IJwtService jwtService;
    private readonly DBContext context;

    public AuthController(IAuthRepository authRepository, DBContext context, IJwtService jwtService)
    {
        this.jwtService = jwtService;
        this.authRepository = authRepository;
        this.context = context;
    }

    // POST: api/Auth/Register
    [HttpPost("Register")]
    [AllowAnonymous] 
    public async Task<ActionResult<LoginResponseDto>> Register([FromBody] RegisterDto register)
    {
        if (await authRepository.EmailExistsAsync(register.Email))
        {
            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = "Email already exists"
            });
        }

        var age = DateTime.Today.Year - register.BirthDay.Year;
        if (register.BirthDay.Date > DateTime.Today.AddYears(-age)) age--;

        if (age < 18 || age > 100)
        {
            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = "Age must be between 18 and 100"
            });
        }

        var user = new User
        {
            Name = register.Name,
            Email = register.Email,
            Phone_Number = register.Phone_Number,
            BirthDay = register.BirthDay,
            IsEmailConfirmed = false,
            isActive = true
        };

        var registeredUser = await authRepository.RegisterAsync(user, register.Password);

        // Assign Role
        var roleId = register.Role switch
        {
            "Doctor" => 1,
            "Patient" => 2,
            "Admin" => 3,
            _ => 2 // Default to Patient
        };

        await context.UserRoles.AddAsync(new UserRole
        {
            User_Id = registeredUser.Id,
            Role_Id = roleId
        });
        await context.SaveChangesAsync();

        var roles = new List<string> { register.Role };
        var token = jwtService.GenerateToken(registeredUser, roles);

        return Ok(new LoginResponseDto
        {
            Success = true,
            Message = "Registration successful",
            Token = token,
            TokenType = "Bearer",
            ExpiresAt = DateTime.UtcNow.AddHours(8),
            User = new UserInfoDto
            {
                Id = registeredUser.Id,
                Name = registeredUser.Name,
                Email = registeredUser.Email,
                Phone = registeredUser.Phone_Number,
                Roles = roles
            }
        });
    }

    // POST: api/Auth/Login
    [HttpPost("Login")]
    [AllowAnonymous] 
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto dto)
    {
        var user = await authRepository.LoginAsync(dto.Email, dto.Password);

        if (user == null)
        {
            return Unauthorized(new LoginResponseDto
            {
                Success = false,
                Message = "Invalid email or password"
            });
        }

        var roles = user.UserRoles.Select(ur => ur.role.Name).ToList();

        if (!roles.Any())
        {
            return BadRequest(new LoginResponseDto
            {
                Success = false,
                Message = "No role assigned to this account"
            });
        }

        var token = jwtService.GenerateToken(user, roles);

        return Ok(new LoginResponseDto
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            TokenType = "Bearer",
            ExpiresAt = DateTime.UtcNow.AddHours(8),
            User = new UserInfoDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone_Number,
                Roles = roles
            }
        });
    }

    // GET: api/Auth/Me
    [HttpGet("Me")]
    [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> GetCurrentUser()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await authRepository.GetUserWithRolesAsync(userId);

        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        var roles = user.UserRoles.Select(ur => ur.role.Name).ToList();

        return Ok(new AuthResponseDto
        {
            Success = true,
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Roles = roles,
            Message = "User retrieved successfully"
        });
    }

    // POST: api/Auth/ChangePassword
    [HttpPost("ChangePassword")]
    [Authorize] 
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Validate FIRST
        if (dto.NewPassword == dto.CurrentPassword)
        {
            return BadRequest(new { Message = "New password cannot be same as current password" });
        }

        if (dto.NewPassword != dto.ConfirmPassword)
        {
            return BadRequest(new { Message = "New password and confirm password don't match" });
        }

        //  Then change
        var success = await authRepository.ChangePasswordAsync(
            userId,
            dto.CurrentPassword,
            dto.NewPassword
        );

        if (!success)
        {
            return BadRequest(new { Message = "Current password is incorrect" });
        }

        return Ok(new { Message = "Password changed successfully" });
    }

   
    // [HttpGet("HashPassword")]
    // [Authorize(Roles = "Admin")]
    // public ActionResult<string> HashPassword([FromQuery] string password) { ... }
}
}
