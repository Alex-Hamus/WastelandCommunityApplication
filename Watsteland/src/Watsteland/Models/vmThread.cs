using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmThread
    {
        public int ThreadId { get; set; }
        public int ForumId { get; set; }
        public int NumberOfPosts { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int Views { get; set; }
        public bool Read { get; set; }
    }
}
