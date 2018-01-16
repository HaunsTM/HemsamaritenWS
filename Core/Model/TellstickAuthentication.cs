using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class TellstickAuthentication : ITellstickAuthentication
    {

        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public int Expires { get; set; }
        public string Token { get; set; }
        public DateTime Received { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual TellstickZNetLiteV2 TellstickZNetLiteV2 { get; set; }

        [ForeignKey("TellstickZNetLiteV2")]
        public int? TellstickZNetLiteV2_Id { get; set; }

        #endregion

        public TellstickAuthentication()
        {

        }

    }
}