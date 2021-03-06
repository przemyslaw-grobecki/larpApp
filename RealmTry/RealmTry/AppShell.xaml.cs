using RealmTry.ViewModels;
using RealmTry.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RealmTry
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ClassSelectionPage), typeof(ClassSelectionPage));
            Routing.RegisterRoute(nameof(SetCharacterDetailsPage), typeof(SetCharacterDetailsPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(FriendsPage), typeof(FriendsPage));
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
            Routing.RegisterRoute(nameof(ManageEventsPage), typeof(ManageEventsPage));
            Routing.RegisterRoute(nameof(InventoryPage), typeof(InventoryPage));
            Routing.RegisterRoute(nameof(CharacterInformationPage), typeof(CharacterInformationPage));
            Routing.RegisterRoute(nameof(ScanItemPage), typeof(ScanItemPage));
            Routing.RegisterRoute(nameof(ChatPageDetails), typeof(ChatPageDetails));
            Routing.RegisterRoute(nameof(ManageHostedEventDetailsPage), typeof(ManageHostedEventDetailsPage));
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Shell.Current.DisplayAlert("Info", "This is a LARPApp. Welcome aboard!", "Ok");
        }

        private void Help_Clicked(object sender, EventArgs e)
        {
            Shell.Current.DisplayAlert("Help", "This application is meant for Role Players to organize and participate in their own events.", "Ok");
        }
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
