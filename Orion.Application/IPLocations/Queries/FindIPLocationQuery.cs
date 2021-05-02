using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orion.Application.Common.Interfaces;
using Orion.Application.IPLocations.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orion.Application.IPLocations.Queries
{

	public class FindIPLocationQuery : IRequest<IPLocationVM>
	{
		#region Public members
		public string IP { get; set; }
		#endregion

		#region Handler			
		public class FindIPLocationQueryHandler : IRequestHandler<FindIPLocationQuery, IPLocationVM>
		{
			private readonly IMediator mediator;
			private readonly IOrionDbContext dbContext;

			public FindIPLocationQueryHandler(IMediator mediator, IOrionDbContext dbContext)
			{
				this.mediator = mediator;
				this.dbContext = dbContext;
			}

			public async Task<IPLocationVM> Handle(FindIPLocationQuery request, CancellationToken cancellationToken)
			{
				var _addr = await mediator.Send(new IP2LongCommand { IP = request.IP });

				var _result = await dbContext.IPLocations.Where(a => _addr <= a.IPTo && a.IPFrom <= _addr)
					.Select(a => new IPLocationVM
					{
						ID = a.ID,
						CityName = a.CityName,
						CountryCode = a.CountryCode,
						CountryName = a.CountryName,
						RegionName = a.RegionName,
						IPFrom = a.IPFrom,
						IPTo = a.IPTo,
					})
					.SingleOrDefaultAsync();

				return _result;
			}

		}
		#endregion
	}
}
