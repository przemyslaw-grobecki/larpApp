using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Models
{
    class Inventory : RealmObject
    {
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string Id { get; set; }
        [Required]
        [MapTo("_userId")]
        public string UserId { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
        [MapTo("equipables")]
        public IList<Models.Equipable> Equipables { get; }
    }
}
