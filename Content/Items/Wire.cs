using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MFM.Content.Items{

public class Wire: ModItem
{
    public override void SetDefaults()
    {
        Item.height = 32;
        Item.width = 32;
        Item.rare = ItemRarityID.White;
        Item.maxStack = 99;
        Item.value = Item.sellPrice(silver: 1);
    }

    public override void AddRecipes()
    {
        Recipe recipe1 = CreateRecipe();
        recipe1.AddIngredient(ItemID.LeadBar, 10);
        recipe1.AddIngredient(ItemID.CopperBar, 20);
        recipe1.AddTile(TileID.Anvils);
        recipe1.Register();
        Recipe recipe2 = CreateRecipe();
        recipe2.AddIngredient(ItemID.IronBar, 10);
        recipe2.AddIngredient(ItemID.CopperBar, 20);
        recipe2.AddTile(TileID.Anvils);
        Recipe recipe3 = CreateRecipe();
        recipe3.AddIngredient(ItemID.LeadBar, 10);
        recipe3.AddIngredient(ItemID.TinBar, 20);
        recipe3.AddTile(TileID.Anvils);
        Recipe recipe4 = CreateRecipe();
        recipe4.AddIngredient(ItemID.IronBar, 10);
        recipe4.AddIngredient(ItemID.TinBar, 20);
        recipe4.AddTile(TileID.Anvils);
        recipe2.Register();
        recipe3.Register();
        recipe4.Register();
    }
}
}