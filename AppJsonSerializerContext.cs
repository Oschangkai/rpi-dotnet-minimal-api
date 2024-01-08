using System.Text.Json.Serialization;

[JsonSerializable(typeof(SystemHealth))]
[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}