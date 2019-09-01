using Core.UI.Registers.Reports;
using Core.UI.Registers.Reports.Model;
using ObjectModel.Insur;
using Platform.Reports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

namespace CIPJS.DAL.FastReports
{
    public class RegisterOfPaymentReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query) { { return "RegisterOfPaymentReport"; } }

        //override -> new  сделано ради компиляции
        public new void InitializeFilterValues(long objId, string senderName, bool initialisation)
        {

        }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(GetInvoiceDataTable(ObjectId.Value));
            dataSet.Tables.Add(GetTotalInvoiceDataTable(ObjectId.Value));
            dataSet.Tables.Add(GetCommonDataTable(query, ObjectId.Value));

            return dataSet;
        }
        private DataTable GetTotalInvoiceDataTable(long objectId)
        {
            DataTable dataTable = new DataTable("TotalInvoice");
            dataTable.Columns.Add("TotalSumOpl");

            string sumOpl = string.Empty;
            List<OMInvoice> oMInvoices = OMInvoice.Where(x => x.LinkReestrPay == objectId).SelectAll().Execute();
            if (oMInvoices.Count > 0)
                sumOpl = (oMInvoices.AsEnumerable().Where(x => x.SumOpl.HasValue).Sum(x => x.SumOpl) ?? 0).ToString("n2");

            dataTable.Rows.Add(sumOpl);

            return dataTable;
        }

        private DataTable GetInvoiceDataTable(long objectId)
        {
            DataTable dataTable = new DataTable("Invoice");
            dataTable.Columns.Add("Subject");
            dataTable.Columns.Add("Inn");
            dataTable.Columns.Add("Kpp");
            dataTable.Columns.Add("BicBank");
            dataTable.Columns.Add("Bank");
            dataTable.Columns.Add("NumCard");
            dataTable.Columns.Add("SumOpl");
            dataTable.Columns.Add("NumAcc");
            dataTable.Columns.Add("DateAcc");
            dataTable.Columns.Add("LicAcc");
            dataTable.Columns.Add("Sort");

            string numInvoice;
            List<OMInvoice> oMInvoices = OMInvoice.Where(x => x.LinkReestrPay == objectId).SelectAll().Execute();

            foreach (OMInvoice oMInvoice in oMInvoices)
            {
                numInvoice = oMInvoice.NumInvoice;
                if (oMInvoice.NumInvoice != null && oMInvoice.NumInvoice.IndexOf("ЖП") != -1)
                    numInvoice = numInvoice.Substring(numInvoice.IndexOf("ЖП")).Replace("ЖП", "");
                if (oMInvoice.NumInvoice != null && oMInvoice.NumInvoice.IndexOf("ОИ") != -1)
                    numInvoice = numInvoice.Substring(numInvoice.IndexOf("ОИ")).Replace("ОИ", "");

                dataTable.Rows.Add(
                    oMInvoice.SubjectName,
                    oMInvoice.InnBank,
                    oMInvoice.KppBank,
                    oMInvoice.BicBank,
                    SubjectLineBreak(oMInvoice.BankName, 16, "г."),
                    oMInvoice.NumCard,
                    (oMInvoice.SumOpl.HasValue ? oMInvoice.SumOpl.Value.ToString("n2") : null),
                    oMInvoice.NumInvoice,
                    oMInvoice.DataZakluchenia.HasValue ? oMInvoice.DataZakluchenia.Value.ToShortDateString() : oMInvoice.dateInvoice?.ToShortDateString(),
                    oMInvoice.RachAcc,
                    numInvoice
                    );
            }

            DataView dv = dataTable.DefaultView;
            dv.Sort = "Sort";
            dataTable = dv.ToTable();

            if (oMInvoices.Count == 0)
            {
                dataTable.Rows.Add("", "", "", "", "", "", "", "");
            }

            return dataTable;
        }

        private DataTable GetCommonDataTable(NameValueCollection query, long objectId)
        {
            DataTable dataTable = new DataTable("Common");
            dataTable.Columns.Add("Year");
            dataTable.Columns.Add("Num");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Sign");
            dataTable.Columns.Add("Department");
            dataTable.Columns.Add("PODPISANTHEAD");
            dataTable.Columns.Add("DEPTHEAD");

            string year = string.Empty,
                   num = string.Empty,
                   date = string.Empty;

            OMReestrPay oMReestrPay = OMReestrPay.Where(x => x.EmpId == objectId).SelectAll().Execute().FirstOrDefault();

            string podpisant = GetQueryParam<string>("PodpisantHead", query);
            bool noSign = GetQueryParam<bool>("NoSignHead", query);
            string[] sep = podpisant.Split('-');
            string signHead = !noSign ? "/" + sep[0].ToString().Trim() + "/" : "/                /",
                   departmentHead = !noSign ? AddLineBreak(sep[1].ToString().Trim(), "городского") : string.Empty;

            podpisant = GetQueryParam<string>("Podpisant", query);
            noSign = GetQueryParam<bool>("NoSign", query);
            sep = podpisant.Split('-');
            string sign = !noSign ? "/" + sep[0].ToString().Trim() + "/" : "/                /",
                    department = !noSign ? AddLineBreak(sep[1].ToString().Trim(), "ГБУ") : string.Empty;

            year = DateTime.Now.Year.ToString();
            if (oMReestrPay != null)
            {
                num = oMReestrPay.Num;
                date = oMReestrPay.Date != null ? oMReestrPay.Date.Value.ToShortDateString() : null;
            }

            dataTable.Rows.Add(year,
                num,
                date,
                sign,
                AddLineBreak(department, "и жилищного"),
                signHead,
                departmentHead);

            return dataTable;
        }

        public string AddLineBreak(string text, string checkText)
        {
            if (text.IndexOf(checkText) > 0)
                return text.Replace(" " + checkText, Environment.NewLine + checkText);
            return text;
        }

        public string SubjectLineBreak(string subjectName, int length, string checkWord)
        {
            //subjectName = @"Филиал";
            //subjectName = @"Филиал ""Центральный"" Банка ВТБ (ПАО) г. Москвы";
            //subjectName = "ПАО Сбербанк г. Москва";
            //subjectName = @"АО ""РАЙФФАЙЗЕНБАНКК"" df dd";
            //subjectName = @"АО ""РАЙФФАЙЗЕНБАНК"" dfser re";

            //subjectName = @"Филиал № 7701 Банка ВТБ (ПАО) в г. Москва";
            //subjectName = @"АО ""Альфа - Банк"" г.Москва";
            //subjectName = @"Тульское отделение № 8604 ПАО СБЕРБАНК";
            //subjectName = @"АО ""РАЙФФАЙЗЕНБАНК""";
            //subjectName = @"ПАО Сбербанк ";
            if (subjectName == null)
                return null;

            string[] subjectArray = subjectName.Split(' ');
            subjectName = "";
            string subjectRow = "";

            for (int i = 0; i < subjectArray.Length; i++)
            {
                if (subjectArray[i].ToString().IndexOf(checkWord) > -1)
                {
                    if (i + 1 < subjectArray.Length)
                    {
                        subjectArray[i + 1] = subjectArray[i].ToString() + " " + subjectArray[i + 1].ToString();
                        i += 1;
                    }
                    else
                        subjectArray[i] = subjectArray[i].ToString();
                }

                if (subjectArray[i].Length + subjectRow.Length >= length)
                {
                    if (subjectArray[i].Length > length)
                    {
                        subjectName += subjectRow.Trim() + Environment.NewLine;
                        for (int j = 0; j < subjectArray[i].Length; j += length)
                        {
                            if (subjectArray[i].ToString().Length - j >= length)

                                subjectName += subjectArray[i].ToString().Substring(j, Math.Min(subjectArray[i].ToString().Length - j, length)) + Environment.NewLine;
                            else
                                subjectRow = subjectArray[i].ToString().Substring(j, Math.Min(subjectArray[i].ToString().Length - j, length));
                        }

                    }
                    else
                    {
                        subjectName += subjectRow.Trim() + Environment.NewLine;

                        subjectRow = subjectArray[i].ToString();
                    }
                }
                else
                    subjectRow += " " + subjectArray[i].ToString();
            }

            if (subjectArray.Length == 1)
                subjectRow = subjectArray[0].ToString();

            return (subjectName + " " + subjectRow).Trim();
        }
    }
}