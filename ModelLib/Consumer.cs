using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib
{
    public class Consumer<T> : ConsumerBase<T, int>
    {
        private string URL;

        public Consumer(string url)
        {
            URL = url;
        }

        public async Task<List<T>> GetAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<List<T>>(() => client.GetAsync(URL));
            }
        }

        public async Task<List<T>> GetAsync(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<List<T>>(() => client.GetAsync(URL + uri));
            }
        }

        public async Task<T> GetOneAsync(int[] ids)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<T>(() => client.GetAsync(URL + RouteIds(ids)));
            }
        }

        public async Task<T> GetOneAsync(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<T>(() => client.GetAsync(URL + uri));
            }
        }

        public async Task<int> PostAsync(T item)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<int>(() => client.PostAsync(URL, content));
            }
        }

        public async Task<int> PostAsync(T item, string uri)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<int>(() => client.PostAsync(URL + uri, content));
            }
        }

        public async Task<int> PutAsync(T item, int[] ids)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<int>(() => client.PutAsync(URL + RouteIds(ids), content));
            }
        }

        public async Task<int> PutAsync(T item, string uri)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<int>(() => client.PutAsync(URL + uri, content));
            }
        }

        public async Task<int> DeleteAsync(int[] ids)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<int>(() => client.DeleteAsync(URL + RouteIds(ids)));
            }
        }

        public async Task<int> DeleteAsync(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<int>(() => client.DeleteAsync(URL + uri));
            }
        }
    }
}
