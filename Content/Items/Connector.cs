using MFM.Content.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using MFM.Content.Systems;
using System.Drawing;
using Terraria.DataStructures;

namespace MFM.Content.Items
{
    public class Connector : ModItem
    {
        public Device choosenDevice = null;
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.consumable = false;
            Item.autoReuse = false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || player.whoAmI != Main.myPlayer)
                return false;
            foreach (Point16 point in ModContent.GetInstance<ElSys>().devices.Keys) //del
            {
                Main.NewText(point.X + " " + point.Y); //del
            }
            Device device = DevUnderCursor();
            if (player.altFunctionUse == 2)
            {
                if (device == null) { return false; }
                if (choosenDevice == null) choosenDevice = device;
                else if (device != choosenDevice) device.Connect(choosenDevice);
                else choosenDevice = null;
                return true;
            }
            if (device == null) return false;
            float sumpow = device.netPower();
            Main.NewText(sumpow);
            return true;
        }

        private Device DevUnderCursor()
        {
            int i = Player.tileTargetX;
            int j = Player.tileTargetY;
            Tile tile = Main.tile[i, j];
            if (!tile.HasTile || !(TileLoader.GetTile(tile.TileType) is BasicDevice)) {return null; }
            else
            {
                TileObjectData data = TileObjectData.GetTileData(tile);
                if (data == null) return ModContent.GetInstance<ElSys>().devices[new Point16(i, j)];
                int width = data.Width;
                int height = data.Height;
                int frameX = tile.TileFrameX;
                int frameY = tile.TileFrameY;
                if (data.StyleHorizontal)
                {
                    int styleWidth = width * 18;
                    int styleIndex = frameX / styleWidth;
                    frameX -= styleIndex * styleWidth;
                }
                int originX = i - (frameX / 18);
                int originY = j - (frameY / 18);
                return ModContent.GetInstance<ElSys>().devices[new Point16(originX, originY)];
            }
        }
    }
}
