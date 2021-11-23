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
    public partial class ClueScanPopupPage : Popup
    {
        public ItemClueViewModel clue;
        private bool isScanning;
        public ClueScanPopupPage(ItemClueViewModel clue, bool isScanning)
        {
            this.isScanning = isScanning;
            this.clue = clue;
            InitializeComponent();
            labelForExclamation.FadeTo(1, 1500);
            labelForDescription.Text = clue.Description;
            labelForDescription.FadeTo(1, 1500);
            clueImage.Source = clue.ClueImageUrl;
            clueImage.FadeTo(1, 1500);
            ContinueFirstButton.FadeTo(1, 1500);
        }

        private async void ContinueFirstButton_Clicked(object sender, EventArgs e)
        {
            contentLabel.Text = clue.ClueContent;
            await contentFrame.FadeTo(1, 1500);
        }

        private void ContinueSecondButton_Clicked(object sender, EventArgs e)
        {
            isScanning = false;
            Dismiss(null);
        }
    }
}