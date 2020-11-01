using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string[] Roles { get; set; }
    }
}
