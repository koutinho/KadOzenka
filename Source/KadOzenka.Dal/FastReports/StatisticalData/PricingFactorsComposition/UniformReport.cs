using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class UniformReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionUniformReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query).ToList();

            var operations = GetOperations(taskIds);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return HadleData(dataSet);
        }


        #region Support Methods

        private List<ReportItem> GetOperations(List<long> taskIds)
        {
            var items = new List<ReportItem>();
            var units = GetUnits(taskIds);
            ////для тестирования
            //units = new List<OMUnit>
            //{
            //    new OMUnit{CadastralNumber = "test 1", Id = 14974931, ObjectId = 11251387 },
            //    new OMUnit{CadastralNumber = "test 2", Id = 13900719, ObjectId = 10433956 }
            //};

            var allObjectsAttributes = GbuObjectService.GetAllAttributes(
                    units.Where(x => x.ObjectId != null).Select(x => x.ObjectId.Value).ToList(), 
                    dateOt: DateTime.Now.GetEndOfTheDay());

            units.ForEach(unit =>
            {
                var objAttributes = allObjectsAttributes.Where(x => x.ObjectId == unit.ObjectId).ToList();
                if (objAttributes.Count > 0)
                {
                    items.Add(new ReportItem
                    {
                        CadastralNumber = unit.CadastralNumber,
                        Attributes = objAttributes.Select(x => new Attribute
                        {
                            RegisterName = x.RegisterData?.Description,
                            AttributeName = x.GetAttributeName()
                        }).ToList()
                    });
                }
            });

            return items;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Data");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("CharacteristicNameTitle");
            dataTable.Columns.Add("CharacteristicName");

            //для формирования матрицы нужен дубляж значения всех строк кроме характеристик
            for (var i = 0; i < operations.Count; i++)
            {
                for (var j = 0; j < operations[i].Attributes.Count; j++)
                {
                    for (var counter = 0; counter < 2; counter++)
                    {
                        string title, value;
                        if (counter % 2 == 0)
                        {
                            title = $"Характеристика объекта {j + 1}";
                            value = operations[i].Attributes.ElementAtOrDefault(j)?.AttributeName;
                        }
                        else
                        {
                            title = $"Итоговый источник информации {j + 1}";
                            value = operations[i].Attributes.ElementAtOrDefault(j)?.RegisterName;
                        }

                        dataTable.Rows.Add(i + 1,
                            operations[i].CadastralNumber, 
                            title,
                            value);
                    }
                   
                }
            }

            return dataTable;
        }

        #endregion


        #region Entities

        private class Attribute
        {
            public string RegisterName { get; set; }
            public string AttributeName { get; set; }
        }

        private class ReportItem
        {
            public string CadastralNumber { get; set; }
            public List<Attribute> Attributes { get; set; }
        }

        #endregion
    }
}
