namespace WebFeatures.Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        string EncodePassword(string password);
        string DecodePassword(string password);
    }
}
