using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MFM.Content.Mounts {
    public class DrillMach : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.jumpHeight = 5; 
			MountData.acceleration = 0.19f; 
			MountData.jumpSpeed = 4f; 
			MountData.blockExtraJumps = false; 
			MountData.heightBoost = 20; 
			MountData.runSpeed = 11f; 
			MountData.dashSpeed = 8f; 
			MountData.flightTimeMax = 0;
        }
    }
}