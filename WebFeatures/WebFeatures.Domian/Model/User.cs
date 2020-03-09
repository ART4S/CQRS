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

        public IReadOnlyCollection<Role> Roles => _userRoles.Select(x => x.Role).ToImmutableList();
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
