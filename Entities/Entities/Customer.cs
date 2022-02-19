using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public int OutletlId { get; set; }

        public virtual Outlet Outletl { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
