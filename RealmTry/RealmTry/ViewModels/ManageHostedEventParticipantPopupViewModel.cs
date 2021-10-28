using Realms;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace RealmTry.ViewModels
{
    class ManageHostedEventParticipantPopupViewModel : BaseViewModel
    {
        private ObservableCollection<string> stats = new ObservableCollection<string>
        {
            "STRENGTH",
            "DEXTERITY",
            "CONSTITUTION",
            "INTELIGENCE",
            "WISDOM",
            "CHARISMA"
        };
        private string damageCounter = "0";
        public string DamageCounter
        {
            get => damageCounter;
            set
            {
                if (int.Parse(damageCounter) <= 10 || int.Parse(damageCounter) >= 0)
                {
                    SetProperty(ref damageCounter, value);
                }
            }
        }
        public Command IncrementDamageCounter
        {
            get; private set;
        }
        public Command DecrementDamageCounter
        {
            get; private set;
        }
        public ObservableCollection<string> Stats
        {
            get => stats;
            set => SetProperty(ref stats, value);
        }
        private string participantId;
        public string ParticipantId
        {
            get => participantId;
            set => SetProperty(ref participantId, value);
        }
        public Command AskForRollCommand
        {
            get; private set;
        }
        public Command DealDamageCommand
        {
            get; private set;
        }
        public ManageHostedEventParticipantPopupViewModel(string participantId)
        {
            ParticipantId = participantId;
            IncrementDamageCounter = new Command(() =>
            {
                if (int.Parse(DamageCounter) < 10)
                {
                    DamageCounter = (int.Parse(DamageCounter) + 1).ToString();
                }
            });
            DecrementDamageCounter = new Command(() =>
            {
                if (int.Parse(DamageCounter) > 0)
                {
                    DamageCounter = (int.Parse(DamageCounter) - 1).ToString();
                }
            });
            AskForRollCommand = new Command(() =>
            {
                //Create a prompt
            });
            DealDamageCommand = new Command(async () =>
            {
                //modify character hp


                using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
                {
                    Models.Prompt damageInfoPrompt = new Models.Prompt()
                    {
                        Id = Services.RealmDB.GetUniqueKey(8),
                        _partitionKey = "_partitionKey",
                        Receiver = participantId,
                        Sender = Services.RealmDB.CurrentlyLoggedUserId,
                        Type = "Information",
                        Information = $"You have been dealt {damageCounter} points of damage",
                        Status = "Waiting",
                        TimeStamp = DateTime.Now.ToString(),
                    };
                    realm.Write(()=>
                    {
                        realm.Add(damageInfoPrompt);
                    });
                }
            });
        }
    }
}
