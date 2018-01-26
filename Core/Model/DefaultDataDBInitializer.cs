using Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Model
{

    public partial class DefaultDataDbInitializer : DropCreateDatabaseIfModelChanges<HemsamaritenWindowsServiceDbContext>
    {
        private TellstickAuthentication Authenticated20171203
        {
            get
            {
                return new TellstickAuthentication
                {
                    Active = true,
                    Expires = 1543826561,
                    Token =                        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImF1ZCI6IkhlbXNhbWFyaXRlbkFwcCIsImV4cCI6MTU0MzgyNjU2MX0.eyJyZW5ldyI6dHJ1ZSwidHRsIjozMTUzNjAwMH0.K-pAl9gNbMSZEYkYYdzDrntUQRkgg9CPCOT3imr6qm0",
                    Received = new DateTime(year: 2017, month: 12, day: 03 )
                };
            }
        }

        private TellstickAuthentication[] Authentications()
        {
            var authentications = new TellstickAuthentication[]
            {
                Authenticated20171203
            };
            return authentications;
        }

        private TellstickZNetLiteV2 BaseDevice20171203
        {
            get
            {
                return new TellstickZNetLiteV2 {BaseIP = "http://10.0.0.100"};
            }
        }

        private TellstickZNetLiteV2[] TellstickZNetLiteV2s()
        {
            var tellstickZNetLiteV2s = new TellstickZNetLiteV2[]
            {
                BaseDevice20171203
            };
            return tellstickZNetLiteV2s;
        }

        private TellstickActionType[] TellstickActionTypes()
        {
            var tellstickActionTypes = new TellstickActionType[]
            {
                new TellstickActionType { Active = true, ActionTypeOption = Enums.ActionTypeOption.TurnOn},
                new TellstickActionType { Active = true, ActionTypeOption = Enums.ActionTypeOption.TurnOff},
                new TellstickActionType { Active = true, ActionTypeOption = Enums.ActionTypeOption.RefreshBearerToken}
            };
            return tellstickActionTypes;
        }

        private TellstickUnit[] TellstickUnits()
        {
            var tellstickUnits = new TellstickUnit[]
                {
                    new TellstickUnit { Active = true, NativeDeviceId = 2, Name = "1", LocationDesciption = "Fönsterlampa vardagsrum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 3, Name = "2", LocationDesciption = "Pianobelysning"},
                    new TellstickUnit { Active = true, NativeDeviceId = 4, Name = "3", LocationDesciption = "Golvlampa i vardagsrum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 5, Name = "4", LocationDesciption = "Uterum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 6, Name = "5", LocationDesciption = "Fönsterlampor i köket"},

                    new TellstickUnit { Active = true, NativeDeviceId = 7, Name = "6", LocationDesciption = "Belysning vid fågelmataren"},
                    new TellstickUnit { Active = true, NativeDeviceId = 8, Name = "7", LocationDesciption = "Golvlampa i TV-rum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 9, Name = "8", LocationDesciption = "Subwoofer"},
                    new TellstickUnit { Active = true, NativeDeviceId = 10, Name = "9", LocationDesciption = "Liten lampa på byrå i TV-rum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 11, Name = "10", LocationDesciption = "Ljusglober i hall utanför toalett"},

                    new TellstickUnit { Active = true, NativeDeviceId = 12, Name = "11", LocationDesciption = "Spegelbordet utanför den lilla toaletten"},
                    new TellstickUnit { Active = true, NativeDeviceId = 13, Name = "12", LocationDesciption = "Ljusglober i Atikas rum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 14, Name = "13", LocationDesciption = "Fönsterkarm Atikas rum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 15, Name = "14", LocationDesciption = "Grön lampa i Atikas rum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 16, Name = "15", LocationDesciption = "Liten bokhylla närmast fönstret Hans"},

                    new TellstickUnit { Active = true, NativeDeviceId = 17, Name = "16", LocationDesciption = "Vinkelbokhylla i hörnet Hans rum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 18, Name = "17", LocationDesciption = "Liten bordslampa Hans rum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 19, Name = "18", LocationDesciption = "Bokhyllebelysning i vardagsrum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 20, Name = "19", LocationDesciption = "Ljusorgel vardagsrum"},
                    new TellstickUnit { Active = true, NativeDeviceId = 21, Name = "20", LocationDesciption = "Ljusstake sovrum"},

                    new TellstickUnit { Active = true, NativeDeviceId = 22, Name = "21", LocationDesciption = ""},
                    new TellstickUnit { Active = true, NativeDeviceId = 23, Name = "22", LocationDesciption = ""},
                    new TellstickUnit { Active = true, NativeDeviceId = 24, Name = "23", LocationDesciption = ""},
                    new TellstickUnit { Active = true, NativeDeviceId = 25, Name = "24", LocationDesciption = ""},
                    new TellstickUnit { Active = true, NativeDeviceId = 1, Name =  "25", LocationDesciption = ""}
                };
            return tellstickUnits;
        }

        private MediaSource[] MediaSources()
        {
            var mediaSources = new MediaSource[]
            {
                new MediaSource { Active=true, Name="SR P1", Url = "http://sverigesradio.se/topsy/direkt/132-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P2", Url = "http://sverigesradio.se/topsy/direkt/2562-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P3", Url = "http://sverigesradio.se/topsy/direkt/164-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Ekot sänder direkt", Url = "http://sverigesradio.se/topsy/direkt/4540-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Plus", Url = "http://sverigesradio.se/topsy/direkt/4951-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Blekinge", Url = "http://sverigesradio.se/topsy/direkt/213-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Dalarna", Url = "http://sverigesradio.se/topsy/direkt/223-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Gotland", Url = "http://sverigesradio.se/topsy/direkt/205-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Gävleborg", Url = "http://sverigesradio.se/topsy/direkt/210-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Göteborg", Url = "http://sverigesradio.se/topsy/direkt/212-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Halland", Url = "http://sverigesradio.se/topsy/direkt/220-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Jämtland", Url = "http://sverigesradio.se/topsy/direkt/200-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Jönköping", Url = "http://sverigesradio.se/topsy/direkt/203-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Kalmar", Url = "http://sverigesradio.se/topsy/direkt/201-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Kristianstad", Url = "http://sverigesradio.se/topsy/direkt/211-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Kronoberg", Url = "http://sverigesradio.se/topsy/direkt/214-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Malmöhus", Url = "http://sverigesradio.se/topsy/direkt/207-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Norrbotten", Url = "http://sverigesradio.se/topsy/direkt/209-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Sjuhärad", Url = "http://sverigesradio.se/topsy/direkt/206-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Skaraborg", Url = "http://sverigesradio.se/topsy/direkt/208-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Stockholm", Url = "http://sverigesradio.se/topsy/direkt/701-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Sörmland", Url = "http://sverigesradio.se/topsy/direkt/202-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Uppland", Url = "http://sverigesradio.se/topsy/direkt/218-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Värmland", Url = "http://sverigesradio.se/topsy/direkt/204-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Väst", Url = "http://sverigesradio.se/topsy/direkt/219-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Västerbotten", Url = "http://sverigesradio.se/topsy/direkt/215-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Västernorrland", Url = "http://sverigesradio.se/topsy/direkt/216-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Västmanland", Url = "http://sverigesradio.se/topsy/direkt/217-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Örebro", Url = "http://sverigesradio.se/topsy/direkt/221-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Östergötland", Url = "http://sverigesradio.se/topsy/direkt/222-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P3 Din gata", Url = "http://sverigesradio.se/topsy/direkt/2576-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P5 Sthlm", Url = "http://sverigesradio.se/topsy/direkt/2842-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P2 Språk och musik", Url = "http://sverigesradio.se/topsy/direkt/163-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P6", Url = "http://sverigesradio.se/topsy/direkt/166-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P3 Star", Url = "http://sverigesradio.se/topsy/direkt/1607-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Radioapans knattekanal", Url = "http://sverigesradio.se/topsy/direkt/2755-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P2 Klassiskt", Url = "http://sverigesradio.se/topsy/direkt/1603-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Sápmi", Url = "http://sverigesradio.se/topsy/direkt/224-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Sisuradio", Url = "http://sverigesradio.se/topsy/direkt/226-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P2 Världen", Url = "http://sverigesradio.se/topsy/direkt/2619-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 01", Url = "http://sverigesradio.se/topsy/direkt/2383-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 02", Url = "http://sverigesradio.se/topsy/direkt/2384-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 03", Url = "http://sverigesradio.se/topsy/direkt/2385-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 04", Url = "http://sverigesradio.se/topsy/direkt/2386-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 05", Url = "http://sverigesradio.se/topsy/direkt/2387-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 06", Url = "http://sverigesradio.se/topsy/direkt/2388-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 07", Url = "http://sverigesradio.se/topsy/direkt/2389-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 08", Url = "http://sverigesradio.se/topsy/direkt/2390-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 09", Url = "http://sverigesradio.se/topsy/direkt/3268-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR Extra 10", Url = "http://sverigesradio.se/topsy/direkt/3269-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P2 Klassisk Jul", Url = "http://sverigesradio.se/topsy/direkt/3036-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="SR P4 Bjällerklang", Url = "http://sverigesradio.se/topsy/direkt/3034-hi-aac.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},

                new MediaSource { Active=true, Name="Malmökanalen", Url =  "http://stream.nsp.se:8000/MNR89_MP3_Lo", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Radio Malmökanalen", Url =  "http://stream.nsp.se:8000/MNR90_MP3_Lo", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Radio AF", Url =  "http://live.radioaf.se:8000/;stream/1", MediaCategoryType = MediaCategoryType.InternetStreamRadio},

                new MediaSource { Active=true, Name="DR P1", Url =  "http://live-icy.gss.dr.dk/A/A03H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P2", Url =  "http://live-icy.gss.dr.dk/A/A04H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P3", Url =  "http://live-icy.gss.dr.dk/A/A05H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Bornholm", Url =  "http://live-icy.gss.dr.dk/A/A06H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Esbjerg", Url =  "http://live-icy.gss.dr.dk/A/A15H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Fyn", Url =  "http://live-icy.gss.dr.dk/A/A07H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 København", Url =  "http://live-icy.gss.dr.dk/A/A08H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Midt & Vest", Url =  "http://live-icy.gss.dr.dk/A/A09H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Nordjylland", Url =  "http://live-icy.gss.dr.dk/A/A10H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Sjælland", Url =  "http://live-icy.gss.dr.dk/A/A11H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Syd", Url =  "http://live-icy.gss.dr.dk/A/A12H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Trekanten", Url =  "http://live-icy.gss.dr.dk/A/A13H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P4 Østjyllands Radio", Url =  "http://live-icy.gss.dr.dk/A/A14H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P5", Url =  "http://live-icy.gss.dr.dk/A/A25L.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="DR P6 BEAT", Url =  "http://live-icy.gss.dr.dk/A/A29H.mp3.m3u", MediaCategoryType = MediaCategoryType.InternetStreamRadio}
            };
            return mediaSources;
        }

        private void AddAndSaveDummyDataWithoutConstraints(HemsamaritenWindowsServiceDbContext context)
        {
            context.TellstickAuthentications.AddRange(this.Authentications());
            context.TellstickZNetLiteV2s.AddRange(this.TellstickZNetLiteV2s());
            context.TellstickActionTypes.AddRange(this.TellstickActionTypes());
            context.TellstickUnits.AddRange(this.TellstickUnits());
            context.Schedulers.AddRange(this.Schedulers());

            context.MediaSources.AddRange(this.MediaSources());

            context.SaveChanges();
        }

        private void InitiallyConnectAuthentication_TellstickZNetLiteV2(HemsamaritenWindowsServiceDbContext context)
        {
            //get references from db
            var tellstickZNetLiteV2 = (from t in context.TellstickZNetLiteV2s.ToList()
                                        where t.BaseIP == BaseDevice20171203.BaseIP
                                        select t).First();
            var authentication = (from t in context.TellstickAuthentications.ToList()
                                  where t.Token == Authenticated20171203.Token
                                  select t).First();

            authentication.TellstickZNetLiteV2_Id = tellstickZNetLiteV2.Id;

            context.TellstickAuthentications.Attach(authentication);
            var entry = context.Entry(authentication);
            entry.Property(e => e.TellstickZNetLiteV2_Id).IsModified = true;
            // other changed properties
            context.SaveChanges();
        }

        private void InitiallyConnectActionTypes_TellstickZNetLiteV2(HemsamaritenWindowsServiceDbContext context)
        {
            //get references from db
            var tellstickZNetLiteV2 = (from t in context.TellstickZNetLiteV2s
                where t.BaseIP == BaseDevice20171203.BaseIP
                select t).First();
            
            var actionTypes = from at in context.TellstickActionTypes
                where at.TellstickZNetLiteV2_Id == null
                select at;

            foreach (var actionType in actionTypes)
            {
                actionType.TellstickZNetLiteV2_Id = tellstickZNetLiteV2.Id;
            }

            // other changed properties
            context.SaveChanges();
        }

        private void AddInitialTellstickActions(HemsamaritenWindowsServiceDbContext context)
        {
            var defaultDataDbInitializerHelper = new DefaultDataDbInitializerHelper(context: context);
            var actionIsActive = true;

            var tellstickActionsToSave = new List<Tuple<bool, string, ActionTypeOption, string>>
            {
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, null, ActionTypeOption.RefreshBearerToken, "0 58 2 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "1", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "2", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "3", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "4", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "5", ActionTypeOption.TurnOn, "0 45 5 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "6", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "7", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "8", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "9", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "10", ActionTypeOption.TurnOn, "0 45 5 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "11", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "12", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "13", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "14", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "15", ActionTypeOption.TurnOn, "0 45 5 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "16", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "17", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "18", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "19", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "20", ActionTypeOption.TurnOn, "0 45 5 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "21", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "22", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "23", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "24", ActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "25", ActionTypeOption.TurnOn, "0 45 5 * * ?"),


                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "1", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "2", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "3", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "4", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "5", ActionTypeOption.TurnOff, "0 45 8 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "6", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "7", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "8", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "9", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "10", ActionTypeOption.TurnOff, "0 45 8 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "11", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "12", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "13", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "14", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "15", ActionTypeOption.TurnOff, "0 45 8 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "16", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "17", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "18", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "19", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "20", ActionTypeOption.TurnOff, "0 45 8 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "21", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "22", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "23", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "24", ActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "25", ActionTypeOption.TurnOff, "0 45 8 * * ?"),



                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "1", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "2", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "3", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "4", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "5", ActionTypeOption.TurnOn, "0 0 16 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "6", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "7", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "8", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "9", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "10", ActionTypeOption.TurnOn, "0 0 16 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "11", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "12", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "13", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "14", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "15", ActionTypeOption.TurnOn, "0 0 16 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "16", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "17", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "18", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "19", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "20", ActionTypeOption.TurnOn, "0 0 16 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "21", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "22", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "23", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "24", ActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "25", ActionTypeOption.TurnOn, "0 0 16 * * ?"),


                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "1", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "2", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "3", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "4", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "5", ActionTypeOption.TurnOff, "0 0 23 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "6", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "7", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "8", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "9", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "10", ActionTypeOption.TurnOff, "0 0 23 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "11", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "12", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "13", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "14", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "15", ActionTypeOption.TurnOff, "0 0 23 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "16", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "17", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "18", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "19", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "20", ActionTypeOption.TurnOff, "0 0 23 * * ?"),

                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "21", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "22", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "23", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "24", ActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, ActionTypeOption, string>(actionIsActive, "25", ActionTypeOption.TurnOff, "0 0 23 * * ?")
            };

            var tellstickActions = defaultDataDbInitializerHelper.TellstickActionsToSave(tellstickActionsToSave);

            context.TellstickActions.AddRange(tellstickActions);

            // other changed properties
            context.SaveChanges();
        }

        protected override void Seed(HemsamaritenWindowsServiceDbContext context)
        {
            AddAndSaveDummyDataWithoutConstraints(context);
            InitiallyConnectAuthentication_TellstickZNetLiteV2(context);
            InitiallyConnectActionTypes_TellstickZNetLiteV2(context);
            AddInitialTellstickActions(context);
        }
    }
}