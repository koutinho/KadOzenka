using CIPJS.DAL.HierarchicalGridAnalysisDamage.Models;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder
{
    interface IModelBuilder<T, K> : IModelBuilder
    {
        List<GroupEl<T>> Create(K objs);
    }

    interface IModelBuilder
    {
        object Create(object objs);
    }
}
