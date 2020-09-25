using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Model;
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
                Attributes = x.Select(y => new Attribute
                {
                    Id = y.FactorId,
                    Name = y.Factor
                }).ToList()
            }).ToList();

            return groupedFactors;
        }

        //TODO переименовать после оптимизации
        public List<PricingFactors> GetGroupedModelFactorsNew(long modelId)
        {
            var query = ModelService.GetModelFactorsQuery(modelId);

            query.AddColumn(OMAttribute.GetColumn(x => x.ValueField, nameof(ModelFactorPure.ValueField)));
            query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelFactorPure.Name)));
            query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelFactorPure.RegisterId)));

            var factors = query.ExecuteQuery<ModelFactorPure>();

            var groupedFactors = factors.GroupBy(x => x.RegisterId, (key, group) => new PricingFactors
            {
                RegisterId = key,
                Attributes = group.Select(y => new Attribute
                {
                    //TODO поменять после правок платформы
                    Id = y.Id,
                    Name = y.ValueField,
                    ValueField = y.Name
                }).ToList()
            }).ToList();

            return groupedFactors;
        }

        public Dictionary<long, List<Attribute>> GetPricingFactorsForUnits(List<long> unitIds, List<PricingFactors> unitFactors)
        {
            var attributes = new Dictionary<long, List<Attribute>>();
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
                    MainRegisterID = (int) factor.RegisterId,
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
                            attributes.Add(unitId, new List<Attribute>());

                        attributes[unitId].Add(new Attribute
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

        public ModelFactorsSql GetSqlForModelFactors(long? modelId, List<PricingFactors> groupedFactors)
        {
            if(modelId == null)
                return new ModelFactorsSql();

            var counter = 0;
            var columns = new StringBuilder();
            var tables = new StringBuilder();
            groupedFactors.ForEach(factors =>
            {
                var register = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == factors.RegisterId);
                if (register == null)
                    throw new Exception($"Не найден реестр с Id='{factors.RegisterId}' для факторов модели с Id='{modelId}'");

                factors.Attributes.ForEach(attribute =>
                {
                    columns.Append($" factorsTable{counter}.{attribute.ValueField} as \"{attribute.Id}\",");
                });

                tables.Append($" left join {register.QuantTable} factorsTable{counter} on unit.id = factorsTable{counter}.Id");
                counter++;
            });
            //удаляем ',' в селекте для последнего столбца
            if (columns.Length > 0)
	            columns.Length--;

            return new ModelFactorsSql
            {
                Columns = columns.ToString(),
                Tables = tables.ToString()
            };
        }

        public List<Attribute> ProcessModelFactors(DataRow row, List<Attribute> attributes)
        {
	        return attributes.Select(attribute =>
		        new Attribute
		        {
			        Id = attribute.Id,
			        Name = attribute.Name,
			        Value = row[attribute.Id.ToString()].ParseToStringNullable()
		        }).ToList();
        }


        #region Entities

        //TODO рефакторить после окончания оптимизации
        public class PricingFactors
        {
            public long RegisterId { get; set; }
            public List<Attribute> Attributes { get; set; }
        }

        public class Attribute
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string ValueField { get; set; }
            public string Value { get; set; }
        }

        public class ModelFactorPure
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string ValueField { get; set; }
            public long RegisterId { get; set; }
        }

        public class ModelFactorsSql
        {
	        public string Columns { get; set; }
	        public string Tables { get; set; }
        }

        #endregion
    }
}
