using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orion.Application.IPLocations.Commands
{

	public class Long2IPCommand : IRequest<IPAddress>
	{
		#region Public members
		public long Value { get; set; }
		#endregion

		#region Handler
		public class Long2IPCommandHandler : IRequestHandler<Long2IPCommand, IPAddress>
		{
			private readonly IMediator mediator;

			public Long2IPCommandHandler(IMediator mediator)
			{
				this.mediator = mediator;
			}

			public async Task<IPAddress> Handle(Long2IPCommand request, CancellationToken cancellationToken)
			{
				return IPAddress.Parse(request.Value.ToString());
			}
		}
		#endregion
	}
}
