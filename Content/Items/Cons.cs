using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace MFM.Content.Items
{
    public class Cons : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Cons>());

            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 9999;
            Item.useTime = 10;
            Item.useAnimation = 10;
        }
    }
}
