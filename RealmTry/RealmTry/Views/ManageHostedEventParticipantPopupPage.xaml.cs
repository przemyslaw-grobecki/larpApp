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
    public partial class ManageHostedEventParticipantPopupPage : Popup
    {
        private string participantId;
        public string ParticipantId 
        { 
            get=>participantId;
            set=>participantId = value; 
        }
        public ManageHostedEventParticipantPopupPage(string participantId)
        {
            InitializeComponent();
            this.participantId = participantId;
            BindingContext = new ManageHostedEventParticipantPopupViewModel(participantId);
        }
    }
}