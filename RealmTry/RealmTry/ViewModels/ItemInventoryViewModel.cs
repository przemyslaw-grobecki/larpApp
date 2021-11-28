using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    public class ItemInventoryViewModel : BaseViewModel
    {
        private string id;
        public string Id { get => id; set => SetProperty(ref id, value); }
        private string userId;
        public string UserId { get => userId; set => SetProperty(ref userId, value); }
        private List<ItemEquipableViewModel> equipables;
        public List<ItemEquipableViewModel> Equipables { get => equipables; set => SetProperty(ref equipables, value); }
    }
}
