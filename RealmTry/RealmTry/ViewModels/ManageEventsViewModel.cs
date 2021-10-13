using Realms;
using Realms.Sync;
using RealmTry.Models;
using RealmTry.Services;
using RealmTry.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace RealmTry.ViewModels
{
    class ManageEventsViewModel : BaseViewModel
    {
        /// <summary>
        /// HOSTED EVENTS SUB-PAGE
        /// </summary>
        public ObservableCollection<ItemEventViewModel> HostedEvents { get; set; }
        private bool isRefreshingHostedEvents = false;
        public bool IsRefreshingHostedEvents
        {
            get => isRefreshingHostedEvents;
            set => SetProperty(ref isRefreshingHostedEvents, value);
        }
        public Command RefreshHostedEventsCommand { get; set; }
        public async void RefreshHostedEvents()
        {
            IsRefreshingHostedEvents = true;
            HostedEvents.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var hostedEvents = realm.All<Models.Event>().Where(e=>e.CreatorId == RealmDB.CurrentlyLoggedUserId);
                foreach(Models.Event ev in hostedEvents)
                {
                    ItemEventViewModel nextEvent = new ItemEventViewModel
                    {
                        Id = ev.Id,
                        _partitionKey = ev._partitionKey,
                        CreatorId = ev.CreatorId,
                        Date = ev.Date,
                        Title = ev.Title,
                        IsPrivate = ev.IsPrivate,
                        FillColorRed = ev.FillColorRed,
                        FillColorGreen = ev.FillColorGreen,
                        FillColorBlue = ev.FillColorBlue,
                        EdgeColorRed = ev.EdgeColorRed,
                        EdgeColorGreen = ev.EdgeColorGreen,
                        EdgeColorBlue = ev.EdgeColorBlue,
                        MaxParticipantsCount = ev.MaxParticipantsCount,
                        GeoMap = new List<Position>(),
                        Participants = new List<string>(),
                    };
                    foreach(GeoPoint point in ev.GeoMap)
                    {
                        nextEvent.GeoMap.Add(new Position(point.Latitude, point.Longtitude));
                    }
                    foreach(string user in ev.Participants)
                    {
                        nextEvent.Participants.Add(user);
                    }

                    HostedEvents.Add(nextEvent);
                }
            }
            IsRefreshingHostedEvents = false;
        }
        public Command ManageHostedEventButtonCommand { get; set; }
        public async void OnManageHostedEventButtonClicked(object ev)
        {
            await Shell.Current.GoToAsync($"{nameof(ManageHostedEventDetailsPage)}?eventId={(ev as ItemEventViewModel).Id}");
        }
        public Command AbondomHostedEventButtonCommand { get; set; }
        public async void OnAbondomHostedEventButtonClicked(object ev)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var eventToDelete = realm.All<Models.Event>().Where(e=>e.Id == (ev as ItemEventViewModel).Id).FirstOrDefault();
                if (eventToDelete != null)
                {
                    realm.Write(() =>
                    {
                        realm.Remove(eventToDelete);
                    });
                }
            }
            RefreshHostedEvents();
        }
        /// <summary>
        /// EVENTS ENROLLED ON SUB-PAGE
        /// </summary>
        public ObservableCollection<ItemEventViewModel> EventsEnrolledOn { get; set; }
        private bool isRefreshingEventsEnrolledOn = false;
        public bool IsRefreshingEventsEnrolledOn
        {
            get => isRefreshingEventsEnrolledOn;
            set => SetProperty(ref isRefreshingEventsEnrolledOn, value);
        }
        public Command RefreshEventsEnrolledOnCommand { get; set; }
        public async void RefreshEventsEnrolledOn()
        {
            IsRefreshingEventsEnrolledOn = true;
            HostedEvents.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var allEvents = realm.All<Models.Event>();
                var eventsEnrolledOn = new List<Models.Event>();
                foreach(Models.Event ev in allEvents)
                {
                    if (ev.Participants.Contains(RealmDB.CurrentlyLoggedUserId)) {
                        eventsEnrolledOn.Add(ev);
                    }
                }
                foreach (Models.Event ev in eventsEnrolledOn)
                {
                    ItemEventViewModel nextEvent = new ItemEventViewModel
                    {
                        Id = ev.Id,
                        _partitionKey = ev._partitionKey,
                        CreatorId = ev.CreatorId,
                        Date = ev.Date,
                        Title = ev.Title,
                        IsPrivate = ev.IsPrivate,
                        FillColorRed = ev.FillColorRed,
                        FillColorGreen = ev.FillColorGreen,
                        FillColorBlue = ev.FillColorBlue,
                        EdgeColorRed = ev.EdgeColorRed,
                        EdgeColorGreen = ev.EdgeColorGreen,
                        EdgeColorBlue = ev.EdgeColorBlue,
                        MaxParticipantsCount = ev.MaxParticipantsCount,
                        GeoMap = new List<Position>(),
                        Participants = new List<string>()
                    };
                    foreach (GeoPoint point in ev.GeoMap)
                    {
                        nextEvent.GeoMap.Add(new Position(point.Latitude, point.Longtitude));
                    }
                    foreach (string user in ev.Participants)
                    {
                        nextEvent.Participants.Add(user);
                    }

                    EventsEnrolledOn.Add(nextEvent);
                }
            }
            IsRefreshingEventsEnrolledOn = false;
        }
        public Command ManageEventEnrolledOnButtonCommand { get; set; }
        public async void OnManageEventEnrolledOnButtonClicked(object ev)
        {
            ItemEventViewModel selectedEvent = ev as ItemEventViewModel;
            await Shell.Current.DisplayAlert("Information about event", 
                "This is the event hosted by -" + selectedEvent.CreatorId + "-. " +
                "It is planned to take place at: " + selectedEvent.Date +
                ". There are " + selectedEvent.Participants.Count + "/" +
                selectedEvent.MaxParticipantsCount + " places taken aldready.", 
                "OK");
        }
        public Command LeaveEventEnrolledOnButtonCommand { get; set; }
        public async void OnLeaveEventEnrolledOnButtonClicked(object ev)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var eventToLeave = realm.All<Models.Event>().Where(e => e.Id == (ev as ItemEventViewModel).Id).FirstOrDefault();
                eventToLeave.Participants.Remove(RealmDB.CurrentlyLoggedUserId);
                if (eventToLeave != null)
                {
                    realm.Write(() =>
                    {
                        realm.Add(eventToLeave);
                    });
                }
            }
            RefreshEventsEnrolledOn();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ManageEventsViewModel()
        {
            //Hosted events

            HostedEvents = new ObservableCollection<ItemEventViewModel>();
            RefreshHostedEventsCommand = new Command(RefreshHostedEvents);
            ManageHostedEventButtonCommand = new Command(OnManageHostedEventButtonClicked);
            AbondomHostedEventButtonCommand = new Command(OnAbondomHostedEventButtonClicked);

            //Events enrolled on

            EventsEnrolledOn = new ObservableCollection<ItemEventViewModel>();
            RefreshEventsEnrolledOnCommand = new Command(RefreshEventsEnrolledOn);
            ManageEventEnrolledOnButtonCommand = new Command(OnManageEventEnrolledOnButtonClicked);
            LeaveEventEnrolledOnButtonCommand = new Command(OnLeaveEventEnrolledOnButtonClicked);

            //Get the wheels spinning...
            RefreshHostedEvents();
            RefreshEventsEnrolledOn();
        }
    }
}
