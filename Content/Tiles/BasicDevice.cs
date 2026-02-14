using System;
using System.Collections.Generic;
using System.Text;

namespace MFM.Content.Tiles
{
    public abstract class BasicDevice : ModTile {

        public int power;
        List<BasicDevice> connected;


        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.SmartCursorShowing = true;
            TileObjectData.addTile(Type);
            TileID.Sets.HasOutlines[Type] = true;
        }

        public void Connect(BasicDevice device)
        {
            if (!connected.Contains(device)) connected.Add(device);
        }

        public void Disconnect(BasicDevice device)
        {
            connected.Remove(device);
        }

        public List<BasicDevice> allConected(List<BasicDevice> visited = [this])
        {
            List<BasicDevice> result = new List<BasicDevice>();
            result.AddRange(connected);
            result.Add(this);
            if (connected.Count == 0) return result;
            foreach (BasicDevice device in connected) {
                if (visited.Contains(device)) continue;
                visited.Add(device);
                result.AddRange(device.allConected(visited));
            }
            return result;
        }
    }
}
