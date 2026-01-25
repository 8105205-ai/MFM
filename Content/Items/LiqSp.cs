using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using ModLiquidLib.ModLoader;
using MFM.Content.Liqs;

namespace MFM.Content.Items
{
    internal class LiqSp : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.consumable = false;
            Item.autoReuse = true;
        }

        public override bool? UseItem(Player player)
        {
            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
            tile.LiquidType = LiquidID.Water;
            tile.LiquidAmount = byte.MaxValue;
            return true;
        }
    }
}
