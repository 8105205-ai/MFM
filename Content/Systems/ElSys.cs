using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MFM.Content.Items;

namespace MFM.Content.Systems
{
    public class ElSys : ModSystem
    {
        public Dictionary<Point16, Device> devices = new Dictionary<Point16, Device>(); // все устройства в мире

        public override void OnWorldLoad()
        {
            Device.usage = 0; // чтобы id нормаьлно работали
        }

        public override void OnWorldUnload()
        {
            Device.usage = 0; // тоже для id
        }

        public override void SaveWorldData(TagCompound tag) // сохранение
        {
            var devicesList = new List<TagCompound>(); // список устройств

            foreach (var device in devices)
            {
                Device dev = device.Value;
                var devTag = new TagCompound();

                devTag["x"] = dev.pos.X;
                devTag["y"] = dev.pos.Y;
                devTag["power"] = dev.power;
                devTag["name"] = dev.name;

                var connections = dev.connected.Select(n => new Vector2(n.pos.X, n.pos.Y)).ToList();
                devTag["connections"] = connections;

                devicesList.Add(devTag);
            }

            tag["devicesData"] = devicesList;
        }

        public override void LoadWorldData(TagCompound tag) // загрузка из сохранения
        {
            devices.Clear();
            if (!tag.ContainsKey("devicesData")) return;

            var devicesList = tag.GetList<TagCompound>("devicesData");

            foreach (var devTag in devicesList) // создание экземпляров Device 
            {
                Point16 pos = new Point16(devTag.GetShort("x"), devTag.GetShort("y"));
                Device dev = new Device(devTag.GetFloat("power"), pos, devTag.GetString("name"));
                devices[pos] = dev;
            }

            foreach (var devTag in devicesList) // связи между устройствами
            {
                Point16 pos = new Point16(devTag.GetShort("x"), devTag.GetShort("y"));
                if (devices.TryGetValue(pos, out Device currentDev))
                {
                    var connections = devTag.GetList<Vector2>("connections");
                    foreach (var connPosVec in connections)
                    {
                        Point16 connPos = new Point16((short)connPosVec.X, (short)connPosVec.Y);
                        if (devices.TryGetValue(connPos, out Device neighbor))
                            if (!currentDev.connected.Contains(neighbor))
                                currentDev.connected.Add(neighbor);
                    }
                }
            }
        }

        public override void PostDrawTiles() // отрисовка проводов
        {
            if (Main.gameMenu) return; // Не рисуем в меню
            if (devices.Count == 0) return;

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

            // Рисуем установленные связи
            foreach (var device in devices.Values)
            {
                foreach (var neighbor in device.connected)
                {
                    if (device.id < neighbor.id)
                    {
                        DrawConnection(device.pos.ToVector2() * 16 + new Vector2(8, 8), neighbor.pos.ToVector2() * 16 + new Vector2(8, 8), Color.Yellow * 0.7f);
                    }
                }
            }

            // отрисока "призрачного" провода при соединении через Connector
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<Connector>())
            {
                var connector = Main.LocalPlayer.HeldItem.ModItem as Connector;
                if (connector.choosenDevice != null)
                {
                    Vector2 start = connector.choosenDevice.pos.ToVector2() * 16 + new Vector2(8, 8);
                    Vector2 end = Main.MouseWorld;
                    DrawConnection(start, end, Color.White * 0.5f);
                }
            }

            Main.spriteBatch.End();
        }

        private void DrawConnection(Vector2 start, Vector2 end, Color color) // рисоваие линии
        {
            Vector2 screenStart = start - Main.screenPosition;
            Vector2 screenEnd = end - Main.screenPosition;
            float dist = Vector2.Distance(screenStart, screenEnd);
            float rot = (screenEnd - screenStart).ToRotation();

            Main.spriteBatch.Draw(Terraria.GameContent.TextureAssets.MagicPixel.Value, screenStart, new Rectangle(0, 0, 1, 1), color, rot, Vector2.Zero, new Vector2(dist, 2f), SpriteEffects.None, 0);
        }
    }
}

