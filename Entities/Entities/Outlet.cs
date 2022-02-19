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
            Customers = new HashSet<Customer>();
        }

        public int OutletId { get; set; }
        public string OutletName { get; set; }
        public string OutletAddresss { get; set; }
        public string EcomUrl { get; set; }
        public string UserId { get; set; }
        public byte? RequestSite { get; set; }
        public string SiteUrl { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
