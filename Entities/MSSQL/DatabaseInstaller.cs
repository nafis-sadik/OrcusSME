using Microsoft.EntityFrameworkCore;
using System;
namespace DataLayer.MSSQL
{
    public class DatabaseInstaller
    {
        public bool InstallDatabase()
        {
            try
            {
                new OrcusSMEContext(new DbContextOptions<OrcusSMEContext>()).Database.EnsureCreated();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}