using System;
using System.Collections.Generic;
using System.Text;
using Realms;
using Realms.Sync;

namespace RealmTry.Models
{
    class FriendRequest: RealmObject
    {
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string Id { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
        [Required]
        [MapTo("sender")]
        public string Sender { get; set; }
        [Required]
        [MapTo("receiver")]
        public string Receiver { get; set; }
        [Required]
        [MapTo("status")]
        public string Status { get; set; }
    }
}
