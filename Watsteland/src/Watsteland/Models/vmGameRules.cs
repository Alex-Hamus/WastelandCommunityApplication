using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmGameRules
    {
        public Game Game { get; set; }
        public List<Rules> Rules { get; set; }
    }

    public class vmGame
    {
        public List<vmGameRules> Games { get; set; }
}
}
