using Realms;
using Realms.Sync;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{

    public class StatCheckResult
    {
        public StatCheckResult(int myRoll, int opposingRoll)
        {
            MyRoll = myRoll;
            OpposingRoll = opposingRoll;
        }
        public int MyRoll { get; set; }
        public int OpposingRoll { get; set; }
        public bool Result { get => MyRoll >= OpposingRoll; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AskForRollPopupPage : Popup
    {
        private ViewModels.ItemPromptViewModel prompt;
        private int difficultyLevel;
        public string RollText { get; set; }
        public AskForRollPopupPage(ViewModels.ItemPromptViewModel prompt)
        {
            InitializeComponent();
            this.prompt = prompt;
            difficultyLevel = int.Parse(prompt.Information.Split(' ')[1]);
            labelForExclamation.Text = $"An event creator has asked u to perform a roll! Your {prompt.Information.Split(' ')[0]} will be put into the test! The difficulty of this test is equal to {prompt.Information.Split(' ')[1]}.";
        }

        private async void ChangePromptStatus(ViewModels.ItemPromptViewModel prompt, string status)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var processingPrompt = realm.All<Models.Prompt>().Where(p => p.Id == prompt.Id).FirstOrDefault();
                realm.Write(() =>
                {
                    processingPrompt.Status = status;
                });
            }
        }       
        private void Popup_Dismissed(object sender, PopupDismissedEventArgs e)
        {
            ChangePromptStatus(prompt, "Processed");
        }

        private StatCheckResult Shuffle(int stat, int difficulty)
        {
            Random random = new Random();
            int myRoll = random.Next(stat + 1);
            int opposingRoll = random.Next(difficulty + 1);
            return new StatCheckResult(myRoll, opposingRoll);
        }
        private int getStat(Models.Character character)
        {
            switch (prompt.Information.Split(' ')[0]) {
                case "STRENGTH":
                    return character.Strength;
                case "DEXTERITY":
                    return character.Dexterity;
                case "CONSTITUTION":
                    return character.Constitution;
                case "INTELIGENCE":
                    return character.Inteligence;
                case "WISDOM":
                    return character.Wisdom;
                case "CHARISMA":
                    return character.Charisma;
                default:
                    return 0;
            }
        }
        private async void Shuffle_Clicked(object sender, EventArgs e)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var myChar = realm.All<Models.Character>().Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId).FirstOrDefault();
                lottieAni.PlayAnimation();
                (sender as Button).IsEnabled = false;
                (sender as Button).IsVisible = false;
                await Task.Delay(5000);
                var statCheck = Shuffle(getStat(myChar), difficultyLevel);
                if (statCheck.Result)
                {

                    labelForRollText.Text = $"Congratulations! You have rolled {statCheck.MyRoll} against {statCheck.OpposingRoll} winning gloriously!";                    
                    lottieAni.Animation = "success.json";
                    lottieAni.Scale = 1.5;
                    lottieAni.PlayAnimation();
                    await Task.Delay(4000);
                    await frameForRollText.FadeTo(1, 1500);
                    this.IsLightDismissEnabled = true;

                    var rePrompt = new Models.Prompt()
                    {
                        Id = Services.RealmDB.GetUniqueKey(8),
                        Receiver = prompt.Sender,
                        Sender = prompt.Receiver,
                        Status = "Waiting",
                        TimeStamp = DateTime.Now.ToString(),
                        _partitionKey = "_partitionKey",
                        Type = "PassInformation",
                        Information = $"{prompt.Receiver} has successfully performed a dice roll scoring {statCheck.MyRoll} against {statCheck.OpposingRoll}"
                    };
                    
                    realm.Write(()=>
                    {
                        realm.Add(rePrompt);
                    });
                    
                    await Task.Delay(4000);
                    Dismiss(null);
                }
                else
                {
                    labelForRollText.Text = $"I am sorry! You have rolled {statCheck.MyRoll} against {statCheck.OpposingRoll} loosing patheticly!";
                    lottieAni.Animation = "failure.json";
                    lottieAni.PlayAnimation();
                    await Task.Delay(4000);
                    await frameForRollText.FadeTo(1, 1500);
                    this.IsLightDismissEnabled = true;
                    var rePrompt = new Models.Prompt()
                    {
                        Id = Services.RealmDB.GetUniqueKey(8),
                        Receiver = prompt.Sender,
                        Sender = prompt.Receiver,
                        Status = "Waiting",
                        TimeStamp = DateTime.Now.ToString(),
                        _partitionKey = "_partitionKey",
                        Type = "PassInformation",
                        Information = $"{prompt.Receiver} has failed performing a dice roll scoring {statCheck.MyRoll} against {statCheck.OpposingRoll}"
                    };
                    
                    realm.Write(() =>
                    {
                        realm.Add(rePrompt);
                    });
                    
                    await Task.Delay(4000);
                    Dismiss(null);
                }
            }
        }
    }
}