using Realms;
using RealmTry.Models;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace RealmTry.ViewModels
{
    class AddCluePopupViewModel : BaseViewModel
    {
        private string eventId;
        private Popup popup;
        private string clueDescription = null;
        public string ClueDescription
        {
            get => clueDescription;
            set => SetProperty(ref clueDescription, value);
        }
        private string clueContent = null;
        public string ClueContent
        {
            get => clueContent;
            set => SetProperty(ref clueContent, value);
        }
        private string alternativeClueContent = null;
        public string AlternativeClueContent
        {
            get => alternativeClueContent;
            set => SetProperty(ref alternativeClueContent, value);
        }
        private string chosenStat = null;
        public string ChosenStat
        {
            get => chosenStat;
            set => SetProperty(ref chosenStat, value);
        }
        private string chosenProffesion = null;
        public string ChosenProffesion
        {
            get => chosenProffesion;
            set => SetProperty(ref chosenProffesion, value);
        }
        public string ChosenIconUrl { get; set; }
        public List<string> ViableProffesions { get; } = new List<string>
        {
            "Scholar",
            "Engineer",
            "Mercenery"
        };
        private List<string> viableIcons;
        public List<string> ViableIcons
        {
            get => viableIcons;
            set => SetProperty(ref viableIcons, value);
        }
        public AddCluePopupViewModel(string evId, Popup popup)
        {

            this.popup = popup;
            eventId = evId;
            ViableIcons = new List<string>
            {
                "https://i.ibb.co/3c8xSRy/not-implemented.png",
                "https://i.ibb.co/cc3TTd4/08-portrait.png",
                "https://i.ibb.co/3c8xSRy/not-implemented.png"
            };
            CreateClueCommand = new Command(CreateClue);
            IncrementRollDifficulty = new Command(() =>
            {
                if (int.Parse(RollDifficultyCounter) < 20)
                {
                    RollDifficultyCounter = (int.Parse(RollDifficultyCounter) + 1).ToString();
                }
            });
            DecrementRollDifficulty = new Command(() =>
            {
                if (int.Parse(RollDifficultyCounter) > 0)
                {
                    RollDifficultyCounter = (int.Parse(RollDifficultyCounter) - 1).ToString();
                }
            });
        }
        public ObservableCollection<string> Stats { get; } = new ObservableCollection<string>
        {
            "STRENGTH",
            "DEXTERITY",
            "CONSTITUTION",
            "INTELIGENCE",
            "WISDOM",
            "CHARISMA"
        };
        private bool rollNeedNo;
        public bool RollNeedNo
        {
            get => rollNeedNo;
            set
            {
                SetProperty(ref rollNeedNo, value);
                SetProperty(ref rollNeedYes, !value);
            }
        }
        private bool rollNeedYes;
        public bool RollNeedYes
        {
            get => rollNeedYes;
            set
            {
                SetProperty(ref rollNeedNo, !value);
                SetProperty(ref rollNeedYes, value);
            }
        }
        private bool requirementMetNeedNo;
        public bool RequirementMetNeedNo
        {
            get => requirementMetNeedNo;
            set
            {
                SetProperty(ref requirementMetNeedNo, value);
                SetProperty(ref requirementMetNeedYes, !value);
            }
        }
        private bool requirementMetNeedYes;
        public bool RequirementMetNeedYes
        {
            get => requirementMetNeedYes;
            set
            {
                SetProperty(ref requirementMetNeedNo, !value);
                SetProperty(ref requirementMetNeedYes, value);
            }
        }
        private string rollDifficultyCounter = "0";
        public string RollDifficultyCounter
        {
            get => rollDifficultyCounter;
            set
            {
                if (int.Parse(rollDifficultyCounter) <= 20 || int.Parse(rollDifficultyCounter) >= 0)
                {
                    SetProperty(ref rollDifficultyCounter, value);
                }
            }
        }
        public Command IncrementRollDifficulty
        {
            get; private set;
        }
        public Command DecrementRollDifficulty
        {
            get; private set;
        }
        public Command CreateClueCommand { get; set; }
        private async void CreateClue()
        {
            if (clueDescription == null)
            {
                await Shell.Current.DisplayAlert("Something went wrong.", "Please provide short description for your clue.", "Ok");
                return;
            }
            if (clueContent == null)
            {
                await Shell.Current.DisplayAlert("Something went wrong.", "Please provide the content for your clue.", "Ok");
            }
            if (RollNeedYes)
            {
                if (chosenStat == null)
                {
                    await Shell.Current.DisplayAlert("Something went wrong.", "Please choose stat to be tested.", "Ok");
                    return;
                }
            }
            if (requirementMetNeedYes)
            {
                if (chosenProffesion == null)
                {
                    await Shell.Current.DisplayAlert("Something went wrong.", "Please choose proffesion to be checked.", "Ok");
                    return;
                }
            }
            if (RollNeedYes && requirementMetNeedYes)
            {
                await Shell.Current.DisplayAlert("Something went wrong.", "You can either test smbd proffesion or ask for dice roll. Both can not be arranged.", "Ok");
                return;
            }
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                Clue clue = new Clue
                {
                    Id = "CLUE" + RealmDB.GetUniqueKey(8),
                    EventId = eventId,
                    NeedsDiceRoll = RollNeedYes,
                    Description = ClueDescription,
                    ClueContent = ClueContent,
                    ClueImageUrl = ChosenIconUrl,
                    TestDifficulty = RollDifficultyCounter,
                    AlternativeClueContent = AlternativeClueContent,
                    NeedsRequirement = requirementMetNeedYes,
                    ClueRequirement = ChosenProffesion,
                    _partitionKey = "_partitionKey",
                    AttributeTested = chosenStat
                };
                realm.Write(() =>
                {
                    realm.Add(clue);
                });
                popup.Dismiss(null);
            }
        }
    }
}
