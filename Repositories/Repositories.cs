using DataLayer.Entities;
using DataLayer.MSSQL;
using DataLayer.MySql;
using Repositories.Abstraction;
using Repositories.Implementation;

namespace Repositories
{
    public interface IActivityTypeRepo : IRepositoryBase<ActivityType> { }
    public class ActivityTypeRepo : RepositoryBase<ActivityType>, IActivityTypeRepo { public ActivityTypeRepo(): base() { } }
    public interface IAddressRepo : IRepositoryBase<Address> { }
    public class AddressRepo : RepositoryBase<Address>, IAddressRepo { public AddressRepo() : base() { } }
    public interface IEmailIdRepo : IRepositoryBase<EmailAddress> { }
    public class EmailIdRepo : RepositoryBase<EmailAddress>, IEmailIdRepo
    {
        public EmailIdRepo(OrcusSMEContext context) : base(context) { }
    }
    public interface INumberRepo : IRepositoryBase<ContactNumber> { }
    public class NumberRepo : RepositoryBase<ContactNumber>, INumberRepo { public NumberRepo() : base() { } }
    public interface IServiceRepo : IRepositoryBase<Service> { }
    public class SubscriptionRepo : RepositoryBase<Service>, IServiceRepo { public SubscriptionRepo() : base() { } }
    public interface ISubscriptionLogRepo : IRepositoryBase<SubscriptionLog> { }
    public class SubscriptionLogRepo : RepositoryBase<SubscriptionLog>, ISubscriptionLogRepo { public SubscriptionLogRepo() : base() { } }
    public interface IUserActivityLogRepo : IRepositoryBase<UserActivityLog> { }
    public class UserActivityLogRepo : RepositoryBase<UserActivityLog>, IUserActivityLogRepo { public UserActivityLogRepo() : base() { } }
    public interface ICategoryRepo : IRepositoryBase<Category> { }
    public class CategoryRepo : RepositoryBase<Category>, ICategoryRepo { 
        public CategoryRepo(OrcusSMEContext context) : base(context) { }
    }
    public interface ICrashLogRepo : IRepositoryBase<Crashlog> { }
    public class CrashLogRepo : RepositoryBase<Crashlog>, ICrashLogRepo
    {
        public CrashLogRepo(OrcusSMEContext context) : base(context) { }
    }
    public interface IOutletManagerRepo : IRepositoryBase<Outlet>
    {
        public new bool Add(Outlet entity);
    }
    public interface IProductUnitTypeRepo : IRepositoryBase<ProductUnitType> { }
    public class ProductUnitTypeRepo : RepositoryBase<ProductUnitType>, IProductUnitTypeRepo
    {
        public ProductUnitTypeRepo(OrcusSMEContext context) : base(context) { }
    }
    public interface IInventoryLogRepo : IRepositoryBase<InventoryLog> { }
    public class InventoryLogRepo : RepositoryBase<InventoryLog>, IInventoryLogRepo
    {
        public InventoryLogRepo(OrcusSMEContext context) : base(context) { }
    }
    public interface IProductRepo : IRepositoryBase<Product> { }
    public class ProductRepo : RepositoryBase<Product>, IProductRepo
    {
        public ProductRepo(OrcusSMEContext context) : base(context) { }
    }
    public interface IFileRepo : IRepositoryBase<File> { }
    public class FileRepo : RepositoryBase<File>, IFileRepo
    {
        public FileRepo(OrcusSMEContext context) : base(context) { }
    }
    public interface IProductPictureRepo : IRepositoryBase<ProductPicture> { }
    public class ProductPictureRepo : RepositoryBase<ProductPicture>, IProductPictureRepo
    {
        public ProductPictureRepo(OrcusSMEContext context) : base(context) { }
    }
}
