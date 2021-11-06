using DBMS.Services.Abstraction;
using Repositories;

namespace DBMS.Services.Implementation
{
    public class MySqlDatabaseInstaller:IDatabaseInstallerService
    {
        public bool? InstallDatabase(int DBSelector)
        {
            return DBInstallerRepo.InstallDB(DBSelector);
        }
    }
}