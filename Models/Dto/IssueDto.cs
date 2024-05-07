namespace TomskNipi.DevelopProgress.Models.Dto
{
    public class IssueDto
    {

        public string Title { get; set; }

        public string WebUrl { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public int TimeEstimate { get; set; }

        public string HumanTimeEstimate { get; set; }
    }
}

