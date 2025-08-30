using LMSCleanArchitecture.Infrastructure.Persisitense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSCleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _users;
        private readonly SignInManager<IdentityUser> _signIn;
        private readonly IConfiguration _config;
        private readonly LMSDbContext _db;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(
            UserManager<IdentityUser> users,
            SignInManager<IdentityUser> signIn,
            IConfiguration config,
            LMSDbContext db,
            IJwtTokenService jwtTokenService)
        {
            _users = users;
            _signIn = signIn;
            _config = config;
            _db = db;
            _jwtTokenService = jwtTokenService;
        }

        public record LoginRequest(string Email, string Password);

        public class RegisterStudentRequest
        {
            public int StudentId { get; set; } // Existing StudentId in the database
            public string UserId { get; set; } = null!; // Explicitly provided UserId
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        public class RegisterInstructorRequest
        {
            public int InstructorId { get; set; } // Existing InstructorId in the database
            public string UserId { get; set; } = null!; // Explicitly provided UserId
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _users.FindByEmailAsync(req.Email);
            if (user == null) return Unauthorized();

            var check = await _signIn.CheckPasswordSignInAsync(user, req.Password, false);
            if (!check.Succeeded) return Unauthorized();

            var roles = await _users.GetRolesAsync(user);

            var student = _db.Students.FirstOrDefault(s => s.UserId == user.Id);
            var instructor = _db.Instructors.FirstOrDefault(i => i.UserId == user.Id);

            var token = _jwtTokenService.GenerateToken(user, roles, student?.Id, instructor?.Id);

            return Ok(new { token });
        }

        [HttpPost("register/student")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentRequest req)
        {
            // Step 1: Validate the StudentId
            var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == req.StudentId);
            if (student == null)
            {
                return NotFound($"Student with ID {req.StudentId} not found.");
            }

            // Step 2: Check if the student is already registered
            if (!string.IsNullOrEmpty(student.UserId))
            {
                return BadRequest("This student is already registered.");
            }

            // Step 3: Validate the provided UserId
            var existingUser = await _users.FindByIdAsync(req.UserId);
            if (existingUser != null)
            {
                return BadRequest("The provided UserId is already associated with another user.");
            }

            // Step 4: Create the user in the authentication system
            var user = new IdentityUser
            {
                Id = req.UserId, // Use the provided UserId
                UserName = req.Email,
                Email = req.Email,
                EmailConfirmed = true
            };
            var result = await _users.CreateAsync(user, req.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Step 5: Assign the "Student" role to the user
            await _users.AddToRoleAsync(user, "Student");

            // Step 6: Link the UserId to the student
            student.UserId = req.UserId;
            _db.Students.Update(student);
            await _db.SaveChangesAsync();

            return Ok(new { student.Id, req.Email, Role = "Student" });
        }

        [HttpPost("register/instructor")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterInstructor([FromBody] RegisterInstructorRequest req)
        {
            // Step 1: Validate the InstructorId
            var instructor = await _db.Instructors.FirstOrDefaultAsync(i => i.Id == req.InstructorId);
            if (instructor == null)
            {
                return NotFound($"Instructor with ID {req.InstructorId} not found.");
            }

            // Step 2: Check if the instructor is already registered
            if (!string.IsNullOrEmpty(instructor.UserId))
            {
                return BadRequest("This instructor is already registered.");
            }

            // Step 3: Validate the provided UserId
            var existingUser = await _users.FindByIdAsync(req.UserId);
            if (existingUser != null)
            {
                return BadRequest("The provided UserId is already associated with another user.");
            }

            // Step 4: Create the user in the authentication system
            var user = new IdentityUser
            {
                Id = req.UserId, // Use the provided UserId
                UserName = req.Email,
                Email = req.Email,
                EmailConfirmed = true
            };
            var result = await _users.CreateAsync(user, req.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Step 5: Assign the "Instructor" role to the user
            await _users.AddToRoleAsync(user, "Instructor");

            // Step 6: Link the UserId to the instructor
            instructor.UserId = req.UserId;
            _db.Instructors.Update(instructor);
            await _db.SaveChangesAsync();

            return Ok(new { instructor.Id, req.Email, Role = "Instructor" });
        }
    }
}