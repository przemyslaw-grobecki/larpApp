using Realms;
using Realms.Sync;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.Extensions;
using RealmTry.Views;

namespace RealmTry.ViewModels
{
    class ManageHostedEventDetailsViewModel : BaseViewModel
    {
        public Command ChangeEventStateCommand { get; set; }
        public async void ChangeEventState()
        {
            switch (eventStatus.Information)
            {
                case "Not Started":
                    Shell.Current.ShowPopup(new EventAboutToStartPopupPage(eventStatus));
                    break;
                case "Started":
                    Shell.Current.ShowPopup(new EventAboutToEndPopupPage(eventStatus, reward));
                    break;
                default:
                    await Shell.Current.DisplayAlert("Something went wrong", "Sorry your event cannot change its state right now...", "Ok");
                    break;
            }
        }
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
            Clues.Clear();
            IsRefreshingClues = true;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var clues = realm.All<Models.Clue>().Where(t => t.EventId == eventId).ToList();
                foreach(Models.Clue clue in clues)
                {
                    Clues.Add(new ItemClueViewModel
                    {
                        ClueContent = clue.ClueContent,
                        ClueImageUrl = clue.ClueImageUrl,
                        AlternativeClueContent = clue.AlternativeClueContent,
                        AttributeTested = clue.AttributeTested,
                        ClueRequirement = clue.ClueRequirement,
                        Description = clue.Description,
                        EventId = clue.EventId,
                        Id = clue.Id,
                        NeedsDiceRoll = clue.NeedsDiceRoll,
                        TestDifficulty = clue.TestDifficulty
                    });
                }
            }
            IsRefreshingClues = false;
        }
        public Command AddClueCommand { get; private set; }
        public void AddClue()
        {
            Shell.Current.ShowPopup(new Views.AddCluePopupPage(EventId));
        }
        public Command EditClueCommand { get; private set; }
        private async void EditClue(object clue)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var clueToEdit = realm.All<Models.Clue>().FirstOrDefault(t => t.Id == (clue as ItemClueViewModel).Id);

                var result = await Shell.Current.DisplayPromptAsync("Editing clue", "Please enter new clue message:", "OK", "Cancel","New Content",-1);

                realm.Write(() =>
                {
                    clueToEdit.ClueContent = result;
                });
            }
            RefreshClues();
        }

        public Command DeleteClueCommand { get; private set; }
        private async void DeleteClue(object clue)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var clueToDelete = realm.All<Models.Clue>().FirstOrDefault(t => t.Id == (clue as ItemClueViewModel).Id);
                realm.Write(() =>
                {
                    realm.Remove(clueToDelete);
                });
            }
            RefreshClues();
        }

        private ItemEquipableViewModel reward = new ItemEquipableViewModel()
        {
            Name = "",
            Rarity = "#FFC800",
            Type = "",
            ImageUrl = "",
            StatBonuses = new List<ItemStatBonusViewModel>() { new ItemStatBonusViewModel { Stat = "", Increase = "" } }
        };
        public ItemEquipableViewModel Reward
        {
            get => reward;
            set => SetProperty(ref reward, value);
        }
        private async void RefreshReward()
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var evReward = realm.All<Models.Reward>().Where(t=>t.EventId == EventId).FirstOrDefault();
                Reward = new ItemEquipableViewModel
                {
                    Name = evReward.Equipable.Name,
                    ImageUrl = evReward.Equipable.ImageUrl,
                    Rarity = evReward.Equipable.Rarity,
                    Type = evReward.Equipable.Type,
                    StatBonuses = new List<ItemStatBonusViewModel>()
                };
                foreach (Models.StatBonus statBonus in evReward.Equipable.StatBonuses)
                {
                    Reward.StatBonuses.Add(new ItemStatBonusViewModel { 
                        Stat = statBonus.Stat,
                        Increase = statBonus.Increase
                    });
                }
            }
        }

        private string eventId;
        public string EventId
        {
            get => eventId;
            set => SetProperty(ref eventId, value);
        }
        public string EnhancedEventId
        {
            get => "EVENT " + eventId;
        }
        private ItemEventStatusViewModel eventStatus = new ItemEventStatusViewModel
        {
            EventId = "loading",
            Id = "loading",
            Information = "loading"
        };
        public ItemEventStatusViewModel EventStatus
        {
            get => eventStatus;
            set => SetProperty(ref eventStatus, value);
        }
        public async void RefreshEventStatus()
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var myStatus = realm.All<Models.EventStatus>().Where(t => t.EventId == EventId).FirstOrDefault();
                EventStatus.EventId = myStatus.EventId;
                EventStatus.Id = myStatus.Id;
                EventStatus.Information = myStatus.Information;
            }
        }
        public ManageHostedEventDetailsViewModel(string evId)
        {
            EventId = evId;
            ChangeEventStateCommand = new Command(ChangeEventState);

            Participants = new ObservableCollection<ItemHostedEventParticipantViewModel>();
            RefreshParticipantsCommand = new Command(RefreshParticipants);

            Clues = new ObservableCollection<ItemClueViewModel>();
            RefreshCluesCommand = new Command(RefreshClues);
            EditClueCommand = new Command(EditClue);
            DeleteClueCommand = new Command(DeleteClue);
            AddClueCommand = new Command(AddClue);

            //Get the wheels spinning...
            RefreshParticipants();
            RefreshClues();
            RefreshReward();
            RefreshEventStatus();
        }
    }
}
