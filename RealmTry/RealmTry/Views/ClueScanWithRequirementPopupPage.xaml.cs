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
    public partial class ClueScanWithRequirementPopupPage : Popup
    {
        public ItemClueViewModel clue;
        private bool isScanning;
        public ClueScanWithRequirementPopupPage(ItemClueViewModel clue, bool isScanning)
        {
            try
            {
                this.isScanning = isScanning;
                this.clue = clue;
                InitializeComponent();
                labelForExclamation.FadeTo(1, 2500);
                labelForDescription.Text = clue.Description;
                labelForDescription.FadeTo(1, 2500);
                clueImage.Source = clue.ClueImageUrl;
                clueImage.FadeTo(1, 2500);
                labelForRequirementNeeded.Text = $"This Clue is best suited to be deciphered by {clue.ClueRequirement}. If You have one in the party, maybe it would be a good idea to show it them.";
                labelForRequirementNeeded.FadeTo(1, 2500);
                ContinueFirstButton.FadeTo(1, 2500);
            }
            catch(Exception ex)
            {
                Console.WriteLine("!!!!!!!!!!\n" + ex.Message + ex.InnerException.Message + "\n!!!!!!!!!!");
            }
        }
        private async Task<ItemCharacterViewModel> GetChar()
        {
            try
            {
                using (var realm = await Realms.Realm.GetInstanceAsync(RealmDB.Configuration))
                {
                    var myChar = realm.All<Models.Character>().Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId).FirstOrDefault();
                    return new ItemCharacterViewModel
                    {
                        CharacterName = myChar.CharacterName,
                        CharacterPortraitUrl = myChar.CharacterPortraitUrl,
                        CharacterSurname = myChar.CharacterSurname,
                        Id = myChar.Id,
                        Charisma = myChar.Charisma,
                        Constitution = myChar.Constitution,
                        Dexterity = myChar.Dexterity,
                        ExperiencePoints = myChar.ExperiencePoints,
                        HealthPoints = myChar.HealthPoints,
                        Inteligence = myChar.Inteligence,
                        Level = myChar.Level,
                        Proffession = myChar.Proffesion,
                        Strength = myChar.Strength,
                        UserId = myChar.UserId,
                        Wisdom = myChar.Wisdom
                    };
                }
            }catch(Exception ex)
            {
                Console.WriteLine("!!!!!!!!!!\n" + ex.Message + ex.InnerException.Message + "\n!!!!!!!!!!");
                return null;
            }
        }

        private async void ContinueFirst_Clicked(object sender, EventArgs e)
        {
            using (var realm = await Realms.Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var myChar = realm.All<Models.Character>().Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId).FirstOrDefault();
                if (myChar.Proffesion == clue.ClueRequirement)
                {
                    labelForRequirementMetText.Text = "Since You are matching with the clue requirements, additional content is unlocked!";
                    labelForClueContent.Text = clue.ClueContent;
                }
                else
                {
                    labelForRequirementMetText.Text = "Since You are not matching with the clue requirements, additional content is not available!";
                    labelForClueContent.Text = clue.AlternativeClueContent;
                }
                await frameRequirementMetText.FadeTo(1, 1500);
            }
        }

        private void ContinueSecond_Clicked(object sender, EventArgs e)
        {
            isScanning = false;
            Dismiss(null);
        }
    }
}