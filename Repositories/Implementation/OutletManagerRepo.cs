using System.Linq;
using Repositories.Abstraction;
using DataLayer.Entities;
using DataLayer.MySql;
using DataLayer.MSSQL;

namespace Repositories.Implementation
{
    public class OutletManagerRepo : RepositoryBase<Outlet>, IOutletManagerRepo {
        public OutletManagerRepo(OrcusSMEContext context) : base(context) { }
        public new bool Add(Outlet outlet)
        {
            if (Db.Outlets.Count(x => x.UserId == outlet.UserId && x.OutletName == outlet.OutletName) > 0)
                return false;
            Db.Outlets.Add(outlet);
            Db.SaveChanges();
            return true;
        }
    }
}
