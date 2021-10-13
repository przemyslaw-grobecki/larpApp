using Realms;
using Realms.Sync;
using RealmTry.Services;
using RealmTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private bool isCreatingAnEvent = false;
        private List<Position> newEventPositions = new List<Position>();
        public List<Position> NewEventPositions
        {
            get => newEventPositions;
            set => newEventPositions = value;
        }
        private List<ItemEventViewModel> events;
        public List<ItemEventViewModel> Events { get => events; set => events = value; }

        public MainPage()
        {
            InitializeComponent();
            events = new List<ItemEventViewModel>();
            //map.MapElements.Add(polygon);
            GetEvents();
            
            FinalizeEventButton.IsEnabled = false;
            FinalizeEventButton.IsVisible = false;
            NewEventPositions = new List<Position>();
            Position position = GetPosition().Result;
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            map.MoveToRegion(mapSpan);
            BindingContext = new MainPageViewModel();
        }

        private async Task<Position> GetPosition()
        {
            var position = await Geolocation.GetLastKnownLocationAsync();
            return new Position(position.Latitude, position.Longitude);
        }

        private async void map_MapClicked(object sender, MapClickedEventArgs e)
        {
            if (!isCreatingAnEvent)
            {
                MapSpan mapSpan = new MapSpan(e.Position, 0.01, 0.01);
                map.MoveToRegion(mapSpan);
                var decision = await Shell.Current.DisplayActionSheet("Event", "Cancel", null, "Create an Event", "Join an Event");
                if (decision == "Create an Event")
                {
                    isCreatingAnEvent = true;
                    FinalizeEventButton.IsEnabled = true;
                    FinalizeEventButton.IsVisible = true;
                    map.Pins.Add(new Pin() {
                        Label = e.Position.Latitude.ToString() + e.Position.Longitude.ToString(),
                        Position = e.Position });
                    newEventPositions.Add(e.Position);
                }
                else if (decision == "Join an Event") {
                    //logic for joining event
                   
                    foreach (ItemEventViewModel ev in events)
                    {
                        if (IsPointInPolygon(e.Position, ev.GeoMap))
                        {
                            if (ev.MaxParticipantsCount <= ev.Participants.Count)
                            {
                                await Shell.Current.DisplayAlert("Event overpopulated", 
                                    "Sorry, this event is full", "Ok");
                            }
                            else if (ev.IsPrivate)
                            {
                                await Shell.Current.DisplayAlert("Found a private event of title: -" + ev.Title + "-",
                                    "Sorry, you are trying to enroll on private event. You may only enroll on this event by special invitation", "Ok");
                            }
                            else
                            {
                                var choice = await Shell.Current.DisplayAlert("Found event called:" +
                                    ev.Title, "Would you like to enroll on the event of host - " +
                                    ev.CreatorId + " - hosted at: " +
                                    ev.Date + "?" + " (" + ev.Participants.Count().ToString() + "/" + ev.MaxParticipantsCount.ToString() + ")", "Most definitely", "Not really");
                                if (choice)
                                {
                                    EnrollOnAnEvent(ev.Id);
                                }
                            }
                        }

                    }
                }
                else
                {

                }
            }
            else
            {
                map.Pins.Add(new Pin() {
                    Label = e.Position.Latitude.ToString() + e.Position.Longitude.ToString(),
                    Position = e.Position });
                newEventPositions.Add(e.Position);
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            //Create polygon and save
            //Polygon polygon = new Polygon();
            if (NewEventPositions.Count() > 2)
            {
                Random random = new Random();
                List<Position> geopath = NewEventPositions;
                Polygon polygon = new Polygon()
                {
                    StrokeWidth = 8,
                    StrokeColor = Color.FromRgba(random.Next(256), random.Next(256), random.Next(256), 150),
                    FillColor = Color.FromRgba(random.Next(256), random.Next(256), random.Next(256), 50)
                };
                foreach(Position point in geopath)
                {
                    polygon.Geopath.Add(point);
                }
                Shell.Current.ShowPopup(new EventCreationConfirmationPopupPage(polygon));
                //Prepare for new interaction
                newEventPositions.Clear();
                map.Pins.Clear();
                FinalizeEventButton.IsEnabled = false;
                FinalizeEventButton.IsVisible = false;
                isCreatingAnEvent = false;
            }
        }

        private async void GetEvents()
        {
            Events.Clear();
            using (var realm = await Realm.GetInstanceAsync(Services.RealmDB.Configuration))
            {
                var allEvents = realm.All<Models.Event>();
                foreach (Models.Event ev in allEvents)
                {
                    //logic to not show too many events can be implemented here

                    ItemEventViewModel nextEvent = new ItemEventViewModel
                    {
                        Id = ev.Id,
                        _partitionKey = ev._partitionKey,
                        Title = ev.Title,
                        CreatorId = ev.CreatorId,
                        Date = ev.Date,
                        IsPrivate = ev.IsPrivate,
                        MaxParticipantsCount = ev.MaxParticipantsCount,
                        EdgeColorRed = ev.EdgeColorRed,
                        EdgeColorGreen = ev.EdgeColorGreen,
                        EdgeColorBlue = ev.EdgeColorBlue,
                        FillColorRed = ev.FillColorRed,
                        FillColorGreen = ev.FillColorGreen,
                        FillColorBlue = ev.FillColorBlue,
                        GeoMap = new List<Position>(),
                        Participants = new List<string>()
                    };
                    foreach (Models.GeoPoint point in ev.GeoMap)
                    {
                        nextEvent.GeoMap.Add(new Position(point.Latitude, point.Longtitude));
                    }
                    foreach (string participant in ev.Participants)
                    {
                        nextEvent.Participants.Add(participant);
                    }
                    events.Add(nextEvent);
                }
            }
            FillMapWithEvents();
        }
        private void FillMapWithEvents()
        {
            map.MapElements.Clear();
            foreach (ItemEventViewModel ev in events)
            {
                Polygon polygon = new Polygon
                {
                    FillColor = new Color(ev.FillColorRed, ev.FillColorGreen, ev.FillColorBlue, 0.25),
                    StrokeColor = new Color(ev.EdgeColorRed, ev.EdgeColorGreen, ev.EdgeColorBlue, 0.50)
                };
                foreach (Position position in ev.GeoMap)
                {
                    polygon.Geopath.Add(position);
                };
                map.MapElements.Add(polygon);
            }
        }
        private bool IsPointInPolygon(Position p, List<Position> polygon)
        {
            double minX = polygon[0].Latitude;
            double maxX = polygon[0].Latitude;
            double minY = polygon[0].Longitude;
            double maxY = polygon[0].Longitude;
            for (int i = 1; i < polygon.Count; i++)
            {
                Position q = polygon[i];
                minX = Math.Min(q.Latitude, minX);
                maxX = Math.Max(q.Latitude, maxX);
                minY = Math.Min(q.Longitude, minY);
                maxY = Math.Max(q.Longitude, maxY);
            }
            if (p.Latitude < minX || p.Latitude > maxX || p.Longitude < minY || p.Longitude > maxY)
            {
                return false;
            }
            bool inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Longitude > p.Longitude) != (polygon[j].Longitude > p.Longitude) &&
                     p.Latitude < (polygon[j].Latitude - polygon[i].Latitude) * (p.Longitude - polygon[i].Longitude) / (polygon[j].Longitude - polygon[i].Longitude) + polygon[i].Latitude)
                {
                    inside = !inside;
                }
            }
            return inside;
        }

        private async void EnrollOnAnEvent(string eventId)
        {
            using (var realm = await Realm.GetInstanceAsync(Services.RealmDB.Configuration))
            {
                var ev = realm.All<Models.Event>().Where(e => e.Id == eventId);
                if (ev.Any())
                {
                    Models.Event eventIParticipateIn = ev.FirstOrDefault();
                    if (eventIParticipateIn.CreatorId == RealmDB.CurrentlyLoggedUserId)
                    {
                        await Shell.Current.DisplayAlert("Something went wrong...",
                            "Sorry, You are creator of this event, hence You cannot participate in it.",
                            "Ok");
                    }
                    else if (eventIParticipateIn.Participants.Contains(RealmDB.CurrentlyLoggedUserId))
                    {
                        await Shell.Current.DisplayAlert("Something went wrong...", 
                            "Sorry, you are aldready enrolled for this event.", 
                            "Ok");
                    }
                    else
                    {
                        realm.Write(() =>
                        {
                            eventIParticipateIn.Participants.Add(RealmDB.CurrentlyLoggedUserId);
                        });
                        await Shell.Current.DisplayAlert("Enrollment", 
                            "Congratulations! You have successfully enrolled on an event!", 
                            "Great!");
                    }
                }
            }
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            GetEvents();
        }
        /* does not work :/
private void AddMapStyle()
{
   var assembly = typeof(MainPage).GetTypeInfo().Assembly;
   var stream = assembly.GetManifestResourceStream($"RealmTry.MapResources.json");
   string styleFile;
   using (var reader = new System.IO.StreamReader(stream))
   {
       styleFile = reader.ReadToEnd();
   }
   //map.MapStyle = MapStyle.FromJson(styleFile);
}
*/

    }
}