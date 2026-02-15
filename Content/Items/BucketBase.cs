using ModLiquidLib.ID;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MFM.Content.Items
{
   
    public abstract class BucketBase : ModItem
    {
        public int BucketLiquidType = ItemID.EmptyBucket;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            ItemID.Sets.AlsoABuildingItem[Type] = true;
            ItemID.Sets.DuplicationMenuToolsFilter[Type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void HoldItem(Player player)
        {
            if (!player.JustDroppedAnItem)
            {
                if (player.whoAmI != Main.myPlayer)
                {
                    return;
                }
                if (player.noBuilding ||
                    !(player.position.X / 16f - Player.tileRangeX - Item.tileBoost <= Player.tileTargetX) ||
                    !((player.position.X + player.width) / 16f + Player.tileRangeX + Item.tileBoost - 1f >= Player.tileTargetX) ||
                    !(player.position.Y / 16f - Player.tileRangeY - Item.tileBoost <= Player.tileTargetY) ||
                    !((player.position.Y + player.height) / 16f + Player.tileRangeY + Item.tileBoost - 2f >= Player.tileTargetY))
                {
                    return;
                }
                if (!Main.GamepadDisableCursorItemIcon)
                {
                    player.cursorItemIconEnabled = true;
                    Main.ItemIconCacheUpdate(Item.type);
                }
                if (!player.ItemTimeIsZero || player.itemAnimation <= 0 || !player.controlUseItem)
                {
                    return;
                }
                Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
                if (tile.LiquidAmount >= 200)
                {
                    return;
                }
                if (tile.HasUnactuatedTile)
                {
                    bool[] tileSolid = Main.tileSolid;
                    if (tileSolid[tile.TileType])
                    {
                        bool[] tileSolidTop = Main.tileSolidTop;
                        if (!tileSolidTop[tile.TileType])
                        {
                            if (tile.TileType != TileID.Grate)
                            {
                                return;
                            }
                        }
                    }
                }
                if (tile.LiquidAmount != 0)
                {
                    if (tile.LiquidType != BucketLiquidType)
                    {
                        return;
                    }
                }
                SoundEngine.PlaySound(SoundID.SplashWeak, player.position);
                tile.LiquidType = BucketLiquidType;
                tile.LiquidAmount = byte.MaxValue;
                WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);
                Item.stack--;
                player.PutItemInInventoryFromItemUsage(ItemID.EmptyBucket, player.selectedItem);
                player.ApplyItemTime(Item);

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                }
            }
        }
    }
}