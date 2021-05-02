using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orion.Application.IPLocations.Commands
{

	public class IP2LongCommand : IRequest<long>
	{
		#region Public members
		public string IP { get; set; }
		#endregion

		#region Handler
		public class IP2LongCommandHandler : IRequestHandler<IP2LongCommand, long>
		{
			private readonly IMediator mediator;

			public IP2LongCommandHandler(IMediator mediator)
			{
				this.mediator = mediator;
			}

			public async Task<long> Handle(IP2LongCommand request, CancellationToken cancellationToken)
			{
				return ToInt(request.IP);
			}

			static long ToInt(string addr)
			{
				// careful of sign extension: convert to uint first;
				// unsigned NetworkToHostOrder ought to be provided.
				return (long)(uint)IPAddress.NetworkToHostOrder(
					 (int)IPAddress.Parse(addr).Address);
			}

			//static long ToInt(string addr)
			//{
			//	// careful of sign extension: convert to uint first;
			//	// unsigned NetworkToHostOrder ought to be provided.
			//	return (long)(uint)IPAddress.NetworkToHostOrder(
			//		 (int)BitConverter.ToUInt32(IPAddress.Parse(addr).GetAddressBytes().Reverse().ToArray()));
			//}
		}
		#endregion
	}
}
