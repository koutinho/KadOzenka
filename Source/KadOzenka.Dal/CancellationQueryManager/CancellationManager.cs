using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.QuerySubsystem;
using Core.Register.QuerySubsystem.SqlExecutor;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Serilog;

namespace KadOzenka.Dal.CancellationQueryManager
{
	public class CancellationManager
	{
		public CancellationToken BaseCancellationToken = CancellationToken.None;

		private readonly ILogger _log = Log.ForContext<CancellationManager>();

		public CancellationManager()
		{
		}

		public List<T> ExecuteQuery<T>(QSQuery<T> query) where T : class, new()
		{
			var cTokenSource = new CancellationTokenSource();
			try
			{
				StartSubscriber(cTokenSource, query);
				var res = query.Execute();
				CancelSubscriber(cTokenSource);
				return res;
			}
			catch (Exception e)
			{
				_log.Error("Ошбка во время запроса данных для отччета {e}", e);
				CancelSubscriber(cTokenSource);
				return new List<T>();
			}
		
		}

		public List<TResult> ExecuteSelect<TSource, TResult>(QSQuery<TSource> query, Expression<Func<TSource, TResult>> expr) where TSource : class, new()
		{
			var cTokenSource = new CancellationTokenSource();
			try
			{
				StartSubscriber(cTokenSource,query);
				var res = query.ExecuteSelect(expr);
				CancelSubscriber(cTokenSource);

				return res;
			}
			catch (Exception e)
			{
				_log.Error("Ошбка во время запроса данных для отччета {e}", e);
				CancelSubscriber(cTokenSource);
				return new List<TResult>();
			}
		}

		public List<TResult> ExecuteSql<TResult>(string sql) where TResult : class, new()
		{
			var cTokenSource = new CancellationTokenSource();
			try
			{
				var executor =  QSQuery.GetSqlExecutor<TResult>(sql);
				StartSubscriber(cTokenSource, executor:executor);
				var res = executor.ExecuteSql();
				CancelSubscriber(cTokenSource);
				return res;
			}
			catch (Exception e)
			{
				_log.Error("Ошбка во время запроса данных для отччета {e}", e);
				CancelSubscriber(cTokenSource);
				return new List<TResult>();
			}
		}

		public DataSet ExecuteSqlStringToDataSet(string sql)
		{
			var cTokenSource = new CancellationTokenSource();
			try
			{
				var command = DBMngr.Main.GetSqlStringCommand(sql);
				var executor = new Executor<DataTable>(command, (dbCommand, key, query) =>  new List<DataTable>());
				StartSubscriber(cTokenSource, executor: executor);
				var dataSet = DBMngr.Main.ExecuteDataSet(command);
				CancelSubscriber(cTokenSource);
				return dataSet;
			}
			catch (Exception e)
			{
				_log.Error("Ошбка во время запроса данных для отччета {e}", e);
				CancelSubscriber(cTokenSource);
				return new DataSet();
			}
		}

		public void CancelSubscriber(CancellationTokenSource cTokenSource)
		{
			cTokenSource.Cancel();
		}

		public CancellationToken GetReportCancellationToken()
		{
			return BaseCancellationToken;
		}

		private void CreateSubscriberToCancellationRequest<T>(CancellationTokenSource cTokenSource, QSQuery<T> query, Executor<T> executor) where T: class, new()
		{
			try
			{
				Task task = new Task(() =>
				{
					while (true)
					{
						if (cTokenSource.IsCancellationRequested)
						{
							break;
						}

						if (BaseCancellationToken.IsCancellationRequested)
						{
							query?.CancelExecuting();
							executor?.CancelExecute();
							break;
						}
						Thread.Sleep(1000);
					}
				});
				task.Start();
			}
			catch (OperationCanceledException e)
			{
				_log.Debug("Отмена ожидания вызова токена отмены для отчетов");
			}
		}

		#region support methods

		private void StartSubscriber<T>(CancellationTokenSource cTokenSource, QSQuery<T> query = null, Executor<T> executor = null) where T: class, new()
		{
			CreateSubscriberToCancellationRequest(cTokenSource,query, executor);
		}


		#endregion
	}
}