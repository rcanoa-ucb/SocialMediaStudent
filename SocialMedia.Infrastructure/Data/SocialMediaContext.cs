using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
namespace SocialMedia.Infrastructure.Data;
public class SocialMediaContext : DbContext
{
    public SocialMediaContext(DbContextOptions<SocialMediaContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("User"); b.HasKey(x => x.Id);
            b.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            b.Property(x => x.Email).HasMaxLength(100).IsRequired();
            b.HasIndex(x => x.Email).IsUnique();
            b.Property(x => x.Telephone).HasMaxLength(15);
        });
        modelBuilder.Entity<Post>(b =>
        {
            b.ToTable("Post"); b.HasKey(x => x.Id);
            b.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            b.Property(x => x.Imagen).HasMaxLength(500);
            b.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Comment>(b =>
        {
            b.ToTable("Comment"); b.HasKey(x => x.Id);
            b.Property(x => x.Description).HasMaxLength(500).IsRequired();
            b.HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
