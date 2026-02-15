using System;
using System.Collections.Generic;
using System.Text;
using Terraria.ObjectData;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MFM.Content.Systems;
using Terraria.DataStructures;

namespace MFM.Content.Tiles
{
    public abstract class BasicDevice : ModTile {

        public float power = 0;


        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.SmartCursorShowing = true;
            TileObjectData.addTile(Type);
            TileID.Sets.HasOutlines[Type] = true;
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            base.PlaceInWorld(i, j, item);
            TileObjectData data = TileObjectData.GetTileData(Type, 0);
            int originX = i - data.Origin.X;
            int originY = j - data.Origin.Y;
            Point16 pos = new Point16(originX, originY);
            Device device = new Device(power, pos);
            ModContent.GetInstance<ElSys>().devices[pos] = device;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Point16 pos = new Point16(i, j);
            ModContent.GetInstance<ElSys>().devices.Remove(pos);
            base.KillMultiTile(i, j, frameX, frameY);
        }
    }
}
