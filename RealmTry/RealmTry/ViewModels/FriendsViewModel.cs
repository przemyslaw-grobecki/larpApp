using RealmTry.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Realms;
using Realms.Sync;
using RealmTry.Services;
using RealmTry.Models;
using System.Linq;

namespace RealmTry.ViewModels
{
    class FriendsViewModel : BaseViewModel
    {
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        } 
        public ObservableCollection<ItemFriendViewModel> Friends { get; set; }
        public ObservableCollection<ItemFriendPendingViewModel> Pendings { get; set; }
        public ObservableCollection<ItemFriendRequestViewModel> Requests { get; set; }
        public Command AddFriendsButtonClicked { get; set; }
        public Command AcceptRequestButtonCommand { get; set; }
        public Command DeclineRequestButtonCommand { get; set; }
        public Command RefreshFriendsCommand { get; set; }
        public Command RefreshPendingsCommand { get; set; }
        public Command RefreshRequestsCommand { get; set; }
        public FriendsViewModel()
        {
            AddFriendsButtonClicked = new Command(OnAddFriendsButtonClicked);
            AcceptRequestButtonCommand = new Command(OnAcceptRequestButtonClicked);
            DeclineRequestButtonCommand = new Command(OnDeclineRequestButtonClicked);
            RefreshFriendsCommand = new Command(RefreshFriends);
            RefreshPendingsCommand = new Command(RefreshPendings);
            RefreshRequestsCommand = new Command(RefreshRequests);

            //Fill observable friends collection
            Friends = new ObservableCollection<ItemFriendViewModel>();
            RefreshFriends();
            //Fill observable Pendings collection
            Pendings = new ObservableCollection<ItemFriendPendingViewModel>();
            RefreshPendings();
            //Fill observable Requests collection
            Requests = new ObservableCollection<ItemFriendRequestViewModel>();
            RefreshRequests();
        }
        public void OnAddFriendsButtonClicked()
        {
            Shell.Current.ShowPopup(new AddFriendsPopupPage());
        }
        public async void OnAcceptRequestButtonClicked(object request)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var acceptedRequest = realm.All<FriendRequest>().FirstOrDefault(t => t.Receiver == RealmDB.CurrentlyLoggedUserId && t.Sender == (request as ItemFriendPendingViewModel).Id);

                realm.Write(() =>
                {
                    acceptedRequest.Status = "Accepted";
                });
            }

            await App.Current.MainPage.DisplayAlert("Request Accepted",
                       "Congratulation! " + (request as ItemFriendPendingViewModel).CharacterName + " is now your friend.", 
                       "Ok");
            RefreshPendings();
        }
        public async void OnDeclineRequestButtonClicked(object request)
        {

            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var acceptedRequest = realm.All<FriendRequest>().FirstOrDefault(t => t.Receiver == RealmDB.CurrentlyLoggedUserId && t.Sender == (request as ItemFriendPendingViewModel).Id);

                realm.Write(() =>
                {
                    acceptedRequest.Status = "Declined";
                });
            }

            await App.Current.MainPage.DisplayAlert("Request Declined",
                       (request as ItemFriendPendingViewModel).CharacterName + " will never be your friend. Such a pity.", 
                       "Ok");
            RefreshPendings();
        }
        public async void RefreshFriends()
        {
            IsRefreshing = true;
            Friends.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var friends = realm.All<FriendRequest>().Where(t => (t.Receiver == RealmDB.CurrentlyLoggedUserId || t.Sender == RealmDB.CurrentlyLoggedUserId) && t.Status == "Accepted");
                
                foreach (FriendRequest friend in friends)
                {
                    if (friend.Sender == RealmDB.CurrentlyLoggedUserId) {
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
        public async void RefreshPendings()
        {
            IsRefreshing = true;
            Pendings.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var pendings = realm.All<FriendRequest>().Where(t => t.Receiver == RealmDB.CurrentlyLoggedUserId && t.Status == "Pending");
                foreach (FriendRequest pending in pendings)
                {
                    if (Pendings.Count(t => t.Id == pending.Sender) == 0)
                    {
                        var friendCharacter = realm.All<Character>().Where(t => t.UserId == pending.Sender).First();
                        Pendings.Add(new ItemFriendPendingViewModel()
                        {
                            Id = pending.Sender,
                            CharacterName = friendCharacter.CharacterName,
                            CharacterSurname = friendCharacter.CharacterSurname,
                            CharacterProffesion = friendCharacter.Proffesion
                        });
                    }
                }
            }
            IsRefreshing = false;
        }
        public async void RefreshRequests()
        {
            IsRefreshing = true;
            Requests.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {             
                var requests = realm.All<FriendRequest>().Where(t => t.Sender == RealmDB.CurrentlyLoggedUserId);
                foreach(FriendRequest request in requests)
                {
                    if(Requests.Where(t=>t.Id == request.Receiver).Count() == 0)
                    {
                        Requests.Add(new ItemFriendRequestViewModel()
                        {
                            Id = request.Receiver,
                            Status = request.Status
                        });
                    }
                }
            }
            IsRefreshing = false;
        }

    }
}
