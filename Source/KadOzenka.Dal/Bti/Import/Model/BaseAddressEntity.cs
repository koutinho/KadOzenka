using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace CIPJS.DAL.Bti.Import.Model
{
    public abstract class BaseAddressEntity
    {
        /// <summary>
        /// кешируем значения для TYPE_REF
        /// </summary>
        protected static ConcurrentDictionary<Tuple<long, string>, long?> CasheTypeRef = new ConcurrentDictionary<Tuple<long, string>, long?>();

        public DataRow Row { get; protected set; }
        public long? SubjectRfId { get; protected set; }
        public string FullName { get; protected set; }
        public string ShortName { get; protected set; }
        public string NameForSort { get; protected set; }
        public long? SteksCode { get; protected set; }
        public string OmkCode { get; protected set; }
        public string Name { get; protected set; }
        public long? TypeRef { get; protected set; }
        public string CodeGivc { get; protected set; }

        public BaseAddressEntity(DataRow row, long? subjectRfId = 2, string fullName = null,
            string shortName = null, string nameForSort = null, long? steksCode = null,
            string omkCode = null, string name = null, long? typeRef = null, string codeGivc = null)
        {

            Row = row;
            CodeGivc = codeGivc;
            FullName = fullName;
            Name = name;
            NameForSort = nameForSort;
            OmkCode = omkCode;
            ShortName = shortName;
            SteksCode = steksCode;
            SubjectRfId = subjectRfId;
            TypeRef = typeRef;
            if (row != null)
            {
                Populate();
            }
        }


        public abstract string IsExistsQuery { get; }
        public abstract IEnumerable<DbParameter> IsExistsParameters { get; }
        public abstract string InsertQuery { get; }
        public abstract IEnumerable<DbParameter> InsertParameters { get; }
        protected virtual string UpdateQuery
        {
            get
            {
                return string.Empty;
            }
        }

        public string AsString
        {
            get
            {
                return ToString();
            }
        }

        /// <summary>
        /// заполняем поля из DataRow
        /// окрага и районы из FKLS
        /// улицы из FKUN
        /// </summary>
        protected virtual void Populate()
        {
            SubjectRfId = 2;//Москва
            FullName = Row.GetValueOrDefault<string>("NM");

            //NM вместо NAK, т.к недоступно в таблице KLS
            ShortName = Row.GetValueOrDefault<string>("NM");

            //NM вместо NNP, т.к недоступно в таблице KLS
            NameForSort = Row.GetValueOrDefault<string>("NM");
            SteksCode = Row.GetValueOrDefault<long?>("KOD");
            CodeGivc = Convert.ToString(SteksCode);
            OmkCode = GetOmkCode(Convert.ToString(SteksCode));

            //NM вместо NAK, т.к недоступно в таблице KLS
            Name = Row.GetValueOrDefault<string>("NM");
            TypeRef = GetTYPE_REF();
        }

        protected abstract long? GetTYPE_REF();

        public virtual long? Merge()
        {
            long? d;
            if (!IsExists(out d))
            {
                d = Insert();
            }
            return d;
        }

        /// <summary>
        /// проверяем существование записи
        /// </summary>
        /// <param name="id">если запись существует, возвращаем ее идентификатор</param>
        /// <returns></returns>
        public virtual bool IsExists(out long? id)
        {
            id = null;
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(IsExistsQuery);
            command.Parameters.AddRange(IsExistsParameters.ToArray());
            object value = DBMngr.Realty.ExecuteScalar(command);
            if (value != null && value != DBNull.Value) {
                id = value.ParseToLong();
            }
            return id.HasValue;
        }

        /// <summary>
        /// вставляем, если запись отсутствует
        /// </summary>
        public virtual long? Insert()
        {
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(InsertQuery);
            command.Parameters.AddRange(InsertParameters.ToArray());
            return DBMngr.Realty.ExecuteScalar(command).ParseToLong();
        }

        public abstract string GetOmkCode(string code);

        protected long? GetTYPE_REF(long pReferenceid, string pCode)
        {
            long? res = null;
            var key = Tuple.Create(pReferenceid, pCode);
            if (CasheTypeRef.ContainsKey(key))
            {
                return CasheTypeRef[key];
            }

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(@"select cri.itemid
from core_reference_item cri
where cri.referenceid = @p_referenceid
and cri.code = @p_code");
            DbParameter idReferenceParameter = command.CreateParameter();
            idReferenceParameter.ParameterName = "p_referenceid";
            idReferenceParameter.DbType = DbType.Int64;
            idReferenceParameter.Value = pReferenceid;
            command.Parameters.Add(idReferenceParameter);
            DbParameter codeParameter = command.CreateParameter();
            codeParameter.ParameterName = "p_code";
            codeParameter.DbType = DbType.AnsiString;
            codeParameter.Value = pCode;
            command.Parameters.Add(codeParameter);
            object obj = DBMngr.Realty.ExecuteScalar(command);
            if (obj != null && obj != DBNull.Value)
            {
                res = obj.ParseToLong();
            }
            CasheTypeRef.TryAdd(key, res);
            return res;
        }

        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (var p in GetType().GetProperties())
                {
                    sb.AppendFormat("{0} - {1}", p.Name, p.GetValue(this, null));
                }
                return sb.ToString();
            }
            catch (Exception exc)
            {
                return base.ToString() + Environment.NewLine + exc;
            }
        }
    }
}
