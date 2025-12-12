using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MFM.Content.Tiles
{
    public class portal_a : ModTile
    {
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
			Main.tileSolid[Type] = false;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = false;
			Main.tileLighted[Type] = false;
            Main.SmartCursorShowing = true;
			DustType = DustID.Stone;
			AddMapEntry(new Color(200, 200, 200));
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.addTile(Type);
            TileID.Sets.HasOutlines[Type] = true;
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.RemoveAllGrapplingHooks();
            player.Spawn(PlayerSpawnContext.RecallFromItem);
            return true;
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            
            // Показываем курсор взаимодействия
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Items.portal_a>();
        }
    }
}