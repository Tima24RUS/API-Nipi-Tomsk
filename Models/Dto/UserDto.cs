using TomskNipi.DevelopProgress.DataAccess;

namespace TomskNipi.DevelopProgress.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    }
}

