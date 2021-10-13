using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RealmTry.ViewModels;

namespace RealmTry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFriendsPopupPage : Popup
    {
        public AddFriendsPopupPage()
        {
            InitializeComponent();
            BindingContext = new AddFriendsPopupViewModel(this);
        }
    }
}