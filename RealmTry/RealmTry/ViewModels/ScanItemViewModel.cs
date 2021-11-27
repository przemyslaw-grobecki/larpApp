using Realms;
using RealmTry.Services;
using RealmTry.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using ZXing;

namespace RealmTry.ViewModels
{
    //OnScanResult -> Another FSM to distinguish Clues with or without requirements
    public class ScanItemViewModel : BaseViewModel
    {
        private bool isScanning = false;
        private Result result;
        public Result Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }
        private string textResult;
        public string TextResult
        {
            get => textResult;
            set => SetProperty(ref textResult, value);
        }
        public Command ScanCommand { get; set; }
        public async void OnScanResult()
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (!isScanning)
                    {
                        isScanning = true;
                        if (Result.Text.Contains("CLUE"))
                        {
                            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
                            {
                                Models.Clue clue = realm.All<Models.Clue>().Where(t => t.Id == Result.Text).FirstOrDefault();
                                if (clue.NeedsDiceRoll)
                                {
                                    Shell.Current.ShowPopup(new ClueScanWithDiceRollPopupPage(new ItemClueViewModel 
                                    { 
                                        AlternativeClueContent = clue.AlternativeClueContent,
                                        AttributeTested = clue.AttributeTested,
                                        ClueContent = clue.ClueContent,
                                        ClueImageUrl = clue.ClueImageUrl,
                                        Description = clue.Description,
                                        Id = clue.Id,
                                        ClueRequirement = clue.ClueRequirement,
                                        NeedsDiceRoll = clue.NeedsDiceRoll,
                                        TestDifficulty = clue.TestDifficulty,
                                        EventId = clue.EventId,
                                        NeedsRequirement = clue.NeedsRequirement
                                    }, isScanning));
                                }
                                else if (clue.NeedsRequirement)
                                {
                                    Shell.Current.ShowPopup(new ClueScanWithRequirementPopupPage(new ItemClueViewModel
                                    {
                                        AlternativeClueContent = clue.AlternativeClueContent,
                                        AttributeTested = clue.AttributeTested,
                                        ClueContent = clue.ClueContent,
                                        ClueImageUrl = clue.ClueImageUrl,
                                        Description = clue.Description,
                                        Id = clue.Id,
                                        ClueRequirement = clue.ClueRequirement,
                                        NeedsDiceRoll = clue.NeedsDiceRoll,
                                        TestDifficulty = clue.TestDifficulty,
                                        EventId = clue.EventId,
                                        NeedsRequirement = clue.NeedsRequirement
                                    }, isScanning));
                                }
                                else
                                {
                                    Shell.Current.ShowPopup(new ClueScanPopupPage(new ItemClueViewModel
                                    {
                                        AlternativeClueContent = clue.AlternativeClueContent,
                                        AttributeTested = clue.AttributeTested,
                                        ClueContent = clue.ClueContent,
                                        ClueImageUrl = clue.ClueImageUrl,
                                        Description = clue.Description,
                                        Id = clue.Id,
                                        ClueRequirement = clue.ClueRequirement,
                                        NeedsDiceRoll = clue.NeedsDiceRoll,
                                        TestDifficulty = clue.TestDifficulty,
                                        EventId = clue.EventId,
                                        NeedsRequirement = clue.NeedsRequirement
                                    }, isScanning));
                                }
                            }
                        }
                        else if (Result.Text.Contains("EVENT "))
                        {
                            string evId = Result.Text.Split(' ')[1];
                            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
                            {
                                var ev = realm.All<Models.Event>().Where(t => t.Id == evId).FirstOrDefault();
                                if(ev.Participants.Contains(RealmDB.CurrentlyLoggedUserId))
                                {
                                    await Shell.Current.DisplayAlert("Failure", "You are aldready enrolled on this event.", "Ok");
                                }
                                else if(ev.MaxParticipantsCount == ev.Participants.Count)
                                {
                                    await Shell.Current.DisplayAlert("Failure", "Max participants number reached.", "Ok");
                                }
                                else
                                {
                                    realm.Write(() => {
                                        ev.Participants.Add(RealmDB.CurrentlyLoggedUserId);
                                    });
                                    await Shell.Current.DisplayAlert("Success", "Congratulations! You have successfully enrolled on private event!", "Ok");
                                }
                            }
                            isScanning = false;
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Not a clue.", "Sorry, the code You are trying to decipher is not a valid Clue.", "Ok");
                            isScanning = false;
                        }
                    }
                });
            }catch(Exception ex)
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!\n" + ex.Message + ex.InnerException.Message + "\n!!!!!!!!!!!!!!!!!!!");
            }
        }
        public ScanItemViewModel()
        {
            ScanCommand = new Command(OnScanResult);
        }
        
    }
}