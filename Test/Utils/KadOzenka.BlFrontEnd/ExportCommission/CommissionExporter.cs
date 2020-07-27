using Core.ErrorManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KadOzenka.BlFrontEnd.ExportCommission
{
    public class NullConvertor
    {
        public static string ToString(object value)
        {
            return (value == null) ? String.Empty : value.ToString();
        }
        public static int DBToInt(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return int.MinValue;
            else
                return Convert.ToInt32(value);
        }
        public static int DBToInt(object value, int _default)
        {
            if ((value == null) || (value == DBNull.Value))
                return _default;
            else
                return Convert.ToInt32(value);
        }
        public static Int64 DBToInt64(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return int.MinValue;
            else
                return Convert.ToInt64(value);
        }
        public static double DBToDouble(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return double.MinValue;
            else
                if (value.ToString() == "-")
                return 0;
            else
                return Convert.ToDouble(value);
        }
        public static DateTime DBToDateTime(object value)
        {
            if ((value == null) || (value == DBNull.Value) || (value.ToString() == String.Empty))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(value);
        }
        public static decimal DBToDecimal(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return decimal.MinValue;
            else
                if (value.ToString() == "-")
                return 0;
            else
                return Convert.ToDecimal(value);
        }
        public static bool ToString(object value, out string result)
        {
            result = ToString(value);
            return (result != null);
        }
        public static bool ToString2(object value, out string result)
        {
            result = ToString(value);
            return (result != null && result!="-" && result!=string.Empty);
        }
        public static bool DBToInt(object value, out int result)
        {
            result = DBToInt(value);
            return (result != int.MinValue);
        }
        public static bool DBToInt64(object value, out long result)
        {
            result = DBToInt64(value);
            return (result != int.MinValue);
        }
        public static bool DBToDouble(object value, out double result)
        {
            result = DBToDouble(value);
            return (result != double.MinValue);
        }
        public static bool DBToDateTime(object value, out DateTime result)
        {
            result = DBToDateTime(value);
            return (result != DateTime.MinValue);
        }
        public static bool DBToDecimal(object value, out decimal result)
        {
            result = DBToDecimal(value);
            return (result != decimal.MinValue);
        }
    }

    public static class CommissionExporter
    {
        private static string SQL_connection = "server= x1carbon; user id=sa; password=dellvostro1015; database=GBU; connection timeout=30";

        public static void DoLoadBd()
        {
            LoadObject();
        }
        public static void DoLoadExcel()
        {
            ImportDataFromExcel();
        }

        public static void LoadObject()
        {
            //ApplicantStatus (Статус заявителя) 
            //0 бит - ДГИ
            //1 бит - ИП
            //2 бит - ОГВ
            //3 бит - ФЛ
            //4 бит - ЮЛ

            //CommissionType (Тип комиссии) 
            //1-по установлению кадастровой стоимости 
            //2-по недостоверности


            //DecisionResult (Решение комиссии) 
            //1-положительное решение
            //2-отказано


            using (SqlConnection connection = new SqlConnection(SQL_connection))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbCostCommission";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Commission.OMCost> Items = new List<ObjectModel.Commission.OMCost>();
                while (myOleDbDataReader.Read())
                {
                    long id = NullConvertor.DBToInt64(myOleDbDataReader["id"]);
                    long tcom = NullConvertor.DBToInt(myOleDbDataReader["type_commission"]);
                    long rd = NullConvertor.DBToInt(myOleDbDataReader["result_decision"]);
                    ObjectModel.Directory.Commission.DecisionResult dr = ObjectModel.Directory.Commission.DecisionResult.Rejected;
                    ObjectModel.Directory.Commission.CommissionType ct = ObjectModel.Directory.Commission.CommissionType.OnUnreliability;
                    if (tcom == 1) ct = ObjectModel.Directory.Commission.CommissionType.OnSetCadCost;
                    if (rd == 1) dr = ObjectModel.Directory.Commission.DecisionResult.Approved;

                    ObjectModel.Commission.OMCost sudObject = new ObjectModel.Commission.OMCost
                    {
                        Id = id,
                        Kn = NullConvertor.ToString(myOleDbDataReader["kn_object"]),
                        DecisionResult_Code = dr,
                        CommissionType_Code = ct,
                    };



                    bool notEmptyKC = (NullConvertor.DBToDecimal(myOleDbDataReader["kc_object"], out decimal kc));
                    if (notEmptyKC) sudObject.Kc = kc;

                    bool notEmptyKCDT = (NullConvertor.DBToDateTime(myOleDbDataReader["date_kc"], out DateTime date_kc));
                    if (notEmptyKCDT) sudObject.DateKc = date_kc;

                    bool notEmptyNS = (NullConvertor.ToString(myOleDbDataReader["num_statement"], out string num_s));
                    if (notEmptyNS) sudObject.StatementNumber = num_s;

                    bool notEmptyDS = (NullConvertor.DBToDateTime(myOleDbDataReader["date_statement"], out DateTime date_s));
                    if (notEmptyDS) sudObject.StatementDate = date_s;

                    bool notEmptySA = (NullConvertor.DBToInt(myOleDbDataReader["status_applicant"], out int sa));
                    if (notEmptySA) sudObject.ApplicantStatus_Code = (ObjectModel.Directory.Commission.ApplicantStatus)sa;//----------------//sa;

                    bool notEmptyND = (NullConvertor.ToString(myOleDbDataReader["num_decision"], out string num_d));
                    if (notEmptyND) sudObject.DecisionNumber = num_d;

                    bool notEmptyDD = (NullConvertor.DBToDateTime(myOleDbDataReader["date_decision"], out DateTime date_d));
                    if (notEmptyDD) sudObject.DecisionDate = date_d;

                    bool notEmptyMV = (NullConvertor.DBToDecimal(myOleDbDataReader["market_value"], out decimal mv));
                    if (notEmptyMV) sudObject.MarketValue = mv;

                    bool notEmptyKCC = (NullConvertor.DBToDecimal(myOleDbDataReader["kc_commission"], out decimal kc_c));
                    if (notEmptyKCC) sudObject.CommissionKc = kc_c;

                    bool notEmptyGC = (NullConvertor.ToString2(myOleDbDataReader["group_commission"], out string gc));
                    if (notEmptyGC) sudObject.CommissionGroup = gc;

                    bool notEmptyCC = (NullConvertor.ToString2(myOleDbDataReader["change_commission"], out string cc));
                    if (notEmptyCC) sudObject.CommissionChange = cc;


                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Commission.OMCost>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Commission.OMCost>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }

        public static Stream ImportDataFromExcel()
        {

            //ExcelFile excelFile;
            //using (var stream = file.OpenReadStream())
            //{
            //    excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
            //}

            string filePath = "C:\\Work\\Комиссии из ПК ГБУ_11112019.xlsx";
            ExcelFile excelFile = ExcelFile.Load(filePath, new XlsxLoadOptions());

            var mainWorkSheet = excelFile.Worksheets[0];

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };

            int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

            mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");
            mainWorkSheet.Rows[0].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
            int count = 0;
            Parallel.ForEach(mainWorkSheet.Rows, options, row =>
            {
                try
                {
                    if (row.Index != 0) //все, кроме заголовков
                    {

                        string kn = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
                        string num_s = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
                        DateTime? date_s = mainWorkSheet.Rows[row.Index].Cells[3].Value.ParseToDateTimeNullable();
                        ObjectModel.Commission.OMCost existObject = ObjectModel.Commission.OMCost.Where(x => x.Kn == kn && x.StatementNumber == num_s && x.StatementDate == date_s).SelectAll().ExecuteFirstOrDefault();
                        bool newobj = false;
                        if (existObject == null)
                        {
                            existObject = new ObjectModel.Commission.OMCost
                            {
                                Id=-1,
                                StatementDate=date_s,
                                StatementNumber = num_s,
                                Kn=kn,
                            };
                            newobj = true;
                        }

                        ObjectModel.Directory.Commission.DecisionResult dr = ObjectModel.Directory.Commission.DecisionResult.Rejected;
                        ObjectModel.Directory.Commission.CommissionType ct = ObjectModel.Directory.Commission.CommissionType.OnUnreliability;
                        string tcom = mainWorkSheet.Rows[row.Index].Cells[10].Value.ParseToString(); 
                        string rd = mainWorkSheet.Rows[row.Index].Cells[6].Value.ParseToString();

                        if (tcom.ToUpper() == "Установление стоимости".ToUpper()) ct = ObjectModel.Directory.Commission.CommissionType.OnSetCadCost;
                        if (rd.ToUpper() == "положительное решение".ToUpper()) dr = ObjectModel.Directory.Commission.DecisionResult.Approved;

                        existObject.DecisionResult_Code = dr;
                        existObject.CommissionType_Code = ct;

                        decimal? d_kc = mainWorkSheet.Rows[row.Index].Cells[8].Value.ParseToDecimalNullable();
                        existObject.Kc = (d_kc!=null)?((d_kc==0)?null:d_kc):(d_kc);
                        existObject.DateKc = mainWorkSheet.Rows[row.Index].Cells[9].Value.ParseToDateTimeNullable();
                        existObject.DecisionNumber = mainWorkSheet.Rows[row.Index].Cells[4].Value.ParseToString();
                        existObject.DecisionDate = mainWorkSheet.Rows[row.Index].Cells[5].Value.ParseToDateTimeNullable();

                        decimal? d_mv = mainWorkSheet.Rows[row.Index].Cells[14].Value.ParseToDecimalNullable();
                        existObject.MarketValue = (d_mv != null) ? ((d_mv == 0) ? null : d_mv) : (d_mv);


                        decimal? d_ckc = mainWorkSheet.Rows[row.Index].Cells[7].Value.ParseToDecimalNullable();
                        existObject.CommissionKc = (d_ckc != null) ? ((d_ckc == 0) ? null : d_ckc) : (d_ckc);
                        existObject.CommissionGroup = mainWorkSheet.Rows[row.Index].Cells[11].Value.ParseToString();
                        existObject.CommissionChange = mainWorkSheet.Rows[row.Index].Cells[12].Value.ParseToString();

                        existObject.ApplicantStatus_Code = (ObjectModel.Directory.Commission.ApplicantStatus)EnumExtensions.GetEnumByDescription<ObjectModel.Directory.Commission.ApplicantStatus>(mainWorkSheet.Rows[row.Index].Cells[13].Value.ParseToString());

                        existObject.Save();


                        if (newobj)
                        {
                            try
                            {
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Новый объект");
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            try
                            {
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Обновлено");
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                            }
                            catch
                            {

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    long errorId = ErrorManager.LogError(ex);
                    mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
                    mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
                    mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                    Console.WriteLine(ex.Message);
                }
                count++;
                if (count % 20 == 0)
                    Console.WriteLine(count.ToString());
            });

            MemoryStream stream = new MemoryStream();
            excelFile.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            excelFile.Save(filePath, SaveOptions.XlsxDefault);
            return stream;
        }

    }
}
