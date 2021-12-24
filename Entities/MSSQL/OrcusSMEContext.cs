using System;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.MSSQL
{
    public partial class OrcusSMEContext : DbContext
    {
        // Scafolding Command
        // Scaffold-DbContext "User Id=DESKTOP-BHB3CJL;Database=OrcusSME;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer
        public OrcusSMEContext()
        {
        }

        public OrcusSMEContext(DbContextOptions<OrcusSMEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CommonCode> CommonCodes { get; set; }
        public virtual DbSet<ContactNumber> ContactNumbers { get; set; }
        public virtual DbSet<Crashlog> Crashlogs { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Outlet> Outlets { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductUnitType> ProductUnitTypes { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<SubscriptionLog> SubscriptionLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("User Id=DESKTOP-BHB3CJL;Database=OrcusSME;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ActivityType>(entity =>
            {
                entity.Property(e => e.ActivityTypeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).ValueGeneratedNever();

                entity.Property(e => e.GoogleMapsLocation).HasColumnType("text");

                entity.Property(e => e.LocationLabel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Addresses_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OutletId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_Categories_Outlets");
            });

            modelBuilder.Entity<CommonCode>(entity =>
            {
                entity.Property(e => e.CommonCodeId).ValueGeneratedNever();

                entity.Property(e => e.CommonCodeName)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ContactNumber>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IsBkash)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IsNagad)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IsRocket)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Numbers_User");
            });

            modelBuilder.Entity<Crashlog>(entity =>
            {
                entity.ToTable("Crashlog");

                entity.Property(e => e.CrashLogId).ValueGeneratedNever();

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Data).HasColumnType("text");

                entity.Property(e => e.ErrorInner).HasColumnType("text");

                entity.Property(e => e.ErrorMessage).HasColumnType("text");

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmailAddress>(entity =>
            {
                entity.HasKey(e => e.EmailPk)
                    .HasName("PK__EmailAdd__41EF875392D05F62");

                entity.Property(e => e.EmailPk)
                    .ValueGeneratedNever()
                    .HasColumnName("EMailPk");

                entity.Property(e => e.EmailAddress1)
                    .HasColumnType("text")
                    .HasColumnName("EmailAddress");

                entity.Property(e => e.IsPrimaryMail)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmailAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailId_User");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.ImageId).ValueGeneratedNever();

                entity.Property(e => e.FkId).HasColumnName("FK_Id");

                entity.Property(e => e.StorageLocation).IsRequired();

                entity.Property(e => e.TableName).IsRequired();
            });

            modelBuilder.Entity<Outlet>(entity =>
            {
                entity.Property(e => e.OutletId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.EcomUrl)
                    .HasColumnType("text")
                    .HasColumnName("EComURL");

                entity.Property(e => e.OutletAddresss).HasColumnType("text");

                entity.Property(e => e.OutletName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteUrl).HasColumnType("text");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Outlets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Outlets_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.HasKey(e => e.AttributeId);

                entity.Property(e => e.AttributeId).ValueGeneratedNever();

                entity.Property(e => e.AttributeValues)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.AttributeTypesNavigation)
                    .WithMany(p => p.ProductAttributes)
                    .HasForeignKey(d => d.AttributeTypes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAttributes_CommonCodes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductAttributes)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAttributes_Products");
            });

            modelBuilder.Entity<ProductUnitType>(entity =>
            {
                entity.HasKey(e => e.UnitTypeIds)
                    .HasName("PK__ProductU__5085C454884EB7EB");

                entity.Property(e => e.UnitTypeIds).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UnitTypeNames)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId)
                    .HasName("PK__Services__9A2B249DF26AE971");

                entity.Property(e => e.SubscriptionId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DurationMonths).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SubscriptionName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SubscriptionPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<SubscriptionLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SubscriptionLog");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Subscription)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");

                entity.Property(e => e.SubscriptionId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

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
                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountBalance).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.ProfilePicLoc).HasColumnType("text");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserActivityLog>(entity =>
            {
                entity.HasKey(e => e.ActivityLogIn)
                    .HasName("PK__UserActi__19A9B7B9B06B38FA");

                entity.ToTable("UserActivityLog");

                entity.Property(e => e.ActivityLogIn).ValueGeneratedNever();

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.ActivityTypeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Browser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress")
                    .IsFixedLength(true);

                entity.Property(e => e.Misc).HasColumnType("text");

                entity.Property(e => e.Os)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("OS")
                    .IsFixedLength(true);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.UserActivityLogs)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .HasConstraintName("FK_UserActivityLog_ActivityTypes");

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
