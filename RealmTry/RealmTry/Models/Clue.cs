using System;
using System.Collections.Generic;
using System.Text;
using Realms;
using Realms.Sync;

namespace RealmTry.Models
{
    public class Clue : RealmObject
    {
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string Id { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
        [Required]
        [MapTo("eventId")]
        public string EventId { get; set; }
        [Required]
        [MapTo("description")]
        public string Description { get; set; }
        [Required]
        [MapTo("clueImageUrl")]
        public string ClueImageUrl { get; set; }
        [MapTo("needsRequirement")]
        public bool NeedsRequirement { get; set; }
        [MapTo("clueRequirement")]
        public string ClueRequirement { get; set; }
        [MapTo("needsDiceRoll")]
        public bool NeedsDiceRoll { get; set; }
        [MapTo("testDifficulty")]
        public string TestDifficulty { get; set; }
        [MapTo("attributeTested")]
        public string AttributeTested { get; set; }
        [Required]
        [MapTo("clueContent")]
        public string ClueContent { get; set; }
        [MapTo("alternativeClueContent")]
        public string AlternativeClueContent { get; set; }
    }
}
