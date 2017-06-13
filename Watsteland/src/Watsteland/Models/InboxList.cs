using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class InboxList
    {
        public List<PrivateMessage> messages { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
