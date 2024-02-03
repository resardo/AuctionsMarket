using DTO.RoleDTO;


namespace DTO.LoginDTO
{
    public class LoginDTO { 
    
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public List<UserRoleDTO> UserRoles { get; set; }


    }
    public class UserRoleDTO
    {

        public Guid? UserId { get; set; }
        public Guid? RoleId { get; set; }
        
        public RoleDTO1 Role { get; set; }

    }
  


}