using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Realms;
using Realms.Sync;
using System.Linq;

namespace RealmTry.ViewModels
{
    class ClassSelectionViewModel : BaseViewModel
    {
        private IEnumerable<Models.Proffesion> proffesionList;
        public IEnumerable<Models.Proffesion> ProffesionList
        {
            get => proffesionList;
            set => SetProperty(ref proffesionList, value);
        }
        public Command ChooseClassCommand { get; }
        private async void OnScholarClassClicked()
        {
            try
            {
                string proffesionName = "Scholar";
                var choice = await App.Current.MainPage.DisplayAlert($"Selected {proffesionName}", "Are You sure? Once selected You cannot change your proffesion.",
                    "I am sure",
                    "Select another proffesion");
                if (choice)
                {
                    using (var realm = await Realm.GetInstanceAsync(Services.RealmDB.Configuration))
                    {
                        var newlyCreatedCharacterId = Services.RealmDB.GetUniqueKey(8);
                        var allIds = realm.All<Models.Character>().Where(t => t.Id == newlyCreatedCharacterId);
                        while (allIds.Any())
                        {
                            newlyCreatedCharacterId = Services.RealmDB.GetUniqueKey(8);
                            allIds = realm.All<Models.Character>().Where(t => t.Id == newlyCreatedCharacterId);
                        }

                        Models.Character newlyCreatedCharacter = new Models.Character()
                        {
                            Id = newlyCreatedCharacterId,
                            UserId = Services.RealmDB.CurrentlyLoggedUserId,
                            _partitionKey = "_partitionKey",
                            CharacterName = null,
                            CharacterSurname = null,
                            Proffesion = proffesionName,
                            Level = 1,
                            ExperiencePoints = 0,
                            HealthPoints = 10,
                            Strength = 10,
                            Dexterity = 10,
                            Constitution = 10,
                            Inteligence = 12,
                            Wisdom = 12,
                            Charisma = 10
                        };

                        realm.Write(() =>
                        {
                            realm.Add<Models.Character>(newlyCreatedCharacter);
                        });

                        await App.Current.MainPage.DisplayAlert($"Selected {proffesionName}", "Ok", "Ok");
                        await Shell.Current.GoToAsync($"SetCharacterDetailsPage");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                await App.Current.MainPage.DisplayAlert("Exception occured", e.InnerException.Message, "Ok");
            }
        }
        private async void OnEngineerClassClicked()
        {
            try
            {
                string proffesionName = "Engineer";
                var choice = await App.Current.MainPage.DisplayAlert($"Selected {proffesionName}", "Are You sure? Once selected You cannot change your proffesion.",
                    "I am sure",
                    "Select another proffesion");
                if (choice)
                {
                    using (var realm = await Realm.GetInstanceAsync(Services.RealmDB.Configuration))
                    {
                        var newlyCreatedCharacterId = Services.RealmDB.GetUniqueKey(8);
                        var allIds = realm.All<Models.Character>().Where(t => t.Id == newlyCreatedCharacterId);
                        while (allIds.Any())
                        {
                            newlyCreatedCharacterId = Services.RealmDB.GetUniqueKey(8);
                            allIds = realm.All<Models.Character>().Where(t => t.Id == newlyCreatedCharacterId);
                        }

                        Models.Character newlyCreatedCharacter = new Models.Character()
                        {
                            Id = newlyCreatedCharacterId,
                            UserId = Services.RealmDB.CurrentlyLoggedUserId,
                            _partitionKey = "_partitionKey",
                            CharacterName = null,
                            CharacterSurname = null,
                            Proffesion = proffesionName,
                            Level = 1,
                            ExperiencePoints = 0,
                            HealthPoints = 10,
                            Strength = 10,
                            Dexterity = 12,
                            Constitution = 10,
                            Inteligence = 12,
                            Wisdom = 10,
                            Charisma = 10
                        };

                        realm.Write(() =>
                        {
                            realm.Add<Models.Character>(newlyCreatedCharacter);
                        });

                        await App.Current.MainPage.DisplayAlert($"Selected {proffesionName}", "Ok", "Ok");
                        await Shell.Current.GoToAsync($"SetCharacterDetailsPage");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                await App.Current.MainPage.DisplayAlert("Exception occured", e.InnerException.Message, "Ok");
            }
        }

        private async void OnMerceneryClassClicked()
        {
            try
            {
                string proffesionName = "Mercenery";
                var choice = await App.Current.MainPage.DisplayAlert($"Selected {proffesionName}", "Are You sure? Once selected You cannot change your proffesion.",
                    "I am sure",
                    "Select another proffesion");
                if (choice)
                {
                    using (var realm = await Realm.GetInstanceAsync(Services.RealmDB.Configuration))
                    {
                        var newlyCreatedCharacterId = Services.RealmDB.GetUniqueKey(8);
                        var allIds = realm.All<Models.Character>().Where(t => t.Id == newlyCreatedCharacterId);
                        while (allIds.Any())
                        {
                            newlyCreatedCharacterId = Services.RealmDB.GetUniqueKey(8);
                            allIds = realm.All<Models.Character>().Where(t => t.Id == newlyCreatedCharacterId);
                        }

                        Models.Character newlyCreatedCharacter = new Models.Character()
                        {
                            Id = newlyCreatedCharacterId,
                            UserId = Services.RealmDB.CurrentlyLoggedUserId,
                            _partitionKey = "_partitionKey",
                            CharacterName = null,
                            CharacterSurname = null,
                            Proffesion = proffesionName,
                            Level = 1,
                            ExperiencePoints = 0,
                            HealthPoints = 10,
                            Strength = 12,
                            Dexterity = 10,
                            Constitution = 12,
                            Inteligence = 10,
                            Wisdom = 10,
                            Charisma = 10
                        };

                        realm.Write(() =>
                        {
                            realm.Add<Models.Character>(newlyCreatedCharacter);
                        });

                        await App.Current.MainPage.DisplayAlert($"Selected {proffesionName}", "Ok", "Ok");
                        await Shell.Current.GoToAsync($"SetCharacterDetailsPage");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                await App.Current.MainPage.DisplayAlert("Exception occured",e.InnerException.Message, "Ok");
            }
        }

        public ClassSelectionViewModel()
        {
            Title = "Class Selection";
            ProffesionList = new List<Models.Proffesion>
            {
                new Models.Proffesion()
                {
                    Name = "Scholar",
                    ImageUrl = "https://i.ibb.co/tmxZXcx/00-portrait.png",
                    ChooseClass = new Command(OnScholarClassClicked)
                },
                new Models.Proffesion()
                {
                    Name = "Engineer",
                    ImageUrl = "https://i.ibb.co/Kq1CrWh/01-portrait.png",
                    ChooseClass = new Command(OnEngineerClassClicked)
                },
                new Models.Proffesion()
                {
                    Name = "Mercenery",
                    ImageUrl = "https://i.ibb.co/64gJvt7/02-portrait.png",
                    ChooseClass = new Command(OnMerceneryClassClicked)
                }
            };
        }
    }
}
