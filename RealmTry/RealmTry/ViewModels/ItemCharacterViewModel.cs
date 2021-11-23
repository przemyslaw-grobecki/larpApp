using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTry.ViewModels
{
    public class ItemCharacterViewModel : BaseViewModel
    {
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string userId;
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }
        private string characterPortraitUrl;
        public string CharacterPortraitUrl
        {
            get => characterPortraitUrl;
            set => SetProperty(ref characterPortraitUrl, value);
        }
        private string characterName;
        public string CharacterName
        {
            get => characterName;
            set => SetProperty(ref characterName, value);
        }
        private string characterSurname;
        public string CharacterSurname
        {
            get => characterSurname;
            set => SetProperty(ref characterSurname, value);
        }
        private string proffession;
        public string Proffession
        {
            get => proffession;
            set => SetProperty(ref proffession, value);
        }
        private int level;
        public int Level
        {
            get => level;
            set => SetProperty(ref level, value);
        }
        private int healthPoints;
        public int HealthPoints
        {
            get => healthPoints;
            set => SetProperty(ref healthPoints, value);
        }
        private int experiencePoints;
        public int ExperiencePoints
        {
            get => experiencePoints;
            set => SetProperty(ref experiencePoints, value);
        }
        private int strength;
        public int Strength
        {
            get => strength;
            set => SetProperty(ref strength, value);
        }
        private int dexterity;
        public int Dexterity
        {
            get => dexterity;
            set => SetProperty(ref dexterity, value);
        }
        private int constitution;
        public int Constitution
        {
            get => constitution;
            set => SetProperty(ref constitution, value);
        }
        private int inteligence;
        public int Inteligence
        {
            get => inteligence;
            set => SetProperty(ref inteligence, value);
        }
        private int wisdom;
        public int Wisdom
        {
            get => wisdom;
            set => SetProperty(ref wisdom, value);
        }
        private int charisma;
        public int Charisma
        {
            get => charisma;
            set => SetProperty(ref charisma, value);
        }
        //lists to be added????....
    }
}
