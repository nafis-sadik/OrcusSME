using DataLayer.Entities;
using DataLayer.MySql;
using Repositories.Abstraction;
using Repositories.Implementation;

namespace Repositories
{
    public interface IActivityTypeRepo : IRepositoryBase<ActivityType> { }
    public class ActivityTypeRepo : RepositoryBase<ActivityType>, IActivityTypeRepo { public ActivityTypeRepo(): base() { } }
    public interface IAddressRepo : IRepositoryBase<Address> { }
    public class AddressRepo : RepositoryBase<Address>, IAddressRepo { public AddressRepo() : base() { } }
    public interface IEmailIdRepo : IRepositoryBase<EmailId> { }
    public class EmailIdRepo : RepositoryBase<EmailId>, IEmailIdRepo { public EmailIdRepo() : base() { } }
    public interface INumberRepo : IRepositoryBase<Number> { }
    public class NumberRepo : RepositoryBase<Number>, INumberRepo { public NumberRepo() : base() { } }
    public interface ISubscriptionRepo : IRepositoryBase<Subscription> { }
    public class SubscriptionRepo : RepositoryBase<Subscription>, ISubscriptionRepo { public SubscriptionRepo() : base() { } }
    public interface ISubscriptionLogRepo : IRepositoryBase<SubscriptionLog> { }
    public class SubscriptionLogRepo : RepositoryBase<SubscriptionLog>, ISubscriptionLogRepo { public SubscriptionLogRepo() : base() { } }
    public interface IUserActivityLogRepo : IRepositoryBase<UserActivityLog> { }
    public class UserActivityLogRepo : RepositoryBase<UserActivityLog>, IUserActivityLogRepo { public UserActivityLogRepo() : base() { } }
    public interface ICategoryRepo : IRepositoryBase<Category> { }
    public class CategoryRepo : RepositoryBase<Category>, ICategoryRepo { 
        public CategoryRepo(OrcusUMSContext context) : base(context) { }
    }
    public interface ICrashLogRepo : IRepositoryBase<CrashLog> { }
    public class CrashLogRepo : RepositoryBase<CrashLog>, ICrashLogRepo
    {
        public CrashLogRepo(OrcusUMSContext context) : base(context) { }
    }

    public interface IOutletManagerRepo : IRepositoryBase<Outlet>
    {
        public new bool Add(Outlet entity);
    }
}
