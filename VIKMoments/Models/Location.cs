using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIKMoments.Models
{
    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string id { get; set; }
        public string street_address { get; set; }
        public string name { get; set; }
    }
}
