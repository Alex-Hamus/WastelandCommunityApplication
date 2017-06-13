using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Watsteland.Models;

namespace Watsteland.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Forum> Forums { get; set; }

        public DbSet<Thread> Threads { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PrivateMessage> PrivateMessages { get; set; }

        public DbSet<CommunityUpdates> CommunityUpdates { get; set; }

        public DbSet<UserInformation> UserInformation { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Rules> Rules { get; set; }

        public DbSet<Poll> Polls { get; set; }

        public DbSet<PollOption> PollOptions { get; set; }

        public DbSet<PollData> PollData { get; set; }

        public DbSet<Update> Updates { get; set; }

        public DbSet<CommunityUpdateComment> CommunityUpdateComments { get; set; }

        public DbSet<ForumData> ForumData { get; set; }

        public DbSet<ThreadData> ThreadData { get; set; }

        public DbSet<WellcomeMessage> WellcomeMessages { get; set; }
    }
}
