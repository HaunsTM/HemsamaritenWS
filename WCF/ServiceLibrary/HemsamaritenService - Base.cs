//Here is the once-per-application setup information
using System;
using System.ServiceModel;
using WCF.ServiceLibrary.Interfaces;

namespace WCF.ServiceLibrary
{
    public partial class HemsamaritenService
    {
        /// <summary>
        /// Creates and initializes a database used for f ex log4net
        /// </summary>
        void IBaseDuplexService.CreateAndInitializeHemsamaritenDB()
        {
            try
            {
                lock (_syncRoot)
                {
                    var databaseDealer = new WCF.BLL.DatabaseDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE_DEBUG_LOG);

                    var databaseCreated = databaseDealer.CreateAndInitializeHemsamaritenDB();
                    if (databaseCreated)
                    {
                        log.Debug(String.Format("Created and initialized HemsamaritenDB!"));
                    }
                    else
                    {
                        throw new Exception(String.Format("Failed in creating and initializing HemsamaritenDB."));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in creating and initializing HemsamaritenDB."), ex);
            }
        }
    }
}
