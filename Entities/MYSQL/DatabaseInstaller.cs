using DataLayer.MSSQL;
using System;

namespace DataLayer.MySql
{
    public class DatabaseInstaller
    {
        public bool InstallDatabase()
        {
            try
            {
                new OrcusSMEContext().Database.EnsureCreated();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}