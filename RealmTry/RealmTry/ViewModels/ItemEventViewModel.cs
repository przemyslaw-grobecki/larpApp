using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace RealmTry.ViewModels
{
    public class ItemEventViewModel
    {
        public string Id { get; set; }
        public string _partitionKey { get; set; }
        public string Title { get; set; }
        public string CreatorId { get; set; }
        public string Date { get; set; }
        public int MaxParticipantsCount { get; set; }
        public bool IsPrivate { get; set; }
        public List<string> Participants { get; set; }
        public List<Position> GeoMap { get; set; }
        public double EdgeColorRed { get; set; }
        public double EdgeColorGreen { get; set; }
        public double EdgeColorBlue { get; set; }
        public double FillColorRed { get; set; }
        public double FillColorGreen { get; set; }
        public double FillColorBlue { get; set; }
    }
}
