using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    public class ItemHostedEventParticipantViewModel : BaseViewModel
    {
        public string ParticipantId { get; set; }
        public string ParticipantCharacterName { get; set; }
        public string ParticipantCharacterSurname { get; set; }
        public string ParticipantPortraitUrl { get; set; }
        public bool IsParticipantReady { get; set; }
    }
}
