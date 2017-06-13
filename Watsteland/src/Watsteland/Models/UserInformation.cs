using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class UserInformation
    {
        [Key]
        public int UserInformatoinId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
