using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orion.Application.IPLocations.Commands;
using Orion.Application.IPLocations.Queries;
using Orion.UI.Models;
using TinyCsvParser;

namespace Orion.UI.Controllers
{
    public class HomeController : Controller
    {        
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Index()
        {           
            return View();
        }

        public async Task<IActionResult> Seed()
        {
            CsvParserOptions _opt = new CsvParserOptions(false, ',');
            IPLocationMapping _mapping = new IPLocationMapping();

            CsvParser<IPLocationVM> _parser = new CsvParser<IPLocationVM>(_opt, _mapping);

            var _result = _parser.ReadFromFile(@"D:\Downloads\IP2LOCATION-LITE-DB3.CSV\IP2LOCATION-LITE-DB3.CSV", Encoding.ASCII)
                .Select(a => a.Result)
                .ToList();

            await mediator.Send(new BulkInsertIPLocationCommand { Data = _result });

            return Json(true);
        }       
    }
}
