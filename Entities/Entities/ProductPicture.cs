using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataLayer.Entities
{
    public partial class ProductPicture
    {
        [Key]
        public int ProductPictureId { get; set; }
        public int FileId { get; set; }
        public int ProductId { get; set; }

        public virtual File File { get; set; }
        public virtual Product ProductPictureNavigation { get; set; }
    }
}
