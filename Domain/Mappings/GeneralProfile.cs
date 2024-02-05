using AutoMapper;
using DTO.AuctionDTO;
using DTO.BidDTO;
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
           CreateMap<User, User1DTO>().ReverseMap();
         
            CreateMap<LoginDTO, User>().ReverseMap();
            CreateMap<UserRoleDTO, UserRole>().ReverseMap();
            CreateMap<LoginCredentialsDTO, User>().ReverseMap();
            CreateMap<AuctionDTO, Auction>().ReverseMap();
            CreateMap<AuctionResponseDTO, Auction>().ReverseMap();
            CreateMap<BidDTO, Bid>().ReverseMap();
            CreateMap<RoleDTO1, Role>().ReverseMap();

        }

        #endregion


    }
}
