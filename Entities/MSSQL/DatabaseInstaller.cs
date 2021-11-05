using System;

namespace DataLayer.MSSQL
{
    public class DatabaseInstaller
    {
        public bool InstallDatabase()
        {
            try
            {
                new OrcusUMSContext().Database.EnsureCreated();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}