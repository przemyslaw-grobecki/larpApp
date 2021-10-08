using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Models
{
    class Reward : RealmObject
    {
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string Id { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
    }
}
