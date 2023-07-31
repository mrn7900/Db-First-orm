using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Herobio
    {
      
        //public string response { get; set; }
        public int id { get; set; }
        public string name { get; set; }

        [JsonProperty("full-name")]
        public string fullname { get; set; }

        [JsonProperty("alter-egos")]
        public string alteregos { get; set; }
        //public List<string> aliases { get; set; }

        [JsonProperty("place-of-birth")]
        public string placeofbirth { get; set; }

        [JsonProperty("first-appearance")]
        public string firstappearance { get; set; }
        public string publisher { get; set; }
        public string alignment { get; set; }
    }
}
