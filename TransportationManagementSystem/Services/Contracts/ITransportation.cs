using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Services.Contracts
{
    public interface ITransportation
    {
        Resource GetResources(string url);
        Starship GetStarships(string url);
        People GetPilots(string url);
    }
}
