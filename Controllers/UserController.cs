using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Controllers
{
    [Route("Dashboard/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }

        [HttpPost("add")]
        public async Task<ActionResult<CreateUserDto>> AddUserAsync(CreateUserDto createUser)
        {
            var user = new User
            {
                Name = createUser.Name,
                Email = createUser.Email,
                BirthDay = createUser.BirthDay,
                Password = createUser.Password,
                IsEmailConfirmed = createUser.IsEmailConfirmed,
                Phone_Number = createUser.Phone_Number,
                isActive = createUser.isActive
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            var Dto = new CreateUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                BirthDay = user.BirthDay,
                Phone_Number = user.Phone_Number,
                isActive = user.isActive
            };

            return CreatedAtRoute("GetUserById", new { id = Dto.Id }, Dto); 
        }

        [HttpGet("All")] // Get : Dashboard/User/All
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllWithIncludesAsync();
            var Dto = users.Select(u => new UserDto   
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                Phone_Number = u.Phone_Number
            }).ToList();

            return Ok(Dto); 
        }

         [HttpGet("{id:int}",Name="GetUserById")] // Get : Dashboard/User/{id}
         public async Task<ActionResult<UserDto>> GetUserByIdAsync(int id)
         {
             var user = await userRepository.FirstOrDefaultWithIncludesAsync(u=>u.Id==id);
             if (user == null)
             {
                 return NotFound(); 
             }
             var Dto = new UserDto
             {
                 Id = user.Id,
                 Name = user.Name,
                 Email = user.Email,
                 Password = user.Password,
                 Phone_Number = user.Phone_Number
             };
             return Ok(Dto); 
        }


        [HttpGet("{id}/GetRole")] // Get : Dashboard/User/{id}/GetRole
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserRolesAsync(int id)
        {
            var user = await userRepository.FirstOrDefaultWithIncludesAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = user.UserRoles.Select(ur => ur.role.Name).ToList();
            var Dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Roles = roles

            };
            return Ok(Dto);
        }

        [HttpGet("email/{email}")] // Get : Dashboard/User/email/{email}
        public async Task<ActionResult<UserDto>> GetUserByEmailAsync([FromRoute] string email)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound(); 
            }
            var Dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Phone_Number = user.Phone_Number
            };
            return Ok(Dto); 
         }
         [HttpGet("name/{name}")] // Get : Dashboard/User/name/{name}
         public async Task<ActionResult<UserDto>> GetUserByNameAsync([FromRoute] string name)
         {
             var user = await userRepository.GetUserByNameAsync(name);
             if (user == null)
             {
                 return NotFound(); 
             }
             var Dto = new UserDto  
             {
                 Id = user.Id,
                 Name = user.Name,
                 Email = user.Email,
                 Password = user.Password,
                 Phone_Number = user.Phone_Number
             };
             return Ok(Dto);
        }
        [HttpPut("ChengePhoneNumber/{userId}")] // Put : Dashboard/User/updatePhoneNumber/{userId}
        public async Task<ActionResult<UserDto>> ChangeUserPhoneNumberAsync([FromRoute]int userId,[FromBody] string newPhoneNumber)
        {
            var user = await userRepository.UpdatePhoneNumperOfUser(userId, newPhoneNumber);
            if (user == null)
            {
                return NotFound(); 
            }
            var Dto = new UserDto
            {
                Id = user.Id,   
                Phone_Number = user.Phone_Number
            };
            return Ok("The phone number has been successfully changed.");
        }

        [HttpPut("ChengeName/{userId}")] // Put : Dashboard/User/updatePhoneNumber/{userId}
        public async Task<ActionResult<UserDto>> ChangeUserNameAsync([FromRoute] int userId, [FromBody] string NewName)
        {
            var user = await userRepository.UpdateNameOfUser(userId, NewName);
            if (user == null)
            {
                return NotFound();
            }
            var Dto = new CreateUserDto
            {
                Id = user.Id,
                Phone_Number = user.Name
            };
            return Ok("The Name has been successfully changed.");
        }

        [HttpPut("ChengePassword/{userId}")] // Put : Dashboard/User/updatePhoneNumber/{userId}
        public async Task<ActionResult<UserDto>> ChangeUserPasswordAsync([FromRoute] int userId, [FromBody] string newPassWord)
        {
            var user = await userRepository.UpdatePasswordOfUser(userId, newPassWord);
            if (user == null)
            {
                return NotFound();
            }
            var Dto = new UserDto
            {
                Id = user.Id,
                Password = user.Password
            };
            return Ok("The Password has been successfully changed.");
        }

            [HttpPut("ChengeEmail/{userId}")] // Put : Dashboard/User/updatePhoneNumber/{userId}
            public async Task<ActionResult<UserDto>> ChangeUserEmailAsync([FromRoute] int userId, [FromBody] string newEmail)
            {
                var user = await userRepository.UpdateEmailOfUser(userId, newEmail);
                if (user == null)
                {
                    return NotFound();
                }
                var Dto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email
                };
                return Ok("The Email has been successfully changed.");
        }

        [HttpPut("{id:int}/Update")] // Put : Dashboard/User/{id}/Update
        public async Task<ActionResult<UserDto>> UpdateUserAsync([FromRoute] int id, [FromBody] CreateUserDto updateUser)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.Password = updateUser.Password;
            user.Phone_Number = updateUser.Phone_Number;
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
            var Dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Phone_Number = user.Phone_Number
            };
            return Ok(Dto);
        }
        [HttpDelete("{id:int}/Delete")] // Delete : Dashboard/User/{id}/Delete
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            userRepository.Delete(user);
            await userRepository.SaveChangesAsync();

            return Ok("The user has been successfully deleted.");

        }

        }
}
