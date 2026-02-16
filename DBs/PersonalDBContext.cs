using BEPersonal.Entities;
using Microsoft.EntityFrameworkCore;

namespace BEPersonal.DBs
{
    public class PersonalDBContext : DbContext
    {
        public PersonalDBContext(DbContextOptions<PersonalDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
    
                // Configura la relazione tra User e Message
                modelBuilder.Entity<Message>()
                    .HasOne(m => m.User)
                    .WithMany(u => u.Messages)
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // Se un utente viene cancellato, cancella anche i suoi messaggi
        }
    }
}
