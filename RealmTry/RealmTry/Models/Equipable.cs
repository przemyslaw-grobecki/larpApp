using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Models
{
    public class Equipable : EmbeddedObject
    {
        [MapTo("name")]
        public string Name { get; set; }
        [MapTo("imageUrl")]
        public string ImageUrl { get; set; }
        [MapTo("rarity")]
        public string Rarity { get; set; }
        [MapTo("type")]
        public string Type { get; set; }
        [MapTo("statBonuses")]
        public IList<Models.StatBonus> StatBonuses { get; }
    }
}
