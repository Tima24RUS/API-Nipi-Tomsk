using TomskNipi.DevelopProgress.DataAccess;
using TomskNipi.DevelopProgress.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace TomskNipi.DevelopProgress.Models
{
    public class UserCreator
    {

        private ActiveDirectoryRepository _activeDirectoryRepository = new ActiveDirectoryRepository();

        public List<UserDto> UserDtos { get; set; } = new List<UserDto>();

        private object _locker = new();

        public void Create(Project project, IGrouping<int, Issue>? issueGroupedByAssignee)
        {
            lock (_locker)
            {
                Assignee assignee = issueGroupedByAssignee.First().Assignee;
                UserDto userDto = UserDtos.FirstOrDefault(n => n.Id == issueGroupedByAssignee.Key);
                if (userDto is null)
                {
                    userDto = new UserDto { Id = issueGroupedByAssignee.Key, UserName = assignee.Username, AvatarUrl = assignee.Avatar_url };
                    userDto.Projects.Add(new ProjectDto { Id = project.Id, Name = project.Name, LastUpdatedAt = project.Last_activity_at });

                    foreach (var issue in issueGroupedByAssignee)
                    {
                        userDto.Projects.Last().Issues.Add(new IssueDto { Title = issue.Title, WebUrl = issue.Web_url, UpdatedAt = issue.Updated_at, CreatedAt = issue.Created_at, TimeEstimate = issue.TimeStats.TimeEstimate, HumanTimeEstimate = issue.TimeStats.HumanTimeEstimate });
                    }

                    userDto.FullName = _activeDirectoryRepository.FindUserFullName(assignee.Username);
                    UserDtos.Add(userDto);
                }
                else
                {
                    userDto.Projects.Add(new ProjectDto { Id = project.Id, Name = project.Name, LastUpdatedAt = project.Last_activity_at });
                    userDto.FullName = _activeDirectoryRepository.FindUserFullName(assignee.Username);

                    foreach (var issue in issueGroupedByAssignee)
                    {
                        userDto.Projects.Last().Issues.Add(new IssueDto { Title = issue.Title, WebUrl = issue.Web_url, UpdatedAt = issue.Updated_at, CreatedAt = issue.Created_at, TimeEstimate = issue.TimeStats.TimeEstimate, HumanTimeEstimate = issue.TimeStats.HumanTimeEstimate });
                    }
                }
            }
        }


    }
}

