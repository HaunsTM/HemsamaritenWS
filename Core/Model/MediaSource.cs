﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public class MediaSource : IMediaSource
    {
        #region IEntity members

        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        #endregion

        public string Url { get; set; }
        public string MediaDataBase64 { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public virtual MediaSourceCategory MediaSourceCategory { get; set; }

        [JsonIgnore]
        public virtual List<MediaAction> MediaActions { get; set; }

        #endregion

        public MediaSource()
        {
            this.MediaActions = new List<MediaAction>();
        }
    }
}
