using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace QTestApiDemo
{
    class QTestApi
    {
        private string HostName { get; set; }
        private string Token { get; set; }

        public QTestApi(string hostname)
        {
            this.HostName = hostname;
            this.Token = "";
        }

        internal object CreateProject(string v)
        {
            throw new NotImplementedException();
        }

        public String Login(string username, string password)
        {
            if (Token == "")
            {
                Token = "bearer " + DoLoginTask(username, password).Result;
            }
            return Token;
        }

        private async Task<String> DoLoginTask(string username, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(HostName);
            client.DefaultRequestHeaders.Add("Authorization", "Basic bGluaC1sb2dpbjo=");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });
            var result = client.PostAsync("/oauth/token", content).Result;
            var resultBody = await result.Content.ReadAsStringAsync();
            Dictionary<string, object> jsonToken =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(resultBody);
            return jsonToken["access_token"] as String;
        }

        public void Logout()
        {      
            if (Token == "")
            {
                throw new CommonApiException("You must do login first");
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(HostName);
            client.DefaultRequestHeaders.Add("Authorization", Token);
            client.PostAsync("/oauth/revoke", null);
            Token = "";
        }

        public string CreateProject(string name, string description, DateTime starDate, DateTime endDate)
        {
            if (Token == "")
            {
                throw new CommonApiException("You must do login first");
            }
            var project = new ProjectJson()
            {
                name = name,
                description = description,
                startDate = starDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"),
                endDate = endDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'")
            };
            return DoCreateProjectTask(project).Result;
        }

        private async Task<String> DoCreateProjectTask(ProjectJson project)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(HostName);
            client.DefaultRequestHeaders.Add("Authorization", Token);
            var result = client.PostAsJsonAsync("/api/v3/projects", project).Result;
            var resultBody = await result.Content.ReadAsStringAsync();
            Dictionary<string, object> jsonToken =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(resultBody);
            return jsonToken["id"] as String;
        }

        public string CreateTestModule(string projectId, string name, string description)
        {
            if (Token == "")
            {
                throw new CommonApiException("You must do login first");
            }
            var module = new ModuleJson()
            {
                name = name,
                description = description
            };
            return DoCreateTestModuleTask(projectId, module).Result;
        }

        private async Task<String> DoCreateTestModuleTask(string projectId, ModuleJson module)
        {
            string uri = "/api/v3/projects/{#projectId}/modules";
            uri = uri.Replace("{#projectId}", projectId);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(HostName);
            client.DefaultRequestHeaders.Add("Authorization", Token);
            var result = client.PostAsJsonAsync(uri, module).Result;
            var resultBody = await result.Content.ReadAsStringAsync();
            Dictionary<string, object> jsonToken =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(resultBody);
            return jsonToken["id"] as String;
        }

    }
}
