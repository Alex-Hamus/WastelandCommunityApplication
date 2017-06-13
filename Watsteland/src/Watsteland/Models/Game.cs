using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string GameDescription { get; set; }
    }
}
