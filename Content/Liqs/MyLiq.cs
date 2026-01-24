using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.GameContent.Liquid;
using ModLiquidLib.ID;
using ModLiquidLib.ModLoader;
using ModLiquidLib.Utils;
using ModLiquidLib.Utils.Structs;
using Microsoft.Xna.Framework;

namespace MFM.Content.Liqs
{
    public class MyLiq : ModLiquid
    {
        public override void SetStaticDefaults()
        {
            LiquidRenderer.VISCOSITY_MASK[Type] = 200;
            LiquidRenderer.WATERFALL_LENGTH[Type] = 20;
            LiquidRenderer.DEFAULT_OPACITY[Type] = 0.95f;
            SlopeOpacity = 1f;
            LiquidfallOpacityMultiplier = 0.5f;
            FallDelay = 2;
            AddMapEntry(new Color(200, 200, 200), CreateMapEntryName());
        }
    }
}
