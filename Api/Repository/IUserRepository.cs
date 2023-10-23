using API.DTOs;

namespace API.Repository
{
    public interface IUserRepository
    {

        public  Task<UserDTO> GetLogin(UserDTO UserDTO);
        Task<UserDTO> PostUser(UserDTO UserDTO);
    }
}