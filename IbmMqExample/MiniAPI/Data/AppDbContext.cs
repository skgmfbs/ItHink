using Microsoft.EntityFrameworkCore;
using MiniAPI.Models;

namespace MiniAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ToDo> ToDos => Set<ToDo>();
        public DbSet<ToDoState> ToDoStates => Set<ToDoState>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoState>()
                .HasOne(p => p.ToDo)
                .WithMany(b => b.States)
                .HasForeignKey(p => p.ToDoId);
        }
    }
}