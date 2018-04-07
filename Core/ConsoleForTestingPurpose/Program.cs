using System;
using System.Linq;
using System.Threading;
using Core.Audio;
using Core.BLL;
using Core.Model.Enums;
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
            //CreateDBTest(DB_CONNECTION_STRING_NAME);
            //var registeredDevice = RegisterDevice(dbConnectionStringName: DB_CONNECTION_STRING_NAME, commander: new NativeTellstickCommander(), name: "Ett namn på en Tellstick", locationDesciption: "Liggandes på skrivbordet i Hans rum", protocol: EnumTellstickProtocol.arctech, modelType: EnumTellstickModelType.codeswitch, modelManufacturer: EnumTellstickModelManufacturer.Nexa, unit: EnumTellstickParameter_Unit._1, house: EnumTellstickParameter_House.A);
            //new TellstickUnitDealer(DB_CONNECTION_STRING_NAME, new NativeTellstickCommander()).TurnOnDevice(
            //    registeredDevice);
            //DisplayInfo();
            //var un12 = UnregisterDevice(nativeDeviceId: 12, dbConnectionStringName: DB_CONNECTION_STRING_NAME, commander: new NativeTellstickCommander());
            //PlaySound();
            //PlaySound(DB_CONNECTION_STRING_NAME);
            TestDealers();

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
            TellstickJobScheduler jobScheduler;
            jobScheduler = new TellstickJobScheduler(DB_CONNECTION_STRING_NAME);
            jobScheduler.Start();
            Thread.Sleep(10000);
            jobScheduler.Stop();
            Thread.Sleep(10000);
            jobScheduler = new TellstickJobScheduler(DB_CONNECTION_STRING_NAME);
            jobScheduler.Start();
        }

        private static void PlaySound()
        {
            Core.Audio.Player.Instance.Play("Media/Animal-Dog-Growl-BullTerrier-02.flac");
            Console.ReadLine();
        }

        private static void PlaySound(string dbConnectionStringName)
        {
            var player = Core.Audio.Player.Instance;
            //player

            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(dbConnectionStringName))
            {
                var mediaSource = db.MediaSources
                    .Where(mS => mS.Active && mS.Url == "Media/Animal-Dog-Growl-BullTerrier-02.flac").Select(x => x)
                    .FirstOrDefault();

                var mediaVolume =
                    db.MediaOutputVolumes.Where(vol => vol.Active && vol.Label == MediaOutputVolumeValue._50).Select(x => x)
                        .FirstOrDefault();

                player.Play(mediaSource, mediaVolume);
            }
            Console.ReadLine();
        }

        private static void TestDealers()
        {
            var cD = new CouyntryDealer(DB_CONNECTION_STRING_NAME);
            var mSD = new MediaSourceDealer(DB_CONNECTION_STRING_NAME);
            var tAD = new TellstickActionsDealer(DB_CONNECTION_STRING_NAME);

//            var allCountries = cD.AllCountriesList();
//            var countriesRepresentedInMediaSourcesList = cD.CountriesRepresentedInMediaSourcesList();
//            var predefinedMediaSourcesList = mSD.PredefinedMediaSourcesList();

            var tADs = tAD.GetAllActions();
        }
    }
}
