using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace RealmTry.Models
{
    class User:RealmObject
    {
        [PrimaryKey]
        [Required]
        [MapTo("_id")]
        public string Id { get; set; }
        [Required]
        [MapTo("_partitionKey")]
        public string _partitionKey { get; set; }
        [Required]
        [MapTo("username")]
        public string Username { get; set; }
        [Required]
        [MapTo("password")]
        public string Password { get; set; }
        [Required]
        [MapTo("emailAddress")]
        public string Email { get; set; }

    }
}
