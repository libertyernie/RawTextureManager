using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawTextureManager {
	public class HexIntConverter : JsonConverter {
		public override bool CanConvert(Type objectType) {
			return objectType == typeof(int);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			string value = reader.Value.ToString();
			if (value.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)) {
				int output;
				if (int.TryParse(value.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out output)) {
					return output;
				}
			}
			return serializer.Deserialize<int>(reader);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			writer.WriteValue("0x" + ((int)value).ToString("X2"));
		}
	}
}
