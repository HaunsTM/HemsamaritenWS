//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Hemsamariten
{
    using System;
    using System.IO;
    using System.ComponentModel;
    using System.ServiceModel;
    using System.Configuration;
    using System.Configuration.Install;
    using System.ServiceProcess;

    using log4net;

    //https://msdn.microsoft.com/en-us/library/ms733069(v=vs.110).aspx
    public class Service : ServiceBase
    {

        #region FIELDS

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ServiceHost serviceHost = null;

        #endregion

        #region CONSTRUCTOR

        public Service()
        {
        }

        #endregion

        private void InitializeComponent()
        {
            // 
            // Service
            // 
            this.ServiceName = "Hemsamariten.HemsamaritenDuplexService";

        }

        #region Main

        public static void Main(string[] args)
        {
            try
            {
                ServiceBase.Run(new WCFServiceLibrary.HemsamaritenDuplexService());
            }
            catch (FileNotFoundException fNFEx)
            {
                log.Fatal(String.Format("Missing file [ {0} ] when setting up Service (from \"public static void Main()\".)", fNFEx.FileName), fNFEx);
                throw fNFEx;
            }
            catch (Exception ex)
            {
                log.Fatal(String.Format("Failed when setting up Service (from \"public static void Main()\".)"), ex);
                throw ex;
            }
        }

        #endregion

        #region OnStart() and OnStop()

        // Start the Windows service.
        protected override void OnStart(string[] args)
        {
            try
            {
                // Override the OnStart(String[]) method by creating and opening 
                // a new ServiceHost instance as shown in the following code.
                if (this.serviceHost != null)
                {
                    this.serviceHost.Close();
                }

                // Create a ServiceHost for the CalculatorService type and 
                // provide the base address.
                this.serviceHost = new ServiceHost(typeof(Service));

                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                this.serviceHost.Open();
            }
            catch (Exception ex)
            {
                log.Fatal(String.Format("Failed when starting Service (from \"protected override void OnStart()\"."), ex);
                throw ex;
            }
        }

        protected override void OnStop()
        {
            try
            {
                // Override the OnStop method closing the ServiceHost.
                if (this.serviceHost != null)
                {
                    this.serviceHost.Close();
                    this.serviceHost = null;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(String.Format("Failed when stopping Service (from \"protected override void OnStop()\"."), ex);
                throw ex;
            }
        }

        #endregion
    }

    #region INSTALLER
    // Provide the ProjectInstaller class which allows 
    // the service to be installed by the Installutil.exe tool
    [System.ComponentModel.RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "HemsamaritenWindowsService";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
    #endregion
}