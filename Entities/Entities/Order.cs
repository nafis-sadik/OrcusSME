using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class Order
    {
        public Order()
        {
            InventoryLogs = new HashSet<InventoryLog>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<InventoryLog> InventoryLogs { get; set; }
    }
}
