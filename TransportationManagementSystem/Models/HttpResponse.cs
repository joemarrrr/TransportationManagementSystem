using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationManagementSystem.Models
{
    public class HttpResponse<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
    }
}
