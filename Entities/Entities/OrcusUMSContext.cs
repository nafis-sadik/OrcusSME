using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Entities
{
    public partial class OrcusUMSContext : DbContext
    {
        public OrcusUMSContext()
        {
        }

        public OrcusUMSContext(DbContextOptions<OrcusUMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<EmailId> EmailIds { get; set; }
        public virtual DbSet<Number> Numbers { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SubscriptionLog> SubscriptionLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;User Id=root;Database=orcus_ums;Port=3306;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ActivityType>(entity =>
            {
                entity.Property(e => e.ActivityTypeId).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ActivityName).HasMaxLength(50);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LocationLabel).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.StreetAddress).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Addresses_User");
            });

            modelBuilder.Entity<EmailId>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EmailId");

                entity.Property(e => e.IsPrimaryMail)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.MailId).IsRequired();

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailId_User");
            });

            modelBuilder.Entity<Number>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IsBkash)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.IsNagad)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.IsRocket)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Number1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Number");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Numbers_User");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.SubscriptionId).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DurationMonths).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.SubscriptionName)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.SubscriptionPrice).HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<SubscriptionLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SubscriptionLog");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");

                entity.Property(e => e.SubscriptionId).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.HasOne(d => d.Subscription)
                    .WithMany()
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FK_SubscriptionLog_Subscriptions");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SubscriptionLog_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.AccountBalance).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserActivityLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserActivityLog");

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.ActivityTypeId).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ActivityType)
                    .WithMany()
                    .HasForeignKey(d => d.ActivityTypeId)
                    .HasConstraintName("FK_UserActivityLog_ActivityTypes");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserActivityLog_User1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
