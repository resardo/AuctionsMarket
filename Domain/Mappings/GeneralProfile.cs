using AutoMapper;
using DTO.LoginDTO;
using DTO.RoleDTO;
using DTO.UserDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappings
{
    public class GeneralProfile : Profile
    {
        #region User
        public GeneralProfile()
        {
           CreateMap<User, UserDTO>().ReverseMap();
         
            CreateMap<LoginDTO, User>().ReverseMap();
            CreateMap<UserRoleDTO, UserRole>().ReverseMap();
            CreateMap<LoginCredentialsDTO, User>().ReverseMap();
            
            CreateMap<RoleDTO1, Role>().ReverseMap();

        }

        #endregion


    }
}
