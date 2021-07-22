#nullable enable
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.QuerySubsystem;
using Core.Register.QuerySubsystem.SqlExecutor;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Npgsql;
using Serilog;

namespace CommonSdks
{
	/// <summary>
	/// Менеджер для отмены запросов
	/// </summary>
	public class QueryManager
	{
		private CancellationToken _baseCancellationToken = CancellationToken.None;

		private readonly ILogger _log = Log.ForContext<QueryManager>();

		//https://www.postgresql.org/docs/9.4/errcodes-appendix.html
		private readonly string _errorCodeQueryCanceled = "57014";

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
			catch (PostgresException e)
			{
				if (!e.SqlState.Equals(_errorCodeQueryCanceled))
				{
					throw;
				}
				_log.Error("Ошибка во время запроса данных {e}", e);
				CancelSubscriber(cTokenSource);
				return new List<T>();
			}
		
		}

		public List<T> ExecuteQuery<T>(QSQuery query) where T : class, new()
		{
			var cTokenSource = new CancellationTokenSource();
			try
			{
				StartSubscriber(cTokenSource, query);
				var res = query.ExecuteQuery<T>();
				CancelSubscriber(cTokenSource);
				return res;
			}
			catch (PostgresException e)
			{
				if (!e.SqlState.Equals(_errorCodeQueryCanceled))
				{
					throw;
				}
				_log.Error("Ошибка во время запроса данных {e}", e);
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
			catch (PostgresException e)
			{
				if (!e.SqlState.Equals(_errorCodeQueryCanceled))
				{
					throw;
				}
				_log.Error("Ошибка во время запроса данных {e}", e);
				CancelSubscriber(cTokenSource);
				return new List<TResult>();
			}
		}

		public List<TResult> ExecuteSql<TResult>(string sql) where TResult : class, new()
		{
			var cTokenSource = new CancellationTokenSource();
			try
			{
				var executor = QSQuery.GetSqlExecutor<TResult>(sql);
				StartSubscriber(cTokenSource, executor: executor);
				var res = executor.ExecuteSql();
				CancelSubscriber(cTokenSource);
				return res;
			}
			catch (PostgresException e)
			{
				if (!e.SqlState.Equals(_errorCodeQueryCanceled))
				{
					throw;
				}

				_log.Error("Ошибка во время запроса данных {e}", e);
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
			catch (PostgresException  e)
			{
				if (!e.SqlState.Equals(_errorCodeQueryCanceled))
				{
					throw;
				}
				_log.Error("Ошибка во время запроса данных {e}", e);
				CancelSubscriber(cTokenSource);
				return new DataSet();
			}
		}

		public int ExecuteCount<TSource>(QSQuery<TSource> query) where TSource : class, new ()
		{
			var cTokenSource = new CancellationTokenSource();

			try
			{
				StartSubscriber(cTokenSource, query);
				var res = query.ExecuteCount();
				CancelSubscriber(cTokenSource);

				return res;
			}
			catch (PostgresException e)
			{
				if (!e.SqlState.Equals(_errorCodeQueryCanceled))
				{
					throw;
				}
				_log.Error("Ошибка во время запроса данных {e}", e);
				CancelSubscriber(cTokenSource);
				return 0;
			}
			
		}

		public DataTable ExecuteQueryToDataTable(QSQuery query)
		{
			var cTokenSource = new CancellationTokenSource();
			try
			{
				StartSubscriber(cTokenSource, query);
				var res = query.ExecuteQuery();
				CancelSubscriber(cTokenSource);
				return res;
			}
			catch (PostgresException e)
			{
				if (!e.SqlState.Equals(_errorCodeQueryCanceled))
				{
					throw;
				}
				_log.Error("Ошибка во время запроса данных {e}", e);
				CancelSubscriber(cTokenSource);
				return new DataTable();
			}
			
		}

		public void SetBaseToken(CancellationToken? token)
		{
			if (token != null)
			{
				_baseCancellationToken = token.GetValueOrDefault();
			}
			
		}

		public bool IsRequestCancellationToken()
		{
			return _baseCancellationToken.IsCancellationRequested;
		}


		#region private methods

		private void CancelSubscriber(CancellationTokenSource cTokenSource)
		{
			cTokenSource.Cancel();
		}

		private void CreateSubscriberToCancellationRequest<T>(CancellationTokenSource cTokenSource, QSQuery<T>? query = null, Executor<T>? executor = null, QSQuery? simpleQuery = null) where T : class, new()
		{
			try
			{
				Task task = new Task(() =>
				{
					while (true)
					{
						if (cTokenSource.IsCancellationRequested)
						{
							_log.ForContext("==> Query", query?.GetSql()).Debug("Отмена ожидания вызова токена отмены");
							break;
						}

						if (_baseCancellationToken.IsCancellationRequested)
						{
							_log.ForContext("==> Query", query?.GetSql()).Debug("Отмена запроса");
							query?.CancelExecuting();
							executor?.CancelExecute();
							simpleQuery?.CancelExecuting();
							break;
						}
						Thread.Sleep(1000);
					}
				});
				task.Start();
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				throw;
			}
		}

		#region support methods

		private void StartSubscriber<T>(CancellationTokenSource cTokenSource, QSQuery<T>? query = null, Executor<T>? executor = null) where T : class, new()
		{
			if (_baseCancellationToken == CancellationToken.None)
			{
				var names = query?.GetType().GenericTypeArguments.Select(x => x.Name) ??
				            executor?.GetType().GenericTypeArguments.Select(x => x.Name) ?? new List<string>();
				_log.ForContext("ObjectType", names, true)
					.Warning("Базовый токен отмены не установлен, менджер работает как обычный запрос без функции отмены");
			}

			CreateSubscriberToCancellationRequest(cTokenSource, query, executor);
		}

		private void StartSubscriber(CancellationTokenSource cTokenSource, QSQuery? simpleQuery = null)
		{
			if (_baseCancellationToken == CancellationToken.None)
			{
				_log.Warning("Базовый токен отмены не установлен, менджер работает как обычный запрос без функции отмены");
			}

			CreateSubscriberToCancellationRequest<object>(cTokenSource,  simpleQuery: simpleQuery);
		}

		#endregion

		#endregion

	}
}