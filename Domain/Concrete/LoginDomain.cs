using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.LoginDTO;
using DTO.RoleDTO;
using DTO.UserDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Domain.Concrete
{
    internal class LoginDomain : DomainBase, ILoginDomain
    {
        private IConfiguration _config;
        public static LoginDTO auth = new LoginDTO();
        public static RoleDTO1 role = new RoleDTO1();
        public LoginDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(unitOfWork, mapper, httpContextAccessor)
        { _config = config; }

        private ILoginRepository loginRepository => _unitOfWork.GetRepository<ILoginRepository>();

        public string AuthUsers(LoginCredentialsDTO dto)
        {

            var data = _mapper.Map<LoginCredentialsDTO, User>(dto);
            var login = loginRepository.Generate(data);
            auth =  (login != null) ?  _mapper.Map<User, LoginDTO>(login) : throw new HttpRequestException("wrong credentials");
            return GenerateParamsForToken();
        }

        public string GenerateParamsForToken()
        {
            var roleList = auth.UserRoles;
            foreach (var roleName in roleList)
            {
                role.Name = roleName.Role.Name;
            }
            return GenerateToken(auth, role);
        }

       
        public string GenerateToken(LoginDTO dto, RoleDTO1 role)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:secret"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,dto.Username),
                new Claim(ClaimTypes.Role,role.Name),
                new Claim(ClaimTypes.SerialNumber, (dto.UserId).ToString()),
                new Claim(ClaimTypes.GroupSid,(role.RoleId).ToString())

            };

            var token = new JwtSecurityToken(_config["jwt:validissuer"],
                _config["jwt:validaudience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}


