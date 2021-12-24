using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class ProductAttribute
    {
        public int AttributeId { get; set; }
        public string AttributeValues { get; set; }
        public int AttributeTypes { get; set; }
        public int ProductId { get; set; }

        public virtual CommonCode AttributeTypesNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
