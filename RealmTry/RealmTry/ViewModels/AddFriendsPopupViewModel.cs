using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Realms;
using Realms.Sync;
using System.Linq;
using RealmTry.Models;
using RealmTry.Services;
using Xamarin.CommunityToolkit.UI.Views;

namespace RealmTry.ViewModels
{
    class AddFriendsPopupViewModel : BaseViewModel
    {
        private string addedFriendId;
        private Popup popupPage;
        public string AddedFriendId
        {
            get => addedFriendId;
            set => SetProperty(ref addedFriendId, value);
        }

        public Command SendBefriendingRequest { get; set; }
        public async void OnSendBefriendingRequestButtonClicked()
        {
            using (var realm = await Realm.GetInstanceAsync(Services.RealmDB.Configuration))
            {
                var allUsers = realm.All<Models.User>().Where(t => t.Id == AddedFriendId);
                var allRequests = realm.All<Models.FriendRequest>().Where(t => t.Receiver == AddedFriendId && t.Sender == RealmDB.CurrentlyLoggedUserId);

                if (allRequests.Any())
                {
                    popupPage.Dismiss(null);
                    await App.Current.MainPage.DisplayAlert(
                        "Error Creating Request",
                        "Sorry, You have aldready send befriending request to this user.",
                        "Ok");
                }
                else
                {
                    if (allUsers.Any())
                    {
                        var newlyCreatedRequestId = Services.RealmDB.GetUniqueKey(8);
                        var allIds = realm.All<FriendRequest>().Where(t => t.Id == newlyCreatedRequestId);
                        while (allIds.Any())
                        {
                            newlyCreatedRequestId = Services.RealmDB.GetUniqueKey(8);
                            allIds = realm.All<FriendRequest>().Where(t => t.Id == newlyCreatedRequestId);
                        }

                        FriendRequest newlyCreatedFriendRequest = new FriendRequest()
                        {
                            Id = newlyCreatedRequestId,
                            _partitionKey = "_partitionKey",
                            Sender = RealmDB.CurrentlyLoggedUserId,
                            Receiver = AddedFriendId,
                            Status = "Pending"
                        };

                        try
                        {
                            realm.Write(() =>
                            {
                                realm.Add<FriendRequest>(newlyCreatedFriendRequest);
                            });
                            popupPage.Dismiss(null);
                        }
                        catch (Exception ex)
                        {
                            popupPage.Dismiss(null);
                            await App.Current.MainPage.DisplayAlert("An error occured during saving the data", ex.Message, "Ok");
                        }
                    }
                    else
                    {
                        popupPage.Dismiss(null);
                        await App.Current.MainPage.DisplayAlert(
                            "Error finding user",
                            "Sorry, there is no user with provided Id",
                            "Ok");
                    }
                }
            }
        }
        public AddFriendsPopupViewModel(Popup popupPage)
        {
            this.popupPage = popupPage;
            SendBefriendingRequest = new Command(OnSendBefriendingRequestButtonClicked);
        }
    }
}
