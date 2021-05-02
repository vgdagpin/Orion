using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Orion.Application.Common.Interfaces;
using Orion.Application.IPLocations.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orion.Application.IPLocations.Commands
{

	public class BulkInsertIPLocationCommand : IRequest
	{
		#region Public members
		public IEnumerable<IPLocationVM> Data { get; set; }
		#endregion

		#region Handler
		public class BulkInsertIPLocationCommandHandler : IRequestHandler<BulkInsertIPLocationCommand>
		{
			private readonly IConfiguration configuration;

			public BulkInsertIPLocationCommandHandler(IConfiguration configuration)
			{
				this.configuration = configuration;
			}

			public async Task<Unit> Handle(BulkInsertIPLocationCommand request, CancellationToken cancellationToken)
			{
				DataTable _dt = request.Data.ToDataTable();

				using (var _connection = new SqlConnection(configuration.GetConnectionString("OrionDBConStr")))
				{
					var bulkCopy = new SqlBulkCopy
						(
							connection: _connection,
							copyOptions: SqlBulkCopyOptions.TableLock |
								SqlBulkCopyOptions.FireTriggers |
								SqlBulkCopyOptions.UseInternalTransaction,
							externalTransaction: null
						);

					bulkCopy.DestinationTableName = "dbo.IPLocations";

					await _connection.OpenAsync();
					await bulkCopy.WriteToServerAsync(_dt);
					await _connection.CloseAsync();
				}

				_dt.Clear();

				return Unit.Value;
			}
		}
		#endregion
	}
}
