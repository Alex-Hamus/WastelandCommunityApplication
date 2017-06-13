using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class Rules
    {
        [Key]
        public int RulesId { get; set; }
        public int GameId { get; set; }
        public string RuleDescription { get; set; }
    }
}
