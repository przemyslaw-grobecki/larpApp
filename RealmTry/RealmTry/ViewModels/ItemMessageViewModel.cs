using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    class ItemMessageViewModel
    {
        private string content;
        public string Content
        {
            get => content;
            set => content = value;
        }
        private string timeStamp;
        public string TimeStamp
        {
            get => timeStamp;
            set => timeStamp = value;
        }
        private string userId;
        public string UserId
        {
            get => userId;
            set => userId = value;
        }
        private string portraitUrl;
        public string PortraitUrl
        {
            get => portraitUrl;
            set => portraitUrl = value;
        }
        private string characterName;
        public string CharacterName
        {
            get => characterName;
            set => characterName = value;
        }
    }
}
