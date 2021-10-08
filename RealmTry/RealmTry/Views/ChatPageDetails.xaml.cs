using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmTry.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ChatId), "chatId")]
    public partial class ChatPageDetails : ContentPage
    {
        public string ChatId
        {
            set => BindingContext = new ChatDetailsViewModel(value, messagesList);
        }

        public ChatPageDetails()
        {
            InitializeComponent();
        }
    }
}