namespace WCF.ConsoleForTestingPurpose
{
    using System;

    class Program
    {

        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            CreateLog4NetDB();
            //TestLogger();
        }

        private static void CreateLog4NetDB()
        {

            var db = new WCF.Model.HemsamaritenContext("name=HemsamaritenDBConnection");

            db.Logs.Add(new WCF.Model.Log {Date = DateTime.Today, Exception = "NO EXCEPTION", Level = "NO LEVEL", Logger = "NO LOGGER", Message = "NO MESSAGE"});
            db.SaveChanges();
        }

        public static void TestLogger()
        {
            log4net.GlobalContext.Properties["testProperty"] = "This is my test property information";

            log.Debug("Other Class - Debug logging");
            log.Info("Other Class - Info logging");
            log.Warn("Other Class - Warn logging");
            log.Error("Other Class - Error logging");
            log.Fatal("Other Class - Fatal logging");
        }
    }
}
