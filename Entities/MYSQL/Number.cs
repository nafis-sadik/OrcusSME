using System;
using System.Collections.Generic;

#nullable disable

namespace MySQL
{
    public partial class Number
    {
        public string UserId { get; set; }
        public string Number1 { get; set; }
        public string IsBkash { get; set; }
        public string IsNagad { get; set; }
        public string IsRocket { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
    }
}
