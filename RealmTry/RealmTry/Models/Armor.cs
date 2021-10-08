using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Models
{
    class Armor : EmbeddedObject, IWearable
    {
        [MapTo("name")]
        public string Name { get; set; }
    }
}
