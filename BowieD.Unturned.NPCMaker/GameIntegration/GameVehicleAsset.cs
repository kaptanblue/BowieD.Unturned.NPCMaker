﻿using BowieD.Unturned.NPCMaker.Parsing;
using System;

namespace BowieD.Unturned.NPCMaker.GameIntegration
{
    public class GameVehicleAsset : GameAsset
    {
        public GameVehicleAsset(DataReader data, string name, ushort id, Guid guid, string type, EGameAssetOrigin origin) : base(name, id, guid, type, origin)
        {

        }
        public override EGameAssetCategory Category => EGameAssetCategory.VEHICLE;
    }
}
