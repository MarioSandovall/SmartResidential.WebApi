using Model.Models.Login;

namespace Service.Interfaces
{
    public interface ILoginService
    {
        string BuildToken(UserLogin user);
    }
}
