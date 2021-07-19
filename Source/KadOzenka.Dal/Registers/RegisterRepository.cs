using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Registers
{
    public class RegisterRepository : GenericRepository<OMRegister>, IRegisterRepository
    {
        protected override QSQuery<OMRegister> GetBaseQuery(Expression<Func<OMRegister, bool>> whereExpression)
        {
            return OMRegister.Where(whereExpression);
        }

        protected override Expression<Func<OMRegister, bool>> GetWhereByIdExpression(long id)
        {
            return x => x.RegisterId == id;
        }
    }
}
