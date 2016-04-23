//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Hemsamariten
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.ServiceModel;
    using System.Configuration.Install;
    using System.Reflection;
    using System.ServiceProcess;

    using log4net;

    //https://msdn.microsoft.com/en-us/library/ms733069(v=vs.110).aspx
    public class HemsamaritenService : ServiceBase
    {

        #region FIELDS

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ServiceHost serviceHost = null;

        #endregion

        #region CONSTRUCTOR

        public HemsamaritenService()
        {
            // Name the Windows Service
            this.ServiceName = "WCFWindowsServiceHemsamariten";
        }

        #endregion

        #region Main

        public static void Main(string[] args)
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    string parameter = string.Concat(args);
                    switch (parameter)
                    {
                        case "--install":
                            ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
                            break;
                        case "--uninstall":
                            ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
                            break;
                    }
                }
                else
                {
                    ServiceBase[] servicesToRun = new ServiceBase[]
                                    {
                                        new WCF.ServiceLibrary.HemsamaritenDuplexService()
                                    };
                    ServiceBase.Run(servicesToRun);

                    log.Debug(String.Format("HemsamaritenDuplexService (WCF)."));

                }
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
                this.serviceHost = new ServiceHost(typeof(HemsamaritenService));

                this.serviceHost.Opening += new EventHandler(host_Opening);
                this.serviceHost.Opened += new EventHandler(host_Opened);
                this.serviceHost.Closing += new EventHandler(host_Closing);
                this.serviceHost.Closed += new EventHandler(host_Closed);
                this.serviceHost.Faulted += new EventHandler(host_Faulted);

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

        #region Event Handlers

        private void host_Faulted(object sender, EventArgs e)
        {
            log.Debug("Host has faulted.");
        }

        private void host_Closed(object sender, EventArgs e)
        {
            log.Debug("Host is closed.");
        }

        private void host_Closing(object sender, EventArgs e)
        {
            log.Debug("Host is closing.");
        }

        private void host_Opened(object sender, EventArgs e)
        {
            log.Debug("Host is opened.");
        }

        private void host_Opening(object sender, EventArgs e)
        {
            log.Debug("Host is opening.");
        }

        #endregion

    }

    #region INSTALLER

    // Provide the ProjectInstaller class which allows 
    // the service to be installed by the Installutil.exe tool
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "WCFWindowsServiceHemsamariten";
            Installers.Add(process);
            Installers.Add(service);
        }
    }

    #endregion

}