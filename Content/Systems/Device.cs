using MFM.Content.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;

namespace MFM.Content.Systems
{
    public class Device
    {
        public float power;
        public List<Device> connected = new List<Device>();
        public Point16 pos;

        public Device(float power, Point16 pos)
        {
            this.power = power;
            this.pos = pos;
        }

        public void Connect(Device device)
        {
            if (connected == null || !connected.Contains(device)) connected.Add(device);
        }

        public void Disconnect(Device device)
        {
            connected.Remove(device);
        }

        public List<Device> allConected(List<Device> visited = null)
        {
            if (connected == null || connected.Count == 0) return new List<Device> {this};
            List<Device> result = new List<Device>();
            if (visited == null) visited = new List<Device>();
            foreach (Device device in connected)
            {
                if (visited.Contains(device)) continue;
                visited.Add(device);
                result.AddRange(device.allConected(visited));
            }
            return result;
        }

        public float netPower()
        {
            List<Device> con = this.allConected();
            float spower = 0;
            foreach (Device device in con)
            {
                spower += device.power;
                Main.NewText(device.power); //del
            }
            return spower;
        }

        public bool isActive()
        {
            if (this.netPower() >= 0) return true;
            return false;
        }
    }
}
