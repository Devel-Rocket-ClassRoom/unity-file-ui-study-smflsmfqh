using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

public class QuaternionConverter : JsonConverter<Quaternion>
{
    public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Quaternion quat = Quaternion.identity;

        JObject jObj = JObject.Load(reader);
        quat.x = (float)jObj["X.rot"];
        quat.y = (float)jObj["Y.rot"];
        quat.z = (float)jObj["Z.rot"];
        quat.w = (float)jObj["W.rot"];

        return quat;
    }

    public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("X.rot");
        writer.WriteValue(value.x);
        writer.WritePropertyName("Y.rot");
        writer.WriteValue(value.y);
        writer.WritePropertyName("Z.rot");
        writer.WriteValue(value.z);
        writer.WritePropertyName("W.rot");
        writer.WriteValue(value.w);
        writer.WriteEndObject();
    }
}
