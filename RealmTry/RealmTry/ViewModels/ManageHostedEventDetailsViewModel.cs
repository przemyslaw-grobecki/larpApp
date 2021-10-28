using Realms;
using Realms.Sync;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RealmTry.ViewModels
{
    class ManageHostedEventDetailsViewModel : BaseViewModel
    {
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
                foreach(string participantId in ev.Participants)
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
        public ObservableCollection<ItemClueViewModel> Clues { get; set; }
        private bool isRefreshingClues = false;
        public bool IsRefreshingClues
        {
            get => isRefreshingClues;
            set => SetProperty(ref isRefreshingClues, value);
        }
        public Command RefreshCluesCommand { get; set; }
        private async void RefreshClues()
        {
            IsRefreshingClues = true;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {

            }
            IsRefreshingClues = false;
        }

        //public ObservableCollection<ItemRewardViewModel> Rewards { get; set; }
        private string eventId;
        public string EventId
        {
            get => eventId;
            set => SetProperty(ref eventId, value);
        }
        public ManageHostedEventDetailsViewModel(string evId)
        {
            EventId = evId;

            Participants = new ObservableCollection<ItemHostedEventParticipantViewModel>();
            RefreshParticipantsCommand = new Command(RefreshParticipants);

            Clues = new ObservableCollection<ItemClueViewModel>();
            RefreshCluesCommand = new Command(RefreshClues);

            //Get the wheels spinning...
            RefreshParticipants();
        }
    }
}
