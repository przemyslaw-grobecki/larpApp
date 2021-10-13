using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using RealmTry.Models;
using RealmTry.Services;
using Realms;
using Realms.Sync;
using System.Linq;
using RealmTry.Views;
using Xamarin.CommunityToolkit.Extensions;

namespace RealmTry.ViewModels
{
    class ChatViewModel : BaseViewModel
    {
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }
        public ObservableCollection<ItemChatViewModel> Chats { get; set; }
        public Command LeaveConversationButtonCommand { get; set; }
        public async void OnLeaveConversationButtonClicked(object chat)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var chatToQuit = realm.All<Chat>().FirstOrDefault(t => t._id == (chat as ItemChatViewModel).Id);
                realm.Write(() =>
                {
                    chatToQuit.ParticipantsIds.Remove(RealmDB.CurrentlyLoggedUserId);
                });
            }
        }
        public Command JoinConversationButtonCommand { get; set; }
        public async void OnJoinConversationButtonClicked(object chat)
        {
            await Shell.Current.GoToAsync($"{nameof(ChatPageDetails)}?chatId={(chat as ItemChatViewModel).Id}");
        }
        public Command RefreshChatsCommand { get; set; }
        public async void RefreshChats()
        {
            IsRefreshing = true;
            Chats.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var chats = realm.All<Chat>();

                foreach(Chat chat in chats)
                {
                    if (chat.ParticipantsIds.Contains(RealmDB.CurrentlyLoggedUserId))
                    {
                        List<ItemMessageViewModel> messages = new List<ItemMessageViewModel>();
                        foreach (Message message in chat.Messages)
                        {
                            messages.Add(new ItemMessageViewModel
                            {
                                UserId = message.UserId,
                                Content = message.Content,
                                TimeStamp = message.TimeStamp
                            });
                        }
                        List<string> participantsIds = new List<string>();
                        foreach(string id in chat.ParticipantsIds)
                        {
                            participantsIds.Add(id);
                        }
                        Chats.Add(new ItemChatViewModel
                        {
                            Id = chat._id,
                            ParticipantsIds = participantsIds,
                            Messages = messages
                        });
                    }
                }
            }
            IsRefreshing = false;
        }
        public Command CreateNewChatCommand { get; set; }
        public void OnCreateNewChatButtonClicked()
        {
            Shell.Current.ShowPopup(new AddChatPopupPage());
        }

        public ChatViewModel()
        {
            LeaveConversationButtonCommand = new Command(OnLeaveConversationButtonClicked);
            JoinConversationButtonCommand = new Command(OnJoinConversationButtonClicked);
            CreateNewChatCommand = new Command(OnCreateNewChatButtonClicked);
            RefreshChatsCommand = new Command(RefreshChats);
            Chats = new ObservableCollection<ItemChatViewModel>();
            RefreshChats();
        }
    }
}
