using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class vmStats
    {
        public int NumberOfForums { get; set; }
        public int NumberOfThreads { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfCommunityUpdates { get; set; }
        public int NumberOfCommunityUpdateComments { get; set; }
        public int NumberOfCommunityMembers { get; set; }
        public int NumberOfPolls { get; set; }
        public int NumberOfPeopleVoted { get; set; }
    }
}
