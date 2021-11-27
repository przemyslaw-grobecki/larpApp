using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Models
{
    class EventStatus : RealmObject
    {
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string Id { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
        [Required]
        [MapTo("eventId")]
        public string EventId { get; set; }
        [Required]
        [MapTo("information")]
        public string Information { get; set; }
    }
}
