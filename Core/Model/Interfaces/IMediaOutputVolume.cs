using System.ComponentModel.DataAnnotations;

namespace Core.Model.Interfaces
{
    public interface IMediaOutputVolume : IEntity
    {
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        int Value { get; set; }
        Enums.MediaOutputVolumeValue Label { get; set; }
    }
}