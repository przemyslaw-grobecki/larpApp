using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeaderTemplate : Grid
    {
        public FlyoutHeaderTemplate()
        {
            InitializeComponent();
        }
    }
}