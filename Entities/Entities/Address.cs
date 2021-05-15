using System;
using System.Collections.Generic;

#nullable disable

namespace Entities
{
    public partial class Address
    {
        public string UserId { get; set; }
        public string StreetAddress { get; set; }
        public string GoogleMapsLocation { get; set; }
        public string LocationLabel { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
    }
}
