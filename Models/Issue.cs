using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TomskNipi.DevelopProgress.Models
{

    public class Issue
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Assignee Assignee { get; set; }

        public string Web_url { get; set; }

        public string Created_at { get; set; }
        
        public string Updated_at { get; set; }

        [JsonPropertyName("time_stats")]
        public TimeStats TimeStats { get; set; }
    }
}
