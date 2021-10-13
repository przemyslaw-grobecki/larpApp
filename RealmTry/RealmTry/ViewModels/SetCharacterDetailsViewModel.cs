using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ZXing;
using Realms;
using Realms.Sync;
using RealmTry.Services;
using RealmTry.Models;
using System.Linq;
using RealmTry.Views;

namespace RealmTry.ViewModels
{
    class SetCharacterDetailsViewModel : BaseViewModel
    {
        private string portraitUrl;
        public string PortraitUrl
        {
            get => portraitUrl;
            set => SetProperty(ref portraitUrl, value);
        }

        private string characterName;
        public string CharacterName
        {
            get => characterName;
            set => SetProperty(ref characterName, value);
        }
        private string characterSurname;
        public string CharacterSurname
        {
            get => characterSurname;
            set => SetProperty(ref characterSurname, value);
        }
        private List<string> viablePortraits;
        public List<string> ViablePortraits
        { 
            get => viablePortraits;
            set => SetProperty(ref viablePortraits, value);
        }
        public Command OnSetDetailsClickedCommand { get; set; }
        public async void OnSetDetailsClicked()
        {
            if(characterName == null)
            {
                return;
            }
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration)){
                Character character = realm.All<Character>().FirstOrDefault(t => t.UserId == RealmDB.CurrentlyLoggedUserId);
                realm.Write(() =>
                {
                    character.CharacterName = CharacterName;
                    character.CharacterSurname = CharacterSurname;
                    character.CharacterPortraitUrl = portraitUrl;
                });
            }
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
        public SetCharacterDetailsViewModel()
        {
            ViablePortraits = new List<string>{
                "https://i.ibb.co/vsKNmzQ/09-portrait.png",
                "https://i.ibb.co/cc3TTd4/08-portrait.png",
                "https://i.ibb.co/JKhB312/07-portrait.png",
                "https://i.ibb.co/Xx5shtt/06-portrait.png",
                "https://i.ibb.co/vJDRR6J/05-portrait.png",
                "https://i.ibb.co/5xs3MKR/04-portrait.png",
                "https://i.ibb.co/d7ZjKP4/03-portrait.png",
                "https://i.ibb.co/64gJvt7/02-portrait.png",
                "https://i.ibb.co/tmxZXcx/00-portrait.png",
                "https://i.ibb.co/Kq1CrWh/01-portrait.png"
            };
            Title = "Set character details";
            OnSetDetailsClickedCommand = new Command(OnSetDetailsClicked);
        }
    }
}
