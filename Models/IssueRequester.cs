using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace TomskNipi.DevelopProgress.Models
{
    public class IssueRequester
    {
        private HttpClient _httpClient = new HttpClient();
        private const string _privateToken = /*"PiqnVsX-RwSfFemeiSPa";*/ /*"qiAsL_QhxA4tD5Yv3tcx1";*/"xbKpA1RWSCyNpyz_MZTH";


        public IssueRequester(HttpClient client)
        {
            _httpClient.DefaultRequestHeaders.Add("PRIVATE-TOKEN", _privateToken);
        }

        public async Task<IEnumerable<Issue>> Get(int projectId)
        {
            string _url;
            //string url = $"http://nipi-gl.tomsknipi.ru/api/v4/projects/{projectId}/issues?Any+labels=Doing,InProgress";
            string address = "http://nipi-gl.tomsknipi.ru/api/v4/projects/";
            string endOfAddress = "/issues?Any+labels=Doing,InProgress";
            _url = $"{address}{projectId}{endOfAddress}";
            using (HttpResponseMessage response = await _httpClient.GetAsync(_url))
            {
                string issueContent = await response.Content.ReadAsStringAsync();
                IEnumerable<Issue>? issues = JsonSerializer.Deserialize<IEnumerable<Issue>>(issueContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return issues.Where(n => n.Assignee != null);

            }
        }

    }
}

