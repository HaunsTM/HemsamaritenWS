namespace Tellstick.Model
{
    using Interfaces;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class ImageData : IImageData
    {
        #region IEntity members

        [Key, ForeignKey("Image")]
        public int Image_Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public byte[] Data { get; set; }
        
        #region Navigation properties

        [JsonIgnore]
        public virtual Image Image { get; set; }

        #endregion

        public ImageData()
        {
        }
    }
}
