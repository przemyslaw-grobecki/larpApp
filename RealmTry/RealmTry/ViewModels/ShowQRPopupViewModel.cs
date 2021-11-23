using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    class ShowQRPopupViewModel : BaseViewModel
    {
        private string clueId;
        public string ClueId
        {
            get => clueId;
            set => SetProperty(ref clueId, value);
        }
        public ShowQRPopupViewModel(string clueId)
        {
            this.ClueId = clueId;
        }
    }
}
