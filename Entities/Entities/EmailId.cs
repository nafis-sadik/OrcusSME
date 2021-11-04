﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class EmailId
    {
        public string UserId { get; set; }
        public int Emailid { get; set; }
        public string IsPrimaryMail { get; set; }
        public string Status { get; set; }
        public string EmailAddress { get; set; }

        public virtual User User { get; set; }
    }
}
