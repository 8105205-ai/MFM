using System;
using System.Collections.Generic;
using System.Text;

namespace MFM.Content.Tiles
{
    public class WindGenerator : BasicDevice
    {
        float windSpeed = Math.abs(Main.windSpeedCurrent)/0.8f; // hz rabotaet li tak esli cho peredelau, ya hz prost schas ne mogu zatestit
        base.SetStaticDefaults();
        power = 10;
            DustType = DustID.Stone;
            AddMapEntry(new Color(200, 200, 200));
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.addTile(Type);
            TileID.Sets.HasOutlines[Type] = true;
    }
}
