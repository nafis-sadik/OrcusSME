using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public static class CommonConstants
    {
        public static class PasswordConfig {
            public static string Salt
            {
                get { return "Keno Megh Ashe, Hridoyo Akash, Tomaye Dekhite Dei Na"; }
                private set { }
            }
            public const double SaltExpire = 7;
            public const int SaltGeneratorLogRounds = 12;
        }
        public static int StandardPageSize
        {
            get { return 20; }
            private set { }
        }
        public static class SortingParam
        {
            public const int Price_LowToHigh = 1;
            public const int Price_HighToLow = 2;
        }
        public static class StatusTypes
        {
            public const string Active = "A";
            public const string Cancel = "C";
            public const string Archived = "D";
        }
    }
}
