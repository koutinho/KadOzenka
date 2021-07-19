using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tours.Repositories
{
	public class TourRepository : GenericRepository<OMTour>, ITourRepository
	{
		protected override QSQuery<OMTour> GetBaseQuery(Expression<Func<OMTour, bool>> whereExpression)
		{
			return OMTour.Where(whereExpression);
		}

		protected override Expression<Func<OMTour, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}
	}
}
