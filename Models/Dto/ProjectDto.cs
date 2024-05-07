namespace TomskNipi.DevelopProgress.Models.Dto
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastUpdatedAt { get; set; }

        public List<IssueDto> Issues { get; set; } = new List<IssueDto>();

    }
}

