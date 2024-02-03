using DTO.LoginDTO;
using DTO.UserDTO;

namespace Domain.Contracts
{
    public interface ILoginDomain
    {
        string AuthUsers(LoginCredentialsDTO dto);
    }
}