using Core.Model.Enums;

namespace Core.Model.ViewModel
{
    public interface IRegisteredMediaSource
    {
        string Name { get; set; }
        string Url { get; set; }
        string MediaCategoryType { get; set; }

        Country MediaSourceCountry { get; set; }
    }
}