using System;
using System.Text;
using System.Data;
using System.Globalization;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

using Core.Register;
using Core.Numerator;
using Core.Shared.Misc;
using Core.Shared.Extensions;
using Core.Register.RegisterEntities;

namespace KadOzenka.Dal.GbuObject
{

    public class GbuObjectAttribute
    {

		public long Id { get; set; }
		public long ObjectId { get; set; }
		public long AttributeId { get; set; }
		public DateTime Ot { get; set; }
		public DateTime S { get; set; }

		public long RefItemId { get; set; }
		public string StringValue { get; set; }
		public decimal? NumValue { get; set; }
		public DateTime? DtValue { get; set; }

		public long ChangeUserId { get; set; }
		public DateTime ChangeDate { get; set; }
		public long ChangeDocId { get; set; }

		public string UserFullname { get; set; }
		public string DocNumber { get; set; }
		public string DocType { get; set; }
		public DateTime DocDate { get; set; }
		
		public long ChangeId { get; set; }
		public long ChangeSetId { get; set; }

		public bool IsActual { get; set; }

		private RegisterAttribute _attributeData;
		public RegisterAttribute AttributeData
		{
			get
			{
				if(_attributeData == null) _attributeData = RegisterCache.GetAttributeData((int)AttributeId);

				return _attributeData;
			}
		}

		private RegisterData _registerData;
		public RegisterData RegisterData
		{
			get
			{
				if (_registerData == null) _registerData = RegisterCache.GetRegisterData(AttributeData.RegisterId);

				return _registerData;
			}
		}
		
		public string GetAttributeName()
		{
			return RegisterCache.GetAttributeData((int)AttributeId).Name;
		}

		public string GetValueInString()
		{
			if(NumValue != null)
			{
				return NumValue.ToString();
			}
			else if(DtValue != null)
			{
				return DtValue.GetString();
			}

			return StringValue;
		}

		public string GetDocument()
		{
			List<string> docFacets = new List<string>();

			if (DocType.IsNotEmpty()) docFacets.Add(DocType);

			if (DocNumber.IsNotEmpty()) docFacets.Add($"№{DocNumber}");

			if (DocDate.GetString().IsNotEmpty()) docFacets.Add($"от {DocDate.GetString()}");

			return String.Join(" ", docFacets);
		}

		public long Save()
		{
			DbCommand insertCommand = DBMngr.Main.GetSqlStringCommand("insert");

			int allpriId = Sequence.GetNextValue("REG_ALLPRI_SEQ");
			
			string postfix;

			switch (AttributeData.Type)
			{
				case RegisterAttributeType.INTEGER:
				case RegisterAttributeType.DECIMAL:
				case RegisterAttributeType.BOOLEAN:
					postfix = "NUM";
					break;
				case RegisterAttributeType.DATE:
					postfix = "DT";
					break;
				case RegisterAttributeType.STRING:
					postfix = "TXT";
					break;
				default:
					throw new Exception($"Не поддерживаемый тип {AttributeData.Type}");
			}

			var d = CrossDBSQL.DefaultParameterDelimiter;

			string sSQLInsert = $"insert into {RegisterData.AllpriTable}_{postfix} (ID,OBJECT_ID,ATTRIBUTE_ID,OT,S,{(AttributeData.Type == RegisterAttributeType.STRING ? "REF_ITEM_ID," : String.Empty)} VALUE, CHANGE_ID, ACTUAL, CHANGE_DATE, CHANGE_USER_ID, CHANGE_DOC_ID) values ({allpriId},{ObjectId},{AttributeId},{d}OT,{d}S, {(AttributeData.Type == RegisterAttributeType.STRING ? (RefItemId != -1 ? RefItemId.ToString(CultureInfo.InvariantCulture) : "NULL") + "," : String.Empty)} {d}VALUE, {d}CHANGE_ID, {d}ACTUAL, {d}CHANGE_DATE, {d}CHANGE_USER_ID, {d}CHANGE_DOC_ID)";

			CrossDBSQL.AddParameter(insertCommand, "OT", DbType.DateTime, Ot);
			CrossDBSQL.AddParameter(insertCommand, "S", DbType.DateTime, S);

			// VALUE-------------------------------------------
			DbParameter valueParameter = CrossDBSQL.AddParameter(insertCommand, "VALUE", null);
			
			if (StringValue.IsNullOrEmpty() && DtValue == null && NumValue == null)
			{
				valueParameter.SourceColumnNullMapping = true;
				valueParameter.Value = DBNull.Value;
			}
			else
			{
				switch (AttributeData.Type)
				{
					case RegisterAttributeType.STRING:
						valueParameter.DbType = DbType.String;
						valueParameter.Value = StringValue;
						break;
					case RegisterAttributeType.BOOLEAN:
					case RegisterAttributeType.DECIMAL:
					case RegisterAttributeType.INTEGER:
						valueParameter.DbType = DbType.Decimal;
						valueParameter.Value = NumValue;
						break;
					case RegisterAttributeType.DATE:
						valueParameter.DbType = DbType.DateTime;
						valueParameter.Value = DtValue;
						break;
					default:
						throw new Exception($"Не поддерживаемый тип показателя {AttributeData.Type}");
				}
			}

			CrossDBSQL.AddParameter(insertCommand, "ACTUAL", DbType.Decimal, IsActual ? 1 : 0);
			CrossDBSQL.AddParameter(insertCommand, "CHANGE_DATE", DbType.DateTime, ChangeDate);
			CrossDBSQL.AddParameter(insertCommand, "CHANGE_USER_ID", DbType.Decimal, ChangeUserId);
			CrossDBSQL.AddParameter(insertCommand, "CHANGE_DOC_ID", DbType.Decimal, ChangeDocId);
			CrossDBSQL.AddParameter(insertCommand, "CHANGE_ID", DbType.Decimal, ChangeId);

			insertCommand.CommandText = sSQLInsert;
			DBMngr.Main.ExecuteNonQuery(insertCommand);

			return Id;
		}

	}

}
