using System;
using System.Collections.Generic;
using System.Text;

namespace MonkeyFinder.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Monkey
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("Details")]
        public string Details { get; set; }

        [JsonProperty("Image")]
        public Uri Image { get; set; }

        [JsonProperty("Population")]
        public long Population { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }
    }

    public partial class Monkey
    {
        public static Monkey[] FromJson(string json) => JsonConvert.DeserializeObject<Monkey[]>(json, MonkeyFinder.Model.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Monkey[] self) => JsonConvert.SerializeObject(self, MonkeyFinder.Model.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

