using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    class ItemFriendPendingViewModel : BaseViewModel
    {
        private string id;
        public string Id {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string characterName;
        public string CharacterName
        {
            get => characterName;
            set => SetProperty(ref characterName, value);
        }
        private string characterSurname;
        public string CharacterSurname
        {
            get => characterSurname;
            set => SetProperty(ref characterSurname, value);
        }
        private string characterProffesion;
        public string CharacterProffesion
        {
            get => characterProffesion;
            set => SetProperty(ref characterProffesion, value);
        }
    }
}
