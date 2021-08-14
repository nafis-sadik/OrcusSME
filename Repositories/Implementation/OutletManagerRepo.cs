using Entities;
using Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class OutletManagerRepo : RepositoryBase<Outlet>, IOutletManagerRepo {
        public OutletManagerRepo() : base() { }

        public bool RegisterNewOutlet(Outlet outlet)
        {
            if (db.Outlets.Where(x => x.UserId == outlet.UserId && x.OutletName == outlet.OutletName).Count() > 0)
                return false;
            else
            {
                db.Outlets.Add(outlet);
                db.SaveChanges();
            }
            return true;
        }

        void Add(Outlet entity) { return; }
    }
}
