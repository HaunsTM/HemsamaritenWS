namespace SurveillanceCam2DB.Model
{
    using Interfaces;

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class Image : IEntity, IImage
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Description { get; set; }
        public long DataLength { get; set; }
        public int ImageQualityPercent { get; set; }
        public string ImageFormat { get; set; }
        public DateTime SnapshotTime { get; set; }


        #region Navigation properties

        [JsonIgnore]
        public virtual Camera Camera { get; set; }
        [JsonIgnore]
        public virtual Position Position { get; set; }
        [JsonIgnore]
        public virtual ImageData ImageData { get; set; }

        [ForeignKey("Camera")]
        public int Camera_Id { get; set; }
        [ForeignKey("Position")]
        public int Position_Id { get; set; }

        #endregion

        public Image()
        {
        }
    }
}
