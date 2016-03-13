namespace Tellstick.ConsoleForTestingPurpose
{
    using System;
    using System.Linq;

    using Tellstick.TelldusNETWrapper;

    class Program
    {

        static void Main(string[] args)
        {
            CreateDBTest();
            //RegisterDevice();
            //DisplayInfo();


        }


        private static void CreateDBTest()
        {
            System.Data.Entity.Database.SetInitializer(new Tellstick.Model.DefaultDataDbInitializer());
            var db = new Model.TellstickDBContext("name=TellstickDBConnection");
#if DEBUG
             db.Database.Initialize(true);
#endif

            //do something random stupid to force seed
            var stupidValue = db.TellstickModels.Count();

        }

        private async static void RegisterDevice()
        {
            var registeredDeviceID = TelldusNETWrapper.tdAddDevice();
            var nameSet = TelldusNETWrapper.tdSetName(registeredDeviceID, "Nexa;Code Switch;Huskod:F;Enhetskod:9");
            var protocolSet = TelldusNETWrapper.tdSetProtocol(registeredDeviceID, "arctech");
            var model = TelldusNETWrapper.tdSetModel(registeredDeviceID, "codeswitch:nexa");

            var unit = TelldusNETWrapper.tdSetDeviceParameter(registeredDeviceID, "unit", "9");
            var house = TelldusNETWrapper.tdSetDeviceParameter(registeredDeviceID, "house", "F");

            var turnedOn = await TelldusNETWrapper.tdTurnOnAsync(registeredDeviceID);

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
