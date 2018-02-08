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
                    Token =  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImF1ZCI6IkhlbXNhbWFyaXRlbkFwcCIsImV4cCI6MTU0MzgyNjU2MX0.eyJyZW5ldyI6dHJ1ZSwidHRsIjozMTUzNjAwMH0.K-pAl9gNbMSZEYkYYdzDrntUQRkgg9CPCOT3imr6qm0",
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
                new TellstickActionType { Active = true, ActionTypeOption = Enums.TellstickActionTypeOption.TurnOn},
                new TellstickActionType { Active = true, ActionTypeOption = Enums.TellstickActionTypeOption.TurnOff},
                new TellstickActionType { Active = true, ActionTypeOption = Enums.TellstickActionTypeOption.RefreshBearerToken}
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

        private MediaOutputVolume[] MediaOutputVolumes()
        {
            var mediaOutputVolumes = new MediaOutputVolume[]
            {
                new MediaOutputVolume { Active = true, Value = 0, Label = MediaOutputVolumeValue._0},
                new MediaOutputVolume { Active = true, Value = 1, Label = MediaOutputVolumeValue._1},
                new MediaOutputVolume { Active = true, Value = 2, Label = MediaOutputVolumeValue._2},
                new MediaOutputVolume { Active = true, Value = 3, Label = MediaOutputVolumeValue._3},
                new MediaOutputVolume { Active = true, Value = 4, Label = MediaOutputVolumeValue._4},
                new MediaOutputVolume { Active = true, Value = 5, Label = MediaOutputVolumeValue._5},
                new MediaOutputVolume { Active = true, Value = 6, Label = MediaOutputVolumeValue._6},
                new MediaOutputVolume { Active = true, Value = 7, Label = MediaOutputVolumeValue._7},
                new MediaOutputVolume { Active = true, Value = 8, Label = MediaOutputVolumeValue._8},
                new MediaOutputVolume { Active = true, Value = 9, Label = MediaOutputVolumeValue._9},
                new MediaOutputVolume { Active = true, Value = 10, Label = MediaOutputVolumeValue._10},
                new MediaOutputVolume { Active = true, Value = 11, Label = MediaOutputVolumeValue._11},
                new MediaOutputVolume { Active = true, Value = 12, Label = MediaOutputVolumeValue._12},
                new MediaOutputVolume { Active = true, Value = 13, Label = MediaOutputVolumeValue._13},
                new MediaOutputVolume { Active = true, Value = 14, Label = MediaOutputVolumeValue._14},
                new MediaOutputVolume { Active = true, Value = 15, Label = MediaOutputVolumeValue._15},
                new MediaOutputVolume { Active = true, Value = 16, Label = MediaOutputVolumeValue._16},
                new MediaOutputVolume { Active = true, Value = 17, Label = MediaOutputVolumeValue._17},
                new MediaOutputVolume { Active = true, Value = 18, Label = MediaOutputVolumeValue._18},
                new MediaOutputVolume { Active = true, Value = 19, Label = MediaOutputVolumeValue._19},
                new MediaOutputVolume { Active = true, Value = 20, Label = MediaOutputVolumeValue._20},
                new MediaOutputVolume { Active = true, Value = 21, Label = MediaOutputVolumeValue._21},
                new MediaOutputVolume { Active = true, Value = 22, Label = MediaOutputVolumeValue._22},
                new MediaOutputVolume { Active = true, Value = 23, Label = MediaOutputVolumeValue._23},
                new MediaOutputVolume { Active = true, Value = 24, Label = MediaOutputVolumeValue._24},
                new MediaOutputVolume { Active = true, Value = 25, Label = MediaOutputVolumeValue._25},
                new MediaOutputVolume { Active = true, Value = 26, Label = MediaOutputVolumeValue._26},
                new MediaOutputVolume { Active = true, Value = 27, Label = MediaOutputVolumeValue._27},
                new MediaOutputVolume { Active = true, Value = 28, Label = MediaOutputVolumeValue._28},
                new MediaOutputVolume { Active = true, Value = 29, Label = MediaOutputVolumeValue._29},
                new MediaOutputVolume { Active = true, Value = 30, Label = MediaOutputVolumeValue._30},
                new MediaOutputVolume { Active = true, Value = 31, Label = MediaOutputVolumeValue._31},
                new MediaOutputVolume { Active = true, Value = 32, Label = MediaOutputVolumeValue._32},
                new MediaOutputVolume { Active = true, Value = 33, Label = MediaOutputVolumeValue._33},
                new MediaOutputVolume { Active = true, Value = 34, Label = MediaOutputVolumeValue._34},
                new MediaOutputVolume { Active = true, Value = 35, Label = MediaOutputVolumeValue._35},
                new MediaOutputVolume { Active = true, Value = 36, Label = MediaOutputVolumeValue._36},
                new MediaOutputVolume { Active = true, Value = 37, Label = MediaOutputVolumeValue._37},
                new MediaOutputVolume { Active = true, Value = 38, Label = MediaOutputVolumeValue._38},
                new MediaOutputVolume { Active = true, Value = 39, Label = MediaOutputVolumeValue._39},
                new MediaOutputVolume { Active = true, Value = 40, Label = MediaOutputVolumeValue._40},
                new MediaOutputVolume { Active = true, Value = 41, Label = MediaOutputVolumeValue._41},
                new MediaOutputVolume { Active = true, Value = 42, Label = MediaOutputVolumeValue._42},
                new MediaOutputVolume { Active = true, Value = 43, Label = MediaOutputVolumeValue._43},
                new MediaOutputVolume { Active = true, Value = 44, Label = MediaOutputVolumeValue._44},
                new MediaOutputVolume { Active = true, Value = 45, Label = MediaOutputVolumeValue._45},
                new MediaOutputVolume { Active = true, Value = 46, Label = MediaOutputVolumeValue._46},
                new MediaOutputVolume { Active = true, Value = 47, Label = MediaOutputVolumeValue._47},
                new MediaOutputVolume { Active = true, Value = 48, Label = MediaOutputVolumeValue._48},
                new MediaOutputVolume { Active = true, Value = 49, Label = MediaOutputVolumeValue._49},
                new MediaOutputVolume { Active = true, Value = 50, Label = MediaOutputVolumeValue._50},
                new MediaOutputVolume { Active = true, Value = 51, Label = MediaOutputVolumeValue._51},
                new MediaOutputVolume { Active = true, Value = 52, Label = MediaOutputVolumeValue._52},
                new MediaOutputVolume { Active = true, Value = 53, Label = MediaOutputVolumeValue._53},
                new MediaOutputVolume { Active = true, Value = 54, Label = MediaOutputVolumeValue._54},
                new MediaOutputVolume { Active = true, Value = 55, Label = MediaOutputVolumeValue._55},
                new MediaOutputVolume { Active = true, Value = 56, Label = MediaOutputVolumeValue._56},
                new MediaOutputVolume { Active = true, Value = 57, Label = MediaOutputVolumeValue._57},
                new MediaOutputVolume { Active = true, Value = 58, Label = MediaOutputVolumeValue._58},
                new MediaOutputVolume { Active = true, Value = 59, Label = MediaOutputVolumeValue._59},
                new MediaOutputVolume { Active = true, Value = 60, Label = MediaOutputVolumeValue._60},
                new MediaOutputVolume { Active = true, Value = 61, Label = MediaOutputVolumeValue._61},
                new MediaOutputVolume { Active = true, Value = 62, Label = MediaOutputVolumeValue._62},
                new MediaOutputVolume { Active = true, Value = 63, Label = MediaOutputVolumeValue._63},
                new MediaOutputVolume { Active = true, Value = 64, Label = MediaOutputVolumeValue._64},
                new MediaOutputVolume { Active = true, Value = 65, Label = MediaOutputVolumeValue._65},
                new MediaOutputVolume { Active = true, Value = 66, Label = MediaOutputVolumeValue._66},
                new MediaOutputVolume { Active = true, Value = 67, Label = MediaOutputVolumeValue._67},
                new MediaOutputVolume { Active = true, Value = 68, Label = MediaOutputVolumeValue._68},
                new MediaOutputVolume { Active = true, Value = 69, Label = MediaOutputVolumeValue._69},
                new MediaOutputVolume { Active = true, Value = 70, Label = MediaOutputVolumeValue._70},
                new MediaOutputVolume { Active = true, Value = 71, Label = MediaOutputVolumeValue._71},
                new MediaOutputVolume { Active = true, Value = 72, Label = MediaOutputVolumeValue._72},
                new MediaOutputVolume { Active = true, Value = 73, Label = MediaOutputVolumeValue._73},
                new MediaOutputVolume { Active = true, Value = 74, Label = MediaOutputVolumeValue._74},
                new MediaOutputVolume { Active = true, Value = 75, Label = MediaOutputVolumeValue._75},
                new MediaOutputVolume { Active = true, Value = 76, Label = MediaOutputVolumeValue._76},
                new MediaOutputVolume { Active = true, Value = 77, Label = MediaOutputVolumeValue._77},
                new MediaOutputVolume { Active = true, Value = 78, Label = MediaOutputVolumeValue._78},
                new MediaOutputVolume { Active = true, Value = 79, Label = MediaOutputVolumeValue._79},
                new MediaOutputVolume { Active = true, Value = 80, Label = MediaOutputVolumeValue._80},
                new MediaOutputVolume { Active = true, Value = 81, Label = MediaOutputVolumeValue._81},
                new MediaOutputVolume { Active = true, Value = 82, Label = MediaOutputVolumeValue._82},
                new MediaOutputVolume { Active = true, Value = 83, Label = MediaOutputVolumeValue._83},
                new MediaOutputVolume { Active = true, Value = 84, Label = MediaOutputVolumeValue._84},
                new MediaOutputVolume { Active = true, Value = 85, Label = MediaOutputVolumeValue._85},
                new MediaOutputVolume { Active = true, Value = 86, Label = MediaOutputVolumeValue._86},
                new MediaOutputVolume { Active = true, Value = 87, Label = MediaOutputVolumeValue._87},
                new MediaOutputVolume { Active = true, Value = 88, Label = MediaOutputVolumeValue._88},
                new MediaOutputVolume { Active = true, Value = 89, Label = MediaOutputVolumeValue._89},
                new MediaOutputVolume { Active = true, Value = 90, Label = MediaOutputVolumeValue._90},
                new MediaOutputVolume { Active = true, Value = 91, Label = MediaOutputVolumeValue._91},
                new MediaOutputVolume { Active = true, Value = 92, Label = MediaOutputVolumeValue._92},
                new MediaOutputVolume { Active = true, Value = 93, Label = MediaOutputVolumeValue._93},
                new MediaOutputVolume { Active = true, Value = 94, Label = MediaOutputVolumeValue._94},
                new MediaOutputVolume { Active = true, Value = 95, Label = MediaOutputVolumeValue._95},
                new MediaOutputVolume { Active = true, Value = 96, Label = MediaOutputVolumeValue._96},
                new MediaOutputVolume { Active = true, Value = 97, Label = MediaOutputVolumeValue._97},
                new MediaOutputVolume { Active = true, Value = 98, Label = MediaOutputVolumeValue._98},
                new MediaOutputVolume { Active = true, Value = 99, Label = MediaOutputVolumeValue._99},
                new MediaOutputVolume { Active = true, Value = 100, Label = MediaOutputVolumeValue._100}
            };
            return mediaOutputVolumes;
        }

        private MediaActionType[] MediaActionTypes()
        {
            var mediaActionTypes = new MediaActionType[]
            {
                new MediaActionType { Active = true, ActionTypeOption = MediaActionTypeOption.Pause},
                new MediaActionType { Active = true, ActionTypeOption = MediaActionTypeOption.Play},
                new MediaActionType { Active = true, ActionTypeOption = MediaActionTypeOption.Stop},
                new MediaActionType { Active = true, ActionTypeOption = MediaActionTypeOption.SetVolume}
            };
            return mediaActionTypes;

        }

        private void AddAndSaveDummyDataWithoutConstraints(HemsamaritenWindowsServiceDbContext context)
        {
            context.TellstickAuthentications.AddRange(this.Authentications());
            context.TellstickZNetLiteV2s.AddRange(this.TellstickZNetLiteV2s());
            context.TellstickActionTypes.AddRange(this.TellstickActionTypes());
            context.TellstickUnits.AddRange(this.TellstickUnits());
            context.Schedulers.AddRange(this.Schedulers());

            context.Countries.AddRange(this.AllCountries());

            context.MediaSources.AddRange(this.MediaSourcesSoundEffects());
            context.MediaOutputVolumes.AddRange(this.MediaOutputVolumes());
            context.MediaActionTypes.AddRange(this.MediaActionTypes());

            context.SaveChanges();
        }

        private void AddRadioStationsWithCountries(HemsamaritenWindowsServiceDbContext context)
        {
            context.MediaSources.AddRange(this.MediaSourcesRadioStations(context));
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

            var tellstickActionsToSave = new List<Tuple<bool, string, TellstickActionTypeOption, string>>
            {
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, null, TellstickActionTypeOption.RefreshBearerToken, "0 58 2 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "1", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "2", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "3", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "4", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "5", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "6", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "7", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "8", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "9", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "10", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "11", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "12", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "13", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "14", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "15", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "16", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "17", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "18", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "19", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "20", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "21", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "22", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "23", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "24", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "25", TellstickActionTypeOption.TurnOn, "0 45 5 * * ?"),


                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "1", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "2", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "3", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "4", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "5", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "6", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "7", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "8", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "9", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "10", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "11", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "12", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "13", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "14", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "15", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "16", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "17", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "18", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "19", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "20", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "21", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "22", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "23", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "24", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "25", TellstickActionTypeOption.TurnOff, "0 45 8 * * ?"),



                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "1", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "2", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "3", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "4", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "5", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "6", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "7", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "8", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "9", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "10", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "11", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "12", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "13", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "14", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "15", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "16", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "17", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "18", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "19", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "20", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "21", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "22", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "23", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "24", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "25", TellstickActionTypeOption.TurnOn, "0 0 16 * * ?"),


                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "1", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "2", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "3", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "4", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "5", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "6", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "7", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "8", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "9", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "10", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "11", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "12", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "13", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "14", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "15", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "16", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "17", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "18", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "19", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "20", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),

                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "21", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "22", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "23", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "24", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?"),
                new Tuple<bool, string, TellstickActionTypeOption, string>(actionIsActive, "25", TellstickActionTypeOption.TurnOff, "0 0 23 * * ?")
            };

            var tellstickActions = defaultDataDbInitializerHelper.TellstickActionsToSave(tellstickActionsToSave);

            context.Actions.AddRange(tellstickActions);

            // other changed properties
            context.SaveChanges();
        }

        private void AddInitialMediaActions(HemsamaritenWindowsServiceDbContext context)
        {
            var defaultDataDbInitializerHelper = new DefaultDataDbInitializerHelper(context: context);
            var actionIsActive = true;
            //(string cronExpression, bool active, string mediaSourceName, MediaActionTypeOption mediaActionTypeOption, MediaOutputTargetType mediaOutputTargetType, MediaOutputVolumeValue mediaOutputVolumeLabel)
            var mediaActionsToSave =
                new List<Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?,
                    MediaOutputVolumeValue?>>
                {
                    new Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>("0 49 21 * * ?", actionIsActive, "SR P2", MediaActionTypeOption.Play, MediaOutputTargetType.WindowsDefaultOutputSpeaker, MediaOutputVolumeValue._50),
                    new Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>("0 50 21 * * ?", actionIsActive, null, MediaActionTypeOption.Stop, null, null),
                    new Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>("0 51 21 * * ?", actionIsActive, "SR P1", MediaActionTypeOption.Play, MediaOutputTargetType.WindowsDefaultOutputSpeaker, MediaOutputVolumeValue._50),
                    new Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>("0 52 21 * * ?", actionIsActive, null, MediaActionTypeOption.Stop, null, null),

                    new Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>("0 53 21 * * ?", actionIsActive, null, MediaActionTypeOption.SetVolume, MediaOutputTargetType.WindowsDefaultOutputSpeaker, MediaOutputVolumeValue._90),
                    new Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>("0 54 21 * * ?", actionIsActive, null, MediaActionTypeOption.Stop, null, null),
                    new Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>("0 55 21 * * ?", actionIsActive, "SR P2", MediaActionTypeOption.Play, MediaOutputTargetType.WindowsDefaultOutputSpeaker, MediaOutputVolumeValue._50),
                    new Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>("0 56 21 * * ?", actionIsActive, null, MediaActionTypeOption.Play, null, MediaOutputVolumeValue._50),
                };


            var mediaActions = defaultDataDbInitializerHelper.MediaActionsToSave(mediaActionsToSave);

            context.Actions.AddRange(mediaActions);

            // other changed properties
            context.SaveChanges();
        }

        protected override void Seed(HemsamaritenWindowsServiceDbContext context)
        {
            AddAndSaveDummyDataWithoutConstraints(context);
            AddRadioStationsWithCountries(context);
            InitiallyConnectAuthentication_TellstickZNetLiteV2(context);
            InitiallyConnectActionTypes_TellstickZNetLiteV2(context);
            AddInitialTellstickActions(context);
            AddInitialMediaActions(context);
        }
    }
}