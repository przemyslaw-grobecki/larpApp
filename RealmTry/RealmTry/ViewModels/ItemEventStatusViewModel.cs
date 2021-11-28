using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    public class ItemEventStatusViewModel : BaseViewModel
    {
        private string id;
        public string Id { get => id; set => SetProperty(ref id, value); }
        private string eventId;
        public string EventId { get =>eventId; set => SetProperty(ref eventId, value); }
        private string information;
        public string Information { get => information; set => SetProperty(ref information, value); }
    }
}
