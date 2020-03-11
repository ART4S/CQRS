using System.Collections.Generic;
using System.Collections.Immutable;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
{
    public class User : BaseEntity, ISoftDelete
    {
        public string Name { get; set; }
        public string Email { get; }
        public string PasswordHash { get; }
        public bool IsDeleted { get; set; }

        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        private User() { } // For EF

        public IReadOnlyCollection<UserRoleRelation> UserRoles => _userRoles.ToImmutableList();
        private HashSet<UserRoleRelation> _userRoles { get; } = new HashSet<UserRoleRelation>();

        public void AssignRole(Role role)
        {
            var rel = new UserRoleRelation(this, role);
            _userRoles.Add(rel);
        }

        public void RemoveRole(Role role)
        {
            var rel = new UserRoleRelation(this, role);
            _userRoles.Remove(rel);
        }
    }
}
