//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace HemsamaritenWindowsService
{
    using System;
    using System.Configuration.Install;
    using System.IO;
    using System.Reflection;
    using System.ServiceProcess;

    static class Program
    {
        #region FIELDS

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
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

                            log.Debug(String.Format("Installed HemsamaritenWindowsService."));
                            break;
                        case "--uninstall":
                            ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });

                            log.Debug(String.Format("Uninstalled HemsamaritenWindowsService."));
                            break;
                    }
                }
                else
                {
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                        new WindowsService()
                    };
                    ServiceBase.Run(ServicesToRun);

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

    }
}
