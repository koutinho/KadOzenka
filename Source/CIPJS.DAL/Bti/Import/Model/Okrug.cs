using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data.Postgres;
using Npgsql;
using NpgsqlTypes;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace CIPJS.DAL.Bti.Import.Model
{
    /*OKRUG_ID      NUMBER(10, 0) NOT NULL,
      SUBJECT_RF_ID NUMBER(10, 0),
      FULL_NAME     VARCHAR2(1000 BYTE),
      SHORT_NAME    VARCHAR2(1000 BYTE),
      NAME_FOR_SORT VARCHAR2(1000 BYTE),
      STEKS_CODE    NUMBER(10, 0),
      OMK_CODE      VARCHAR2(100 BYTE),
      NAME          VARCHAR2(1000 BYTE),
      TYPE_REF      NUMBER(10, 0),
      CODE_GIVC     VARCHAR2(500 BYTE)*/
    public class Okrug : BaseAddressEntity
    {
        private string _tableName = "ref_addr_okrug";

        public decimal OKRUG_ID { get; private set; }

        public Okrug(DataRow row, decimal _OKRUG_ID, long? _SUBJECT_RF_ID = null, string _FULL_NAME = null, string _SHORT_NAME = null, string _NAME_FOR_SORT = null, long? _STEKS_CODE = null,
            string _OMK_CODE = null, string _NAME = null, long? _TYPE_REF = null, string _CODE_GIVC = null)
            : this(row, _SUBJECT_RF_ID, _FULL_NAME, _SHORT_NAME, _NAME_FOR_SORT,
                _STEKS_CODE, _OMK_CODE, _NAME, _TYPE_REF, _CODE_GIVC)
        {
            this.OKRUG_ID = _OKRUG_ID;
        }
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="_SUBJECT_RF_ID">субьект РФ 2 Москва</param>
        /// <param name="_FULL_NAME">полное наименование поле NM</param>
        /// <param name="_SHORT_NAME">Краткое наименование поля NAK</param>
        /// <param name="_NAME_FOR_SORT">Наименование для сортировки NNP</param>
        /// <param name="_STEKS_CODE">KOD</param>
        /// <param name="_OMK_CODE">P1</param>
        /// <param name="_NAME">NM</param>
        /// <param name="_TYPE_REF">всегда 16</param>
        /// <param name="_CODE_GIVC">вроде совпадает с KOD</param>
        public Okrug(DataRow row, long? _SUBJECT_RF_ID = null, string _FULL_NAME = null, string _SHORT_NAME = null, string _NAME_FOR_SORT = null, long? _STEKS_CODE = null,
           string _OMK_CODE = null, string _NAME = null, long? _TYPE_REF = null, string _CODE_GIVC = null)
            : base(row, _SUBJECT_RF_ID, _FULL_NAME, _SHORT_NAME, _NAME_FOR_SORT,
                _STEKS_CODE, _OMK_CODE, _NAME, _TYPE_REF, _CODE_GIVC)
        { }
        #region реализация абстрактного класса
        public override string IsExistsQuery
        {
            get
            {
                return $"select rao.okrug_id from {_tableName} rao where rao.steks_code = @p_steks_code";
            }
        }

        public override IEnumerable<DbParameter> IsExistsParameters
        {
            get
            {
                if (DBMngr.Realty is PostgresDatabase)
                {
                    yield return new NpgsqlParameter("p_steks_code", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SteksCode ?? (object)DBNull.Value };
                }
                else if (DBMngr.Realty is OracleDatabase)
                {
                    yield return new OracleParameter("p_steks_code", OracleDbType.Int64, SteksCode, System.Data.ParameterDirection.Input);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        public override long? Insert()
        {
            return base.Insert();
        }
        public override string InsertQuery
        {
            get
            {
                if (DBMngr.Realty is PostgresDatabase)
                {
                    return $"insert into {_tableName} (okrug_id,subject_rf_id,full_name,short_name,name_for_sort,steks_code,omk_code,name,type_ref,code_givc) " +
                        $"values ((select coalesce(max(okrug_id),0)+1 from {_tableName}),@p_subject_rf_id,@p_full_name,@p_short_name,@p_name_for_sort,@p_steks_code,@p_omk_code,@p_name,@p_type_ref,@p_code_givc) " +
                        $"returning okrug_id";
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public override IEnumerable<DbParameter> InsertParameters
        {
            get
            {
                if (DBMngr.Realty is PostgresDatabase)
                {
                    yield return new NpgsqlParameter("p_subject_rf_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SubjectRfId ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_full_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = FullName ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_short_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = ShortName ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_name_for_sort", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = NameForSort ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_steks_code", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SteksCode ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_omk_code", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = OmkCode ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = Name ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_type_ref", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = TypeRef ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_code_givc", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = CodeGivc ?? (object)DBNull.Value };
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        protected override void Populate()
        {
            base.Populate();
            string tmp = Convert.ToString(SteksCode);
            this.OmkCode = GetOmkCode(tmp);
        }
        protected override long? GetTYPE_REF()
        {
            return 16;
        }
        /// <summary>
        /// из строки типа 1 получаем 0100
        /// из 11 получаем 1100
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public override string GetOmkCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }

            return code.PadLeft(4, '0');
        }
        #endregion

    }
}
