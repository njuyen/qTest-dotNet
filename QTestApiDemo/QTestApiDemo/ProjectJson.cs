using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTestApiDemo
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ProjectJson
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public string startDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public string endDate { get; set; }
    }
}
