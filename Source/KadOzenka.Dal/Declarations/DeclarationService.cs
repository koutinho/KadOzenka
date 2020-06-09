using System.Data;
using System.Transactions;
using Core.Shared.Extensions;
using KadOzenka.Dal.Declarations.Dto;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;
using ObjectModel.SPD;

namespace KadOzenka.Dal.Declarations
{
	public class DeclarationService
	{
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
	}
}