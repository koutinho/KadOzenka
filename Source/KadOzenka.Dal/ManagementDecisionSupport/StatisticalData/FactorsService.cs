using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
    public class FactorsService
    {
        private BaseModelingService ModelService { get; set; }
        private ModelFactorsService ModelFactorsService { get; set; }

        public FactorsService()
        {
            ModelService = new BaseModelingService(new DictionaryService());
            ModelFactorsService = new ModelFactorsService();
        }


        public List<PricingFactors> GetGroupedModelFactors(long modelId)
        {
            var query = ModelFactorsService.GetModelFactorsQuery(modelId);

            query.AddColumn(OMModelFactor.GetColumn(x => x.FactorId, nameof(ModelFactorPure.FactorId)));
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
                    Id = y.FactorId,
                    Name = y.ValueField,
                    ValueField = y.Name
                }).ToList()
            }).ToList();

            return groupedFactors;
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

        public List<Attribute> ProcessModelFactors(DataRow row, List<Attribute> generalAttributes)
        {
	        return generalAttributes.Select(attribute =>
		        new Attribute
		        {
			        Id = attribute.Id,
			        Name = attribute.Name,
			        Value = row[attribute.Id.ToString()].ParseToStringNullable()
		        }).ToList();
        }


        #region Entities

        public class PricingFactors
        {
            public long RegisterId { get; set; }
            public List<Attribute> Attributes { get; set; }
        }

        //TODO объединить классы Attribute и ModelFactorPure. сейчас это невозможно из-за ошибки платформы
        //TODO (ORM делает неправильный маппинг при ExecuteQuery)
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
            public long FactorId { get; set; }
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
