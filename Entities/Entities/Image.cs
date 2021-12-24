using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public string TableName { get; set; }
        public int FkId { get; set; }
        public string StorageLocation { get; set; }
    }
}
