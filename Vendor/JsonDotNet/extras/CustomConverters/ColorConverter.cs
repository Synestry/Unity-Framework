
using System;

using Newtonsoft.Json;

using UnityEngine;

namespace JsonDotNet.Extras.CustomConverters
{
	public class ColorConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				value = new Color();
			}

			var v = (Color)value;

			writer.WriteStartObject();

			writer.WritePropertyName("r");
			serializer.Serialize(writer, v.r);

			writer.WritePropertyName("g");
			serializer.Serialize(writer, v.g);

			writer.WritePropertyName("b");
			serializer.Serialize(writer, v.b);

			writer.WritePropertyName("a");
			serializer.Serialize(writer, v.a);

			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.None)
			{
				return new Color32();
			}

			//reader.Read(); // {

			reader.Read(); // Property R        
			reader.Read(); // Value R
			var r = (Byte)serializer.Deserialize(reader, typeof(Byte));

			reader.Read(); // Property G
			reader.Read(); // Value G
			var g = (Byte)serializer.Deserialize(reader, typeof(Byte));

			reader.Read(); // Property B
			reader.Read(); // Value B
			var b = (Byte)serializer.Deserialize(reader, typeof(Byte));

			reader.Read(); // Property A
			reader.Read(); // Value A
			var a = (Byte)serializer.Deserialize(reader, typeof(Byte));

			reader.Read(); // }

			return new Color32(r, g, b, a);
		}

		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(Color32));
		}
	}
}