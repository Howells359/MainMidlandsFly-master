using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Models
{
    public class HomeAddress
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Locality { get; set; }
        public string TownOrCity { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
    }
}
