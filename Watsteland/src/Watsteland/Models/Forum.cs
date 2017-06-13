using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Watsteland.Models
{
    public class Forum
    {
        [Key]
        public int ForumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfThreads { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int Views { get; set; }

    }

    public class Thread
    {
        [Key]
        public int ThreadId { get; set; }
        public int ForumId { get; set; }
        public int NumberOfPosts { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int Views { get; set; }

    }

    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public int ThreadId { get; set; }
        public int ForumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int Views { get; set; }

    }

    public class Poll
    {
        [Key]
        public int PollId { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserId { get; set; }
        public int Views { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<PollOption> PollOptions { get; set; }
    }

    public class PollOption
    {
        [Key]
        public int PollOptionsId { get; set; }
        public int PollId { get; set; }
        public string OptionDescription { get; set; }
        public int Votes { get; set; }

        public virtual Poll Poll { get; set; }
    }

    public class PollData
    {
        [Key]
        public int PollDataId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string PollOption { get; set; }
        public int PollId { get; set; }
    }

    public class ForumData
    {
        [Key]
        public int ForumDataId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int ForumId { get; set; }
    }

    public class ThreadData
    {
        [Key]
        public int ThreadDataId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int ThreadId { get; set; }
        public int FourmId { get; set; }
    }
}
