using Realms;
using RealmTry.Services;
using RealmTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealmTry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : TabbedPage
    {
        public InventoryPage()
        {
            BindingContext = new InventoryViewModel();
            InitializeComponent();
        }

        private async void UnEquipItem(string itemName)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
            }
        }
        private async void EquipItem(string itemName)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                ItemEquipableViewModel chosenItem = null;
                foreach(ItemEquipableViewModel item in equipablesList.ItemsSource)
                {
                    if(item.Name == itemName)
                    {
                        chosenItem = item;
                        break;
                    }
                }

                Models.Equipable equipable = new Models.Equipable
                {
                    Name = chosenItem.Name,
                    ImageUrl = chosenItem.ImageUrl,
                    Rarity = chosenItem.Rarity,
                    Type = chosenItem.Type                    
                };

                foreach (ItemStatBonusViewModel statBonus in chosenItem.StatBonuses)
                {
                    equipable.StatBonuses.Add(new Models.StatBonus { Increase = statBonus.Increase, Stat = statBonus.Stat });
                }

                Models.Character character = realm.All<Models.Character>().Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId).FirstOrDefault();
                realm.Write(()=> {
                    character.EquipedItems.Add(equipable);
                });
                foreach(ItemStatBonusViewModel statBonus in chosenItem.StatBonuses)
                {
                    switch (statBonus.Stat)
                    {
                        case "STRENGTH":
                            realm.Write(()=> {
                                character.Strength += int.Parse(statBonus.Increase);
                            });
                            break;
                        case "DEXTERITY":
                            realm.Write(() => {
                                character.Dexterity = character.Dexterity + int.Parse(statBonus.Increase);
                            });
                            break;
                        case "CONSTITUTION":
                            realm.Write(() => {
                                character.Constitution = character.Constitution + int.Parse(statBonus.Increase);
                            });
                            break;
                        case "INTELIGENCE":
                            realm.Write(() => {
                                character.Inteligence = character.Inteligence + int.Parse(statBonus.Increase);
                            });
                            break;
                        case "WISDOM":
                            realm.Write(() => {
                                character.Wisdom = character.Wisdom + int.Parse(statBonus.Increase);
                            });
                            break;
                        case "CHARISMA":
                            realm.Write(() => {
                                character.Charisma = character.Charisma + int.Parse(statBonus.Increase);
                            });
                            break;
                        default:
                            break;
                    }
                }


            }
        }

        private async void armorButton_Clicked(object sender, EventArgs e)
        {
            
            var itemList = new List<string>();
            foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
            {
                if (item.Type == "CHEST")
                {
                    itemList.Add(item.Name);
                }
            }
            if (itemList.Count() == 0) return;
            var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
        }

        private async void helmetButton_Clicked(object sender, EventArgs e)
        {
            var itemList = new List<string>();
            foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
            {
                if (item.Type == "HEAD")
                {
                    itemList.Add(item.Name);
                }
            }
            if (itemList.Count() == 0) return;
            var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
        }

        private async void hand_rightButton_Clicked(object sender, EventArgs e)
        {
            var itemList = new List<string>();
            foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
            {
                if (item.Type == "RIGHTHAND")
                {
                    itemList.Add(item.Name);
                }
            }
            if (itemList.Count() == 0) return;
            var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
        }

        private async void hand_leftButton_Clicked(object sender, EventArgs e)
        {
            var itemList = new List<string>();
            foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
            {
                if (item.Type == "LEFTHAND")
                    itemList.Add(item.Name);
            }
            if (itemList.Count() == 0) return;
            var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
        }

        private async void bootsButton_Clicked(object sender, EventArgs e)
        {
            var itemList = new List<string>();
            foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
            {
                if (item.Type == "FEET")
                {
                    itemList.Add(item.Name);
                }
            }
            if (itemList.Count() == 0) return;
            var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
        }

        private void equipablesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}