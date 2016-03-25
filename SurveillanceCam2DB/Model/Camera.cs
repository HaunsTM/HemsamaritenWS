namespace SurveillanceCam2DB.Model
{
    using SurveillanceCam2DB.Model.Interfaces;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;
    
    public class Camera : IEntity, ICamera
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Name { get; set; }

        public string CameraNetworkUser { get; set; }

        public string CameraNetworkUserPassword { get; set; }

        /// <summary>
        /// This should be the URL from where we could get an image, like http://192.0.1.2.3:4567?currentPic.jpg
        /// </summary>
        public string GetPicURL { get; set; }

        public int DefaultImageQualityPercent { get; set; }

        public int DefaultMaxImageWidth { get; set; }
        public int DefaultMaxImageHeight { get; set; }

        public bool PreserveImageAspectRatio { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual List<Image> Images { get; set; }
        [JsonIgnore]
        public virtual List<Action> Actions { get; set; }

        #endregion

        public Camera()
        {
            this.Images = new List<Image>();
            this.Actions = new List<Action>();
        }
    }
}
