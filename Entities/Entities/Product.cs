using System;
using System.Collections.Generic;

#nullable disable

namespace Entities
{
    public partial class Product
    {
        public Product()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
