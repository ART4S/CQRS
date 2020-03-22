using System;
using System.Collections.Generic;
using System.Linq;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
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

        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();
        private readonly List<UserRole> _userRoles = new List<UserRole>();

        public void AssignRole(Guid roleId)
        {
            var rel = new UserRole(Id, roleId);

            if (!_userRoles.Any(x => x.UserId == Id && x.RoleId == roleId))
            {
                _userRoles.Add(rel);
            }
        }
    }
}
