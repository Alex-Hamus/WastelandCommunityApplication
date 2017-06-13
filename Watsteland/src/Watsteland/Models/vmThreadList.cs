using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmThreadList
    {
        public List<vmThread> threads { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ForumId { get; set; }
        public int ThreadId { get; set; }
        public Forum baseForum { get; set; }
    }
}
