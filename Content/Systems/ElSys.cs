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

namespace MFM.Content.Systems
{
    public class ElSys : ModSystem
    {
        public Dictionary<Point16, Device> devices = new Dictionary<Point16, Device>();



        public override void SaveWorldData(TagCompound tag)
        {

            string json = JsonConvert.SerializeObject(devices, new Point16Converter());
            tag["devicesJson"] = json;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            string json = tag.GetString("devicesJson");
            devices = JsonConvert.DeserializeObject<Dictionary<Point16, Device>>(json);
        }

        public class Point16Converter : JsonConverter<Point16>
        {
            public override void WriteJson(JsonWriter writer, Point16 value, JsonSerializer serializer)
            {
                writer.WriteValue($"{value.X},{value.Y}");
            }

            public override Point16 ReadJson(JsonReader reader, Type objectType, Point16 existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                var parts = reader.Value.ToString().Split(',');
                return new Point16(int.Parse(parts[0]), int.Parse(parts[1]));
            }
        }
    }
}

