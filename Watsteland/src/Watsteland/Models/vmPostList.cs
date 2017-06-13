using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmPostList
    {

        public List<Post> posts { get; set; }
        public int ForumId { get; set; }
        public int ThreadId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public Thread BaseThread { get; set; }
        public Forum BaseForum { get; set; }
    }
}
