using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataLayer.Entities;

#nullable disable

namespace DataLayer.MSSQL
{
    public partial class OrcusSMEContext : DbContext
    {
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
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<InventoryLog> InventoryLogs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Outlet> Outlets { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductPicture> ProductPictures { get; set; }
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
                entity.HasIndex(e => e.UserId, "IX_Addresses_UserId");

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
                entity.HasIndex(e => e.OutletId, "IX_Categories_OutletId");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

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
                entity.Property(e => e.CommonCodeName)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ContactNumber>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.UserId, "IX_ContactNumbers_UserId");

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

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ContactNumber).HasMaxLength(255);

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.HasOne(d => d.Outletl)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.OutletlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_Outlets");
            });

            modelBuilder.Entity<EmailAddress>(entity =>
            {
                entity.HasKey(e => e.EmailPk)
                    .HasName("PK__EmailAdd__41EF875392D05F62");

                entity.HasIndex(e => e.UserId, "IX_EmailAddresses_UserId");

                entity.Property(e => e.EmailPk)
                    .ValueGeneratedNever()
                    .HasColumnName("EMailPk");

                entity.Property(e => e.EmailAddress1)
                    .HasMaxLength(50)
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

            modelBuilder.Entity<InventoryLog>(entity =>
            {
                entity.ToTable("InventoryLog");

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryUpdateType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.InventoryLogs)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_InventoryLog_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InventoryLogs)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryLog_Products");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customers");
            });

            modelBuilder.Entity<Outlet>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Outlets_UserId");

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
                entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.UnitType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UnitTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductUnitTypes");
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.HasKey(e => e.AttributeId);

                entity.HasIndex(e => e.AttributeTypes, "IX_ProductAttributes_AttributeTypes");

                entity.HasIndex(e => e.ProductId, "IX_ProductAttributes_ProductId");

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

            modelBuilder.Entity<ProductPicture>(entity =>
            {
                entity.HasOne(d => d.File)
                    .WithMany(p => p.ProductPictures)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPictures_Files1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPictures)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPictures_Products2");
            });

            modelBuilder.Entity<ProductUnitType>(entity =>
            {
                entity.HasKey(e => e.UnitTypeIds)
                    .HasName("PK__ProductU__5085C454884EB7EB");

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

                entity.HasIndex(e => e.SubscriptionId, "IX_SubscriptionLog_SubscriptionId");

                entity.HasIndex(e => e.UserId, "IX_SubscriptionLog_UserId");

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

                entity.HasIndex(e => e.ActivityTypeId, "IX_UserActivityLog_ActivityTypeId");

                entity.HasIndex(e => e.UserId, "IX_UserActivityLog_UserId");

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
