using Microsoft.EntityFrameworkCore;

namespace MessagingMicroservice.Models
{
    public partial class DatabaseContext : DbContext
    {
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=MessageService;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.FirstUserId).IsRequired();
                entity.Property(e => e.SecondUserId).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired().HasColumnType("datetime");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.Content).IsRequired().HasMaxLength(500);
                entity.Property(e => e.SenderId).IsRequired();
                entity.Property(e => e.ConversationId).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired().HasColumnType("datetime");
                entity.Property(e => e.UpdatedAt).IsRequired().HasColumnType("datetime");
            });

            modelBuilder.Entity<Message>()
                .HasOne(e => e.Conversation)
                .WithMany(e => e.Messages);
        }
    }
}
