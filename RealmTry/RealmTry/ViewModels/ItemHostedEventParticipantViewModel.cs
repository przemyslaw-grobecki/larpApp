using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    class ItemHostedEventParticipantViewModel : BaseViewModel
    {
        public string ParticipantId { get; set; }
        public string ParticipantCharacterName { get; set; }
        public string ParticipantCharacterSurname { get; set; }
        public string ParticipantPortrait { get; set; }
    }
}
