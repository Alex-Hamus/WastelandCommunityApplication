using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmPollOptions
    {
        public Poll Poll { get; set; }
        public bool AreadyVoted { get; set; }
        public List<PollOption> PollOptions { get; set; }
    }
}
