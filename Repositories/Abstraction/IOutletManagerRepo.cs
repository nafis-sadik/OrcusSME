using DataLayer.Entities;

namespace Repositories.Abstraction
{
    public interface IOutletManagerRepo : IRepositoryBase<Outlet> {
        public bool RegisterNewOutlet(Outlet outlet);
    }
}
