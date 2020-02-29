namespace WebFeatures.Application.Interfaces.Security
{
    public interface IAuthService
    {
        bool LoginExists(string login);
    }
}
