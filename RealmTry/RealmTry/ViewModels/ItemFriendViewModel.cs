using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    class ItemFriendViewModel
    {
        public string UserId { get; set; }
        public string CharacterName { get; set; }
        public string CharacterSurname { get; set; }
        public string CharacterProffesion { get; set; }
        public string CharacterDescription
        {
            get => $"{CharacterName} {CharacterSurname}, whose proffesion is {CharacterProffesion} *(id={UserId})*";
        }
    }
}
