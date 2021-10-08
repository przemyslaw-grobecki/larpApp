using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    class ItemFriendRequestViewModel : BaseViewModel
    {
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string status;
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
    }
}
