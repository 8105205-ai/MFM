using MFM.Content.Liqs;
using ModLiquidLib.ID;
using ModLiquidLib.ModLoader;
using Terraria.ID;

namespace MFM.Content.Items
{
    public class LiquidBucket : BucketBase
    {
        public LiquidBucket()
        {
            BucketLiquidType = LiquidLoader.LiquidType<MyLiq>();
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.WaterBucket;
            LiquidID_TLmod.Sets.CreateLiquidBucketItem[LiquidLoader.LiquidType<MyLiq>()] = Type;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.WaterBucket).Register();
        }
    }
}