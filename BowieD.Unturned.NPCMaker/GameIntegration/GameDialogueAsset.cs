﻿using BowieD.Unturned.NPCMaker.GameIntegration.Thumbnails;
using BowieD.Unturned.NPCMaker.NPC;
using BowieD.Unturned.NPCMaker.Parsing;
using System;

namespace BowieD.Unturned.NPCMaker.GameIntegration
{
    public class GameDialogueAsset : GameAsset, IHasNameOverride, ISearchNameOverride
    {
        public GameDialogueAsset(NPCDialogue dialogue, EGameAssetOrigin origin) : base($"Asset_{dialogue.ID}", dialogue.ID, Guid.Parse(dialogue.GUID), "Dialogue", origin)
        {
            this.dialogue = dialogue;
        }
        public GameDialogueAsset(DataReader data, DataReader local, string name, ushort id, Guid guid, string type, EGameAssetOrigin origin) : base(name, id, guid, type, origin)
        {
            dialogue = new Parsing.ParseTool(data, local).ParseDialogue();
        }

        public NPCDialogue dialogue;

        public override EGameAssetCategory Category => EGameAssetCategory.NPC;

        public string NameOverride => dialogue.ContentPreview;

        public string SearchNameOverride => dialogue.FullText;
    }
}
