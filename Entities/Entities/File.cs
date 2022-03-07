using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class File
    {
        public File()
        {
            ProductPictures = new HashSet<ProductPicture>();
        }

        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
    }
}
