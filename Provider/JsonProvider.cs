using System.Text.Json;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;



namespace Student_Api.Provider
{
    public static class JsonProvider
    {
            public static bool IsJson(this string data)
            {
                try { _ = JToken.Parse(data); return true; } catch { return false; }
            }
            public static string ToJson(this object data)
            {
                return JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            public static T FromJson<T>(this string json)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            public static string ToFormattedJson(this object data)
            {
                return JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            public static string AddSpaceBeforeCapital(this string data) => string.Concat(data.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

    }
}
