using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TomskNipi.DevelopProgress.Models.Dto;
using TomskNipi.DevelopProgress.Models;
using System.Security.Principal;
using TomskNipi.DevelopProgress.DataAccess;
using Microsoft.AspNetCore.Authorization;


namespace TomskNipi.DevelopProgress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private const string _privateToken = "xbKpA1RWSCyNpyz_MZTH";

        [Route("GetUsers")]
        [HttpGet]
        public async Task<IActionResult> NewGet()
        {
            ActiveDirectoryRepository activeDirectoryRepository = new ActiveDirectoryRepository();
            UserCreator userCreator = new UserCreator();
            using (HttpClient client = new HttpClient())
            {
                ProjectRequester projectRequester = new ProjectRequester(client);
                IssueRequester issueRequester = new IssueRequester(client);
                client.DefaultRequestHeaders.Add("PRIVATE-TOKEN", _privateToken);
                while (true)
                {
                    var projects = await projectRequester.Get();
                    if (projects.Count() == 0)
                    {
                        break;
                    }
                    Parallel.ForEach(projects, async (project) =>
                    {
                        try
                        {
                            IssueRequester issueRequester = new IssueRequester(client);
                            var issues = await issueRequester.Get(project.Id);
                            var issuesGroupedByAssignee = issues.GroupBy(n => n.Assignee.Id);

                            foreach (var issueGroupedByAssignee in issuesGroupedByAssignee)
                            {
                                userCreator.Create(project, issueGroupedByAssignee);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }
            }
            return Ok(userCreator.UserDtos);
        }

    }
}
