using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class PrivateMessage
    {
        [Key]
        public int MessageId { get; set; }
        public string ToUserId { get; set; }
        public string ToUserName { get; set; }
        public string FromUserId { get; set; }
        public string FromUserName { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDescription { get; set; }
        public bool Read { get; set; }
        public DateTime SentDate { get; set; }

    }
}
