using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadAppraisalDataApi.Models
{
    public class GraphFactorsData
    {
        /// <summary>
        /// Кадастровый номер
        /// </summary>
        public string CadNum { get; set; }
        /// <summary>
        /// Дата оценки
        /// </summary>
        public DateTime DateAppraisal { get; set; }
        /// <summary>
        /// Список графических факторов
        /// </summary>
        public List<GraphFactor> GraphFactors { get; set; }
    }

    public class GraphFactor
    {
        /// <summary>
        /// Дата расчета
        /// </summary>
        public DateTime DateCalc { get; set; }
        /// <summary>
        /// Кадастровый квартал источника графики
        /// </summary>
        public string CadBlock { get; set; }
        /// <summary>
        /// Наименование слоя источника
        /// </summary>
        public string LayerSourceName { get; set; }
        /// <summary>
        /// Наименование слоя расчета фактора
        /// </summary>
        public string LayerTargetName { get; set; }
        /// <summary>
        /// Тип расчета
        /// </summary>
        public int CalcType { get; set; }
        /// <summary>
        /// Наименование фактора
        /// </summary>
        public string FactorName { get; set; }
        /// <summary>
        /// Значение фактора
        /// </summary>
        public decimal FactorValue { get; set; }
        /// <summary>
        /// Наименование объекта
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// Значение фактора по кварталу
        /// </summary>
        public decimal FactorValueByCadBlock { get; set; }
        /// <summary>
        /// Наименование объекта по кварталу
        /// </summary>
        public string ObjectNameByCadBlock { get; set; }
    }
}