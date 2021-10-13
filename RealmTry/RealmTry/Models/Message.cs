using System;
using System.Collections.Generic;
using System.Text;
using Realms;
using Realms.Sync;
namespace RealmTry.Models
{
    class Message : EmbeddedObject
    {
        [MapTo("content")]
        public string Content { get; set; }
        [MapTo("timeStamp")]
        public string TimeStamp { get; set; }
        [MapTo("userId")]
        public string UserId { get; set; }
    }
}
