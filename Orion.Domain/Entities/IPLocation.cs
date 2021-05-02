using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Orion.Domain.Entities
{
    public class IPLocation
    {
        public int ID { get; set; }
        public double IPFrom { get; set; }
        public double IPTo { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
    }
}
