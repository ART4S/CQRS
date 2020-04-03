﻿using System;
using WebFeatures.Domian.Attibutes;

namespace WebFeatures.Domian.Entities
{
    [TableName("UserRoles")]
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
