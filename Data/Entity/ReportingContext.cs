using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entity
{
    public partial class ReportingContext : DbContext
    {
        public virtual DbSet<YammerHashtags> YammerHashtags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<YammerHashtags>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BonusAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.BonusValue).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Hashtag).HasMaxLength(128);

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.MessageUrl).HasMaxLength(500);

                entity.Property(e => e.Recipient).HasMaxLength(50);

                entity.Property(e => e.Sender).HasMaxLength(50);

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.ThreadId).HasColumnName("ThreadID");
            });
        }
    }
}