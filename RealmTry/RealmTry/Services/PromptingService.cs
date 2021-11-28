using System;
using Realms;
using Realms.Sync;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;

namespace RealmTry.Services
{
    class PromptingService
    {
        private int delay = 2000;
        private bool isServiceWorking = false;
        private List<ViewModels.ItemPromptViewModel> prompts = new List<ViewModels.ItemPromptViewModel>();
        private Task promptingManagement;
        private async Task UpdatePrompts()
        {
            prompts.Clear();
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var allPrompts = realm.All<Models.Prompt>().Where(t => t.Receiver == RealmDB.CurrentlyLoggedUserId && (t.Status == "Waiting" || t.Status == "Processing"));
                foreach(Models.Prompt prompt in allPrompts)
                {
                    prompts.Add(new ViewModels.ItemPromptViewModel()
                    {
                        Id = prompt.Id,
                        _partitionKey = prompt._partitionKey,
                        Type = prompt.Type,
                        Information = prompt.Information,
                        TimeStamp = prompt.TimeStamp,
                        Status = prompt.Status,
                        Sender = prompt.Sender,
                        Receiver = prompt.Receiver
                    });
                }
            }
        }
        private async void ChangePromptStatus(ViewModels.ItemPromptViewModel prompt, string status)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var processingPrompt = realm.All<Models.Prompt>().Where(p => p.Id == prompt.Id).FirstOrDefault();
                realm.Write(() =>
                {
                    processingPrompt.Status = status;
                });
            }
        }
        private void HandleAskForRoll(ViewModels.ItemPromptViewModel prompt)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.ShowPopup(new Views.AskForRollPopupPage(prompt));
            });
        }
        private void HandleAskForItemCheck(ViewModels.ItemPromptViewModel prompt)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.ShowPopup(new Views.AskForItemCheckPopupPage(prompt));
            });
        }
        private void HandleAskForStatCheck(ViewModels.ItemPromptViewModel prompt)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.ShowPopup(new Views.AskForStatCheckPopupPage(prompt));
            });
        }
        private void HandleAskForAbilityCheck(ViewModels.ItemPromptViewModel prompt)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.ShowPopup(new Views.AskForAbilityCheckPopupPage(prompt));
            });
        }
        private async Task HandlePassInformation(ViewModels.ItemPromptViewModel prompt)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert(
                    "Prompt!",
                    $"You have just received an information!\n\n\"{prompt.Information}\"" +
                    $"\n{prompt.TimeStamp}",
                    "Got that.");
                    ChangePromptStatus(prompt, "Processed");
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"THIS IS THE BUG\n\n{ex.Message}\n{ex.InnerException.Message}\n\nTHIS IS THE BUG");
            }
        }
        private async Task HandleReadyCheck(ViewModels.ItemPromptViewModel prompt)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string answer;
                var result = await Shell.Current.DisplayAlert("Prompt!", $"Event creator ~{prompt.Sender}~, has asked You to perform a ready check. Are You indeed a READY person?", "Yes", "No");
                if (result)
                {
                    answer = "Yes, I am ready.";
                }
                else
                {
                    answer = "No, I am not ready.";
                }
                using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
                {
                    Models.Prompt rePrompt = new Models.Prompt
                    {
                        Id = RealmDB.GetUniqueKey(8),
                        Sender = RealmDB.CurrentlyLoggedUserId,
                        Receiver = prompt.Sender,
                        Status = "Waiting",
                        TimeStamp = DateTime.Now.ToString(),
                        Type = "PassInformation",
                        Information = $"An event participant ~{RealmDB.CurrentlyLoggedUserId}~ has responded to your ready check request. His answer is: {answer}",
                        _partitionKey = "_partitionKey"
                    };
                    realm.Write(() => { realm.Add(rePrompt); });
                }
                ChangePromptStatus(prompt, "Processed");
            });
        }
        private async void PromptingFSM()
        {

            if (!prompts.Where<ViewModels.ItemPromptViewModel>(t=>t.Status == "Processing").Any()) {
                if (prompts.Any())
                {
                    var prompt = prompts.FirstOrDefault();
                    prompts.Remove(prompt);
                    ChangePromptStatus(prompt, "Processing");
                    switch (prompt.Type)
                    {
                        case "AskForRoll":
                            HandleAskForRoll(prompt);
                            break;
                        case "AskForItemCheck":
                            HandleAskForItemCheck(prompt);
                            break;
                        case "AskForStatCheck":
                            HandleAskForStatCheck(prompt);
                            break;
                        case "AskForAbilityCheck":
                            HandleAskForAbilityCheck(prompt);
                            break;
                        case "PassInformation":
                            await HandlePassInformation(prompt);
                            break;
                        case "ReadyCheck":
                            await HandleReadyCheck(prompt);
                            break;
                        default:
                            ChangePromptStatus(prompt, "Failure");
                            break;
                    }
                }
            }
        }


        public async void ServiceStart()
        {
            if (isServiceWorking == false) {
                isServiceWorking = true;
                promptingManagement = Task.Run(async () =>
                {
                    while (isServiceWorking)
                    {
                        if (RealmDB.CurrentlyLoggedUserId != null)
                        {
                            await UpdatePrompts();
                            PromptingFSM();
                        }
                        await Task.Delay(delay);
                    }
                });
            }
        }
        public void ServiceStop()
        {
            isServiceWorking = false;
        }
        public PromptingService()
        {
            this.delay = 2000;
        }
        public PromptingService(int delay)
        {
            this.delay = delay;
        }
    }
}
