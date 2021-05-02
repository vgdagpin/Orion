using Orion.Application.IPLocations.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace Orion.UI.Models
{
    public class IPLocationMapping : CsvMapping<IPLocationVM>
    {
        public IPLocationMapping() : base()
        {
            MapProperty(0, a => a.IPFrom);
            MapProperty(1, a => a.IPTo);
            MapProperty(2, a => a.CountryCode);
            MapProperty(3, a => a.CountryName);
            MapProperty(4, a => a.RegionName);
            MapProperty(5, a => a.CityName);
        }
    }
}
