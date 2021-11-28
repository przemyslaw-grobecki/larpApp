using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Models
{
    public class StatBonus : EmbeddedObject
    {
        [MapTo("stat")]
        public string Stat { get; set; }
        [MapTo("increase")]
        public string Increase { get; set; }
    }
}
