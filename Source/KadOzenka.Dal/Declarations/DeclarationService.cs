using System.Transactions;
using Core.Shared.Extensions;
using KadOzenka.Dal.Declarations.Dto;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

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
					Flat = newSubject.Flat.ToString(),
					House = newSubject.House,
					Mail = newSubject.Mail,
					Telefon = newSubject.Phone,
					I_Name = newSubject.FirstName,
					O_Name = newSubject.MiddleName,
					Street = newSubject.Street
				}.Save();

				ts.Complete();
			}


			return resId;
		}
	}
}