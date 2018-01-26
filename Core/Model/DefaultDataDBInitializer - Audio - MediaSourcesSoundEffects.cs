using Core.Model.Enums;

namespace Core.Model
{
    public partial class DefaultDataDbInitializer
    {
        private const string MEDIA_DIRECTORY_URL = "Media/";

        private MediaSource[] MediaSourcesSoundEffects()
        {
            var mediaSources = new MediaSource[]
            {
               new MediaSource { Active=true, Name="Animal_Dog_Bark_01", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Bark-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Animal_Dog_Bark_02", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Bark-02.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Animal_Dog_Growl_01", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Growl-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Animal_Dog_Growl_BullTerrier_01", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Growl-BullTerrier-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Animal_Dog_Growl_BullTerrier_02", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Growl-BullTerrier-02.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Animal_Dog_Growl_Doberman_01", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Growl-Doberman-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Animal_Dog_Growl_Doberman_02", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Growl-Doberman-02.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Animal_Dog_Growl_Doberman_03", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Growl-Doberman-03.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Animal_Dog_Howl", Url =  MEDIA_DIRECTORY_URL + "Animal-Dog-Howl.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Alarm_Danger_01", Url =  MEDIA_DIRECTORY_URL + "Emergency-Alarm-Danger-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Horn_Honk_01", Url =  MEDIA_DIRECTORY_URL + "Emergency-Horn-Honk-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Horn_ShortDouble_01", Url =  MEDIA_DIRECTORY_URL + "Emergency-Horn-ShortDouble-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Siren_HighLow_01", Url =  MEDIA_DIRECTORY_URL + "Emergency-Siren-HighLow-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Siren_HighLow_02", Url =  MEDIA_DIRECTORY_URL + "Emergency-Siren-HighLow-02.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Siren_Wail_01", Url =  MEDIA_DIRECTORY_URL + "Emergency-Siren-Wail-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Siren_Yelp_01", Url =  MEDIA_DIRECTORY_URL + "Emergency-Siren-Yelp-01.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Siren_Yelp_02", Url =  MEDIA_DIRECTORY_URL + "Emergency-Siren-Yelp-02.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
                new MediaSource { Active=true, Name="Emergency_Siren_Yelp_03", Url =  MEDIA_DIRECTORY_URL + "Emergency-Siren-Yelp-03.flac" , MediaCategoryType = MediaCategoryType.InternetStreamRadio},
            };

            return mediaSources;
        }
    }
}

