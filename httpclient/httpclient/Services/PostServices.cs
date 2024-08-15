using httpclient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace httpclient.Services
{
    internal class PostServices
    {
        private HttpClient httpClient;
        private ObservableCollection<Posts> posts;
        private JsonSerializerOptions jsonSerializerOptions;

        public PostServices()
        {
            httpClient = new HttpClient();
            jsonSerializerOptions= new JsonSerializerOptions { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

        }

        public async Task<ObservableCollection<Posts>> GetPostsAsync()
        {
            Uri uri = new Uri("https://jsonplaceholder.typicode.com/posts");
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    posts = JsonSerializer.Deserialize<ObservableCollection<Posts>>(content, jsonSerializerOptions);
                }
            }
            catch
            {
                
            }
            return posts;
        }
    }
}
