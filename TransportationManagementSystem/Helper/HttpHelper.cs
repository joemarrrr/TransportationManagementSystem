using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Helper
{
    public class HttpHelper
    {
        public static HttpResponse<T> Get<T>(string url, string parameters = null)
        {
            HttpResponse<T> httpResponse = new HttpResponse<T>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(parameters ?? "").Result;
                if (response.IsSuccessStatusCode)
                {
                    httpResponse.StatusCode = response.StatusCode.ToString();
                    httpResponse.Result = response.Content.ReadAsAsync<T>().Result;
                }
                else
                {
                    httpResponse.StatusCode = response.StatusCode.ToString();
                    httpResponse.Message = response.ReasonPhrase;
                }
            }
            return httpResponse;
        }
    }
}
