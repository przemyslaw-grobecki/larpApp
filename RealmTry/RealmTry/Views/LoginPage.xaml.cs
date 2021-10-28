using RealmTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : TabbedPage
    {
        private bool isLoginAnimationRunning = true;
        private Task animateBackgroundLogin = null;
        private bool isRegisterAnimationRunning = true;
        private Task animateBackgroundRegister = null;
        public LoginPage()
        {
            InitializeComponent();
            animateBackgroundLogin = Task.Run(AnimateBackgroundLogin);
            animateBackgroundRegister = Task.Run(AnimateBackgroundRegister);
            BindingContext = new LoginViewModel();
        }
        private async void AnimateBackgroundLogin()
        {
            Action<double> forward = input => loginGradient.AnchorY = input;
            Action<double> backward = input => loginGradient.AnchorY = input;
            while (isLoginAnimationRunning)
            {
                loginGradient.Animate(name: "forward", callback: forward, start: 0, end: 1, length: 10000, easing: Easing.CubicInOut);
                await Task.Delay(10500);
                loginGradient.Animate(name: "backward", callback: backward, start: 1, end: 0, length: 10000, easing: Easing.CubicInOut);
                await Task.Delay(10500);
            }
        }
        private async void AnimateBackgroundRegister()
        {
            Action<double> forward = input => registerGradient.AnchorY = input;
            Action<double> backward = input => registerGradient.AnchorY = input;
            while (isRegisterAnimationRunning)
            {
                registerGradient.Animate(name: "forward", callback: forward, start: 0, end: 1, length: 10000, easing: Easing.CubicInOut);
                await Task.Delay(10500);
                registerGradient.Animate(name: "backward", callback: backward, start: 1, end: 0, length: 10000, easing: Easing.CubicInOut);
                await Task.Delay(10500);
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await animateBackgroundLogin;
            await animateBackgroundRegister;
            isLoginAnimationRunning = true;
            isRegisterAnimationRunning = true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            isLoginAnimationRunning = false;
            isRegisterAnimationRunning = false;
        }
    }
}