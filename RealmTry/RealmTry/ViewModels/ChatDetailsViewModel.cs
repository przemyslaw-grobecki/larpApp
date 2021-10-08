using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using Xamarin.Forms;
using RealmTry.Services;
using RealmTry.Models;
using System.Linq;

namespace RealmTry.ViewModels
{
    class ChatDetailsViewModel : BaseViewModel
    {
        public ListView MessagesList { get; set; }
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }
        private string chatId;
        public string ChatId { get => chatId; set => SetProperty(ref chatId, value); }
        private string messageContent;
        public string MessageContent
        {
            get => messageContent;
            set => SetProperty(ref messageContent, value);
        }
        public ObservableCollection<ItemMessageViewModel> Messages { get; set; }
        public Command RefreshMessagesCommand { get; set; }
        public async void RefreshMessages()
        {
            IsRefreshing = true;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var chat = realm.All<Chat>().Where(t => t._id == chatId).FirstOrDefault();
                var newMessagesCount = chat.Messages.Count - Messages.Count;
                if (newMessagesCount > 0)
                {
                    for (int i = Messages.Count ; i < chat.Messages.Count; i++)
                    {
                        var userCharacter = realm.All<Character>().Where(t => t.UserId == chat.Messages[i].UserId).FirstOrDefault();

                        Messages.Add(new ItemMessageViewModel
                        {
                            UserId = chat.Messages[i].UserId,
                            Content = chat.Messages[i].Content,
                            TimeStamp = chat.Messages[i].TimeStamp,
                            CharacterName = userCharacter.CharacterName + " " + userCharacter.CharacterSurname,
                            PortraitUrl = userCharacter.CharacterPortraitUrl
                        });
                    }
                    MessagesList.ScrollTo(Messages.Last(), ScrollToPosition.End, true);
                }
            }        
            IsRefreshing = false;
        }
        public Command SendMessageCommand { get; set; }
        public async void OnSendMessageButtonClicked()
        {
            if (messageContent.Length != 0)
            {
                try
                {
                    using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
                    {
                        var chat = realm.All<Chat>().Where(t => t._id == chatId).FirstOrDefault();
                        realm.Write(() =>
                        {
                            chat.Messages.Add(new Message
                            {
                                Content = messageContent,
                                TimeStamp = DateTime.Now.ToString(),
                                UserId = RealmDB.CurrentlyLoggedUserId
                            });
                        });
                    }
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert(e.Message,e.Message,e.Message);
                }
            }
            RefreshMessages();
        }
        public ChatDetailsViewModel(string chatId, ListView listView)
        {
            RefreshMessagesCommand = new Command(RefreshMessages);
            SendMessageCommand = new Command(OnSendMessageButtonClicked);
            Messages = new ObservableCollection<ItemMessageViewModel>();

            ChatId = chatId;
            MessagesList = listView;

            //Get the wheels spinning...
            RefreshMessages();
        }
    }
}
