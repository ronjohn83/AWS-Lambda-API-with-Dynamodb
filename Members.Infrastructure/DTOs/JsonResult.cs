using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Members.Infrastructure.DTOs
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class JsonResult
    {
        [JsonProperty(PropertyName = "Results")]
        public List<Results> Results { get; set; }

        [JsonProperty(PropertyName = "Info")]
        public Info Info { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Info
    {
        public string Seed { get; set; }

        public string Results { get; set; }
        public string Page { get; set; }

        public string Version { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Results
    {
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public Name Name { get; set; }

        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "Cell")]
        public string Cell { get; set; }

        [JsonProperty(PropertyName = "Location")]
        public Location Location { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Location
    {
        [JsonProperty(PropertyName = "Street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "City")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "State")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "PostCode")]

        public string PostCode { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Name
    {
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "First")]
        public string First { get; set; }

        [JsonProperty(PropertyName = "Last")]
        public string Last { get; set; }
    }
}
