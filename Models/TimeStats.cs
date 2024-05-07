using System.Text.Json.Serialization;

namespace TomskNipi.DevelopProgress.Models
{
    public class TimeStats
    {
        [JsonPropertyName("time_estimate")]
        public int TimeEstimate { get; set; }

        [JsonPropertyName("human_time_estimate")]
        public string HumanTimeEstimate { get; set;}
    }
}
