namespace WebFeatures.Application.Interfaces.Security
{
    public interface IUserManager
    {
        RegistrationResult CreateUser(string name, string email, string password);
    }

    public class RegistrationResult
    {
        public string UserId { get; }

        public string[] Errors { get; }

        private RegistrationResult(string userId = default, string[] errors = default) 
            => (UserId, Errors) = (userId, errors);

        public static RegistrationResult Success(string userId)
            => new RegistrationResult(userId: userId);

        public static RegistrationResult Failure(string[] errors) 
            => new RegistrationResult(errors: errors);
    }
}
