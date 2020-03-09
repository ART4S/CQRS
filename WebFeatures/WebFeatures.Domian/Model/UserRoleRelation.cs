using System;

namespace WebFeatures.Domian.Model
{
    public class UserRoleRelation
    {
        public Guid UserId { get; }
        public User User { get; }

        public Guid RoleId { get; }
        public Role Role { get; }

        public UserRoleRelation(User user, Role role)
        {
            UserId = user.Id;
            User = user;

            RoleId = role.Id;
            Role = role;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;

            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode() ^ RoleId.GetHashCode();
        }
    }
}
