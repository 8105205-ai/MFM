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
        public int id; 
        public static int usage = 0; 
        public String name; 

        public Device(float power, Point16 pos, String name) 
        {
            this.power = power;
            this.pos = pos;
            id = usage; 
            usage++; 
            this.name = name; 
        }

        public void Connect(Device device)
        {
            if (connected == null || !connected.Contains(device))
            {
                connected.Add(device);
                device.connected.Add(this);
            }
            else if (connected.Contains(device))
            {
                connected.Remove(device);
                device.connected.Remove(this);
            }
        }

        //public void Disconnect(Device device)
        //{
        //    connected.Remove(device);
        //}

        public List<Device> allConected()
        {
            //if (connected == null || connected.Count == 0) return new List<Device> {this};
            //List<Device> result = new List<Device>();
            //result.Add(this);
            //foreach (Device device in connected)
            //{
            //    if (visited != null && visited.Contains(device)) continue;
            //    if (visited == null) visited = new List<Device>();
            //    visited.Add(device);
            //    result.AddRange(device.allConected(result.Union(visited).ToList()));
            //}
            //return result;
            HashSet<Device> visited = new HashSet<Device>();

            // Очередь для обхода
            Queue<Device> queue = new Queue<Device>();

            // Начинаем с текущего устройства
            queue.Enqueue(this);
            visited.Add(this);

            while (queue.Count > 0)
            {
                Device current = queue.Dequeue();

                foreach (Device neighbor in current.connected)
                {
                    // Если мы еще не были у этого соседа — добавляем его в список
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // Превращаем HashSet обратно в список и возвращаем
            return visited.ToList();
        }

        public float netPower()
        {
            List<Device> con = this.allConected();
            float spower = 0;
            foreach (Device device in con)
            {
                spower += device.power;
                Main.NewText(device.name+device.id+": "+device.power); //del
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
