using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Models
{
    public class Prompt : RealmObject
    {
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string Id { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
        [Required]
        [MapTo("receiver")]
        public string Receiver { get; set; }
        [Required]
        [MapTo("sender")]
        public string Sender { get; set; }
        [Required]
        [MapTo("type")]
        public string Type { get; set; }
        [Required]
        [MapTo("status")]
        public string Status { get; set;}
        [Required]
        [MapTo("information")]
        public string Information { get; set; }
        [Required]
        [MapTo("timeStamp")]
        public string TimeStamp { get; set; }
    }
}
