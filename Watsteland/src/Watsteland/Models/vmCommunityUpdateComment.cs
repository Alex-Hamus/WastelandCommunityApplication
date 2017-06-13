using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmCommunityUpdateComment
    {
        public CommunityUpdates CommunityUpdate { get; set; }
        public List<CommunityUpdateComment> Comments { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
