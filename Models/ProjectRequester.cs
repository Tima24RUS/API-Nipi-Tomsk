using System.Text.Json;

namespace TomskNipi.DevelopProgress.Models
{
    public class ProjectRequester
    {
        private const string _adress = $"http://nipi-gl.tomsknipi.ru/api/v4/projects?per_page=100&page=";
        private int _page = 1;
        private HttpClient _httpClient;

        public ProjectRequester(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<IEnumerable<Project>> Get()
        {
            string url = $"{_adress}{_page}";
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                string projectContents = await response.Content.ReadAsStringAsync();
                var projects = JsonSerializer.Deserialize<IEnumerable<Project>>(projectContents,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _page++;

                return projects;
            }
        }
    }
}

