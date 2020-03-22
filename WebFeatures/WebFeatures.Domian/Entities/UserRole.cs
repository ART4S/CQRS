using System;

namespace WebFeatures.Domian.Entities
{
    public class UserRole
    {
        public Guid UserId { get; }
        public User User { get; }

        public Guid RoleId { get; }
        public Role Role { get; }

        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        private UserRole() { } // For EF
    }
}
