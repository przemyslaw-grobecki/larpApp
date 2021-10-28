using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmTry.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(EventId), "eventId")]
    public partial class ManageHostedEventDetailsPage : ContentPage
    {
        public string EventId
        {
            set => BindingContext = new ManageHostedEventDetailsViewModel(value);
        }
        public ManageHostedEventDetailsPage()
        {
            InitializeComponent();
        }

        private void messagesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Shell.Current.ShowPopup(new ManageHostedEventParticipantPopupPage((e.Item as ItemHostedEventParticipantViewModel).ParticipantId));
        }
    }
}