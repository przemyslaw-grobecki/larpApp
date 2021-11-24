using System;
using System.Collections.Generic;
using System.Text;
using Realms;
using Realms.Sync;

namespace RealmTry.Models
{
    class Character : RealmObject
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
        [MapTo("characterPortraitUrl")]
        public string CharacterPortraitUrl { get; set; }
        [MapTo("characterName")]
        public string CharacterName { get; set; }
        [MapTo("characterSurname")]
        public string CharacterSurname { get; set; }
        [MapTo("characterProffesion")]
        public string Proffesion { get; set; }
        [MapTo("level")]
        public int Level { get; set; }
        [MapTo("healthPoints")]
        public int HealthPoints { get; set; }
        [MapTo("experiencePoints")]
        public int ExperiencePoints { get; set; }
        [MapTo("strength")]
        public int Strength { get; set; }
        [MapTo("dexterity")]
        public int Dexterity { get; set; }
        [MapTo("constitution")]
        public int Constitution { get; set; }
        [MapTo("inteligence")]
        public int Inteligence { get; set; }
        [MapTo("wisdom")]
        public int Wisdom { get; set; }
        [MapTo("charisma")]
        public int Charisma { get; set; }
    }
}
