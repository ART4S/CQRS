namespace WebFeatures.Application.Interfaces.Security
{
    public interface IPasswordEncoder
    {
        string EncodePassword(string password);
        string DecodePassword(string password);
    }
}
