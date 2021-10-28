using Realms;
using Realms.Sync;
using RealmTry.Services;
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
    public partial class AskForAbilityCheckPopupPage : Popup
    {
        private ViewModels.ItemPromptViewModel prompt;
        public AskForAbilityCheckPopupPage(ViewModels.ItemPromptViewModel prompt)
        {
            InitializeComponent();
            this.prompt = prompt;
        }
        private async void ChangePromptStatus(ViewModels.ItemPromptViewModel prompt, string status)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var processingPrompt = realm.All<Models.Prompt>().Where(p => p.Id == prompt.Id).FirstOrDefault();
                realm.Write(() =>
                {
                    processingPrompt.Status = status;
                });
            }
        }
        private void Popup_Dismissed(object sender, PopupDismissedEventArgs e)
        {
            ChangePromptStatus(prompt, "Processed");
        }
    }
}