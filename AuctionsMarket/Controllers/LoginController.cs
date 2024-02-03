using Domain.Contracts;
using DTO.LoginDTO;
using DTO.RoleDTO;
using DTO.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HumanResourceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public static LoginDTO auth = new LoginDTO();
        public static RoleDTO1 role = new RoleDTO1();
        private IConfiguration _config;
        private readonly ILoginDomain _loginDomain;

        public LoginController(IConfiguration config, ILoginDomain loginDomain)
        {
            _config = config;
            _loginDomain = loginDomain;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("loginAuthentication")]
        public IActionResult Authenticate([FromBody] LoginCredentialsDTO login)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(login);
                
                var auth = _loginDomain.AuthUsers(login);
                return Ok(auth);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
