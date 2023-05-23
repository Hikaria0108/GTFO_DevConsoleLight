using System.Threading.Tasks;
using System.Net.Http;
using System;
using Steamworks;
using System.Linq;
using System.Threading;
using Hikaria.DevConsoleLight;
using Il2CppSystem.Collections.Generic;

namespace Hikaria.DevConsoleLight.Utils
{
    internal static class HttpHelper
    {
        public static async Task<string[]> Get(string url)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10); //设置10秒超时
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                string[] splitContent = content.Split('\n');
                if (splitContent[0] != "404: Not Found")
                {
                    return splitContent;
                }
                return null;
            }
            return null;
        }
    }
}
