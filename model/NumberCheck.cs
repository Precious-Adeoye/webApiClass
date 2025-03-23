using Newtonsoft.Json;

namespace webApiClass.model
{
    public class NumberCheck
    {
        [JsonProperty("data")]
        public Root data { get; set; }
    }

   
    public class Root
    {
        [JsonProperty("vaild")]
        public bool valid { get; set; }
        [JsonProperty("number")]
        public string number { get; set; }
        [JsonProperty("local_format")]
        public string local_format { get; set; }
        [JsonProperty("international_format")]
        public string international_format { get; set; }
        [JsonProperty("country_prefix")]
        public string country_prefix { get; set; }
        [JsonProperty("country_code")]
        public string country_code { get; set; }
        [JsonProperty("country_name")]
        public string country_name { get; set; }
        [JsonProperty("location")]
        public string location { get; set; }
        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("line_type")]
        public string line_type { get; set; }
    }


}
