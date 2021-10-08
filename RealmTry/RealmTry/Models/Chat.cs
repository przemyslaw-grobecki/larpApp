using System;
using System.Collections.Generic;
using System.Text;
using Realms.Sync;
using Realms;

namespace RealmTry.Models
{
    class Chat : RealmObject
    {   
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string _id { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
        [Required]
        [MapTo("participantsIds")]
        public IList<string> ParticipantsIds { get; }
        [MapTo("messages")]
        public IList<Message> Messages { get; }
    }
}
