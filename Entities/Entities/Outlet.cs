using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class Outlet
    {
        public Outlet()
        {
            Categories = new HashSet<Category>();
        }

        public decimal OutletId { get; set; }
        public string OutletName { get; set; }
        public string OutletAddresss { get; set; }
        public string EcomUrl { get; set; }
        public string UserId { get; set; }
        public bool? RequestSite { get; set; }
        public string SiteUrl { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
