using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class WellcomeMessage
    {
        [Key]
        public int WellcomeMessageId { get; set; }
        public string WellcomeTitle { get; set; }
        public string MessageDescription { get; set; }
    }
}
