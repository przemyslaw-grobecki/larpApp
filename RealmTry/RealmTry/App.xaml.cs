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
        private CancellationTokenSource testTaskCancellationSource = new CancellationTokenSource();
        private CancellationToken testTaskCancellationToken;
        Stopwatch sw = new Stopwatch();
        Task t;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
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
            t = Task.Run(testTask, testTaskCancellationSource.Token);
        }

        private async void testTask()
        {
            try
            {
                testTaskCancellationToken = testTaskCancellationSource.Token;
                while (!testTaskCancellationToken.IsCancellationRequested)
                {
                    sw.Stop();
                    Console.WriteLine($"Task ended delay... {sw.Elapsed} s");
                    sw.Start();
                    await Task.Delay(5000);
                }
                testTaskCancellationToken.ThrowIfCancellationRequested();
            }
            catch(OperationCanceledException e)
            {
                Console.WriteLine($"{e.Message} + *******************************************");
            }
        }

        protected override void OnSleep()
        {
            testTaskCancellationSource.Cancel();
        }

        protected async override void OnResume()
        {
            testTaskCancellationSource = new CancellationTokenSource();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            t = Task.Run(testTask, testTaskCancellationSource.Token);
        }
    }
}
