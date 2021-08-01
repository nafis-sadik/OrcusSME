using System;
using System.Collections.Generic;

#nullable disable

namespace MySQL
{
    public partial class Emailid
    {
        public string UserId { get; set; }
        public string MailId { get; set; }
        public string IsPrimaryMail { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
    }
}
