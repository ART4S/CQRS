using System;

namespace WebFeatures.Domian.Entities
{
    public class UserRole
    {
        public Guid UserId { get; }
        public User User { get; }

        public Guid RoleId { get; }
        public Role Role { get; }

        public UserRole(User user, Role role)
        {
            UserId = user.Id;
            User = user;

            RoleId = role.Id;
            Role = role;
        }

        private UserRole() { } // For EF
    }
}
