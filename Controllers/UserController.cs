using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")] // All endpoints for Admin only
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: api/User/All
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllWithIncludesAsync();

            var dtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phone_Number = u.Phone_Number
            });

            return Ok(dtos);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            var dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone_Number = user.Phone_Number
            };

            return Ok(dto);
        }

        // GET: api/User/Email/{email}
        [HttpGet("Email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmailAsync(string email)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound();

            var dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone_Number = user.Phone_Number
            };

            return Ok(dto);
        }

        // GET: api/User/{id}/Roles
        [HttpGet("{id}/Roles")]
        public async Task<ActionResult<UserDto>> GetUserRolesAsync(int id)
        {
            var user = await userRepository.FirstOrDefaultWithIncludesAsync(
                u => u.Id == id,
                u => u.UserRoles
            );

            if (user == null)
                return NotFound();

            var roles = user.UserRoles.Select(ur => ur.role.Name).ToList();

            var dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = roles
            };

            return Ok(dto);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] CreateUserDto updateDto)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            // Update fields
            if (!string.IsNullOrWhiteSpace(updateDto.Name))
                user.Name = updateDto.Name;

            if (!string.IsNullOrWhiteSpace(updateDto.Email))
            {
                // Check if email already exists for another user # Abood the ( < :
                var existingUser = await userRepository.GetUserByEmailAsync(updateDto.Email);
                if (existingUser != null && existingUser.Id != id)
                {
                    return BadRequest(new { Message = "Email already exists" });
                }
                user.Email = updateDto.Email;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Phone_Number))
                user.Phone_Number = updateDto.Phone_Number;

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            userRepository.Delete(user);
            await userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}