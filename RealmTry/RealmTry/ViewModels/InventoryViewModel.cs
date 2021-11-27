using Realms;
using RealmTry.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RealmTry.ViewModels
{
    class InventoryViewModel : BaseViewModel
    {
        public ObservableCollection<ItemEquipableViewModel> Equipables { get; set; } = new ObservableCollection<ItemEquipableViewModel>();
        public Command RefreshEquipablesCommand { get; set; }
        public async void RefreshEquipables()
        {
            IsRefreshingEquipables = true;
            Equipables.Clear();
            int counter = 0;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var myEquips = realm.All<Models.Inventory>().Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId).FirstOrDefault().Equipables.ToList();
                foreach(Models.Equipable equip in myEquips)
                {
                    Equipables.Add(new ItemEquipableViewModel
                    {
                        Name = equip.Name,
                        ImageUrl = equip.ImageUrl,
                        Type = equip.Type,
                        Rarity = equip.Rarity,
                        StatBonuses = new List<ItemStatBonusViewModel>()
                    });
                    foreach(Models.StatBonus statBonus in equip.StatBonuses)
                    {
                        Equipables[counter].StatBonuses.Add(new ItemStatBonusViewModel
                        {
                            Increase = statBonus.Increase,
                            Stat = statBonus.Stat
                        });
                    }
                    counter++;
                }
                IsRefreshingEquipables = false;
            }
 
        }
        private bool isRefreshingEquipables = false;
        public bool IsRefreshingEquipables
        {
            get => isRefreshingEquipables;
            set => SetProperty(ref isRefreshingEquipables, value);
        }

        public InventoryViewModel()
        {
            RefreshEquipablesCommand = new Command(RefreshEquipables);
            //Get the wheels spinning...
            RefreshEquipables();
        }
    }
}
