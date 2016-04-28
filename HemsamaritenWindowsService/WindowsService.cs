using System.ServiceModel;
using System.ServiceProcess;

namespace HemsamaritenWindowsService
{
    public partial class WindowsService : ServiceBase
    {

        internal static ServiceHost myServiceHost = null;

        public WindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (myServiceHost != null)
            {
                myServiceHost.Close();
            }
            myServiceHost = new ServiceHost(typeof(WCF.ServiceLibrary.HemsamaritenDuplexService));
            myServiceHost.Open();
        }
        protected override void OnStop()
        {
            if (myServiceHost != null)
            {
                myServiceHost.Close();
                myServiceHost = null;
            }
        }
    }
}
