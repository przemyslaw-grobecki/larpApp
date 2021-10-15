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
            "STR",
            "DEX",
            "CON",
            "INT",
            "WIS",
            "CHA"
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
        public Command ConfirmCommand
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
            ConfirmCommand = new Command(() =>
            {

            });
        }
    }
}
