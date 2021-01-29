using System;
using TransportationManagementSystem.Helper;
using TransportationManagementSystem.Models;
using TransportationManagementSystem.Services.Contracts;

namespace TransportationManagementSystem.Services.Concretes
{
    public class Transportation : ITransportation
    {
        public Transportation() { }
        public Resource GetResources(string url)
        {
            HttpResponse<Resource> httpResponse = HttpHelper.Get<Resource>(url);
            if (httpResponse.StatusCode != "OK")
                throw new Exception(httpResponse.Message);
            return httpResponse.Result;
        }
        public Starship GetStarships(string url)
        {
            HttpResponse<Starship> httpResponse = HttpHelper.Get<Starship>(url);
            if (httpResponse.StatusCode != "OK")
                throw new Exception(httpResponse.Message);
            return httpResponse.Result;
        }
        public People GetPilots(string url)
        {
            HttpResponse<People> httpResponse = HttpHelper.Get<People>(url);
            if (httpResponse.StatusCode != "OK")
                throw new Exception(httpResponse.Message);
            return httpResponse.Result;
        }
    }
}
