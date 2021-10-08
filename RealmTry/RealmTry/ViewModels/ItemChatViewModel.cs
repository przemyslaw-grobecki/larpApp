using System;
using System.Collections.Generic;
using System.Text;
using RealmTry.Models;

namespace RealmTry.ViewModels
{
    class ItemChatViewModel
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => _id = value;
        }
        private List<string> participantsIds;
        public List<string> ParticipantsIds
        {
            get => participantsIds;
            set => participantsIds = value;
        }
        private List<ItemMessageViewModel> messages;
        public List<ItemMessageViewModel> Messages
        {
            get => messages;
            set => messages = value;
        }

        public string GetParticipants
        {
            get
            {
                string participants = "";
                foreach (string participant in participantsIds)
                {
                    participants += (", " + participant);
                }
                return participants;
            }
        }
    }
}
