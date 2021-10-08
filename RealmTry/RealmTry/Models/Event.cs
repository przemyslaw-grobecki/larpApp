using System;
using System.Collections.Generic;
using System.Text;
using Realms;
using Realms.Sync;
using Xamarin.Forms.Maps;

namespace RealmTry.Models
{
    class Event : RealmObject
    {
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string Id { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
        [Required]
        [MapTo("title")]
        public string Title { get; set; }
        [Required]
        [MapTo("creatorId")]
        public string CreatorId { get; set; }
        [Required]
        [MapTo("date")]
        public string Date { get; set; }
        [MapTo("maxParticipantsCount")]
        public int MaxParticipantsCount { get; set; }
        [MapTo("isPrivate")]
        public bool IsPrivate { get; set; }
        [Required]
        [MapTo("participants")]
        public IList<string> Participants { get; }
        [MapTo("geoMap")]
        public IList<GeoPoint> GeoMap { get; }
        [MapTo("edgeColorRed")]
        public double EdgeColorRed { get; set; }
        [MapTo("edgeColorGreen")]
        public double EdgeColorGreen { get; set; }
        [MapTo("edgeColorBlue")]
        public double EdgeColorBlue { get; set; }
        [MapTo("fillColorRed")]
        public double FillColorRed { get; set; }
        [MapTo("fillColorGreen")]
        public double FillColorGreen { get; set; }
        [MapTo("fillColorBlue")]
        public double FillColorBlue { get; set; }
    }
}
