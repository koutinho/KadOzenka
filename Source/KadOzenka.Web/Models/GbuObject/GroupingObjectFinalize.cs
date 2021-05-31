using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Models.Filters;

namespace KadOzenka.Web.Models.GbuObject
{
    public class GroupingObjectFinalize : PartialCharacteristicViewModel, IValidatableObject
    {
        /// <summary>
        /// Выборка по всем объектам
        /// </summary>
        public bool SelectAllObject { get; set; } = true;

        public bool IsDataActualUsed { get; set; } = false;
        public bool IsTaskFilterUsed { get; set; } = false;

        /// <summary>
        /// Список значений фильтра
        /// </summary>
        [Display(Name = "Задания на оценку")]
        public List<long> TaskFilter { get; set; }

        [Display(Name = "Статус")] public List<ObjectChangeStatus> ObjectChangeStatus { get; set; }

        /// <summary>
        /// Дата на которую делается гармонизация
        /// </summary>
        [Display(Name = "Дата актулизации")]
        public DateTime? DataActual { get; set; }

        /// <summary>
        /// Преобразовываемый атрибут
        /// </summary>
        public LevelItemGroup IdAttributeSource { get; set; }

        /// <summary>
        /// Настройки 2 уровня группировки
        /// </summary>
        public LevelItemGroup IdAttributeForSelectionBetween2 { get; set; }

        public Filters Filter1ForSelectionBetween2 { get; set; }
        public Filters Filter2ForSelectionBetween2 { get; set; }

        /// <summary>
        /// Настройки 3 уровня группировки
        /// </summary>
        public LevelItemGroup IdAttributeForSelectionBetween3 { get; set; }

        public Filters Filter1ForSelectionBetween3 { get; set; }
        public Filters Filter2ForSelectionBetween3 { get; set; }
        public Filters Filter3ForSelectionBetween3 { get; set; }


        public GroupingObjectFinalize()
        {
            IdAttributeSource = new LevelItemGroup();
            IdAttributeForSelectionBetween2 = new LevelItemGroup();
            IdAttributeForSelectionBetween3 = new LevelItemGroup();
            Filter1ForSelectionBetween2 = new Filters();
            Filter2ForSelectionBetween2 = new Filters();
            Filter1ForSelectionBetween3 = new Filters();
            Filter2ForSelectionBetween3 = new Filters();
            Filter3ForSelectionBetween3 = new Filters();
        }


        public GroupingSettingsFinal CovertToGroupingSettings()
        {
            return new()
            {
                IdAttributeResult = IdAttributeResult,
                IdAttributeSource = IdAttributeSource.IdFactor,

                IdAttributeFor2Selections = IdAttributeForSelectionBetween2.IdFactor,
                Filter1ForSelectionBetween2 = Filter1ForSelectionBetween2,
                Filter2ForSelectionBetween2 = Filter2ForSelectionBetween2,

                IdAttributeFor3Selections = IdAttributeForSelectionBetween3.IdFactor,
                Filter1ForSelectionBetween3 = Filter1ForSelectionBetween3,
                Filter2ForSelectionBetween3 = Filter2ForSelectionBetween3,
                Filter3ForSelectionBetween3 = Filter3ForSelectionBetween3,

                SelectAllObject = SelectAllObject,
                DateActual = DataActual,
                TaskFilter = TaskFilter ?? new List<long>(),
                ObjectChangeStatus = ObjectChangeStatus
            };
        }

        #region Validation

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!SelectAllObject && IsDataActualUsed && !DataActual.HasValue)
            {
                yield return
                    new ValidationResult(errorMessage: "Поле Дата актулизации обязательное",
                        memberNames: new[] {nameof(DataActual)});
            }

            if (!SelectAllObject && IsTaskFilterUsed && TaskFilter?.Count is null or 0)
            {
                yield return
                    new ValidationResult(errorMessage: "Список заданий на оценку не может быть пустым",
                        memberNames: new[] {nameof(TaskFilter)});
            }

            foreach (var validationResult in CheckResultAttribute()) yield return validationResult;

            foreach (var validationResult1 in CheckFilterTypes()) yield return validationResult1;

            foreach (var validationResultBoundariesFor2 in CheckForBoundariesFor2Attributes())
                yield return validationResultBoundariesFor2;

            foreach (var validationResultBoundariesFor3 in CheckForBoundariesFor3Attributes())
                yield return validationResultBoundariesFor3;

