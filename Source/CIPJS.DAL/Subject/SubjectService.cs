using Core.Register.QuerySubsystem;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.DAL.Subject
{
    public class SubjectService
    {
        public SubjectDto Get(long? id)
        {
            return SubjectDto.OMMap(id);
        }

        public void Save(SubjectDto model)
        {
            OMSubject subject = SubjectDto.OMMap(model);
            subject?.Save();
        }

        public List<SubjectDto> GetByName(string name, SubjectType? subjectType)
        {
            QSQuery<OMSubject> query = OMSubject.Where(x => x.SubjectName.ToLower().Contains(name.ToLower()));

            if (subjectType.HasValue)
            {
                query.And(x => x.TypeSubject_Code == subjectType.Value);
            }

            List<SubjectDto> result = query.SelectAll()
                    .Select(x => x.ParentBank.BankName)
                    .Select(x => x.ParentBank.BankInn)
                    .Select(x => x.ParentBank.BankKpp)
                    .Select(x => x.ParentBank.BankBic)
                    .Select(x => x.ParentBank.BankKorAcc)
                    .Execute().Select(x => SubjectDto.OMMap(x)).ToList();

            return result;
        }

        public void CheckDelete(long id, out string name)
        {
            OMSubject omSubject = OMSubject
                .Where(x => x.EmpId == id)
                .Select(x => x.SubjectName)
                .ExecuteFirstOrDefault();

            if (omSubject == null) throw new Exception("Субъект не найден!");

            if (OMInvoice.Where(x => x.SubjectId == id).ExecuteExists() ||
                OMParamCalculation.Where(x => x.SubjectId == id).ExecuteExists())
                throw new Exception("Внимание, удалить субъект невозможно, в Системе присутствуют сущности, связанные с субъектом (расчеты/счета)");

            name = omSubject.SubjectName;
        }

        public void Delete(long id)
        {
            OMSubject omSubject = OMSubject.Where(x => x.EmpId == id).ExecuteFirstOrDefault();

            if (omSubject == null) throw new Exception("Субъект не найден!");

            omSubject.Destroy();
        }
    }
}
