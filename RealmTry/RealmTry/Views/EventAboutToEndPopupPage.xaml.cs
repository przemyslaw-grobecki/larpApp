using Realms;
using RealmTry.Models;
using RealmTry.Services;
using RealmTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventAboutToEndPopupPage : Popup
	{
		private ItemEquipableViewModel equipable;
		private ItemEventStatusViewModel eventStatusViewModel;
		public EventAboutToEndPopupPage (ItemEventStatusViewModel eventStatus, ItemEquipableViewModel equipable)
		{
			InitializeComponent ();
			eventStatusViewModel = eventStatus;
			this.equipable = equipable;
			BindingContext = new EventAboutToEndPopupViewModel(eventStatus.EventId, equipable);
		}

        private async void participants_ItemTapped(object sender, ItemTappedEventArgs e)
        {
			var result = await Shell.Current.DisplayAlert("Warning", $"Are You sure that {(e.Item as ItemHostedEventParticipantViewModel).ParticipantCharacterName} {(e.Item as ItemHostedEventParticipantViewModel).ParticipantCharacterSurname} should be rewarded?", "Yes, I am sure", "Let me change my mind");
			if(!result) return;
			using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
			{
				var rewardedCharacterInventory = realm.All<Models.Inventory>().Where(t => t.UserId == (e.Item as ItemHostedEventParticipantViewModel).ParticipantId).FirstOrDefault();
				if (rewardedCharacterInventory == null)
                {
					Inventory inventory = new Inventory
					{
						Id = RealmDB.GetUniqueKey(8),
						UserId = (e.Item as ItemHostedEventParticipantViewModel).ParticipantId,
						_partitionKey = "_partitionKey"
					};
					realm.Write(() => {
						realm.Add(inventory);
					});
					rewardedCharacterInventory = realm.All<Models.Inventory>().Where(t => t.UserId == (e.Item as ItemHostedEventParticipantViewModel).ParticipantId).FirstOrDefault();
				}
				Models.Equipable rewardedEquipable = new Models.Equipable
				{
					Name = equipable.Name,
					ImageUrl = equipable.ImageUrl,
					Rarity = equipable.Rarity,
					Type = equipable.Type
				};

				foreach(ItemStatBonusViewModel statBonus in equipable.StatBonuses)
                {
					rewardedEquipable.StatBonuses.Add(new Models.StatBonus
					{
						Stat = statBonus.Stat,
						Increase = statBonus.Increase,
					});
                }

				realm.Write(()=>
				{
					rewardedCharacterInventory.Equipables.Add(rewardedEquipable);
				});

				var status = realm.All<Models.EventStatus>().Where(t => t.EventId == eventStatusViewModel.EventId).FirstOrDefault();
				realm.Write(() =>
				{
					status.Information = "Ended";
				});
				eventStatusViewModel.Information = "Ended";
				Dismiss(null);
			}
        }
    }
}