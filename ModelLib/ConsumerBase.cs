using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ModelLib
{
    public abstract class ConsumerBase<T, TB>
    {
        protected virtual async Task<TC> HandleHTTPResponseAsync<TC>(Func<Task<HttpResponseMessage>> clientResponse)
        {
            HttpResponseMessage response = await clientResponse();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                TC content = JsonConvert.DeserializeObject<TC>(jsonString);
                return content;
            }
            throw new HttpRequestException(response.StatusCode.ToString());
        }

        protected virtual string RouteIds(TB[] ids)
        {
            string route = "";
            foreach (TB id in ids)
            {
                route += "/" + id;
            }

            return route;
        }

        protected virtual StringContent EncodeContent(T item)
        {
            string jsonStr = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
