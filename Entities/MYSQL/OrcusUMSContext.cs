using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;

#nullable disable

namespace DataLayer.MySql
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
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CrashLog> CrashLogs { get; set; }
        public virtual DbSet<EmailId> EmailIds { get; set; }
        public virtual DbSet<Number> Numbers { get; set; }
        public virtual DbSet<Outlet> Outlets { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SubscriptionLog> SubscriptionLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Database=OrcusUMS;Trusted_Connection=True;");
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

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.OutletId).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_Categories_Outlets");
            });

            modelBuilder.Entity<CrashLog>(entity =>
            {
                entity.ToTable("CrashLog");

                entity.Property(e => e.CrashLogId).ValueGeneratedNever();

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmailId>(entity =>
            {
                entity.HasKey(e => e.Emailid);

                entity.ToTable("EmailId");

                entity.Property(e => e.Emailid)
                    .ValueGeneratedNever()
                    .HasColumnName("EMailId");

                entity.Property(e => e.IsPrimaryMail)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmailIds)
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

            modelBuilder.Entity<Outlet>(entity =>
            {
                entity.Property(e => e.OutletId).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.EcomUrl).HasColumnName("EComURL");

                entity.Property(e => e.OutletName).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Outlets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Outlets_User");
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

                entity.Property(e => e.Subscription)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");

                entity.Property(e => e.SubscriptionId).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.HasOne(d => d.SubscriptionNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FK_SubscriptionLog_Subscriptions1");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SubscriptionLog_User1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.AccountBalance).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

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
                entity.HasKey(e => e.ActivityLogIn);

                entity.ToTable("UserActivityLog");

                entity.Property(e => e.ActivityLogIn).ValueGeneratedNever();

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.ActivityTypeId).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Browser)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(15)
                    .HasColumnName("IPAddress")
                    .IsFixedLength(true);

                entity.Property(e => e.Os)
                    .HasMaxLength(10)
                    .HasColumnName("OS")
                    .IsFixedLength(true);

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.UserActivityLogs)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .HasConstraintName("FK_UserActivityLog_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserActivityLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserActivityLog_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
