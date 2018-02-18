//Here is the once-per-application setup information

using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using WCF.ServiceLibrary.Interfaces;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace WCF.ServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class HemsamaritenService : ServiceBase, IHemsamaritenDuplexService
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly object _syncRoot = new object();

        private const string DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE = "name=HemsamaritenWindowsServiceDBConnection";
        private const string DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE_DEBUG_LOG = "name=HemsamaritenWindowsServiceDebugLogDBConnection";

        private void SetResponseHttpStatus(HttpStatusCode statusCode)
        {
            var context = WebOperationContext.Current;
            context.OutgoingResponse.StatusCode = statusCode;
        }
    }
}
