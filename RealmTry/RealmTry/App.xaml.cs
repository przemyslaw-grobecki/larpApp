using RealmTry.Services;
using RealmTry.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Realms;
using Realms.Sync;
using System.Threading.Tasks;

namespace RealmTry
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
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

            Task t = Task.Run(() => {
                while (true)
                {
                    Task.Delay(5000).Wait();
                    Console.WriteLine("Task ended delay...");
                }
            });
        }

        protected override void OnSleep()
        {

        }

        protected async override void OnResume()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            Task t = Task.Run(() => {
                while (true)
                {
                    Task.Delay(5000).Wait();
                    Console.WriteLine("Task ended delay...");
                }
            });
        }
    }
}
