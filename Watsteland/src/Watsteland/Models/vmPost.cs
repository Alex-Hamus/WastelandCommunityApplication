using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmPost
    {
        public int ForumId { get; set; }
        public int ThreadId { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
