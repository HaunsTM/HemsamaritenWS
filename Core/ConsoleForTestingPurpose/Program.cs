using System.Linq;
using System.Threading;
using Core.Audio;
using Core.BLL;
using log4net;

//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Tellstick.ConsoleForTestingPurpose
{

    class Program
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string DB_CONNECTION_STRING_NAME = "name=HemsamaritenWindowsServiceDBConnection";
        static void Main(string[] args)
        {
            //TestScheduler();
            CreateDBTest(DB_CONNECTION_STRING_NAME);
            //var registeredDevice = RegisterDevice(dbConnectionStringName: DB_CONNECTION_STRING_NAME, commander: new NativeTellstickCommander(), name: "Ett namn på en Tellstick", locationDesciption: "Liggandes på skrivbordet i Hans rum", protocol: EnumTellstickProtocol.arctech, modelType: EnumTellstickModelType.codeswitch, modelManufacturer: EnumTellstickModelManufacturer.Nexa, unit: EnumTellstickParameter_Unit._1, house: EnumTellstickParameter_House.A);
            //new TellstickUnitDealer(DB_CONNECTION_STRING_NAME, new NativeTellstickCommander()).TurnOnDevice(
            //    registeredDevice);
            //DisplayInfo();
            //var un12 = UnregisterDevice(nativeDeviceId: 12, dbConnectionStringName: DB_CONNECTION_STRING_NAME, commander: new NativeTellstickCommander());
            //PlaySound();
        }


        private static void CreateDBTest(string dbConnectionStringName)
        {
            System.Data.Entity.Database.SetInitializer(new Core.Model.DefaultDataDbInitializer());
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(dbConnectionStringName))
            {
#if DEBUG
                db.Database.Initialize(true);
#endif
                //do something random stupid to force seed
                var stupidValue = db.Schedulers.Count();
            }
        }
        
        private static void TestScheduler()
        {
            JobScheduler jobScheduler;
            jobScheduler = new JobScheduler(DB_CONNECTION_STRING_NAME);
            jobScheduler.Start();
            Thread.Sleep(10000);
            jobScheduler.Stop();
            Thread.Sleep(10000);
            jobScheduler = new JobScheduler(DB_CONNECTION_STRING_NAME);
            jobScheduler.Start();
        }


        private static void PlaySound()
        {
            var mediaPlayer = new WMPLib.WindowsMediaPlayer();
            var windowsNativeAudioSystem = new SystemVolumeConfigurator();
            var player = new Core.Audio.Player(mediaPlayer, windowsNativeAudioSystem);
            player.Bark();
        }
        
    }
}
