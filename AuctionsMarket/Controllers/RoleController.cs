using Domain.Contracts;
using DTO.RoleDTO;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleDomain _roleDomain;
        private IConfiguration _config;

        public RoleController(IConfiguration config, IRoleDomain roleDomain)
        {
           _config = config;
           _roleDomain = roleDomain;
        }


        [HttpGet("AllRoles")]
        public IActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                return Ok(_roleDomain.GetAllRoles());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       [HttpPost("AddRoles")]
        public IActionResult Create([FromBody] RoleDTO1 role)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                else
                {
                    var r = _roleDomain.CreateRole(role);
                    return Ok(r);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("GetRolesByname/{role}")]
        public IActionResult GetByNameRole(string role)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                return Ok(_roleDomain.GetByRoleName(role));
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("GetRolesById/{id}")]
        public IActionResult GetById(Guid id)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(_roleDomain.GetById(id));
        }
         
       
        [HttpPut("UpdateRole")]
        public IActionResult UpdatePermit([FromBody] RoleDTO1 role)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            
            else
            {
                _roleDomain.Update(role);
                return Ok("updated");
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        public IActionResult DeleteRole(Guid userId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                _roleDomain.Remove(userId);

                return Ok("update completed");
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

