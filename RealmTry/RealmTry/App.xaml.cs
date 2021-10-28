using RealmTry.Services;
using RealmTry.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Realms;
using Realms.Sync;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace RealmTry
{
    public partial class App : Application
    {
        Stopwatch sw = new Stopwatch();
        PromptingService promptingService;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            promptingService = new PromptingService();
        }

        protected async override void OnStart()
        {        
            sw.Start();
            try
            {
                var app = RealmDB.App;
                RealmDB.Login();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            promptingService.ServiceStart();
        }

        protected override void OnSleep()
        {
            promptingService.ServiceStop();
            RealmDB.CurrentlyLoggedUserId = null;
        }

        protected async override void OnResume()
        {
            promptingService.ServiceStart();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
