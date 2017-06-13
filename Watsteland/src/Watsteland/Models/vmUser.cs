using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmUser
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserPassowrd { get; set; }
        public List<IdentityUserRole<string>> UserRoles { get; set; }
        public List<vmRole> vmUserRoles { get; set; }
        public string RoleName { get; set; }
    }
}
