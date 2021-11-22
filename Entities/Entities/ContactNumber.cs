using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class ContactNumber
    {
        public string UserId { get; set; }
        public string Number { get; set; }
        public string IsBkash { get; set; }
        public string IsNagad { get; set; }
        public string IsRocket { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
    }
}
