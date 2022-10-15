using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.DAL;
using MyBackendApp.DTO;

namespace MyBackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registration(AddUserDTO addUserDTO)
        {
            try
            {
                await _user.Registration(addUserDTO);
                return Ok($"Registrasi user {addUserDTO.Username} berhasil");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(AddUserDTO userDTO)
        {
            try
            {
                var user = await _user.Authenticate(userDTO);
                if (user == null)
                    return BadRequest("Username or Password doesn't match");
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
