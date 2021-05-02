using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orion.Application.IPLocations.Commands;
using Orion.Application.IPLocations.Queries;
using System.Net;
using System.Threading.Tasks;

namespace Orion.UnitTests
{
    [TestClass]
    public class SearchIPLocationTests
    {
        [TestMethod]
        public async Task CanSearch()
        {
            IPLocationVM _result = null;

            using (var _services = new TestServiceProvider())
            {
                var _mediator = _services.Mediator;

                _result = await _mediator.Send(new FindIPLocationQuery { IP = "180.191.245.112" });
            }

            Assert.IsNotNull(_result);
            Assert.AreEqual("PH", _result.CountryCode);
        }

        [TestMethod]
        public async Task CanConvertIP2Long()
        {
            using (var _services = new TestServiceProvider())
            {
                var _mediator = _services.Mediator;

                var _result = await _mediator.Send(new IP2LongCommand { IP = "64.233.187.99" });

                Assert.AreEqual(1089059683, _result);
            }
        }

        [TestMethod]
        public async Task CanConvertLong2IP()
        {
            using (var _services = new TestServiceProvider())
            {
                var _mediator = _services.Mediator;

                var _result = await _mediator.Send(new Long2IPCommand { Value = 1089059683 });

                Assert.AreEqual(IPAddress.Parse("64.233.187.99"), _result);
            }
        }
    }
}