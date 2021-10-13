using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Models
{
    class GeoPoint : EmbeddedObject
    {
        [MapTo("latitude")]
        public double Latitude { get; set; }
        [MapTo("longtitude")]
        public double Longtitude { get; set; }
    }
}
