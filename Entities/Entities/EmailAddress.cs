using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class EmailAddress
    {
        public int EmailPk { get; set; }
        public string UserId { get; set; }
        public string IsPrimaryMail { get; set; }
        public string Status { get; set; }
        public string EmailAddress1 { get; set; }

        public virtual User User { get; set; }
    }
}
