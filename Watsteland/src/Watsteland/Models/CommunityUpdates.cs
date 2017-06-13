using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class CommunityUpdates
    {
        [Key]
        public int CommunityUpdateId { get; set; }
        public string UpdateTitle { get; set; }
        public string UdateInformation { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set;  }
        public int NumberOfComments { get; set; }
    }

    public class CommunityUpdateComment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentTitle { get; set; }
        public string CommentDescription { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int CommunityUpdateId { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Update
    {
        [Key]
        public int UpdateId { get; set; }
        public string UpdateInformation { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
