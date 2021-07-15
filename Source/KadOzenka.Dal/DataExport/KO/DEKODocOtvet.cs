using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using GemBox.Document;
using GemBox.Document.Tables;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Entities;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dictionaries;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataExport
{
    /// <summary>
    /// Класс выгрузки в Word ответных документов по отдельным объектам.
    /// </summary>
    public class DEKODocOtvet
    {
        /// <summary>
        /// Экспорт в Word - ответные документы по объектам.
        /// </summary>
        public static Stream ExportToDoc(OMUnit _unit)
        {
            if (_unit == null)
            {
                throw new Exception($"Не найдена единица оценки.");
            }

            // Проверка КС, игнорируем = 0
            if (CheckNullEmpty.CheckDecimal(_unit.CadastralCost.Value) == 0)
            {
                throw new Exception($"У единицы оценки не определена кадастровая стоимость.");
            }

            OMTask task = OMTask.Where(x => x.Id == _unit.TaskId).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                throw new Exception($"Не найдено задание на оценку для Единицы оценки с Id = '{_unit.Id}'");
            }
            OMGroup group_unit = OMGroup.Where(x => x.Id == _unit.GroupId).SelectAll().ExecuteFirstOrDefault();
            if (group_unit == null)
            {
                throw new Exception($"Не найдена группа для Единицы оценки с Id = '{_unit.Id}'");
            }
            OMGroup calc_group = null;
            OMUnit calc_unit = null;
            if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding)//  subgroup.Type_SubGroup == 9)
            {
                // Определяем объект, по которому был расcчитан _unit, и у этого объекта берем группу
                GetCalcGroupFromUnit(_unit, task.EstimationDate, out calc_unit, out calc_group);
            }

            try
            {
                Console.WriteLine("Выгрузка Otvet ...");
                ComponentInfo.SetLicense("DN-2020Feb27-7KwYZ43Y+lJR5YBeTLWW8F+pXE9Aj3uU2ru+Jk1lHxILYWKJhT8TZQLCztE1qx6MQx/MnAR8BGGPC6QpAmIgm2EZh0w==A");
                var document = new DocumentModel();
                document.DefaultParagraphFormat.SpaceAfter = 0;

                Console.WriteLine(" - создание таблицы ...");
                #region Создание таблицы      
                int idx_row = -1;
                int count_cells = 4;
                var table = new Table(document);
                table.Columns.Add(new TableColumn(96f / 2.54f * 1.50f));
                table.Columns.Add(new TableColumn(96f / 2.54f * 7.25f));
                table.Columns.Add(new TableColumn(96f / 2.54f * 2.95f));
                table.Columns.Add(new TableColumn(96f / 2.54f * 5.80f));
                table.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
                table.TableFormat.Alignment = HorizontalAlignment.Center;
                table.TableFormat.Borders.ClearBorders();

                #endregion
                Console.WriteLine(" - создание таблицы выполнено");

                #region Сбор и формирование данных по объекту
                Console.WriteLine(" - заголовок ...");
                string strDateApp = (task.EstimationDate != null) ? task.EstimationDate.Value.ToString("dd.MM.yyyy") : "-";
                string strActReq_01_06 = "-";
                string strActReq_01_07 = "-";
                string strActReq_01_10 = "01.01.2019";
                string str_3_0 = "-";
                string value_attr = "";

                #region Нашли и записали в список входящий документ
                KoNoteType doc_status = task.NoteType_Code;
                #endregion
                OMInstance doc_out = OMInstance.Where(x => x.Id == _unit.ResponseDocId).SelectAll().ExecuteFirstOrDefault();
                if (doc_out != null)
                {
                    switch (doc_status)
                    {
                        case KoNoteType.Day:      // STATUS_DOC == СтатусДокумента.Ежедневка)
                            strActReq_01_10 = "-";
                            strActReq_01_06 = DataExportCommon.GetFullNameDoc(doc_out);
                            strActReq_01_07 = "Ковалев Д.В." + "$ $" + "Капитонов К.С.";
                            str_3_0 = "Кадастровая стоимость объекта недвижимости определена в соответствии с положениями статьи 16 Федерального закона от 03 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке».";

                            break;
                        default:     //СтатусДокумента.Иное
                            str_3_0 = "Кадастровая стоимость объекта недвижимости определена в соответствии с положениями части 9 статьи 24 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке».";
                            break;
                    }

                }

                #region 0. Заголовок:
                // Добавляем символ разделитель строки "$". Потом он обрабатывается в методе DataExportCommon.SetTextToCellDoc
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "Приложение", 12, HorizontalAlignment.Right, false, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "Разъяснения, связанные с определением кадастровой стоимости", 14, HorizontalAlignment.Center, false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "Государственное бюджетное учреждение города Москвы" + "$" + "«Городской центр имущественных платежей и жилищного страхования»", 12, HorizontalAlignment.Center, false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "«__» _______ " + DateTime.Now.Year.ToString() + " г.",
                    "№______________", 12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "На основании обращения от _____________, поступившего" + /*Environment.NewLine +*/ "  " +
                                                                                          "_________________ г., сообщаем относительно определения кадастровой" + "$" +
                                                                                          "стоимости объекта недвижимости с кадастровым номером " + _unit.CadastralNumber, 12, HorizontalAlignment.Center, false, true);
                #endregion
                Console.WriteLine(" - заголовок выполнено");

                #region 1. Общие сведения:
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "1. Общие сведения:", 12, HorizontalAlignment.Left, false, true);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "№ п/п",
                    "Наименование показателя",
                    "Значение, описание",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center,
                    true, true);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.1",
                    "Кадастровая стоимость",
                    _unit.CadastralCost.ToString()?.Replace(",", "."),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.2",
                    "Дата, по состоянию на которую определена кадастровая стоимость (дата определения кадастровой стоимости)",
                    strDateApp,
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);

                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                if (_unit.PropertyType_Code == PropertyTypes.Stead)
                    DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                        "1.3",
                        "Реквизиты отчета об итогах государственной кадастровой оценки, составленного в соответствии со статьей 14 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке»",
                        "Отчет от 19.11.2018 № 2/2018 «Об итогах государственной кадастровой оценки земельных участков(категория земель «земли населенных пунктов»), расположенных на территории города Москвы по состоянию на 01.01.2018»",
                        12,
                        HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                        true, false);
                else
                    DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                        "1.3",
                        "Реквизиты отчета об итогах государственной кадастровой оценки, составленного в соответствии со статьей 14 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке»",
                        "Отчет от 19.11.2018 № 1/2018 «Об итогах государственной кадастровой оценки зданий, помещений, объектов незавершенного строительства, машино-мест и сооружений, расположенных на территории города Москвы по состоянию на 01.01.2018»",
                        12,
                        HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                        true, false);

                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                string sss = "http://cadastre.gcgs.ru/state/" + "$" + "https://rosreestr.ru/wps/portal/p/cc_ib_portal_services/cc_ib_ais_fdgko";
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.4",
                    "Полный электронный адрес размещения отчета об итогах государственной кадастровой оценки в информационно - телекоммуникационной сети «Интернет»",
                    "http://cadastre.gcgs.ru/state/" + "$" + "https://rosreestr.ru/wps/portal/p/cc_ib_portal_services/cc_ib_ais_fdgko",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.5",
                    "Сведения о работнике бюджетного учреждения, созданного субъектом Российской Федерации и наделенного полномочиями, связанными с определением кадастровой стоимости, подготовившем отчет об итогах государственной кадастровой оценки",
                    "Ковалев Д.В." + "$" + "Капитонов К.С.",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.6",
                    "Реквизиты акта определения кадастровой стоимости, составленного в соответствии со статьей 16 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке»",
                    strActReq_01_06,
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.7",
                    "Сведения о работнике бюджетного учреждения, созданного субъектом Российской Федерации и наделенного полномочиями, связанными с определением кадастровой стоимости, определившем кадастровую стоимость в соответствии со статьей 16 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке»",
                    strActReq_01_07,
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.8",
                    "Дата внесения сведений о кадастровой стоимости в Единый государственный реестр недвижимости",
                    "-",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.9",
                    "Дата подачи заявления об оспаривании кадастровой стоимости, по результатам рассмотрения которого определена кадастровая стоимость по решению комиссии по рассмотрению споров о результатах определения кадастровой стоимости или по решению суда",
                    "-",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.10",
                    "Дата начала применения кадастровой стоимости, в том числе в случае изменения кадастровой стоимости по решению комиссии по рассмотрению споров о результатах определения кадастровой стоимости или по решению суда",
                    strActReq_01_10,
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "1.11",
                    "Сведения об органе, его местонахождении, официальном сайте в информационно-телекоммуникационной сети «Интернет», адресе электронной почты, контактных телефонах, в который следует обращаться в отношении исчисления налогов, исчисляемых от кадастровой стоимости объекта недвижимости",
                    "Управление Федеральной налоговой службы по г. Москве" + "$" +
                    "Адрес: 125284, г. Москва, Хорошевское шоссе, д. 12А" + "$" +
                    "https://www.nalog.ru" + "$" +
                    "Телефоны:" + "$" +
                    "Для справок: +7 (495) 400-67-90, +7(495) 400-67- 68" + "$" +
                    "Единый контакт-центр: 8-800-222-2222",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                #endregion

                #region  2. 
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                    "2.Кадастровая стоимость объекта недвижимости определена на основании следующей информации:",
                    12,
                    HorizontalAlignment.Left,
                    false, true);
                #endregion

                Console.WriteLine(" - характеристики ...");
                #region  2.1.  О  характеристиках объекта недвижимости, с использованием которых была определена его кадастровая стоимость:
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                    "2.1. О характеристиках объекта недвижимости, с использованием которых была определена его кадастровая стоимость:",
                    12,
                    HorizontalAlignment.Left,
                    false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);

                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "№ п/п",
                    "Наименование показателя",
                    "Значение, описание",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, true);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.1",
                    "Кадастровый номер объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(_unit.CadastralNumber),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.2",
                    "Вид объекта недвижимости (земельный участок, здание, сооружение, помещение, машино-место, объект незавершенного строительства, единый недвижимый комплекс, предприятие как имущественный комплекс или иной вид)",
                    CheckNullEmpty.CheckStringOut(_unit.PropertyType),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 600, out value_attr);               //Код 600 - Адрес 
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.3",
                    "Адрес объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 8, out value_attr);                 //Код 8 - Местоположение PLACE_OBJECT
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.4",
                    "Описание местоположения объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.5",
                    "Площадь (для земельного участка, здания, помещения или машино-места) или иная основная характеристика (протяженность, глубина, глубина залегания, площадь, объем, высота, площадь застройки - для сооружения, объекта незавершенного строительства) объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(_unit.Square.ToString()?.Replace(",", ".")),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 3, out value_attr); //Код 3 - Категория земель
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.6",
                    "Категория земель, к которой относится земельный участок, если объектом недвижимости является земельный участок",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 4, out value_attr); //Код 4 - Вид использования по документам
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.7",
                    "Вид разрешенного использования объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                long number_attr = -1;
                if (_unit.PropertyType_Code == PropertyTypes.Building) number_attr = 14;
                else if (_unit.PropertyType_Code == PropertyTypes.Construction) number_attr = 22;
                else if (_unit.PropertyType_Code == PropertyTypes.Pllacement) number_attr = 23;
                if (number_attr > 0)
                    DataExportCommon.GetObjectAttribute(_unit, number_attr, out value_attr); //Код  - Назначение
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.8",
                    "Назначение (для зданий, сооружений, помещения, единого недвижимого комплекса, предприятия как имущественного комплекса), проектируемое назначение (для объектов незавершенного строительства) объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 17, out value_attr); //Код 17 - Количество этажей
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.9",
                    "Этажность объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 21, out value_attr); //Код 21 - Материал стен
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.10",
                    "Материал наружных стен объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                //DataExportCommon.GetObjectAttribute(_unit, 21, out value_attr); //Код 21 - Материал стен
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.11",
                    "Обременения (ограничения) объекта недвижимости, использованные при определении кадастровой стоимости",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 46, out value_attr);            //Код 46 - Процент готовности
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.12",
                    "Степень готовности объекта незавершенного строительства в процентах",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                value_attr = "";
                //DataExportCommon.GetObjectAttribute(_unit, 21, out value_attr); //Код 21 - Материал стен
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.1.13",
                    "Иные сведения об объекте недвижимости, использованные при определении кадастровой стоимости",
                    CheckNullEmpty.CheckStringOut(value_attr),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                #endregion
                Console.WriteLine(" - характеристики выполнено");

                #region 2.2. О рынке недвижимости:
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                    "2.2. О рынке недвижимости:",
                    12,
                    HorizontalAlignment.Left,
                    false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "№ п/п",
                    "Наименование показателя",
                    "Значение, описание",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center,
                    true, true);

                //OMGroupToMarketSegmentRelation segment = OMGroupToMarketSegmentRelation.Where(x => x.GroupId == group_unit.Id).SelectAll().ExecuteFirstOrDefault();
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.2.1",
                    "Сегмент рынка объектов недвижимости, к которому отнесен объект недвижимости",
                    CheckNullEmpty.CheckStringOut(group_unit.MarketSegment),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.2.2",
                    "Краткая характеристика особенностей функционирования сегмента рынка объектов недвижимости, к которому отнесен объект недвижимости (с указанием на страницы отчета об итогах государственной кадастровой оценки, где содержится полная характеристика сегмента рынка объектов недвижимости, в том числе анализ рыночной информации о ценах сделок (предложений) в таком сегменте, затрат на строительство объектов недвижимости)",
                    CheckNullEmpty.CheckStringOut(group_unit.MarketSegmentFunctioningFeatures),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.2.3",
                    "Характеристика ценовой зоны, в которой находится объект недвижимости, в том числе характеристика типового объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(group_unit.PriceZoneCharacteristic),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);

                #endregion

                Console.WriteLine(" - факторы ...");
                #region 2.3. Факторы:
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                    "2.3. Перечень ценообразующих факторов, использованных для определения кадастровой стоимости объекта недвижимости, их значения и источники сведений о них:",
                    12,
                    HorizontalAlignment.Left,
                    false, true);

                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells);
                DataExportCommon.SetText4Doc(document, table.Rows[idx_row],
                    "№ п/п",
                    "Наименование",
                    "Значение",
                    "Источник",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center,
                    true, true);

                int pp = 0;

                #region Calc Group
                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding)//  subgroup.Type_SubGroup == 9)
                {
                    if (calc_group != null)
                    {
                        OMModel model_calc = OMModel.Where(x => x.GroupId == calc_group.Id && x.IsActive.Coalesce(false) == true).SelectAll().ExecuteFirstOrDefault();
                        if (model_calc != null)
                        {
                            if (model_calc.ModelFactor.Count == 0)
                                model_calc.ModelFactor = OMModelFactor.Where(x => x.ModelId == model_calc.Id && x.AlgorithmType_Code == model_calc.AlgoritmType_Code).SelectAll().Execute();

                            foreach (OMModelFactor factor in model_calc.ModelFactor)
                            {
                                string attribute_name = "-";
                                long attribute_id = -1;
                                string attribute_value = "-";
                                string attribute_source = "-";

                                GetAttributeValue(_unit, calc_group, task, factor.FactorId, factor.SignMarket,
                                    out attribute_id,
                                    out attribute_name,
                                    out attribute_value,
                                    out attribute_source);

                                pp++;
                                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells);
                                DataExportCommon.SetText4Doc(document, table.Rows[idx_row],
                                    "2.3." + pp.ToString(),
                                    attribute_name,
                                    attribute_value,
                                    attribute_source,
                                    12,
                                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Center,
                                    true, false);
                            }
                        }
                    }
                }
                #endregion

                #region Group
                OMModel model = OMModel.Where(x => x.GroupId == group_unit.Id && x.IsActive.Coalesce(false) == true).SelectAll().ExecuteFirstOrDefault();
                if (model != null)
                {
                    if (model.ModelFactor.Count == 0)
                        model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id && x.AlgorithmType_Code == model.AlgoritmType_Code).SelectAll().Execute();

                    foreach (OMModelFactor factor in model.ModelFactor)
                    {
                        string attribute_name = "-";
                        long attribute_id = -1;
                        string attribute_value = "-";
                        string attribute_source = "-";

                        GetAttributeValue(_unit, group_unit, task, factor.FactorId, factor.SignMarket,
                            out attribute_id,
                            out attribute_name,
                            out attribute_value,
                            out attribute_source);

                        pp++;
                        idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells);
                        DataExportCommon.SetText4Doc(document, table.Rows[idx_row],
                            "2.3." + pp.ToString(),
                            attribute_name,
                            attribute_value,
                            attribute_source,
                            12,
                            HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Center,
                            true, false);
                    }
                }

                #endregion

                #endregion
                Console.WriteLine(" - факторы выполнено");

                Console.WriteLine(" - методология расчета ...");
                #region 2.4.   Кадастровая   стоимость   объекта   недвижимости   определена  в соответствии со следующей методологией:
                string formula = string.Empty;
                string pr_kn = string.Empty;
                string parent_calc_number = (_unit.ParentCalcNumber == null) ? string.Empty : _unit.ParentCalcNumber;
                bool dd = false;
                bool jj = true;

                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding)//  subgroup.Type_SubGroup == 9)
                {
                    if ((calc_unit != null) && ((parent_calc_number == calc_unit.CadastralNumber) || parent_calc_number == string.Empty))
                    {
                        formula = OMGroup.GetFormulaKoeff(calc_group, true, "УПКС здания, в котором расположено помещение(" + calc_unit.CadastralNumber + ")");
                        dd = true;
                    }
                    else
                    {
                        string[] kk = parent_calc_number.Split(':');
                        string calc_group_num = group_unit.Number;
                        if (kk.Length == 1)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", по субъекту)") +
                                      "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 2)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом районе " + _unit.ParentCalcNumber + ")") +
                                      "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 3)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом квартале " + _unit.ParentCalcNumber + ")") +
                                      "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        jj = false;
                    }
                }
                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.AVG) //subgroup.Type_SubGroup == 10)
                {
                    string[] kk = parent_calc_number.Split(':');
                    string calc_group_num = group_unit.Number;
                    if (kk.Length == 1)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + _unit.ParentCalcNumber + ", по субъекту)") +
                                  "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    if (kk.Length == 2)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + _unit.ParentCalcNumber + ", в кадастровом районе " + _unit.ParentCalcNumber + ")") +
                                  "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    if (kk.Length == 3)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + _unit.ParentCalcNumber + ", в кадастровом квартале " + _unit.ParentCalcNumber + ")") +
                                  "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                }
                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.UnComplited) //subgroup.Type_SubGroup == 11)
                {
                    string[] kk = parent_calc_number.Split(':');
                    string calc_group_num = group_unit.Number;
                    if (kk.Length == 1)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", по субъекту)") +
                                  "*Степень готовности объекта незавершенного строительства=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    if (kk.Length == 2)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом районе " + _unit.ParentCalcNumber + ")") +
                                  "*Степень готовности объекта незавершенного строительства=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    if (kk.Length == 3)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом квартале " + _unit.ParentCalcNumber + ")") +
                                  "*Степень готовности объекта незавершенного строительства=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                }
                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.Min) //subgroup.Type_SubGroup == 12)
                {
                    string[] kk1 = parent_calc_number.Split('(');
                    if (kk1.Length > 1)
                    {
                        string ppkk = kk1[1].Replace(")", "").Replace(" ", "");
                        string[] kk = ppkk.Split(':');

                        string calc_group_num = group_unit.Number;
                        if (kk.Length == 1)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Минимальное значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", по субъекту)") +
                                      "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 2)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Минимальное значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом районе " + ppkk + ")") +
                                      "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 3)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Минимальное значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом квартале " + ppkk + ")") +
                                      "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                    }
                }
                if ((group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.Model) && jj)
                {
                    if (!dd)
                    {
                        formula = OMGroup.GetFormulaFull(group_unit, true) + "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    else
                    {
                        formula = formula + "$$" + "УПКС здания, в котором расположено помещение (" + pr_kn + ")=" +
                                  OMGroup.GetFormulaFull(calc_group, false) + "=" + calc_unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                }
                if ((group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.AVG) && (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding))
                {
                    if ((calc_unit != null) && ((parent_calc_number == calc_unit.CadastralNumber) || parent_calc_number == string.Empty))
                    {
                        string[] kk = parent_calc_number.Split(':');
                        string calc_group_num = calc_group.Number;
                        if (kk.Length == 1)
                        {
                            formula = formula + "$$" +
                                      "УПКС здания, в котором расположено помещение (" + pr_kn + ")=" +
                                      OMGroup.GetFormulaKoeff(calc_group, false, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", по субъекту)") +
                                      "=" + calc_unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 2)
                        {
                            formula = formula + "$$" + "УПКС здания, в котором расположено помещение (" + pr_kn + ")=" +
                                      OMGroup.GetFormulaKoeff(calc_group, false, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом районе " + calc_unit.CadastralNumber + ")") +
                                      "=" + calc_unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 3)
                        {
                            formula = formula + "$$" + "УПКС здания, в котором расположено помещение (" + pr_kn + ")=" +
                                      OMGroup.GetFormulaKoeff(calc_group, false, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом квартале " + calc_unit.CadastralNumber + ")") +
                                      "=" + calc_unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                    }
                }

                string dopmodel = group_unit.ModelJustification;
                if (dopmodel != string.Empty)
                    formula = formula + " " + dopmodel;

                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                    "2.4. Кадастровая стоимость объекта недвижимости определена в соответствии со следующей методологией:",
                    12,
                    HorizontalAlignment.Left,
                    false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "№ п/п",
                    "Наименование показателя",
                    "Значение, описание",
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center,
                    true, true);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.4.1",
                    "Примененные подходы при определении кадастровой стоимости объекта недвижимости с обоснованием их выбора",
                    CheckNullEmpty.CheckStringOut(group_unit.AppliedApproachesInCadastralCost),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.4.2",
                    "Примененные методы оценки при определении кадастровой стоимости объекта недвижимости с обоснованием их выбора",
                    CheckNullEmpty.CheckStringOut(group_unit.AppliedEvaluationMethodsInCadastralCost),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.4.3",
                    "Способ определения кадастровой стоимости объекта недвижимости (массовая или индивидуальная оценка в отношении объектов недвижимости) с обоснованием его выбора",
                    CheckNullEmpty.CheckStringOut(group_unit.CadastralCostDetermingMethod),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.4.4",
                    "Модель определения кадастровой стоимости объекта недвижимости с обоснованием ее выбора",
                    CheckNullEmpty.CheckStringOut(formula),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.4.5",
                    "Сегмент объектов недвижимости, к которому относится объект недвижимости, с обоснованием его выбора",
                    CheckNullEmpty.CheckStringOut(group_unit.ObjectsSegment),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.4.6",
                    "Группа (подгруппа) объектов недвижимости, к которой относится объект недвижимости, с обоснованием ее выбора",
                    CheckNullEmpty.CheckStringOut(group_unit.ObjectsSubgroup),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                    "2.4.7",
                    "Краткое описание последовательности определения кадастровой стоимости объекта недвижимости",
                    CheckNullEmpty.CheckStringOut(group_unit.CadastralCostCalculationOrderDescription),
                    12,
                    HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                    true, false);

                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                #endregion
                Console.WriteLine(" - методология расчета выполнено");

                #region  3. 
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row], "3. Иная информация по запросу заявителя:", str_3_0, 12, HorizontalAlignment.Left, HorizontalAlignment.Center, false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                #endregion

                #region  Подпись 
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "Начальник отдела по работе с разъяснениями",
                    " ",
                    12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "ГБУ «Центр имущественных платежей и жилищного страхования»",
                    "Н.А. Завьялова",
                    12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "Исполнитель:",
                    " ",
                    12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "Асоян А.С./Андреева И.В./Прусенкова О.В./Илюхин Б.В.",
                    " ",
                    12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                #endregion

                #endregion

                Console.WriteLine(" - сохранение ...");
                #region Задаем параметры страницы и сохраняем таблицу и документ
                var section = new Section(document, table);
                PageSetup ps = new PageSetup();
                ps.PageHeight = 16838f / 15f;
                ps.PageWidth = 11906f / 15f;
                PageMargins pm = new PageMargins();
                pm.Left = 96f / 2.54f * 2.0f;
                pm.Right = 96f / 2.54f * 1.5f;
                pm.Top = 96f / 2.54f * 2.0f;
                pm.Bottom = 96f / 2.54f * 2.0f;
                ps.PageMargins = pm;
                section.PageSetup = ps;
                document.Sections.Add(section);

                MemoryStream stream = new MemoryStream();
                document.Save(stream, GemBox.Document.SaveOptions.DocxDefault);
                stream.Seek(0, SeekOrigin.Begin);
                #endregion
                Console.WriteLine(" - сохранение выполнено");
                Console.WriteLine("Выгрузка Otvet выполнено");

                return stream;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ErrorManager.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Получить значения аттрибутов фактора
        /// </summary>
        public static void GetAttributeValue(OMUnit _unit, OMGroup _group, OMTask _task, long? _factor_id, bool _sign_market,
            out long attr_id, out string attr_name, out string attr_value, out string attr_source)
        {
            RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(_factor_id));

            attr_id = (attribute_factor != null) ? attribute_factor.Id : -1;
            attr_name = (attribute_factor != null) ? attribute_factor.Name : "-";
            attr_value = "-";
            attr_source = "-";

            int? factorReestrId = OMGroup.GetFactorReestrId(_group);
            List<CalcItem> FactorValuesGroup = new List<CalcItem>();
            DataTable data = (factorReestrId != null) ?
                RegisterStorage.GetAttributes((int)_unit.Id, factorReestrId.Value) :
                null;
            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    var numberValue = row.ItemArray[7].ParseToDecimalNullable();
                    if (numberValue.HasValue)
                        FactorValuesGroup.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(), numberValue.ToString().Replace(",", ".")));
                    else
                        FactorValuesGroup.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(), row.ItemArray[7].ParseToString()));
                }
                CalcItem factor_item = (FactorValuesGroup.Count > 0) ?
                    FactorValuesGroup.Find(x => x.FactorId == _factor_id) :
                    null;
                if (factor_item != null)
                {
                    attr_value = factor_item.Value;

                    //Ищем источник в OMFactorSetting в KO, если нет, то в ГБУ
                    OMFactorSettings factor_sett = OMFactorSettings.Where(x => x.FactorId == attribute_factor.Id).SelectAll().ExecuteFirstOrDefault();
                    attr_source = (factor_sett != null) ? factor_sett.Source : GetSourceAttributeFromGbu(_unit, attr_id, factor_item.Value, _task.EstimationDate);

                    #region Если есть метка, получаем результирущий коэффициент в подставляемое значение
                    if (_sign_market.ParseToBoolean())
                    {
	                    var marks = new List<OMModelingDictionariesValues>();
	                    var dictionaryId = new ModelingService().GetDictionaryId(_group.Id, factor_item.FactorId);
                        if(dictionaryId == null)
                            return;
                        
	                    marks.AddRange(new ModelDictionaryService().GetMarks(dictionaryId.Value));

	                    string temp_val = attr_value;
	                    var mc = marks.Find(x => x.Value.ToUpper() == temp_val.ToUpper().Replace('.', ','));
	                    if (mc == null)
		                    mc = marks.Find(x => x.Value.ToUpper() == temp_val.ToUpper().Replace(',', '.'));

	                    if (mc != null)
	                    {
		                    attr_value = attr_value + " (подставляемое значение: " + mc.CalculationValue.ToString().Replace(',', '.').Replace(".00000000000000000000", ".00") + ")";
	                    }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// Получить расчетную группц объекта оценки
        /// </summary>
        public static void GetCalcGroupFromUnit(OMUnit _unit, DateTime? _estimatedate, out OMUnit unit_out, out OMGroup group_out)
        {
            unit_out = null;
            group_out = null;

            string value_attr = "";
            DataExportCommon.GetObjectAttribute(_unit, 604, out value_attr);  //Код 604 - Кадастровый номер здания или сооружения, в котором расположено помещение

            List<OMUnit> units_parent = OMUnit.Where(x => x.CadastralNumber == value_attr).SelectAll().Execute();
            if (units_parent.Count == 0) return;

            DateTime? date_temp = new DateTime(2010, 1, 1);
            TimeSpan? date_near = _estimatedate - date_temp;
            foreach (OMUnit unit_par in units_parent)
            {
                OMTask task = OMTask.Where(x => x.Id == unit_par.TaskId).SelectAll().ExecuteFirstOrDefault();
                if (task != null)
                {
                    if (_estimatedate - task.EstimationDate < date_near)
                    {
                        date_near = _estimatedate - task.EstimationDate;
                        unit_out = unit_par;
                        group_out = OMGroup.Where(x => x.Id == unit_par.GroupId).SelectAll().ExecuteFirstOrDefault();
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Получить источник аттрибута объекта КО из ГБУ
        /// </summary>
        /// <param name="_unit">Объект КО</param>
        /// <param name="_id">Идентификатор аттрибута объекта КО</param>
        /// <param name="_value">Значение аттрибута объекта КО</param>
        /// <param name="_date">Дата задания на оценку</param>
        /// <returns></returns>
        public static string GetSourceAttributeFromGbu(OMUnit _unit, long _id, string _value, DateTime? _date)
        {
            string source_out = "-";

            OMTransferAttributes transferAttribute = OMTransferAttributes
                .Where(x => x.KoId == _id)
                .SelectAll()
                .ExecuteFirstOrDefault();

            if (transferAttribute != null)
            {
                List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(_unit.ObjectId.Value,
                    null,
                    new List<long> { transferAttribute.GbuId },
                    _date, 
                    attributesToDownload: new List<GbuColumnsToDownload> {GbuColumnsToDownload.Value});

                if (attribs.Count > 0)
                {
                    var attribute_source = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == attribs.First().RegisterData.Id);
                    if (attribute_source != null)
                    {
                        source_out = attribute_source.Description;
                    }
                }
            }

            return source_out;
        }
    }
}