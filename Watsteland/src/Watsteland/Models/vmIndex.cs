using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmIndex
    {
        public IOrderedQueryable<CommunityUpdates> CommmunityUpdates { get; set; }
        public List<vmPollOptions> Polls { get; set; }
        public List<PollData> PollData { get; set; }
        public string UserId { get; set; }
        public vmStats Stats { get; set; }
        public IOrderedQueryable<Update> Updates { get; set; }
        public List<WellcomeMessage> WellcomeMessages { get; set; }
    }
}
