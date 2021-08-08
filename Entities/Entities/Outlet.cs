using System;
using System.Collections.Generic;

#nullable disable

namespace Entities
{
    public partial class Outlet
    {
        public decimal OutletId { get; set; }
        public string OutletName { get; set; }
        public string OutletAddresss { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
    }
}
