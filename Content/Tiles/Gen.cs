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
    public class Gen : BasicDevice
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            name = "Gen"; 
            power = 10;
            DustType = DustID.Stone;
            AddMapEntry(new Color(200, 200, 200));
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            TileID.Sets.HasOutlines[Type] = false;
            TileObjectData.addTile(Type);
        }
    }
}
