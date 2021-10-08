using Realms;
using Realms.Sync;
using RealmTry.Models;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace RealmTry.ViewModels
{
    class AddChatPopupViewModel : BaseViewModel
    {
        private Popup popupPage;
        public ObservableCollection<ItemFriendViewModel> Friends { get; set; }
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }
        public Command RefreshFriendsCommand { get; set; }
        public async void RefreshFriends()
        {
            IsRefreshing = true;
            Friends.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var friends = realm.All<FriendRequest>().Where(t => (t.Receiver == RealmDB.CurrentlyLoggedUserId || t.Sender == RealmDB.CurrentlyLoggedUserId) && t.Status == "Accepted");

                foreach (FriendRequest friend in friends)
                {
                    if (friend.Sender == RealmDB.CurrentlyLoggedUserId)
                    {
                        var friendCharacter = realm.All<Character>().Where(t => t.UserId == friend.Receiver).First();
                        Friends.Add(new ItemFriendViewModel()
                        {
                            UserId = friend.Receiver,
                            CharacterName = friendCharacter.CharacterName,
                            CharacterSurname = friendCharacter.CharacterSurname,
                            CharacterProffesion = friendCharacter.Proffesion
                        });
                    }
                    else
                    {
                        var friendCharacter = realm.All<Character>().Where(t => t.UserId == friend.Sender).First();
                        Friends.Add(new ItemFriendViewModel()
                        {
                            UserId = friend.Sender,
                            CharacterName = friendCharacter.CharacterName,
                            CharacterSurname = friendCharacter.CharacterSurname,
                            CharacterProffesion = friendCharacter.Proffesion
                        });
                    }
                }
            }
            IsRefreshing = false;
        }

        private ObservableCollection<ItemFriendViewModel> newChatParticipants;
        public ObservableCollection<ItemFriendViewModel> NewChatParticipants 
        { 
            get=>newChatParticipants;
            set => SetProperty(ref newChatParticipants, value);
        }
        public Command AddNewChatParticipantCommand { get; set; }
        public void OnAddNewChatParticipantButtonClicked(object user)
        {
            if (!NewChatParticipants.Contains(user as ItemFriendViewModel))
            {
                NewChatParticipants.Add(user as ItemFriendViewModel);
            }
        }
        public Command DeleteNewChatParticipantCommand { get; set; }
        public void OnDeleteNewChatParticipantButtonClicked(object user)
        {
            if (NewChatParticipants.Contains(user as ItemFriendViewModel))
            {
                _ = NewChatParticipants.Remove(user as ItemFriendViewModel);
            }
        }
        public Command CreateNewChatCommand { get; set; }
        public async void CreateNewChat()
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var newlyCreatedUserId = RealmDB.GetUniqueKey(8);
                var allIds = realm.All<Chat>().Where(t => t._id == newlyCreatedUserId);
                while (allIds.Any())
                {
                    newlyCreatedUserId = RealmDB.GetUniqueKey(8);
                    allIds = realm.All<Chat>().Where(t => t._id == newlyCreatedUserId);
                }
                Chat chat = new Chat
                {
                    _id = newlyCreatedUserId,
                    _partitionKey = "_partitionKey"
                };
                foreach(ItemFriendViewModel participant in NewChatParticipants)
                {
                    chat.ParticipantsIds.Add(participant.UserId);
                }
                chat.ParticipantsIds.Add(RealmDB.CurrentlyLoggedUserId);
                realm.Write(()=>
                {
                    realm.Add(chat);
                });
            }
            popupPage.Dismiss(null);

        }

        public Command DismissPopupCommand { get; set; }
        public void OnDismissPopupButtonClicked()
        {
            popupPage.Dismiss(null);
        }
        public AddChatPopupViewModel(Popup popupPage)
        {
            this.popupPage = popupPage;
            RefreshFriendsCommand = new Command(RefreshFriends);
            AddNewChatParticipantCommand = new Command(OnAddNewChatParticipantButtonClicked);
            DeleteNewChatParticipantCommand = new Command(OnDeleteNewChatParticipantButtonClicked);
            Friends = new ObservableCollection<ItemFriendViewModel>();
            RefreshFriends();
            NewChatParticipants = new ObservableCollection<ItemFriendViewModel>();
            CreateNewChatCommand = new Command(CreateNewChat);
            DismissPopupCommand = new Command(OnDismissPopupButtonClicked);
        }
    }
}
