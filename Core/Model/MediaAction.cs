﻿using System.ComponentModel.DataAnnotations.Schema;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class MediaAction : Action, IMediaAction
    {
        #region Navigation properties
        
        [JsonIgnore]
        public virtual MediaSource MediaSource { get; set; }
        [JsonIgnore]
        public virtual MediaOutput MediaOutput { get; set; }
        [JsonIgnore]
        public virtual MediaOutputSetting MediaOutputSetting { get; set; }
        [JsonIgnore]
        public virtual MediaActionType MediaActionType { get; set; }

        [ForeignKey("MediaSource")]
        public int? MediaSource_Id { get; set; }
        [ForeignKey("MediaOutput")]
        public int MediaOutput_Id { get; set; }
        [ForeignKey("MediaOutputSetting")]
        public int? MediaOutputSetting_Id { get; set; }
        [ForeignKey("MediaActionType")]
        public int? MediaActionType_Id { get; set; }

        #endregion

        public MediaAction()
        {
        }
    }
}
