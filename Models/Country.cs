using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countries.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public CountryName Name { get; set; }

        [JsonProperty("altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonProperty("capital")]
        public List<string> Capital { get; set; }

        [JsonProperty("borders")]
        public List<string> Borders { get; set; }

        [JsonProperty("translations")]
        public CountryNameTranslation Translations { get; set; }
    }

    public class CountryName
    {
        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }
    }

    public class CountryNameTranslation
    {
        [JsonProperty("deu")]
        public CountryName German { get; set; }

        [JsonProperty("spa")]
        public CountryName Spanish { get; set; }

        [JsonProperty("fra")]
        public CountryName French { get; set; }

        [JsonProperty("jpn")]
        public CountryName Japanese { get; set; }

        [JsonProperty("ita")]
        public CountryName Italian { get; set; }
    }
}