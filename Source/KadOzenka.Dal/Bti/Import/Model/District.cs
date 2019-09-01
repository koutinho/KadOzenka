using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Postgres;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace CIPJS.DAL.Bti.Import.Model
{
    /*  DISTRICT_ID   NUMBER(10, 0) NOT NULL,
      OKRUG_ID      NUMBER(10, 0),
      SUBJECT_RF_ID NUMBER(10, 0),
      FULL_NAME     VARCHAR2(1000 BYTE),
      SHORT_NAME    VARCHAR2(1000 BYTE),
      NAME_FOR_SORT VARCHAR2(1000 BYTE),
      STEKS_CODE    NUMBER(10, 0),
      OMK_CODE      VARCHAR2(100 BYTE),
      TYPE_REF      NUMBER(10, 0),
      NAME          VARCHAR2(1000 BYTE),
      CODE_GIVC     VARCHAR2(500 BYTE)*/
    public class District : BaseAddressEntity
    {
        private string _tableName = "ref_addr_district";
        public long? DISTRICT_ID { get; set; }
        public long? OKRUG_ID { get; set; }

        public District(DataRow row, long? _DISTRICT_ID = null, long? _OKRUG_ID = null,
            long? _SUBJECT_RF_ID = null, string _FULL_NAME = null, string _SHORT_NAME = null,
            string _NAME_FOR_SORT = null, long? _STEKS_CODE = null,
            string _OMK_CODE = null, long? _TYPE_REF = null, string _NAME = null, string _CODE_GIVC = null)
            : base(row, _SUBJECT_RF_ID, _FULL_NAME, _SHORT_NAME, _NAME_FOR_SORT,
                _STEKS_CODE, _OMK_CODE, _NAME, _TYPE_REF, _CODE_GIVC)
        {
            if (!DISTRICT_ID.HasValue && _DISTRICT_ID.HasValue)
            {
                DISTRICT_ID = _DISTRICT_ID;
            }
            if (!OKRUG_ID.HasValue && _OKRUG_ID.HasValue)
            {
                OKRUG_ID = _OKRUG_ID;
            }
        }


        /// <summary>
        /// конструктор для поиска по STEKS_CODE для поиска для связок для улиц
        /// </summary>
        /// <param name="_STEKS_CODE"></param>
        /// <param name="_SUBJECT_RF_ID"></param>
        public District(long? _STEKS_CODE, long? _SUBJECT_RF_ID = 2)
            : base(null, subjectRfId: _SUBJECT_RF_ID, steksCode: _STEKS_CODE)
        { }

        #region реализация абстрактного класса

        public override string IsExistsQuery
        {
            get
            {
                if (DBMngr.Realty is PostgresDatabase)
                {
                    return $"with r1 as (select rad.district_id from {_tableName} rad where rad.okrug_id = @p_okrug_id and rad.steks_code = @p_steks_code), " +
                        $"r2 as (select rad.district_id from {_tableName} rad where rad.omk_code = @p_omk_code and rad.steks_code = @p_steks_code and rad.full_name = @p_full_name) " +
                        $"select district_id from r1 " +
                        $"union all select district_id from r2 where not exists(select district_id from r1) " +
                        $"union all select rad.district_id from {_tableName} rad where rad.subject_rf_id = @p_subject_rf_id and rad.steks_code = @p_steks_code and not exists(select district_id from r1) and not exists(select district_id from r2)";
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public override IEnumerable<DbParameter> IsExistsParameters
        {
            get
            {
                if (DBMngr.Realty is PostgresDatabase)
                {
                    yield return new NpgsqlParameter("p_okrug_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = OKRUG_ID ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_steks_code", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SteksCode ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_omk_code", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = OmkCode ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_full_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = FullName ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_subject_rf_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SubjectRfId ?? (object)DBNull.Value };
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public override string InsertQuery
        {
            get
            {
                if (DBMngr.Realty is PostgresDatabase)
                {
                    return $"insert into {_tableName} (district_id,okrug_id,subject_rf_id,full_name,short_name,name_for_sort,steks_code,omk_code,type_ref,name,code_givc) " +
                        $"values ((select coalesce(max(district_id),0)+1 from {_tableName}),@p_okrug_id,@p_subject_rf_id,@p_full_name,@p_short_name,@p_name_for_sort,@p_steks_code,@p_omk_code,@p_type_ref,@p_name,@p_code_givc) " +
                        $"returning district_id";
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
                yield return new NpgsqlParameter("p_okrug_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = OKRUG_ID ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_steks_code", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SteksCode ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_omk_code", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = OmkCode ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_full_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = FullName ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_subject_rf_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SubjectRfId ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_short_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = ShortName ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_name_for_sort", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = NameForSort ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_type_ref", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = TypeRef ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = Name ?? (object)DBNull.Value };
                yield return new NpgsqlParameter("p_code_givc", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = CodeGivc ?? (object)DBNull.Value };
            }
        }

        /// <summary>
        /// если длина аргумента 3, добавляем первый "0"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public override string GetOmkCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;
            if (code.Length == 3)
                return "0" + code;
            return code;
        }

        protected override long? GetTYPE_REF()
        {
            return 18;
        }

        #endregion

        public int GetOkrugKodBti()
        {
            return (int)(SteksCode.HasValue ? (SteksCode.Value / 100) * 100 : 0);
        }
    }
}
