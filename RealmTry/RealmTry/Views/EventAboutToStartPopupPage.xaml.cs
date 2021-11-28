using Realms;
using RealmTry.Services;
using RealmTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventAboutToStartPopupPage : Popup
	{
        private ItemEventStatusViewModel eventStatus;
        public EventAboutToStartPopupPage (ItemEventStatusViewModel eventStatus)
		{
            this.eventStatus = eventStatus;
			InitializeComponent ();
		}

        private async void ReadyCheck_Clicked(object sender, EventArgs e)
        {
            StartEvent.IsVisible = true;
            ReadyCheck.IsEnabled = false;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                Models.Event ev = realm.All<Models.Event>().Where(t => t.Id == eventStatus.EventId).First();
                foreach (string participant in ev.Participants)
                {
                    Models.Prompt prompt = new Models.Prompt
                    {
                        Id = RealmDB.GetUniqueKey(8),
                        _partitionKey = "_partitionKey",
                        Type = "ReadyCheck",
                        Information = "-",
                        TimeStamp = DateTime.Now.ToString(),
                        Status = "Waiting",
                        Sender = RealmDB.CurrentlyLoggedUserId,
                        Receiver = participant
                    };
                    realm.Write(()=> {
                        realm.Add(prompt);
                    });
                }
            } 
            await Task.Delay(10000);
            ReadyCheck.IsEnabled = true;
        }

        private async void StartEvent_Clicked(object sender, EventArgs e)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                Models.Event ev = realm.All<Models.Event>().Where(t => t.Id == eventStatus.EventId).First();
                foreach (string participant in ev.Participants)
                {
                    Models.Prompt prompt = new Models.Prompt
                    {
                        Id = RealmDB.GetUniqueKey(8),
                        _partitionKey = "_partitionKey",
                        Type = "PassInformation",
                        Information = $"An event ~{ev.Title}~ hosted by ~{ev.CreatorId}~ has just begun ({DateTime.Now.ToString()}). Go ahead and enjoy every minute of it :)",
                        TimeStamp = DateTime.Now.ToString(),
                        Status = "Waiting",
                        Sender = RealmDB.CurrentlyLoggedUserId,
                        Receiver = participant
                    };
                    realm.Write(() => {
                        realm.Add(prompt);
                    });
                }
                Models.EventStatus evStatus = realm.All<Models.EventStatus>().Where(t => t.EventId == eventStatus.EventId).FirstOrDefault();
                realm.Write(() => {
                    evStatus.Information = "Started";
                });
            }
            eventStatus.Information = "Started";
            Dismiss(null);
        }
    }
}