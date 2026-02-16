using BEPersonal.DTOs.In;

namespace BEPersonal.Services
{
    public interface IUserService
    {
            // Task<UserDTOOut> GetUserById(Guid id);
            Task<bool> Login(UserDTOIn userDTO);
            // Task<UserDTOOut> CreateUser(UserDTOIn userDtoIn);
            // Task<UserDTOOut> UpdateUser(int id, UserDTOIn userDtoIn);
            // Task<bool> DeleteUser(int id);
    }
}
