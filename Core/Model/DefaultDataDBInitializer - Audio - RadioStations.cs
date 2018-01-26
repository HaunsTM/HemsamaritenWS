using Core.Model.Enums;

namespace Core.Model
{
    public partial class DefaultDataDbInitializer
    {
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
    }
}