using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ObjectData;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace MFM.Content.Tiles
{
    public class WindGenerator : BasicDevice
    {
        public override void SetStaticDefaults()
        {
            float windSpeed = Math.Abs(Main.windSpeedCurrent) / 0.8f; // hz rabotaet li tak esli cho peredelau, ya hz prost schas ne mogu zatestit
            base.SetStaticDefaults();
            power = 10;
            String name = "WindGenerator";
            DustType = DustID.Stone;
            AddMapEntry(new Color(200, 200, 200));
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.addTile(Type);
        }
    }
}
