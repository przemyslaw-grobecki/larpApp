using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    class ItemClueViewModel : BaseViewModel
    {
        private string id;
        public string Id
        { 
            get => id;
            set => SetProperty(ref id, value);
        }
        private string eventId;
        public string EventId
        { 
            get => eventId; 
            set => SetProperty(ref eventId, value);
        }
        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        private string clueImageUrl;
        public string ClueImageUrl
        {
            get => clueImageUrl;
            set => SetProperty(ref clueImageUrl, value);
        }
        private string clueRequirement;
        public string ClueRequirement
        {
            get => clueRequirement;
            set => SetProperty(ref clueRequirement, value);
        }
        private bool needsDiceRoll;
        public bool NeedsDiceRoll
        {
            get => needsDiceRoll;
            set => SetProperty(ref needsDiceRoll, value);
        }
        private string testDifficulty;
        public string TestDifficulty
        {
            get => testDifficulty;
            set => SetProperty(ref testDifficulty, value);
        }
        private string attributeTested;
        public string AttributeTested 
        {
            get => attributeTested; 
            set => SetProperty(ref attributeTested, value); 
        }
        private string clueContent;
        public string ClueContent 
        {
            get => clueContent;
            set => SetProperty(ref clueContent, value);
        }
        private string alternativeClueContent;
        public string AlternativeClueContent 
        {
            get => alternativeClueContent; 
            set => SetProperty(ref alternativeClueContent, value);
        }
    }
}
