using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comment");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.HasIndex(e => e.PostId, "FK_Comment_Post");
            builder.HasIndex(e => e.UserId, "FK_Comment_User");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Date).HasColumnType("datetime");
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.IsActive).HasColumnType("bit(1)");

            builder.HasOne(d => d.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Post");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_User");
        }
    }
}