            foreach (var validationResultIntervals in CheckForIntervalCorrectness())
                yield return validationResultIntervals;
        }

        private IEnumerable<ValidationResult> CheckFilterTypes()
        {
            if (Filter1ForSelectionBetween2.Type != Filter2ForSelectionBetween2.Type
                || Filter1ForSelectionBetween3.Type != Filter2ForSelectionBetween3.Type &&
                Filter1ForSelectionBetween3.Type != Filter3ForSelectionBetween3.Type)
            {
                yield return new ValidationResult("Расхождение в типах фильтров внутри одного блока настроек");
            }
        }

        private IEnumerable<ValidationResult> CheckResultAttribute()
        {
            if (IsNewAttribute)
            {
                if (string.IsNullOrEmpty(NameNewAttribute))
                {
                    yield return
                        new ValidationResult(errorMessage: "Имя нового атрибута не может быть пустым",
                            memberNames: new[] {nameof(NameNewAttribute)});
                }

                if (TypeNewAttribute == null)
                {
                    yield return
                        new ValidationResult(errorMessage: "Тип нового атрибута не может быть пустым",
                            memberNames: new[] {nameof(TypeNewAttribute)});
                }

                if (RegistryId == null)
                {
                    yield return
                        new ValidationResult(errorMessage: "Выберите реестр",
                            memberNames: new[] {nameof(RegistryId)});
                }
            }

            else if (IdAttributeResult == null)
            {
                yield return
                    new ValidationResult(errorMessage: "Заполните результирующую характеристику",
                        memberNames: new[] {nameof(IdAttributeResult)});
            }
        }

        private IEnumerable<ValidationResult> CheckForBoundariesFor2Attributes()
        {
            if (IdAttributeForSelectionBetween2 == null) yield break;

            bool check2 = false;
            if (Filter1ForSelectionBetween2.Type == FilteringType.Date &&
                Filter2ForSelectionBetween2.Type == FilteringType.Date)
            {
                check2 |= CheckForBoundaryIntersection(Filter1ForSelectionBetween2.DateFilter,
                    Filter2ForSelectionBetween2.DateFilter);
            }

            if (Filter1ForSelectionBetween2.Type == FilteringType.Number &&
                Filter2ForSelectionBetween2.Type == FilteringType.Number)
            {
                check2 |= CheckForBoundaryIntersection(Filter1ForSelectionBetween2.NumberFilter,
                    Filter2ForSelectionBetween2.NumberFilter);
            }

            if (check2)
            {
                yield return new ValidationResult("Наложение интервалов в условиях для 2 кодов");
            }
        }

        private IEnumerable<ValidationResult> CheckForBoundariesFor3Attributes()
        {
            if (IdAttributeForSelectionBetween3 == null) yield break;

            bool check3 = false;
            if (Filter1ForSelectionBetween2.Type == FilteringType.Date &&
                Filter2ForSelectionBetween2.Type == FilteringType.Date)
            {
                check3 |= CheckForBoundaryIntersection(Filter1ForSelectionBetween3.DateFilter,
                    Filter2ForSelectionBetween3.DateFilter);
                check3 |= CheckForBoundaryIntersection(Filter1ForSelectionBetween3.DateFilter,
                    Filter3ForSelectionBetween3.DateFilter);
                check3 |= CheckForBoundaryIntersection(Filter2ForSelectionBetween3.DateFilter,
                    Filter3ForSelectionBetween3.DateFilter);
            }

            if (Filter1ForSelectionBetween2.Type == FilteringType.Number &&
                Filter2ForSelectionBetween2.Type == FilteringType.Number)
            {
                check3 |= CheckForBoundaryIntersection(Filter1ForSelectionBetween3.NumberFilter,
                    Filter2ForSelectionBetween3.NumberFilter);
                check3 |= CheckForBoundaryIntersection(Filter1ForSelectionBetween3.NumberFilter,
                    Filter3ForSelectionBetween3.NumberFilter);
                check3 |= CheckForBoundaryIntersection(Filter2ForSelectionBetween3.NumberFilter,
                    Filter3ForSelectionBetween3.NumberFilter);
            }

            if (check3)
            {
                yield return new ValidationResult("Наложение интервалов в условиях для 3 кодов");
            }
        }

        private IEnumerable<ValidationResult> CheckForIntervalCorrectness()
        {
            static bool CheckIntervals(Filters filters)
            {
                var res = filters.Type switch
                {
                    FilteringType.Date when
                        filters.DateFilter.FilteringType is FilteringTypeDate.InRange or FilteringTypeDate
                            .InRangeIncludingBoundaries &&
                        filters.DateFilter.Value >= filters.DateFilter.Value2 => false,
                    FilteringType.Number when
                        filters.NumberFilter.FilteringType is FilteringTypeNumber.InRange or FilteringTypeNumber
                            .InRangeIncludingBoundaries &&
                        filters.NumberFilter.Value >= filters.NumberFilter.Value2 => false,
                    _ => true
                };

                return res;
            }

            var intervalCorrectness = true;
            intervalCorrectness &= CheckIntervals(Filter1ForSelectionBetween2);
            intervalCorrectness &= CheckIntervals(Filter2ForSelectionBetween2);
            intervalCorrectness &= CheckIntervals(Filter1ForSelectionBetween3);
            intervalCorrectness &= CheckIntervals(Filter2ForSelectionBetween3);
            intervalCorrectness &= CheckIntervals(Filter3ForSelectionBetween3);
            if (!intervalCorrectness)
            {
                yield return new ValidationResult("Верхняя граница интервала не может быть меньше нижней");
            }
        }

        private static bool CheckForBoundaryIntersection(NumberFilter filter1, NumberFilter filter2)
        {
            var res = (filter1, filter2) switch
            {
                var (x1, x2)
                    when x1.FilteringType is FilteringTypeNumber.Greater or FilteringTypeNumber.GreaterOrEqual
                         && x2.FilteringType is FilteringTypeNumber.Greater or FilteringTypeNumber.GreaterOrEqual =>
                    true,

                var (x1, x2)
                    when x1.FilteringType is FilteringTypeNumber.Less or FilteringTypeNumber.LessOrEqual
                         && x2.FilteringType is FilteringTypeNumber.Less or FilteringTypeNumber.LessOrEqual => true,

                var (x1, x2)
                    when x1.FilteringType is FilteringTypeNumber.InRange
                         && x1.Value < x2.Value && x2.Value < x1.Value2 => true,

                var (x1, x2)
                    when x1.FilteringType is FilteringTypeNumber.InRangeIncludingBoundaries
                         && x1.Value <= x2.Value && x2.Value <= x1.Value2 => true,

                var (x1, x2)
                    when x2.FilteringType is FilteringTypeNumber.InRange
                         && x2.Value < x1.Value && x1.Value < x2.Value2 => true,

                var (x1, x2)
                    when x2.FilteringType is FilteringTypeNumber.InRangeIncludingBoundaries
                         && x2.Value <= x1.Value && x1.Value <= x2.Value2 => true,

                (_, _) => false
            };
            return res;
        }

        private static bool CheckForBoundaryIntersection(DateFilter filter1, DateFilter filter2)
        {
            var res = (filter1, filter2) switch
            {
                var (x1, x2)
                    when x1.FilteringType is FilteringTypeDate.After or FilteringTypeDate.AfterIncludingBoundary
                         && x2.FilteringType is FilteringTypeDate.After or FilteringTypeDate.AfterIncludingBoundary =>
                    true,

                var (x1, x2)
                    when x1.FilteringType is FilteringTypeDate.Before or FilteringTypeDate.BeforeIncludingBoundary
                         && x2.FilteringType is FilteringTypeDate.Before or FilteringTypeDate.BeforeIncludingBoundary =>
                    true,

                var (x1, x2)
                    when x1.FilteringType is FilteringTypeDate.InRange
                         && x1.Value < x2.Value && x2.Value < x1.Value2 => true,

                var (x1, x2)
                    when x1.FilteringType is FilteringTypeDate.InRangeIncludingBoundaries
                         && x1.Value <= x2.Value && x2.Value <= x1.Value2 => true,

                var (x1, x2)
                    when x2.FilteringType is FilteringTypeDate.InRange
                         && x2.Value < x1.Value && x1.Value < x2.Value2 => true,

                var (x1, x2)
                    when x2.FilteringType is FilteringTypeDate.InRangeIncludingBoundaries
                         && x2.Value <= x1.Value && x1.Value <= x2.Value2 => true,

                (_, _) => false
            };
            return res;
        }

        #endregion
    }
}