namespace WebFeatures.Application.Interfaces
{
    public interface IPasswordEncoder
    {
        string EncodePassword(string password);
        string DecodePassword(string password);
    }
}
