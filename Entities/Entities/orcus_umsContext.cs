using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Entities
{
    public partial class orcus_umsContext : DbContext
    {
        public orcus_umsContext()
        {
        }

        public orcus_umsContext(DbContextOptions<orcus_umsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activitytype> Activitytypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Crashlog> Crashlogs { get; set; }
        public virtual DbSet<Emailid> Emailids { get; set; }
        public virtual DbSet<Number> Numbers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Subscriptionlog> Subscriptionlogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Useractivitylog> Useractivitylogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")).Build();
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL(configuration.GetConnectionString("XAMPP"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activitytype>(entity =>
            {
                entity.ToTable("activitytypes");

                entity.Property(e => e.ActivityTypeId).HasColumnType("decimal(18,0)");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("addresses");

                entity.HasIndex(e => e.UserId, "IX_Addresses_UserId");

                entity.Property(e => e.GoogleMapsLocation).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LocationLabel)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Addresses_User");
            });

            modelBuilder.Entity<Crashlog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("crashlog");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Data).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ErrorInner)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MethodName)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Emailid>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("emailid");

                entity.HasIndex(e => e.UserId, "IX_EmailId_UserId");

                entity.Property(e => e.IsPrimaryMail)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'NULL'")
                    .IsFixedLength(true);

                entity.Property(e => e.MailId).IsRequired();

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'NULL'")
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

                entity.ToTable("numbers");

                entity.HasIndex(e => e.UserId, "IX_Numbers_UserId");

                entity.Property(e => e.IsBkash)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'NULL'")
                    .IsFixedLength(true);

                entity.Property(e => e.IsNagad)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'NULL'")
                    .IsFixedLength(true);

                entity.Property(e => e.IsRocket)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'NULL'")
                    .IsFixedLength(true);

                entity.Property(e => e.Number1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Number");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'NULL'")
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

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.ProductId).HasColumnType("int(11)");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("subscriptions");

                entity.HasIndex(e => e.ProductId, "SubscriptionOfProduct");

                entity.Property(e => e.SubscriptionId).HasColumnType("decimal(18,0)");

                entity.Property(e => e.DurationMonths)
                    .HasColumnType("decimal(18,0)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SubscriptionName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SubscriptionPrice)
                    .HasColumnType("decimal(18,0)")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("SubscriptionOfProduct");
            });

            modelBuilder.Entity<Subscriptionlog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("subscriptionlog");

                entity.HasIndex(e => e.SubscriptionId, "IX_SubscriptionLog_SubscriptionId");

                entity.HasIndex(e => e.UserId, "IX_SubscriptionLog_UserId");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnType("decimal(18,0)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

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
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.AccountBalance)
                    .HasColumnType("decimal(18,0)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.ProfilePicLoc).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Useractivitylog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("useractivitylog");

                entity.HasIndex(e => e.ActivityTypeId, "IX_UserActivityLog_ActivityTypeId");

                entity.HasIndex(e => e.UserId, "IX_UserActivityLog_UserId");

                entity.Property(e => e.ActivityTypeId)
                    .HasColumnType("decimal(18,0)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

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
