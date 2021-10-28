using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    public class ItemPromptViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public string _partitionKey { get; set; }
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Information { get; set; }
        public string TimeStamp { get; set; }
    }
}
