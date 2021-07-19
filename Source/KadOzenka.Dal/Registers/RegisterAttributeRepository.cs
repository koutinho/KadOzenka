using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Registers
{
    public class RegisterAttributeRepository : GenericRepository<OMAttribute>, IRegisterAttributeRepository
    {
        protected override QSQuery<OMAttribute> GetBaseQuery(Expression<Func<OMAttribute, bool>> whereExpression)
        {
            return OMAttribute.Where(whereExpression);
        }

        protected override Expression<Func<OMAttribute, bool>> GetWhereByIdExpression(long id)
        {
            return x => x.RegisterId == id;
        }
    }
}
