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
                        new DataLayer.MySql.DatabaseInstaller().InstallDatabase();
                        return true;
                        break;
                    case 2:
                        break;
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
