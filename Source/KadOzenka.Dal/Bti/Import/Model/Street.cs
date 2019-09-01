using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data.Postgres;
using Npgsql;
using NpgsqlTypes;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CIPJS.DAL.Bti.Import.Model
{
    public class Street : BaseAddressEntity
    {
        readonly IList<long> _districts = new List<long>();
        /// <summary>
        /// районы привязанные к улице
        /// </summary>
        public IList<long> Districts
        {
            get
            {
                return _districts;
            }
        }

        readonly IList<long> _okrugs = new List<long>();
        /// <summary>
        /// округа привязанные к улице
        /// </summary>
        public IList<long> Okrugs
        {
            get
            {
                return _okrugs;
            }
        }

        public long? StreetId { get; private set; }
        public long? PseId { get; private set; }
        public long? TownId { get; private set; }
        public string KladrCode { get; private set; }

        public Street(DataRow row, long? streetId = null, IEnumerable<long> districts = null,
            long? pseId = null, long? townId = null, long? subjectRfId = null,
            string fullName = null, string shortName = null, string nameForSort = null,
            long? steksCode = null,
            string omkCode = null, long? typeRef = null, string kladrCode = null, string name = null,
            string codeGivc = null)
            : base(row, subjectRfId, fullName, shortName, nameForSort,
                steksCode, omkCode, name, typeRef, codeGivc)
        {
            KladrCode = kladrCode;
            PseId = pseId;
            StreetId = streetId;
            TownId = townId;
            if (districts != null)
                Districts.AddItems(districts);
        }

        public Street(string codeGivc = null, long? subjectRfId = null)
            : base(null, codeGivc: codeGivc, subjectRfId: subjectRfId)
        { }


        #region реализация абстрактного класса
        public override string IsExistsQuery
        {
            get
            {
                return $"select ras.street_id from {Importer.RefAddrStreetTableName} ras where ras.subject_rf_id = @p_subject_rf_id and ras.code_givc = @p_code_givc";
            }
        }

        public override IEnumerable<DbParameter> IsExistsParameters
        {
            get
            {
                if (DBMngr.Realty is PostgresDatabase)
                {
                    yield return new NpgsqlParameter("p_subject_rf_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SubjectRfId ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_code_givc", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = CodeGivc ?? (object)DBNull.Value };
                }
                else if (DBMngr.Realty is OracleDatabase)
                {
                    yield return new OracleParameter("p_subject_rf_id", OracleDbType.Int64, SubjectRfId, ParameterDirection.Input);
                    yield return new OracleParameter("p_code_givc", OracleDbType.Varchar2, CodeGivc, ParameterDirection.Input);
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
                    return $"insert into {Importer.RefAddrStreetTableName} (street_id,pse_id,town_id,subject_rf_id,full_name,short_name,name_for_sort,steks_code,omk_code,type_ref,kladr_code,name,code_givc) " +
                        $"values ((select coalesce(max(street_id),0)+1 from {Importer.RefAddrStreetTableName}),@p_pse_id,@p_town_id,@p_subject_rf_id,@p_full_name,@p_short_name,@p_name_for_sort,@p_steks_code,@p_omk_code,@p_type_ref,@p_kladr_code,@p_name,@p_code_givc) " +
                        $"returning street_id";
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// у улиц другая таблица
        /// </summary>
        protected override void Populate()
        {
            SubjectRfId = 2;
            FullName = Row.GetValueOrDefault<string>("NM");
            ShortName = Row.GetValueOrDefault<string>("NAK");

            //MANIN_NM вместо MAIN_NM
            NameForSort = Row.GetValueOrDefault<string>("MANIN_NM");
            long stCode = Row.GetValueOrDefault<long>("UL_OLD");
            SteksCode = stCode == 0 ? (long?)null : stCode;
            double codGivz = Row.GetValueOrDefault<double>("KOD_GIVZ");
            CodeGivc = codGivz == 0 ? null : codGivz.ToString();
            double omkCode = Row.GetValueOrDefault<double>("KOD_FO");
            OmkCode = omkCode == 0 ? null : omkCode.ToString();
            Name = Row.GetValueOrDefault<string>("NM");
            string tpTop = Convert.ToString(Row.GetValueOrDefault<double>("TP_TOP"));
            TypeRef = GetTYPE_REF(15, tpTop);
        }

        public override IEnumerable<DbParameter> InsertParameters
        {
            get
            {
                if (DBMngr.Realty is PostgresDatabase)
                {
                    yield return new NpgsqlParameter("p_pse_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = PseId ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_town_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = TownId ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_subject_rf_id", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SubjectRfId ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_full_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = FullName ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_short_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = ShortName ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_name_for_sort", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = NameForSort ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_steks_code", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = SteksCode ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_omk_code", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = OmkCode ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_type_ref", NpgsqlDbType.Bigint) { Direction = ParameterDirection.Input, Value = TypeRef ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_kladr_code", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = KladrCode ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_name", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = Name ?? (object)DBNull.Value };
                    yield return new NpgsqlParameter("p_code_givc", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Input, Value = CodeGivc ?? (object)DBNull.Value };
                }
                else if (DBMngr.Realty is OracleDatabase)
                {
                    yield return new OracleParameter("p_pse_id", OracleDbType.Int64, PseId, ParameterDirection.Input);
                    yield return new OracleParameter("p_town_id", OracleDbType.Int64, TownId, ParameterDirection.Input);
                    yield return new OracleParameter("p_subject_rf_id", OracleDbType.Int64, SubjectRfId, ParameterDirection.Input);
                    yield return new OracleParameter("p_full_name", OracleDbType.Varchar2, FullName, ParameterDirection.Input);
                    yield return new OracleParameter("p_short_name", OracleDbType.Varchar2, ShortName, ParameterDirection.Input);
                    yield return new OracleParameter("p_name_for_sort", OracleDbType.Varchar2, NameForSort, ParameterDirection.Input);
                    yield return new OracleParameter("p_steks_code", OracleDbType.Int64, SteksCode, ParameterDirection.Input);
                    yield return new OracleParameter("p_omk_code", OracleDbType.Varchar2, OmkCode, ParameterDirection.Input);
                    yield return new OracleParameter("p_type_ref", OracleDbType.Int64, TypeRef, ParameterDirection.Input);
                    yield return new OracleParameter("p_kladr_code", OracleDbType.Varchar2, KladrCode, ParameterDirection.Input);
                    yield return new OracleParameter("p_name", OracleDbType.Varchar2, Name, ParameterDirection.Input);
                    yield return new OracleParameter("p_code_givc", OracleDbType.Varchar2, CodeGivc, ParameterDirection.Input);
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
        protected override long? GetTYPE_REF()
        {
            var obj = Row["TP_TOP"];
            if (obj.IsNullOrDbNull())
                return null;
            return GetTYPE_REF(15, Convert.ToString(obj));
        }
        public override string GetOmkCode(string code)
        {
            var obj = Row["KOD_FO"];
            if (obj.IsNullOrDbNull())
                return null;
            return Convert.ToString(obj);
        }

        /*
         this.SUBJECT_RF_ID = 2;//Москва
            this.FULL_NAME = Row.GetValueOrDefault<string>("NM");
            this.SHORT_NAME = Row.GetValueOrDefault<string>("NAK");
            this.NAME_FOR_SORT = Row.GetValueOrDefault<string>("NNP");
            this.STEKS_CODE = (decimal)Row.GetValueOrDefault<double>("KOD");
            this.CODE_GIVC = Convert.ToString(this.STEKS_CODE);
            this.OMK_CODE = GetOmkCode(Convert.ToString(STEKS_CODE));
            this.NAME = Row.GetValueOrDefault<string>("NM");
            this.TYPE_REF = GetTYPE_REF();
         */
        public override long? Merge()
        {
            long? d;
            if (!IsExists(out d))
            {
                d = Insert();
            }
            else
            {
                Update(d.Value);
            }
            return d;
        }

        public void Update(long id)
        {
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(UpdateQuery);
            command.Parameters.AddRange(InsertParameters.ToArray());
            DbParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "p_street_id";
            idParameter.DbType = DbType.Int64;
            idParameter.Value = id;
            command.Parameters.Add(idParameter);
            DBMngr.Realty.ExecuteNonQuery(command);
        }

        protected override string UpdateQuery
        {
            get
            {
                return $"update {Importer.RefAddrStreetTableName} t set pse_id = @p_pse_id,town_id = @p_town_id,subject_rf_id = @p_subject_rf_id,full_name = @p_full_name,short_name = @p_short_name,name_for_sort = @p_name_for_sort,steks_code = @p_steks_code,omk_code = @p_omk_code,name = @p_name,code_givc = @p_code_givc where t.street_id = @p_street_id";
            }
        }
        
        /// <summary>
        /// парсим и добавляем связку районов для улицы
        /// </summary>
        /// <param name="districtId"></param>
        public void AddDistrict(long? districtId)
        {
            if (districtId.HasValue && !Districts.Contains(districtId.Value))
            {
                Districts.Add(districtId.Value);
            }
        }

        public void AddOkrug(long? okrugId)
        {
            if (okrugId.HasValue && !Okrugs.Contains(okrugId.Value))
            {
                Okrugs.Add(okrugId.Value);
            }
        }
        #endregion
    }
}
