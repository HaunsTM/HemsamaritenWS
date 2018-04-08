using System;

namespace Core.Model.ViewModel
{
    public class RegisteredMediaSource
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string MediaCategoryType { get; set; }

        public string MediaSourceCountry { get; set; }
        public string MediaSourceCountry_ISOAlpha2 { get; set; }
    }   
}