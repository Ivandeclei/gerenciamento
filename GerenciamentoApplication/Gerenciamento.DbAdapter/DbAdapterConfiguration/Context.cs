using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter.DbAdapterConfiguration
{
    public class Context : DbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<TaskProject> Task { get; set; }
        //public DbSet<User> User { get; set; }
        //public DbSet<Comments> Comments { get; set; }
        public DbSet<HistoryUpdate> UpdateHistory { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("gerenciamento");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Project).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskProject).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HistoryUpdate).Assembly);


            modelBuilder.Entity<Project>()
                .HasMany(x => x.Tasks);

            modelBuilder.Entity<TaskProject>()
            .HasOne(tp => tp.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(tp => tp.ProjectId);

            modelBuilder.Entity<HistoryUpdate>()
                .HasOne(c => c.TaskProject)
                .WithMany(tp => tp.Histories)
                .HasForeignKey(c => c.TaskProjectId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
