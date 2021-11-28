using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    public class ItemEquipableViewModel : BaseViewModel
    {
        private string name;
        public string Name { get => name; set => SetProperty(ref name, value); }
        private string imageUrl;
        public string ImageUrl { get => imageUrl; set => SetProperty(ref imageUrl, value); }
        private string rarity;
        public string Rarity { get => rarity; set => SetProperty(ref rarity, value); }
        private string type;
        public string Type { get => type; set => SetProperty(ref type, value); }
        private List<ItemStatBonusViewModel> statBonuses;
        public List<ItemStatBonusViewModel> StatBonuses { get => statBonuses; set => SetProperty(ref statBonuses, value); }
        public void CreateItemEquipableViewModel(Models.Equipable equipable)
        {
            name = equipable.Name;
            rarity = equipable.Rarity;
            type = equipable.Type;
            imageUrl = equipable.ImageUrl;
            foreach(Models.StatBonus statBonus in equipable.StatBonuses)
            {
                statBonuses.Add(new ItemStatBonusViewModel { Stat = statBonus.Stat, Increase = statBonus.Increase });
            }
        }
        
    }
}
