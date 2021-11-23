using Realms;
using RealmTry.Services;
using RealmTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterInformationPage : ContentPage
    {
        private ItemCharacterViewModel character;
        public CharacterInformationPage()
        {     
            InitializeComponent();
            Init();
        }

        private async Task GetChar()
        {
            try
            {
                using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
                {
                    var myChar = realm.All<Models.Character>().Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId).FirstOrDefault();
                    character = new ItemCharacterViewModel
                    {
                        Id = myChar.Id,
                        UserId = myChar.UserId,
                        CharacterName = myChar.CharacterName,
                        CharacterSurname = myChar.CharacterSurname,
                        CharacterPortraitUrl = myChar.CharacterPortraitUrl,
                        Charisma = myChar.Charisma,
                        Constitution = myChar.Constitution,
                        Dexterity = myChar.Dexterity,
                        ExperiencePoints = myChar.ExperiencePoints,
                        HealthPoints = myChar.HealthPoints,
                        Inteligence = myChar.Inteligence,
                        Level = myChar.Level,
                        Proffession = myChar.Proffesion,
                        Strength = myChar.Strength,
                        Wisdom = myChar.Wisdom
                    };
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!\n" + ex.Message + " " + ex.InnerException.Message + "\n!!!!!!!!!!!!!!!!!");
            }
        }
        private async Task updateBars()
        {
            try
            {
                nameLabel.Text = character.CharacterName;
                surnameLabel.Text = character.CharacterSurname;
                portraitImage.Source = character.CharacterPortraitUrl;
                professionLabel.Text = character.Proffession;
                expLabel.Text = character.ExperiencePoints.ToString() + "/1000";
                levelLabel.Text = "Level: " + character.Level;
                healthLabel.Text = "Health: " + character.HealthPoints.ToString() + "/10";
                await expProgress.ProgressTo((float)(character.ExperiencePoints / 1000.0f), 500, Easing.CubicInOut);
                strProgress.ProgressTo((float)(character.Strength / 20.0f), 1500, Easing.CubicInOut);
                dexProgress.ProgressTo((float)(character.Dexterity / 20.0f), 1500, Easing.CubicInOut);
                conProgress.ProgressTo((float)(character.Constitution / 20.0f), 1500, Easing.CubicInOut);
                intProgress.ProgressTo((float)(character.Inteligence / 20.0f), 1500, Easing.CubicInOut);
                wisProgress.ProgressTo((float)(character.Wisdom / 20.0f), 1500, Easing.CubicInOut);
                chaProgress.ProgressTo((float)(character.Charisma / 20.0f), 1500, Easing.CubicInOut);
            }
            catch (Exception ex)
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!\n" + ex.Message + " " + ex.InnerException.Message + "\n!!!!!!!!!!!!!!!!!");
            }
        }
        private async void Init()
        {
            await GetChar();
            await updateBars();
        }
    }
}