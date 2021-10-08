using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace RealmTry.Models
{
    class Item : EmbeddedObject
    {
        [MapTo("name")]
        public string Name { get; set; }
    }
}
