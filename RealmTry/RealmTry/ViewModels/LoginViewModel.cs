using RealmTry.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Realms;
using Realms.Sync;
using RealmTry.Services;
using System.Linq;

namespace RealmTry.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        /// <summary>
        /// Loggin in
        /// </summary>
        public Command LoginCommand { get; }

        private string loginUsername;
        public string LoginUsername
        {
            get => loginUsername;
            set => SetProperty(ref loginUsername, value);
        }

        private string loginPassword;
        public string LoginPassword
        {
            get => loginPassword;
            set => SetProperty(ref loginPassword, value);
        }

        /// <summary>
        /// Registration
        /// </summary>
        public Command RegisterCommand { get; }

        private string registeredUsername;
        public string RegisteredUsername
        {
            get => registeredUsername;
            set => SetProperty(ref registeredUsername, value);
        }
        private string registeredPassword;

        public string RegisteredPassword
        {
            get => registeredPassword;
            set => SetProperty(ref registeredPassword, value);
        }
        private string registeredEmail;
        public string RegisteredEmail
        {
            get => registeredEmail;
            set => SetProperty(ref registeredEmail, value);
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
                {
                    var appUser = realm.All<Models.User>().Where(t => t.Username == loginUsername);
                    RealmDB.CurrentlyLoggedUserId = appUser.First<Models.User>().Id;
                    if (appUser.Any())
                    {
                        if (Services.SecurePasswordHasher.Verify(loginPassword, appUser.First<Models.User>().Password))
                        {                        
                            await Shell.Current.GoToAsync($"/{nameof(MainPage)}");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert(
                                "Wrong Password", 
                                "sorry :(", 
                                "Ok :CCCC");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(
                            "No user found!", 
                            "sorry :(", 
                            "Ok :CCCC");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private async void OnRegisterClicked()
        {
            //App.Current.MainPage.DisplayAlert(registeredUsername, registeredPassword, registeredEmail);
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                //Validate entered username
                if(registeredUsername.Length < 6)
                {
                    await App.Current.MainPage.DisplayAlert("Invalid username", 
                        "Your username is either too short or too long!", "Ok :(");
                    return;
                }
                var username = realm.All<Models.User>().Where(t => t.Username == registeredUsername);
                if(username.Any())
                {
                    await App.Current.MainPage.DisplayAlert("Invalid username", 
                        "I am sorry. Somebody took your username! Try something else.", "Ok :(");
                    return;
                }
                //Validate entered password
                if(registeredPassword.Length < 6)
                {
                    await App.Current.MainPage.DisplayAlert("Invalid password",
                       "Your password is either too short or too long!", "Ok :(");
                    return;
                }
                //Validate entered email
                var emailAddress = realm.All<Models.User>().Where(t => t.Email == registeredEmail);
                if (emailAddress.Any())
                {
                    await App.Current.MainPage.DisplayAlert("Invalid email",
                        "I am sorry. Somebody took your email! Try something else.", "Ok :(");
                    return;
                }

                //Register user to database
                var newlyCreatedUserId = RealmDB.GetUniqueKey(8);
                var allIds = realm.All<Models.User>().Where(t => t.Id == newlyCreatedUserId);
                while (allIds.Any())
                {
                    newlyCreatedUserId = RealmDB.GetUniqueKey(8);
                    allIds = realm.All<Models.User>().Where(t => t.Id == newlyCreatedUserId);
                }          
                RealmDB.CurrentlyLoggedUserId = newlyCreatedUserId;
                Models.User newlyCreatedAppUser = new Models.User(){
                    Id = newlyCreatedUserId,
                    _partitionKey = "_partitionKey",
                    Username = registeredUsername,
                    Password = Services.SecurePasswordHasher.Hash(registeredPassword),
                    Email = registeredEmail
                };

                realm.Write(() =>
                {
                    realm.Add<Models.User>(newlyCreatedAppUser);
                });

                RealmDB.CurrentlyLoggedUserId = newlyCreatedUserId;

                await Shell.Current.GoToAsync("ClassSelectionPage");
            }
        }
    }
}
