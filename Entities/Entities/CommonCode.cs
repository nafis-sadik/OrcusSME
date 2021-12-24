using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class CommonCode
    {
        public CommonCode()
        {
            ProductAttributes = new HashSet<ProductAttribute>();
        }

        public int CommonCodeId { get; set; }
        public string CommonCodeName { get; set; }

        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
