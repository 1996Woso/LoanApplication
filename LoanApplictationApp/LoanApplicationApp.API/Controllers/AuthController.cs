using AutoMapper;
using LoanApplicationApp.API.Models.DTO.Auth;
using LoanApplictationApp.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplicationApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public AuthController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequestDTO.Email,
                Email = registerRequestDTO.Email,
                Name = registerRequestDTO.Name,
                Surname = registerRequestDTO.Surname
            };
            var result = await userManager.CreateAsync(user, registerRequestDTO.Password);
            if (result.Succeeded)
            {
                if(registerRequestDTO.Role != null)
                {
                    result = await userManager.AddToRoleAsync(user, registerRequestDTO.Role);
                }
                return Ok("User is successfully registered.");
            }
            return BadRequest("Error has occured while registering");
        }
        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Email);
            if (user != null)
            {
                //Check if password is correct
                var result = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (result)
                {
                    //Get user roles
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                   
                        return Ok("User is successfully logged in.");
                    }
                }
                else
                {
                    return BadRequest("Incorrect password.");
                }
            }
            return BadRequest($"Invalid Log in, {loginRequestDTO.Email} does not exists.");
        }
    }
}
