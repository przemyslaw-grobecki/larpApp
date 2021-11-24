using RealmTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.Factories
{
    class Item
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
    class ItemEquipableFactory
    {
        private List<string> stats = new List<string>
        {
            "STRENGTH",
            "DEXTERITY",
            "CONSTITUTION",
            "INTELIGENCE",
            "WISDOM",
            "CHARISMA"
        };
        Random random = new Random();
        private readonly string Legendary = "#FFFFC800";
        private readonly string Epic = "#FF9F05B7";
        private readonly string Rare = "#FF1837B7";
        private readonly string Uncommon = "#FF0CE926";
        private readonly string Common = "#FFFFFFFF";
        private readonly string Junk = "#FF666060";
        private readonly Dictionary<string, string> FirstWordsForHelmet = new Dictionary<string, string>
        {
            { "Helmet","https://i.ibb.co/9VhZZhg/equipable-helmet.png" },
            { "Hood","https://i.ibb.co/tKrZx9w/equipable-hood.png"},
            { "Hat","https://i.ibb.co/Wf6NN6t/equipable-hat.png" },
            { "Cap","https://i.ibb.co/Wf6NN6t/equipable-hat.png" }
        };
        private readonly Dictionary<string, string> FirstWordsForArmor = new Dictionary<string, string>
        {
            { "Armor" ,"https://i.ibb.co/LnWYKgx/equipable-armor.png"},
            { "Chain mail","https://i.ibb.co/rZSxjRH/equipable-hauberk.png" },
            { "Full plate","https://i.ibb.co/FxRkrFv/equipable-full-plate.png" },
            { "Hauberk", "https://i.ibb.co/rZSxjRH/equipable-hauberk.png" },
            { "Robe", "https://i.ibb.co/pWrCZPg/equipable-robe.png" },
            { "Jacket", "https://i.ibb.co/8bqpZWx/equipable-jacket.png" },
            { "Half plate", "https://i.ibb.co/FxRkrFv/equipable-full-plate.png" },
        };
        private readonly Dictionary<string, string> FirstWordsForBoots = new Dictionary<string, string>
        {
            { "Boots", "https://i.ibb.co/5GvVh1y/equipable-boots.png" },
            { "Sandals", "https://i.ibb.co/JQQhGY3/equipable-sandals.png" }
        };
        private readonly Dictionary<string, string> FirstWordsForHandGear = new Dictionary<string, string>
        {
            { "Sword", "https://i.ibb.co/mFnLG37/equipable-sword.png" },
            { "Bow", "https://i.ibb.co/ygfKQqr/equipable-bow.png" },
            { "Crossbow", "https://i.ibb.co/ccRbCZ3/equipable-crossbow.png" },
            { "Dagger", "https://i.ibb.co/qBt2S3s/equipable-dagger.png" },
            { "Machete", "https://i.ibb.co/n1Qmk2X/equipable-machete.png" },
            { "Tome", "https://i.ibb.co/tXGGFxC/equipable-book.png" },
            { "Staff", "https://i.ibb.co/jyRZWMb/equipable-staff.png" },
            { "Axe", "https://i.ibb.co/ph4LV26/equipable-axe.png" },
            { "Mace", "https://i.ibb.co/fCx4PD4/equipable-mace.png" },
            { "Libram", "https://i.ibb.co/YTLc15X/equipable-libram.png" },
            { "Hammer", "https://i.ibb.co/px8fXsr/equipable-hammer.png" },
            { "Shovel", "https://i.ibb.co/jG987S8/equipable-shovel.png" },
            { "Shield", "https://i.ibb.co/2YdVtLV/equipable-shield.png" }
        };
        private readonly List<string> SecondWordForGear = new List<string>
        {
            "Mighty",
            "Scary",
            "Proud",
            "Thirsty",
            "Hungry",
            "Fast",
            "Creepy",
            "Boastfull",
            "Blusterous",
            "Incomplete",
            "Non-differentiable",
            "Ugly",
            "#!@$",
            "Handsome",
            "Nice",
            "Happy",
            "Modern",
            "Beatifull",
            "Smart",
            "Clever",
            "Easy-Going"
        };
        private readonly List<string> ThirdWordForGear = new List<string>
        {
            "Monkey",
            "Whale",
            "Tiger",
            "Dziekan",
            "Student",
            "Ph. D",
            "Enginner",
            "Scholar",
            "Outlaw",
            "Zebra",
            "Cat",
            "Dog",
            "Giraffe",
            "Snail",
            "Snot",
            "Rektor",
            "Dean",
            "Bird",
            "Fish",
            "Influencer"
        };
        private Item GetItem()
        {
            string itemName;
            string itemType;
            List<string> values;
            List<string> keys;

            int armorPartChosen = random.Next(4);
            switch (armorPartChosen)
            {
                case 0:
                    keys = new List<string>(FirstWordsForHelmet.Keys);
                    values = new List<string>(FirstWordsForHelmet.Values);
                    itemType = "HEAD";
                    break;
                case 1:
                    keys = new List<string>(FirstWordsForArmor.Keys);
                    values = new List<string>(FirstWordsForArmor.Values);
                    itemType = "CHEST";
                    break;
                case 2:
                    keys = new List<string>(FirstWordsForBoots.Keys);
                    values = new List<string>(FirstWordsForBoots.Values);
                    itemType = "FEET";
                    break;
                case 3:
                    keys = new List<string>(FirstWordsForHandGear.Keys);
                    values = new List<string>(FirstWordsForHandGear.Values);
                    itemType = "WEAPON";
                    break;
                default:
                    keys = new List<string>(FirstWordsForHandGear.Keys);
                    values = new List<string>(FirstWordsForHandGear.Values);
                    itemType = "WEAPON";
                    break;
            }
            var rnd = random.Next(keys.Count);
            itemName = keys[rnd] + " of " + SecondWordForGear[random.Next(SecondWordForGear.Count)] + " " + ThirdWordForGear[random.Next(ThirdWordForGear.Count)];
            return new Item
            {
                Name = itemName,
                Type = itemType,
                ImageUrl = values[rnd]
            };
        }
        public ItemEquipableViewModel ProduceJunk()
        {
            List<string> availableStats = new List<string>(stats);
            Item item = GetItem();
            string chosenStat = availableStats[random.Next(6)];
            availableStats.Remove(chosenStat);
            ItemEquipableViewModel junk = new ItemEquipableViewModel()
            {
                Rarity = Junk,
                StatBonuses = new List<ItemStatBonusViewModel>
                {
                    {
                        new ItemStatBonusViewModel
                        {
                            Stat = chosenStat,
                            Increase = -1
                        }
                    }
                },
                ImageUrl = item.ImageUrl,
                Type = item.Type,
                Name = "Terrible " + item.Name,
            };
            return junk;
        }


        public ItemEquipableViewModel ProduceCommon()
        {
            List<string> availableStats = new List<string>(stats);
            Item item = GetItem();
            string chosenStat = availableStats[random.Next(6)];
            availableStats.Remove(chosenStat);
            ItemEquipableViewModel common = new ItemEquipableViewModel()
            {
                Rarity = Common,
                StatBonuses = new List<ItemStatBonusViewModel>
                {
                    {
                        new ItemStatBonusViewModel
                        {
                            Stat = chosenStat,
                            Increase = 1
                        }
                    }
                },
                ImageUrl = item.ImageUrl,
                Type = item.Type,
                Name = item.Name,
            };
            return common;
        }
        public ItemEquipableViewModel ProduceUncommon()
        {
            List<string> availableStats = new List<string>(stats);
            Item item = GetItem();
            string chosenPrimaryStat = availableStats[random.Next(availableStats.Count)];
            availableStats.Remove(chosenPrimaryStat);
            string chosenSecondaryStat = availableStats[random.Next(availableStats.Count)];

            ItemEquipableViewModel uncommon = new ItemEquipableViewModel()
            {
                Rarity = Uncommon,
                StatBonuses = new List<ItemStatBonusViewModel>
                {
                    new ItemStatBonusViewModel
                    {
                        Stat = chosenPrimaryStat,
                        Increase = 1
                    },
                    new ItemStatBonusViewModel
                    {
                        Stat = chosenSecondaryStat,
                        Increase = 1
                    }
                },
                ImageUrl = item.ImageUrl,
                Type = item.Type,
                Name = "Uncommon " + item.Name,
            };
            return uncommon;
        }
        public ItemEquipableViewModel ProduceRare()
        {
            List<string> availableStats = new List<string>(stats);
            Item item = GetItem();
            string chosenPrimaryStat = availableStats[random.Next(availableStats.Count)];
            availableStats.Remove(chosenPrimaryStat);

            ItemEquipableViewModel rare = new ItemEquipableViewModel()
            {
                Rarity = Rare,
                StatBonuses = new List<ItemStatBonusViewModel>
                {
                    new ItemStatBonusViewModel
                    {
                        Stat = chosenPrimaryStat,
                        Increase = 2
                    }
                },
                ImageUrl = item.ImageUrl,
                Type = item.Type,
                Name = "Rare " + item.Name,
            };
            return rare;
        }
        public ItemEquipableViewModel ProduceEpic()
        {
            List<string> availableStats = new List<string>(stats);
            Item item = GetItem();
            string chosenPrimaryStat = availableStats[random.Next(availableStats.Count)];
            availableStats.Remove(chosenPrimaryStat);
            string chosenSecondaryStat = availableStats[random.Next(availableStats.Count)];

            ItemEquipableViewModel epic = new ItemEquipableViewModel()
            {
                Rarity = Epic,
                StatBonuses = new List<ItemStatBonusViewModel>
                {
                    new ItemStatBonusViewModel
                    {
                        Stat = chosenPrimaryStat,
                        Increase = 2
                    },
                    new ItemStatBonusViewModel
                    {
                        Stat = chosenSecondaryStat,
                        Increase = 1
                    }
                },
                ImageUrl = item.ImageUrl,
                Type = item.Type,
                Name = "Epic " + item.Name,
            };
            return epic;
        }
        public ItemEquipableViewModel ProduceLegendary()
        {
            List<string> availableStats = new List<string>(stats);
            Item item = GetItem();
            ItemEquipableViewModel uncommon = new ItemEquipableViewModel()
            {
                Rarity = Legendary,
                StatBonuses = new List<ItemStatBonusViewModel>
                {
                    new ItemStatBonusViewModel
                    {
                        Stat = "STRENGTH",
                        Increase = 1
                    },
                    new ItemStatBonusViewModel
                    {
                        Stat = "DEXTERITY",
                        Increase = 1
                    },
                    new ItemStatBonusViewModel
                    {
                        Stat = "CONSTITUTION",
                        Increase = 1
                    },
                    new ItemStatBonusViewModel
                    {
                        Stat = "INTELIGENCE",
                        Increase = 1
                    },
                    new ItemStatBonusViewModel
                    {
                        Stat = "WISDOM",
                        Increase = 1
                    },
                    new ItemStatBonusViewModel
                    {
                        Stat = "CHARISMA",
                        Increase = 1
                    }
                },
                ImageUrl = item.ImageUrl,
                Type = item.Type,
                Name = "Legendary " + item.Name,
            };
            return uncommon;
        }
        public ItemEquipableViewModel ProduceRandom()
        {
            int itemChoice = random.Next(101);
            if (itemChoice >= 0 && itemChoice < 10)
            {
                return ProduceJunk();
            }
            else if (itemChoice >= 10 && itemChoice < 60)
            {
                return ProduceCommon();
            }
            else if (itemChoice >= 60 && itemChoice < 85)
            {
                return ProduceUncommon();
            }
            else if (itemChoice >= 85 && itemChoice < 95)
            {
                return ProduceRare();
            }
            else if (itemChoice >= 95 && itemChoice < 100)
            {
                return ProduceEpic();
            }
            else
            {
                return ProduceLegendary();
            }
        }
    }
}
