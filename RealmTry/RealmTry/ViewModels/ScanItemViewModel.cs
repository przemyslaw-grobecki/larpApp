using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using ZXing;

namespace RealmTry.ViewModels
{
    public class ScanItemViewModel : BaseViewModel
    {
        private Result result;
        public Result Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }
        private string textResult;
        public string TextResult
        {
            get => textResult;
            set => SetProperty(ref textResult, value);
        }
        public Command ScanCommand { get; set; }
        public void OnScanResult()
        {
            TextResult = Result.Text;
        }

        public ScanItemViewModel()
        {
            ScanCommand = new Command(OnScanResult);
        }
        
    }
}