using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace RealmTry.ViewModels
{
    class ManageHostedEventDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<ItemHostedEventParticipantViewModel> Participants { get; set; }
        public ObservableCollection<ItemClueViewModel> Clues { get; set; }
        //public ObservableCollection<ItemRewardViewModel> Rewards { get; set; }
        public Command RefreshCluesCommand { get; set; }
        public Command RefreshParticipantsCommand { get; set; }
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
            Clues = new ObservableCollection<ItemClueViewModel>();
        }
    }
}
