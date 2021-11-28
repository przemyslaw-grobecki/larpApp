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

        private async void UnequipItem(string itemType)
        {
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                Models.Character character = realm.All<Models.Character>().Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId).FirstOrDefault();
                switch (itemType)
                {
                    case "HEAD":
                    Models.Equipable head = character.EquipedItems.Where(t => t.Type == "HEAD").FirstOrDefault();
                    foreach(Models.StatBonus statBonus in head.StatBonuses)
                    {
                        switch (statBonus.Stat)
                        {
                            case "STRENGTH":
                                realm.Write(() => {
                                    character.Strength -= int.Parse(statBonus.Increase);
                                });
                                break;
                            case "DEXTERITY":
                                realm.Write(() => {
                                    character.Dexterity -= int.Parse(statBonus.Increase);
                                });
                                break;
                            case "CONSTITUTION":
                                realm.Write(() => {
                                    character.Constitution -= int.Parse(statBonus.Increase);
                                });
                                break;
                            case "INTELIGENCE":
                                realm.Write(() => {
                                    character.Inteligence -= int.Parse(statBonus.Increase);
                                });
                                break;
                            case "WISDOM":
                                realm.Write(() => {
                                    character.Wisdom -= int.Parse(statBonus.Increase);
                                });
                                break;
                            case "CHARISMA":
                                realm.Write(() => {
                                    character.Charisma -= int.Parse(statBonus.Increase);
                                });
                                break;
                            default:
                                break;
                        }
                    }
                    realm.Write(() => {
                        character.EquipedItems.Remove(head);
                    });
                    break;
                    case "CHEST":
                        Models.Equipable chest = character.EquipedItems.Where(t => t.Type == "CHEST").FirstOrDefault();
                        foreach (Models.StatBonus statBonus in chest.StatBonuses)
                        {
                            switch (statBonus.Stat)
                            {
                                case "STRENGTH":
                                    realm.Write(() => {
                                        character.Strength -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "DEXTERITY":
                                    realm.Write(() => {
                                        character.Dexterity -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "CONSTITUTION":
                                    realm.Write(() => {
                                        character.Constitution -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "INTELIGENCE":
                                    realm.Write(() => {
                                        character.Inteligence -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "WISDOM":
                                    realm.Write(() => {
                                        character.Wisdom -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "CHARISMA":
                                    realm.Write(() => {
                                        character.Charisma -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                default:
                                    break;
                            }
                        }
                        realm.Write(() => {
                            character.EquipedItems.Remove(chest);
                        });
                        break;
                    case "FEET":
                        Models.Equipable feet = character.EquipedItems.FirstOrDefault(t => t.Type == "FEET");
                        foreach (Models.StatBonus statBonus in feet.StatBonuses)
                        {
                            switch (statBonus.Stat)
                            {
                                case "STRENGTH":
                                    realm.Write(() => {
                                        character.Strength -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "DEXTERITY":
                                    realm.Write(() => {
                                        character.Dexterity -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "CONSTITUTION":
                                    realm.Write(() => {
                                        character.Constitution -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "INTELIGENCE":
                                    realm.Write(() => {
                                        character.Inteligence -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "WISDOM":
                                    realm.Write(() => {
                                        character.Wisdom -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "CHARISMA":
                                    realm.Write(() => {
                                        character.Charisma -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                default:
                                    break;
                            }
                        }
                        realm.Write(() => {
                            character.EquipedItems.Remove(feet);
                        });
                        break;
                    case "LEFTHAND":
                        Models.Equipable leftHand = character.EquipedItems.Where(t => t.Type == "LEFTHAND").FirstOrDefault();
                        foreach (Models.StatBonus statBonus in leftHand.StatBonuses)
                        {
                            switch (statBonus.Stat)
                            {
                                case "STRENGTH":
                                    realm.Write(() => {
                                        character.Strength -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "DEXTERITY":
                                    realm.Write(() => {
                                        character.Dexterity -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "CONSTITUTION":
                                    realm.Write(() => {
                                        character.Constitution -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "INTELIGENCE":
                                    realm.Write(() => {
                                        character.Inteligence -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "WISDOM":
                                    realm.Write(() => {
                                        character.Wisdom -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "CHARISMA":
                                    realm.Write(() => {
                                        character.Charisma -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                default:
                                    break;
                            }
                        }
                        realm.Write(() => {
                            character.EquipedItems.Remove(leftHand);
                        });
                        break;
                    case "RIGHTHAND":
                        Models.Equipable rightHand = character.EquipedItems.Where(t => t.Type == "RIGHTHAND").FirstOrDefault();
                        foreach (Models.StatBonus statBonus in rightHand.StatBonuses)
                        {
                            switch (statBonus.Stat)
                            {
                                case "STRENGTH":
                                    realm.Write(() => {
                                        character.Strength -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "DEXTERITY":
                                    realm.Write(() => {
                                        character.Dexterity -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "CONSTITUTION":
                                    realm.Write(() => {
                                        character.Constitution -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "INTELIGENCE":
                                    realm.Write(() => {
                                        character.Inteligence -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "WISDOM":
                                    realm.Write(() => {
                                        character.Wisdom -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                case "CHARISMA":
                                    realm.Write(() => {
                                        character.Charisma -= int.Parse(statBonus.Increase);
                                    });
                                    break;
                                default:
                                    break;
                            }
                        }
                        realm.Write(() => {
                            character.EquipedItems.Remove(rightHand);
                        });
                        break;
                    default:
                        break;
                }
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
                                character.Dexterity += int.Parse(statBonus.Increase);
                            });
                            break;
                        case "CONSTITUTION":
                            realm.Write(() => {
                                character.Constitution += int.Parse(statBonus.Increase);
                            });
                            break;
                        case "INTELIGENCE":
                            realm.Write(() => {
                                character.Inteligence += int.Parse(statBonus.Increase);
                            });
                            break;
                        case "WISDOM":
                            realm.Write(() => {
                                character.Wisdom += int.Parse(statBonus.Increase);
                            });
                            break;
                        case "CHARISMA":
                            realm.Write(() => {
                                character.Charisma += int.Parse(statBonus.Increase);
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
            bool unequipItem = false;
            bool equipItem = false;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var characterArmor = realm.All<Models.Character>().Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId).FirstOrDefault().EquipedItems.Where(t => t.Type == "CHEST").FirstOrDefault();
                if (characterArmor == null)
                {
                    equipItem = await Shell.Current.DisplayAlert("Chest", "Would You like to equip chest armor?", "Yes", "No");
                    if (equipItem == false) return;
                }
                else
                {
                    unequipItem = await Shell.Current.DisplayAlert("Chest", "Would You like to unequip chest armor?", "Yes", "No");
                    if (unequipItem == false) return;
                }
            }

            if (equipItem)
            {
                var itemList = new List<string>();
                foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
                {
                    if (item.Type == "CHEST")
                    {
                        itemList.Add(item.Name);
                    }
                }
                if (itemList.Count() == 0)
                {
                    await Shell.Current.DisplayAlert("No items of this category", "Sorry, You do not have any items of this type.", "Ok");
                    return;
                }
                var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
                EquipItem(choice);
            }
            else if (unequipItem)
            {
                UnequipItem("CHEST");
            }
        }

        private async void helmetButton_Clicked(object sender, EventArgs e)
        {
            bool unequipItem = false;
            bool equipItem = false;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var characterArmor = realm
                    .All<Models.Character>()
                    .Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId)
                    .FirstOrDefault()
                    .EquipedItems
                    .Where(t => t.Type == "HEAD")
                    .FirstOrDefault();
                if (characterArmor == null)
                {
                    equipItem = await Shell.Current.DisplayAlert("Head", "Would You like to equip head armor?", "Yes", "No");
                    if (equipItem == false) return;
                }
                else
                {
                    unequipItem = await Shell.Current.DisplayAlert("Head", "Would You like to unequip head armor?", "Yes", "No");
                    if (unequipItem == false) return;
                }
            }

            if (equipItem)
            {
                var itemList = new List<string>();
                foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
                {
                    if (item.Type == "HEAD")
                    {
                        itemList.Add(item.Name);
                    }
                }
                if (itemList.Count() == 0)
                {
                    await Shell.Current.DisplayAlert("No items of this category", "Sorry, You do not have any items of this type.", "Ok");
                    return;
                }
                var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
                EquipItem(choice);
            }
            else if (unequipItem)
            {
                UnequipItem("HEAD");
            }
        }

        private async void hand_rightButton_Clicked(object sender, EventArgs e)
        {
            bool unequipItem = false;
            bool equipItem = false;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var characterArmor = realm
                    .All<Models.Character>()
                    .Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId)
                    .FirstOrDefault()
                    .EquipedItems
                    .Where(t => t.Type == "RIGHTHAND")
                    .FirstOrDefault();
                if (characterArmor == null)
                {
                    equipItem = await Shell.Current.DisplayAlert("Right hand", "Would You like to equip right hand weapon?", "Yes", "No");
                    if (equipItem == false) return;
                }
                else
                {
                    unequipItem = await Shell.Current.DisplayAlert("Right hand", "Would You like to unequip right hand weapon?", "Yes", "No");
                    if (unequipItem == false) return;
                }
            }

            if (equipItem)
            {
                var itemList = new List<string>();
                foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
                {
                    if (item.Type == "RIGHTHAND")
                    {
                        itemList.Add(item.Name);
                    }
                }
                if (itemList.Count() == 0)
                {
                    await Shell.Current.DisplayAlert("No items of this category", "Sorry, You do not have any items of this type.", "Ok");
                    return;
                }
                var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
                EquipItem(choice);
            }
            else if (unequipItem)
            {
                UnequipItem("RIGHTHAND");
            }
        }

        private async void hand_leftButton_Clicked(object sender, EventArgs e)
        {
            bool unequipItem = false;
            bool equipItem = false;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var characterArmor = realm
                    .All<Models.Character>()
                    .Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId)
                    .FirstOrDefault()
                    .EquipedItems
                    .Where(t => t.Type == "LEFTHAND")
                    .FirstOrDefault();
                if (characterArmor == null)
                {
                    equipItem = await Shell.Current.DisplayAlert("Left hand", "Would You like to equip left hand weapon?", "Yes", "No");
                    if (equipItem == false) return;
                }
                else
                {
                    unequipItem = await Shell.Current.DisplayAlert("Left hand", "Would You like to unequip left hand weapon?", "Yes", "No");
                    if (unequipItem == false) return;
                }
            }

            if (equipItem)
            {
                var itemList = new List<string>();
                foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
                {
                    if (item.Type == "LEFTHAND")
                    {
                        itemList.Add(item.Name);
                    }
                }
                if (itemList.Count() == 0)
                {
                    await Shell.Current.DisplayAlert("No items of this category", "Sorry, You do not have any items of this type.", "Ok");
                    return;
                }
                var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
                EquipItem(choice);
            }
            else if (unequipItem)
            {
                UnequipItem("LEFTHAND");
            }
        }

        private async void bootsButton_Clicked(object sender, EventArgs e)
        {
            bool unequipItem = false;
            bool equipItem = false;
            using (var realm = await Realm.GetInstanceAsync(RealmDB.Configuration))
            {
                var characterArmor = realm
                    .All<Models.Character>()
                    .Where(t => t.UserId == RealmDB.CurrentlyLoggedUserId)
                    .FirstOrDefault()
                    .EquipedItems
                    .Where(t => t.Type == "FEET")
                    .FirstOrDefault();
                if (characterArmor == null)
                {
                    equipItem = await Shell.Current.DisplayAlert("Feet", "Would You like to equip feet armor?", "Yes", "No");
                    if (equipItem == false) return;
                }
                else
                {
                    unequipItem = await Shell.Current.DisplayAlert("Head", "Would You like to unequip feet armor?", "Yes", "No");
                    if (unequipItem == false) return;
                }
            }

            if (equipItem)
            {
                var itemList = new List<string>();
                foreach (ItemEquipableViewModel item in equipablesList.ItemsSource)
                {
                    if (item.Type == "FEET")
                    {
                        itemList.Add(item.Name);
                    }
                }
                if (itemList.Count() == 0)
                {
                    await Shell.Current.DisplayAlert("No items of this category", "Sorry, You do not have any items of this type.", "Ok");
                    return;
                }
                var choice = await Shell.Current.DisplayActionSheet("Pick your item", "Cancel", null, itemList.ToArray());
                EquipItem(choice);
            }
            else if (unequipItem)
            {
                UnequipItem("FEET");
            }
        }

        private void equipablesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}