using System.Linq;
using Repositories.Abstraction;
using DataLayer.Entities;
using DataLayer.MySql;

namespace Repositories.Implementation
{
    public class OutletManagerRepo : RepositoryBase<Outlet>, IOutletManagerRepo {
        public OutletManagerRepo(OrcusUMSContext context) : base(context) { }
        public bool RegisterNewOutlet(Outlet outlet)
        {
            if (Db.Outlets.Count(x => x.UserId == outlet.UserId && x.OutletName == outlet.OutletName) > 0)
                return false;
            Db.Outlets.Add(outlet);
            Db.SaveChanges();
            return true;
        }
    }
}
