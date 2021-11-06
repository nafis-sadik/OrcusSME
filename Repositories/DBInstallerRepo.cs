using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class DBInstallerRepo
    {
        public static bool? InstallDB(int DBSelector)
        {
            try
            {
                switch (DBSelector)
                {
                    case 1:
                        return new DataLayer.MySql.DatabaseInstaller().InstallDatabase();
                    case 2:
                        return new DataLayer.MSSQL.DatabaseInstaller().InstallDatabase();
                    default:
                        break;
                }
                return null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
