using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmForumList
    {
        public List<vmForum> forums { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<ForumData> Data { get; set; }
    }
}
