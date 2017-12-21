using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Tellstick.Model;

namespace Tellstick.BLL
{
    using Tellstick.BLL.Interfaces;

    using RestSharp;
    using Newtonsoft.Json;
    using System.Web;
    using System.Net.Http.Headers;
    using System.Web.Util;
    using System.Web.Script.Serialization;

    using log4net;
    public class LoginStep1
    {
        public string authUrl { get; set; }
        public string token { get; set; }
    }

    public class ReqToken
    {
        public string allowRenew { get; set; }
        public string expires { get; set; }
        public string token { get; set; }
    }

    public class Device
    {

        public int id { get; set; }

        public int methods { get; set; }
        public string name { get; set; }
        public int state { get; set; }
        public string statevalue { get; set; }
        public string type { get; set; }
    }

    public class DeviceRootObject
    {
        public List<Device> device { get; set; }
    }

    public class RefreshToken
    {
        public int expires { get; set; }
        public string token { get; set; }
    }

    public class Sensor
    {
        public int id { get; set; }
        public string model { get; set; }
        public string name { get; set; }
        public bool novalues { get; set; }
        public string protocol { get; set; }
        public int sensorId { get; set; }
    }

    public class SensorRootObject
    {
        public List<Sensor> sensor { get; set; }
    }

    public class Data
    {
        public string name { get; set; }
        public int scale { get; set; }
        public double value { get; set; }
    }

    public class SensorData
    {
        public List<Data> data { get; set; }
        public int id { get; set; }
        public string model { get; set; }
        public string name { get; set; }
        public string protocol { get; set; }
        public int sensorId { get; set; }
    }
    public static class Helper
    {
        public static string AsJsonList<T>(List<T> tt)
        {
            return new JavaScriptSerializer().Serialize(tt);
        }
        public static string AsJson<T>(T t)
        {
            return new JavaScriptSerializer().Serialize(t);
        }
        public static List<T> AsObjectList<T>(string tt)
        {
            return new JavaScriptSerializer().Deserialize<List<T>>(tt);
        }
        public static T AsObject<T>(string t)
        {
            return new JavaScriptSerializer().Deserialize<T>(t);
        }
    }

    public class TellStickZNetCommander : ITellStickZNetCommander
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private Authentication _bearerToken = null;

        public string TellstickURL { get; set; }

        public string DbConnectionStringName { get; private set; }

        public TellStickZNetCommander(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;

            using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
            {
                DefaultTellstickZNetLiteV2s = (from s in db.TellstickZNetLiteV2s
                    orderby s.Id ascending
                    select s).First();
            }
        }

        private TellstickZNetLiteV2 DefaultTellstickZNetLiteV2s { get; set; }

        private RestClient Client
        {
            get
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var client = new RestClient(DefaultTellstickZNetLiteV2s.BaseIP);
                    client.AddDefaultHeader("Authorization", " Bearer " + TellstickAuthentication.Token);
                    return client;
                }
            }
        }

        private Authentication TellstickAuthentication
        {
            set
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    value.Received = DateTime.Now;
                    db.Authentications.Add(value);
                    db.SaveChanges();
                    _bearerToken = value;
                }
            }
            get
            {
                if (_bearerToken == null)
                {
                    using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                    {
                        var bearerToken = (from a in db.Authentications
                            where a.Active == true && a.TellstickZNetLiteV2_Id == DefaultTellstickZNetLiteV2s.Id
                            orderby a.Received descending
                            select a).First();

                        return bearerToken;
                    }
                }
                else
                {
                    return _bearerToken;
                }
            }
        }

        public bool RefreshBearerToken()
        {
            bool refreshed = false;

            try
            {
                RestRequest request = new RestRequest("/api/refreshToken", Method.GET);

                var response = Client.Execute(request);

                var refreshedBearerToken = JsonConvert.DeserializeObject<RefreshToken>(response.Content);
                var authentication = new Authentication { Active = true, Expires = refreshedBearerToken.expires, Received = DateTime.Now, Token = refreshedBearerToken.token, TellstickZNetLiteV2_Id = DefaultTellstickZNetLiteV2s.Id };

                TellstickAuthentication = authentication;
                refreshed = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return refreshed;
        }

        public DeviceRootObject GetListOfDevices()
        {
            DeviceRootObject devRoot = null;

            try
            {
                RestRequest request = new RestRequest("/api/devices/list", Method.GET);
                request.AddParameter("supportedMethods", "3");

                var response = Client.Execute(request);

                devRoot = JsonConvert.DeserializeObject<DeviceRootObject>(response.Content);
                return devRoot;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public SensorRootObject GetListOfSensors()
        {
            SensorRootObject senRoot = null;

            try
            {
                RestRequest request = new RestRequest("/api/sensors/list", Method.GET);

                var response = Client.Execute(request);

                senRoot = JsonConvert.DeserializeObject<SensorRootObject>(response.Content);
                return senRoot;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public SensorData GetSensordata(int SensorID)
        {
            SensorData sensData = null;

            try
            {
                RestRequest request = new RestRequest("/api/sensor/info", Method.GET);
                request.AddParameter("id", SensorID);

                var response = Client.Execute(request);

                sensData = JsonConvert.DeserializeObject<SensorData>(response.Content);
                return sensData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Turns a device on.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
        public void TurnOn(int DeviceID)
        {
            try
            {
                RestRequest request = new RestRequest("/api/device/turnOn", Method.GET);
                request.AddParameter("id", DeviceID);

                var response = Client.Execute(request);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Turns a device off.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn off</param>
        /// <returns>If turn off message were sent</returns>
        public void TurnOff(int DeviceID)
        {
            try
            {
                RestRequest request = new RestRequest("/api/device/turnOff", Method.GET);
                request.AddParameter("id", DeviceID);

                var response = Client.Execute(request);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
