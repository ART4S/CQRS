namespace WebFeatures.Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        string ComputeHash(string password);

        bool Verify(string hashedPassword, string expectedPassword);
    }
}
