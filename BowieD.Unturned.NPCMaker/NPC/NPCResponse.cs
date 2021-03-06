﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Condition = BowieD.Unturned.NPCMaker.NPC.Conditions.Condition;
using Reward = BowieD.Unturned.NPCMaker.NPC.Rewards.Reward;

namespace BowieD.Unturned.NPCMaker.NPC
{
    [System.Serializable]
    public class NPCResponse
    {
        public NPCResponse()
        {
            mainText = "";
            conditions = new List<Condition>();
            rewards = new List<Reward>();
            visibleIn = new int[0];
        }

        public string mainText;
        public ushort openDialogueId;
        public ushort openVendorId;
        public ushort openQuestId;
        public List<Condition> conditions;
        public List<Reward> rewards;
        public int[] visibleIn;
        [XmlIgnore]
        public bool VisibleInAll => visibleIn == null || visibleIn.All(d => d == 1) || visibleIn.All(d => d == 0); // last condition may cause invalid logic, but it works for now
    }
}
