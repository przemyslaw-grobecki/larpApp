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
    public partial class ClueScanWithDiceRollPopupPage : Popup
    {
        public ItemClueViewModel clue;
        private bool isScanning;
        public ClueScanWithDiceRollPopupPage(ItemClueViewModel clue, bool isScanning)
        {
            this.isScanning = isScanning;
            this.clue = clue;
            InitializeComponent();
            labelForExclamation.FadeTo(1, 1500);
            labelForDescription.Text = clue.Description;
            labelForDescription.FadeTo(1, 1500);
            clueImage.Source = clue.ClueImageUrl;
            clueImage.FadeTo(1, 1500);
            labelForStatTested.Text = $"In this Clue Your {clue.AttributeTested} will be tested. The test difficulty is equal to: {clue.TestDifficulty}";
            labelForStatTested.FadeTo(1, 1500);
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
            switch (clue.AttributeTested)
            {
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

                (sender as Button).IsEnabled = false;
                (sender as Button).IsVisible = false;

                var result = Shuffle(getStat(myChar),int.Parse(clue.TestDifficulty));

                lottieAni.PlayAnimation();
                await Task.Delay(5000);
                if (result.Result) {
                    lottieAni.Animation = "success.json";
                    lottieAni.PlayAnimation();
                    await Task.Delay(4000);
                    labelForRollText.Text = $"You passed the test! The score was {result.MyRoll} vs {result.OpposingRoll}.";
                    labelForClueContent.Text = clue.ClueContent;
                    await frameForRollText.FadeTo(1, 1500);
                    await ContinueButton.FadeTo(1, 1500);
                }
                else
                {
                    lottieAni.Animation = "failure.json";
                    lottieAni.PlayAnimation();
                    await Task.Delay(4000);
                    labelForRollText.Text = $"You passed the test! The score was {result.MyRoll} vs {result.OpposingRoll}.";
                    labelForClueContent.Text = clue.AlternativeClueContent;
                    await frameForRollText.FadeTo(1, 1500);
                    await ContinueButton.FadeTo(1, 1500);
                }
            }
        }

        private void Continue_Clicked(object sender, EventArgs e)
        {
            isScanning = false;
            this.Dismiss(null);
        }
    }
}