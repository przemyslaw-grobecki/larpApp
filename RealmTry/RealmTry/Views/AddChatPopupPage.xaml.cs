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
    public partial class AddChatPopupPage : Popup
    {
        public AddChatPopupPage()
        {
            InitializeComponent();
            BindingContext = new AddChatPopupViewModel(this);
        }
    }
}