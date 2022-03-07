using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class ProductPicture
    {
        public int ProductPictureId { get; set; }
        public int FileId { get; set; }
        public int ProductId { get; set; }

        public virtual File File { get; set; }
        public virtual Product Product { get; set; }
    }
}
