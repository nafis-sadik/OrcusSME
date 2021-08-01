using System;
using System.Collections.Generic;

#nullable disable

namespace MySQL
{
    public partial class Crashlog
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorInner { get; set; }
        public string Data { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
