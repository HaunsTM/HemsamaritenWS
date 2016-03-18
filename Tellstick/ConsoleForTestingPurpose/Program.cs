﻿//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Tellstick.ConsoleForTestingPurpose
{
    using System;
    using System.Linq;
    using System.Threading;

    using log4net;

    using Tellstick.BLL;
    using Tellstick.BLL.Interfaces;
    using Tellstick.Model.Enums;
    using Tellstick.Model.Interfaces;
    using Tellstick.TelldusNETWrapper;

    class Program
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string DB_CONNECTION_STRING_NAME = "name=TellstickDBConnection";
        static void Main(string[] args)
        {
            TestScheduler();
            //CreateDBTest(DB_CONNECTION_STRING_NAME);
            //var registeredDevice = RegisterDevice(dbConnectionStringName: DB_CONNECTION_STRING_NAME, commander: new NativeTellstickCommander(), name: "Ett namn på en Tellstick", locationDesciption: "Liggandes på skrivbordet i Hans rum", protocol: EnumTellstickProtocol.arctech, modelType:EnumTellstickModelType.codeswitch, modelManufacturer: EnumTellstickModelManufacturer.Nexa, unit: EnumTellstickParameter_Unit._1, house: EnumTellstickParameter_House.A );
            //new TellstickUnitDealer(DB_CONNECTION_STRING_NAME, new NativeTellstickCommander()).TurnOnDevice(
            //    registeredDevice);
            //DisplayInfo();


        }


        private static void CreateDBTest(string dbConnectionStringName)
        {
            System.Data.Entity.Database.SetInitializer(new Tellstick.Model.DefaultDataDbInitializer());
            using (var db = new Model.TellstickDBContext(dbConnectionStringName))
            {
#if DEBUG
                db.Database.Initialize(true);
#endif
                //do something random stupid to force seed
                var stupidValue = db.TellstickModels.Count();
            }
        }

        private static ITellstickUnit RegisterDevice(string dbConnectionStringName, INativeTellstickCommander commander, string name, string locationDesciption, EnumTellstickProtocol protocol, EnumTellstickModelType modelType, EnumTellstickModelManufacturer modelManufacturer, EnumTellstickParameter_Unit unit, EnumTellstickParameter_House house)
        {
            var tellstickUnitDealer = new TellstickUnitDealer(dbConnectionStringName, commander);
            var createdTellstickUnit = tellstickUnitDealer.AddDevice(name, locationDesciption, protocol, modelType, modelManufacturer, unit, house);
            return createdTellstickUnit;
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
        

        private static void DisplayInfo()
        {
            var numberOfRegisteredDevices = TelldusNETWrapper.tdGetNumberOfDevices();
            for (int i = 0; i < numberOfRegisteredDevices; i++)
            {
                var currentDeviceID = TelldusNETWrapper.tdGetDeviceId(i);
                var currentDeviceName = TelldusNETWrapper.tdGetName(currentDeviceID);

                var deviceType = TelldusNETWrapper.tdGetDeviceType(currentDeviceID);
                var model = TelldusNETWrapper.tdGetModel(currentDeviceID);

                var currentDeviceProtocol = TelldusNETWrapper.tdGetProtocol(currentDeviceID);
                var currentDeviceMethods = TelldusNETWrapper.tdMethods(currentDeviceID, 10);

                var tdGetDeviceParameter_Unit = TelldusNETWrapper.tdGetDeviceParameter(currentDeviceID, "unit", "NO UNIT SET");
                var tdGetDeviceParameter_House = TelldusNETWrapper.tdGetDeviceParameter(currentDeviceID, "house", "NO HOUSE SET");

                var lastSentCommand = TelldusNETWrapper.tdLastSentCommand(currentDeviceID, 10);
                var lastSentValue = TelldusNETWrapper.tdLastSentValue(currentDeviceID);



                var result = "currentDeviceID: " + currentDeviceID + "\n" +
                             "currentDeviceName: " + currentDeviceName + "\n" +

                             "|\n|\n" +

                             "deviceType: " + deviceType + "\n" +
                             "model: " + model + "\n" +

                             "currentDeviceProtocol: " + currentDeviceProtocol + "\n" +
                             "currentDeviceMethods: " + currentDeviceMethods + "\n" +

                             "tdGetDeviceParameter_Unit: " + tdGetDeviceParameter_Unit + "\n" +
                             "tdGetDeviceParameter_House: " + tdGetDeviceParameter_House + "\n" +

                             "lastSentCommand: " + lastSentCommand + "\n" +
                             "lastSentValue: " + lastSentValue + "\n" +
                             "***********************";



                Console.WriteLine(result);


            }
            Console.ReadLine();

        }
    }
}
