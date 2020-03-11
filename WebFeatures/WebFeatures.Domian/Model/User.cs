using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; }
        public string PasswordHash { get; }

        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        private User() { } // For EF

        public IReadOnlyCollection<UserRoleRelation> UserRoles => _userRoles.ToImmutableList();
        private readonly HashSet<UserRoleRelation> _userRoles = new HashSet<UserRoleRelation>();

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
