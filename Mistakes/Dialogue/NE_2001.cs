﻿using BowieD.Unturned.NPCMaker.NPC;
using System;
using System.Linq;

namespace BowieD.Unturned.NPCMaker.Mistakes.Dialogue
{
    /// <summary>
    /// No pages in message
    /// </summary>
    public class NE_2001 : Mistake
    {
        public override IMPORTANCE Importance => IMPORTANCE.NO_EXPORT;
        public override bool IsMistake
        {
            get
            {
                foreach (NPCDialogue dialogue in MainWindow.CurrentNPC.dialogues)
                {
                    if (dialogue.MessagesAmount > 0 && dialogue.messages.Any(d => d.PagesAmount == 0))
                    {
                        errorDialogue = dialogue;
                        return true;
                    }
                }
                return false;
            }
        }

        public override string MistakeNameKey => "NE_2001";
        public override string MistakeDescKey => MainWindow.Localize("NE_2001_Desc", errorDialogue.id);
        public override bool TranslateName => false;
        public override bool TranslateDesc => false;
        private NPCDialogue errorDialogue;
        public override Action OnClick
        {
            get
            {
                return new Action(() =>
                {
                    if (MainWindow.Instance.CurrentDialogue.id == 0)
                        return;
                    MainWindow.Instance.Dialogue_SaveButtonClick(null, null);
                    MainWindow.Instance.CurrentDialogue = errorDialogue;
                    MainWindow.Instance.mainTabControl.SelectedIndex = 2;
                });
            }
        }
    }
}
