using Entities;

namespace Repositories
{
    public interface IActivityTypeRepo : IRepositoryBase<Activitytype> { }
    public class ActivityTypeRepo : RepositoryBase<Activitytype>, IActivityTypeRepo { public ActivityTypeRepo(): base() { } }
    public interface IAddressRepo : IRepositoryBase<Address> { }
    public class AddressRepo : RepositoryBase<Address>, IAddressRepo { public AddressRepo() : base() { } }
    public interface IEmailIdRepo : IRepositoryBase<Emailid> { }
    public class EmailIdRepo : RepositoryBase<Emailid>, IEmailIdRepo { public EmailIdRepo() : base() { } }
    public interface INumberRepo : IRepositoryBase<Number> { }
    public class NumberRepo : RepositoryBase<Number>, INumberRepo { public NumberRepo() : base() { } }
    public interface ISubscriptionRepo : IRepositoryBase<Subscription> { }
    public class SubscriptionRepo : RepositoryBase<Subscription>, ISubscriptionRepo { public SubscriptionRepo() : base() { } }
    public interface ISubscriptionLogRepo : IRepositoryBase<Subscriptionlog> { }
    public class SubscriptionLogRepo : RepositoryBase<Subscriptionlog>, ISubscriptionLogRepo { public SubscriptionLogRepo() : base() { } }
    public interface IUserRepo : IRepositoryBase<User> { }
    public class UserRepo : RepositoryBase<User>, IUserRepo { public UserRepo() : base() { } }
    public interface IUserActivityLogRepo : IRepositoryBase<Useractivitylog> { }
    public class UserActivityLogRepo : RepositoryBase<Useractivitylog>, IUserActivityLogRepo { public UserActivityLogRepo() : base() { } }
    public interface ICrashLogRepo : IRepositoryBase<Crashlog> { }
    public class CrashLogRepo : RepositoryBase<Crashlog>, ICrashLogRepo { public CrashLogRepo() : base() { } }
}
