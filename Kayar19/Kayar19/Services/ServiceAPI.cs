using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kayar19.Models;
using Newtonsoft.Json;

namespace Kayar19.Services
{
    public class ServiceAPI
    {
        public static async Task<List<User>> GetUsersAsync(string access_token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Authorization", "Bearer" + access_token);

            var json = await client.GetStringAsync(Constants.BaseAddress + "/Users");

            var users = JsonConvert.DeserializeObject<List<User>>(json);

            return users;
        }


    }
}
