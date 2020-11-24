using System;
using System.Data;
using System.Transactions;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.Declarations.Dto;
using KadOzenka.Dal.Extentions;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;
using ObjectModel.SPD;
using Platform.CalendarHolidays;

namespace KadOzenka.Dal.Declarations
{
	public class DeclarationService
	{
		/// <summary>
		/// Срок рассмотрения декларации в соответствии с Приказом 318 составляет 50 рабочих дней со дня поступления декларации
		/// </summary>
		public static int DurationWorkDaysCount => 50;

		/// <summary>
		/// Срок рассмотрения декларации составляет 5 рабочих дней со дня поступления декларации eсли заявителю отказано в рассмотрении
		/// </summary>
		public static int DurationWorkDaysCountForRejectedDeclaration => 5;

		/// <summary>
		/// Плановая дата рассмотрения должна быть +5 раб.дней к "Вх. дате ГБУ"
		/// </summary>
		public static int DateCheckPlanDaysCount => 5;

		/// <summary>
		/// "Контрольный срок" должен быть -10 рабочих дней от "Срок рассмотрения"
		/// </summary>
		public static int DaysDiffBetweenDateCheckTimeAndDurationDateIn => 10;

		public OMSubject GetSubjectById(long subjectId)
		{
			return OMSubject.Where(x => x.Id == subjectId).SelectAll().ExecuteFirstOrDefault();
		}

		public OMSubject GetSubjectByName(string sName)
		{
			return OMSubject
				.Where(x => ((x.Name == sName && (long) x.Type_Code == (long) SubjectType.Ul) ||
				             (x.F_Name == sName && (long) x.Type_Code == (long) SubjectType.Fl)))
				.SelectAll().ExecuteFirstOrDefault();
		}

		public long CreateSubject(CreateSubjectDto newSubject)
		{
			int resId;
			using (var ts = new TransactionScope())
			{

				resId = new OMSubject
				{
					Name = newSubject.Name,
					Type_Code = newSubject.Type,
					F_Name = newSubject.Surname,
					Building = newSubject.Building,
					City = newSubject.City,
					Flat = newSubject.Flat,
					House = newSubject.House,
					Mail = newSubject.Mail,
					Telefon = newSubject.Phone,
					I_Name = newSubject.FirstName,
					O_Name = newSubject.MiddleName,
					Street = newSubject.Street,
					Zip = newSubject.Zip
				}.Save();

				ts.Complete();
			}


			return resId;
		}

		public OMSubject GetOrCreateSubject(OMRequestRegistration requestRegistration)
		{
			OMSubject res = null;

			if (requestRegistration.CustomXML == null) return res;

			DataSet dataSet = new DataSet();
			var xmlSR = new System.IO.StringReader(requestRegistration.CustomXML);
			dataSet.ReadXml(xmlSR, XmlReadMode.ReadSchema);
			var row = dataSet.Tables["AppApplicants"].Rows[0];

			SubjectType currenType = SubjectType.Ul;
			if (row["APPLICANTTYPE"].ToString() == "1") currenType = SubjectType.Fl;

			string firstName = row["FNAME"].ToString();
			string lastName = row["SNAME"].ToString();
			string middleName = row["MNAME"].ToString();
			string phone = row["APPPHONE"].ToString();
			string name = row["NAME"].ToString();
			string email = row["EMAIL"].ToString();
	

			if (currenType == SubjectType.Fl)
			{
				res = OMSubject.Where(x =>
					x.Type_Code == currenType && x.F_Name == lastName && x.I_Name == firstName &&
					x.O_Name == middleName && x.Telefon == phone).SelectAll().ExecuteFirstOrDefault();
			}
			else if(currenType == SubjectType.Ul)
			{
				
				res = OMSubject.Where(x =>
					x.Type_Code == currenType && x.Name == name && x.Telefon == phone && email == x.Mail).SelectAll().ExecuteFirstOrDefault();

			}

			if (res == null)
			{
				string city = row["POST_PUNKT"].ToString();
				string street = row["POST_STREET"].ToString();
				string house = row["POST_DOM"].ToString();
				string zip = row["POST_INDEX"].ToString();
				string flat = row["POST_KVART"].ToString();

				var subjectDto = new CreateSubjectDto
				{
					Name = name,
					City = city,
					Street = street,
					House = house,
					Zip = zip,
					Mail = email,
					Phone = phone,
					Type = currenType,
					FirstName = firstName,
					Surname = lastName,
					MiddleName = middleName,
					Flat = flat,
				};

				long id = CreateSubject(subjectDto);

				res = GetSubjectById(id);
			}

			return res;
		}

		public void CalculateSynchronizedDates(DateTime? dateIn, DateTime? prevDurationDateIn, StatusDec? status, out DateTime? durationDateIn,
			out DateTime? dateCheckPlan, out DateTime? checkTime)
		{
			if (dateIn.HasValue)
			{
				durationDateIn =
					status.GetValueOrDefault() == StatusDec.Rejection
						? CalendarHolidays.GetDateFromWorkDays(dateIn.Value.AddDays(-1),
							DurationWorkDaysCountForRejectedDeclaration)
						: CalendarHolidays.GetDateFromWorkDays(dateIn.Value.AddDays(-1),
							DurationWorkDaysCount);
				dateCheckPlan = CalendarHolidays.GetDateFromWorkDays(
					dateIn.Value.AddDays(-1),
					DateCheckPlanDaysCount);
			}
			else
			{
				durationDateIn = null;
				dateCheckPlan = null;
			}

			var isEditDeclarationProcessingBlock =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_PROCESSING_BLOCK);
			CalculateSynchronizedCheckTimeDate(isEditDeclarationProcessingBlock ? durationDateIn : prevDurationDateIn, status, out checkTime);
		}

		public void CalculateSynchronizedCheckTimeDate(DateTime? durationDateIn, StatusDec? status, out DateTime? checkTime)
		{
			checkTime = status.GetValueOrDefault() == StatusDec.Rejection
					? durationDateIn
					: durationDateIn?.GetStartWorkDate(DaysDiffBetweenDateCheckTimeAndDurationDateIn - 1);
		}
	}
}