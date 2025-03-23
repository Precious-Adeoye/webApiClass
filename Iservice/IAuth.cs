using webApiClass.DTO;
using static webApiClass.DTO.Responses;

namespace webApiClass.Iservice
{
    public interface IAuth
    {
        Task <GeneralResponse> CreateUser(RegisterDTO registerDTO);

        Task <LogInResponse> LogInUser(LoginDTO loginDTO);
    }
}
