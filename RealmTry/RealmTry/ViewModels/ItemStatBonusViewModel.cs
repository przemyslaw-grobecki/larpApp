using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    class ItemStatBonusViewModel : BaseViewModel
    {
        private string stat;
        public string Stat { get => stat; set => SetProperty(ref stat, value); }
        private string increase;
        public string Increase { get => increase; set => SetProperty(ref increase, value); }
    }
}
