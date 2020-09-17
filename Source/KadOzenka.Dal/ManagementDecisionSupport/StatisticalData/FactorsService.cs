using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Model;
using KadOzenka.Dal.Model.Dto;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
    public class FactorsService
    {
        private ModelService ModelService { get; set; }

        public FactorsService()
        {
            ModelService = new ModelService();
        }


        public List<PricingFactors> GetGroupedModelFactors(long modelId)
        {
            var factors = ModelService.GetModelFactors(modelId);

            var groupedFactors = factors.GroupBy(x => x.RegisterId).Select(x => new PricingFactors
            {
                RegisterId = (int)x.Key,
                Attributes = x.Select(y => new PricingFactor
                {
                    Id = y.FactorId,
                    Name = y.Factor
                }).ToList()
            }).ToList();

            return groupedFactors;
        }

        //TODO rename after finish report optimization
        public List<PricingFactors> GetGroupedModelFactors2(long modelId)
        {
            var query = ModelService.GetModelFactorsQuery(modelId);

            query.AddColumn(OMModelFactor.GetColumn(x => x.FactorId, nameof(ModelFactorPure.FactorId)));
            query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelFactorPure.Name)));
            query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelFactorPure.RegisterId)));

            var factors = query.ExecuteQuery<ModelFactorPure>();

            var groupedFactors = factors.GroupBy(x => x.RegisterId).Select(x => new PricingFactors
            {
                RegisterId = (int)x.Key,
                Attributes = x.Select(y => new PricingFactor
                {
                    Id = y.FactorId,
                    Name = y.Name
                }).ToList()
            }).ToList();

            return groupedFactors;
        }

        public Dictionary<long, List<PricingFactor>> GetPricingFactorsForUnits(List<long> unitIds, List<PricingFactors> unitFactors)
        {
            var attributes = new Dictionary<long, List<PricingFactor>>();
            unitFactors.ForEach(factor =>
            {
                //улучшение производительности: не делаем join c юнитами, если можем найти атрибут с ID основного ключа
                var cacheAttribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == factor.RegisterId && x.IsPrimaryKey);

                var condition = cacheAttribute == null
                    ? new QSConditionSimple(OMUnit.GetColumn(x => x.Id), QSConditionType.In, unitIds.Select(x => (double)x))
                    : new QSConditionSimple
                    {
                        ConditionType = QSConditionType.In,
                        LeftOperand = new QSColumnSimple(cacheAttribute.Id),
                        RightOperand = new QSColumnConstant(unitIds)
                    };

                var query = new QSQuery
                {
                    MainRegisterID = factor.RegisterId,
                    Columns = factor.Attributes.Select(x => (QSColumn)new QSColumnSimple(x.Id, x.Id.ToString())).ToList(),
                    Condition = condition
                };

                var table = query.ExecuteQuery();
                foreach (DataRow row in table.Rows)
                {
                    factor.Attributes.ForEach(attribute =>
                    {
                        var unitId = row["ID"].ParseToLong();
                        var value = row[attribute.Id.ToString()].ParseToStringNullable();

                        if (!attributes.ContainsKey(unitId))
                            attributes.Add(unitId, new List<PricingFactor>());

                        attributes[unitId].Add(new PricingFactor
                        {
                            Id = attribute.Id,
                            Name = attribute.Name,
                            Value = value
                        });
                    });
                }
            });

            return attributes;
        }


        #region Entities

        public class PricingFactors
        {
            public int RegisterId { get; set; }
            public List<PricingFactor> Attributes { get; set; }
        }

        public class PricingFactor
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }

        #endregion
    }
}
