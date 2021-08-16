using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public static class CommonConstants
    {
        public const string True = "Y";
        public const string False = "N";

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
            public const string Pending = "P";
            public const int LogedIn = 1;
            public const int Guest = 0;
        }
        public static class ActivityTypes
        {
            public const string LogIn = "Log In Attempt";
            public const string SignUp = "Sign Up Attempt";
            public const string GetData = "D";
            public const string PostData = "P";
        }
        public static class HttpResponseMessages
        {
            public const string UserNotFound = "User Not Found";
            public const string PasswordMismatched = "Password Mismatched";
            public const string UserNameExists = "User Name Already Exists,Please Try a Different User Name";
            public const string MailExists = "Account with this Email Id already exists,Try resting password";
            public const string Exception = "An error occurred while executing operation. Please contact support team.";
        }

        public const string MsgInInnerException = "An error occurred while updating the entries. See the inner exception for details.";

        public const int DefaultCreditBalance = 1000;
    }
}
