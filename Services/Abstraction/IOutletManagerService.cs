using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOutletManagerService
    {
        public List<Outlet> AddOutlet(Outlet outlet);
        public List<Outlet> UpdateOutlet(Outlet outlet);
        public List<Outlet> ArchiveOutlet(Outlet outlet);
        public List<Outlet> GetOutletsByUserId(string UserId);
        public Outlet GetOutlet(decimal OutletId);
        public bool? OrderSite(decimal OutletId, out string response);
    }
}
