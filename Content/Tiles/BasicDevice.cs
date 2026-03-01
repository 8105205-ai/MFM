using System;
using System.Collections.Generic;
using System.Text;
using Terraria.ObjectData;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MFM.Content.Systems;
using Terraria.DataStructures;

namespace MFM.Content.Tiles
{
    public abstract class BasicDevice : ModTile {

        public float power = 0;
        public String name = "Dev"; 


       

        public override void PlaceInWorld(int i, int j, Item item) // выполняется при установке блока
        {
            base.PlaceInWorld(i, j, item);
            TileObjectData data = TileObjectData.GetTileData(Type, 0);
            int originX = i - data.Origin.X;
            int originY = j - data.Origin.Y;
            Point16 pos = new Point16(originX, originY);
            Device device = new Device(power, pos, name); // создаёт экземпляр Device
            ModContent.GetInstance<ElSys>().devices[pos] = device;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) // при разрушении блока
        {
            Point16 pos = new Point16(i, j);
            var elSys = ModContent.GetInstance<ElSys>();

            if (elSys.devices.TryGetValue(pos, out Device device))
            {
                foreach (Device neighbor in device.connected) // удаление всех связей
                {
                    neighbor.connected.Remove(device);
                }
                device.connected.Clear();

                elSys.devices.Remove(pos);
            }

            base.KillMultiTile(i, j, frameX, frameY);
        }

    }
}
