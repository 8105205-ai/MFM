using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MFM.Content.Items
{
    public class portal_a : ModItem //d
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.portal_a>());

            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 9999;
            Item.useTime = 10;
            Item.useAnimation = 10;
        }
    }
}