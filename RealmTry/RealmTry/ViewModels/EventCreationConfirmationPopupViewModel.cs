using Realms;
using Realms.Sync;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace RealmTry.ViewModels
{
    class EventCreationConfirmationPopupViewModel : BaseViewModel
    {
        private string eventTitle;
        public string EventTitle { get => eventTitle; set => SetProperty(ref eventTitle, value); }
        private Polygon polygon;
        public Polygon Polygon { get; set; }
        private bool isPrivateChecked;
        public bool IsPrivateChecked
        {
            get => isPrivateChecked;
            set
            {
                SetProperty(ref isPrivateChecked, value);
                SetProperty(ref isPublicChecked, !value);
            }
        }
        private bool isPublicChecked;
        public bool IsPublicChecked
        {
            get => isPublicChecked;
            set
            {
                SetProperty(ref isPublicChecked, value);
                SetProperty(ref isPrivateChecked, !value);
            }
        }
        private string minDate;
        public string MinDate
        {
            get => minDate;
            set => SetProperty(ref minDate, value);
        }
        private string maxDate;
        public string MaxDate
        {
            get => maxDate;
            set => SetProperty(ref maxDate, value);
        }
        private string date;
        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        private string selectedTime;
        public string SelectedTime
        {
            get => selectedTime;
            set => SetProperty(ref selectedTime, value);
        }
        private string participantsNumber = "0";
        public string ParticipantsNumber
        {
            get => participantsNumber;
            set => SetProperty(ref participantsNumber, value);
        }
        public Command AddCommand { get; set; }
        public Command SubtractCommand { get; set; }
        public void OnAddButtonClicked()
        {
            if (int.Parse(ParticipantsNumber) < 26)
            {
                ParticipantsNumber = (int.Parse(ParticipantsNumber) + 1).ToString();
            }
        }
        public void OnSubtractButtonClicked()
        {
            if (int.Parse(ParticipantsNumber) > 0)
            {
                ParticipantsNumber = (int.Parse(ParticipantsNumber) - 1).ToString();
            }
        }

        public Command CreateEventCommand { get; set; }
        public async void OnCreateEventCommandButtonClicked()
        {
            if (!(polygon == null)) {
                using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
                {
                    var newlyCreatedEventId = RealmDB.GetUniqueKey(8);
                    var allIds = realm.All<Models.Event>().Where(t => t.Id == newlyCreatedEventId);
                    while (allIds.Any())
                    {
                        newlyCreatedEventId = RealmDB.GetUniqueKey(8);
                        allIds = realm.All<Models.Event>().Where(t => t.Id == newlyCreatedEventId);
                    }
                    Models.Event newlyCreatedEvent = new Models.Event
                    {
                        Id = newlyCreatedEventId,
                        _partitionKey = "_partitionKey",
                        Title = eventTitle,
                        CreatorId = RealmDB.CurrentlyLoggedUserId,
                        Date = date + " " + selectedTime,
                        IsPrivate = isPrivateChecked,
                        MaxParticipantsCount = int.Parse(participantsNumber),
                        EdgeColorRed = polygon.StrokeColor.R,
                        EdgeColorGreen = polygon.StrokeColor.G,
                        EdgeColorBlue = polygon.StrokeColor.B,
                        FillColorRed = polygon.FillColor.R,
                        FillColorGreen = polygon.FillColor.G,
                        FillColorBlue = polygon.FillColor.B
                    };
                    foreach (Position position in polygon.Geopath)
                    {
                        newlyCreatedEvent.GeoMap.Add(new Models.GeoPoint
                        {
                            Latitude = position.Latitude,
                            Longtitude = position.Longitude
                        });
                    }
                    try
                    {
                        realm.Write(() =>
                        {
                            realm.Add(newlyCreatedEvent);
                        });
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public EventCreationConfirmationPopupViewModel(Polygon polygon)
        {
            this.polygon = polygon;
            CreateEventCommand = new Command(OnCreateEventCommandButtonClicked);
            AddCommand = new Command(OnAddButtonClicked);
            SubtractCommand = new Command(OnSubtractButtonClicked);
        }
    }
}

