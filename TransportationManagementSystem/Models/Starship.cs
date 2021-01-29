using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationManagementSystem.Models
{
    public class Starship
    {
        public string Next { get; set; }
        public List<StarhipDetails> Results { get; set; }
    }
    public class StarhipDetails
    {
        public string Name { get; set; }
        public string Passengers { get; set; }
        public string[] Pilots { get; set; }
    }
}
