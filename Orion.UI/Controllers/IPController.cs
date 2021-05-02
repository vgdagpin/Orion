using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orion.Application.IPLocations.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.UI.Controllers
{
    public class IPController : Controller
    {
        private readonly IMediator mediator;

        public IPController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("/IP/{ip}")]
        public async Task<IActionResult> Index(string ip)
        {
            var _result = await mediator.Send(new FindIPLocationQuery { IP = ip });

            if (_result== null)
            {
                return Json(null);
            }

            return Json(new
            {
                _result.CountryCode,
                _result.CountryName,
                _result.RegionName,
                _result.CityName
            });
        }
    }
}