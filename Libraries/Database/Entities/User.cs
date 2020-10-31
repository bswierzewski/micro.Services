using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Database.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public bool IsActive { get; set; }
    }
}