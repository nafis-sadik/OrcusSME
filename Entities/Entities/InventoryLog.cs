using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class InventoryLog
    {
        public int InventoryLogId { get; set; }
        public int ProductId { get; set; }
        public string InventoryUpdateType { get; set; }
        public DateTime ActivityDate { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public virtual Product Product { get; set; }
    }
}
