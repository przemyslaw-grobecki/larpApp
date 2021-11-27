using Realms;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RealmTry.ViewModels
{
    class EventAboutToEndPopupViewModel : BaseViewModel
    {
        public ItemEquipableViewModel equipable;
        public string EventId { get; private set; }
        public ObservableCollection<ItemHostedEventParticipantViewModel> Participants { get; set; }
        private bool isRefreshingParticipants = false;
        public bool IsRefreshingParticipants
        {
            get => isRefreshingParticipants;
            set => SetProperty(ref isRefreshingParticipants, value);
        }
        public Command RefreshParticipantsCommand { get; set; }
        private async void RefreshParticipants()
        {
            IsRefreshingParticipants = true;
            Participants.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var ev = realm.All<Models.Event>().Where(t => t.Id == EventId).FirstOrDefault();
                foreach (string participantId in ev.Participants)
                {
                    var participantChar = realm.All<Models.Character>().Where(t => t.UserId == participantId).FirstOrDefault();
                    ItemHostedEventParticipantViewModel hostedEventParticipant = new ItemHostedEventParticipantViewModel
                    {
                        ParticipantId = participantId,
                        ParticipantCharacterName = participantChar.CharacterName,
                        ParticipantCharacterSurname = participantChar.CharacterSurname,
                        ParticipantPortraitUrl = participantChar.CharacterPortraitUrl
                    };
                    Participants.Add(hostedEventParticipant);
                }
            }
            IsRefreshingParticipants = false;
        }
        public EventAboutToEndPopupViewModel(string eventId, ItemEquipableViewModel equipable)
        {
            this.EventId = eventId;
            this.equipable = equipable;
            Participants = new ObservableCollection<ItemHostedEventParticipantViewModel>();
            RefreshParticipantsCommand = new Command(RefreshParticipants);

            //Get the wheels spinning...
            RefreshParticipants();
        }
    }
}
