using DataLayer.Entities;

namespace Repositories.Abstraction
{
    public interface IUserRepo : IRepositoryBase<User>
    {
        public User FindUser(string userName, string pass);
    }
}
