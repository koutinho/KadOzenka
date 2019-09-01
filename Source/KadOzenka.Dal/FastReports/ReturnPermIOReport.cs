using ObjectModel.Insur;
using Platform.Reports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

namespace CIPJS.DAL.FastReports
{
    public class ReturnPermIOReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query) { { return "ReturnPermIOReport"; } }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(GetInvoiceDataTable(query, ObjectId.Value));
            dataSet.Tables.Add(GetTotalInvoiceDataTable(ObjectId.Value));
            dataSet.Tables.Add(GetCommonDataTable(query, ObjectId.Value));

            return dataSet;
        }
        private DataTable GetTotalInvoiceDataTable(long objectId)
        {
            DataTable dataTable = new DataTable("TotalInvoice");
            dataTable.Columns.Add("TotalSumPay");

            string sumOpl = string.Empty;
            List<OMInvoice> oMInvoices = OMInvoice.Where(x => x.LinkReestrPay == objectId).SelectAll().Execute();
            if (oMInvoices.Count > 0)
                sumOpl = (oMInvoices.AsEnumerable().Where(x => x.SumOpl.HasValue).Sum(x => x.SumOpl) ?? 0).ToString("n2", CultureRu);

            dataTable.Rows.Add(sumOpl);

            return dataTable;
        }

        private DataTable GetInvoiceDataTable(NameValueCollection query, long objectId)
        {
            bool unionAcc = GetQueryParam<bool>("UnionAcc", query);
       
            DataTable dataTable = new DataTable("Invoice");
            dataTable.Columns.Add("Subject");
            dataTable.Columns.Add("Inn");
            dataTable.Columns.Add("Kpp");
            dataTable.Columns.Add("BicBank");
            dataTable.Columns.Add("Bank");
            dataTable.Columns.Add("RasAcc");
            dataTable.Columns.Add("NumAcc");
            dataTable.Columns.Add("DateAcc");
            dataTable.Columns.Add("SumPay");
            dataTable.Columns.Add("Sort");

            List<OMInvoice> oMInvoices = OMInvoice.Where(x => x.LinkReestrPay == objectId).SelectAll().Execute();
            if (unionAcc)
            {
                foreach (var oMInvoice in oMInvoices.GroupBy(x => new { x.Inn, x.NumInvoice, x.dateInvoice }))
                {
                    dataTable.Rows.Add(
                            BankLineBreak(oMInvoice.Select(x => x.SubjectName).FirstOrDefault(), 23, "л/с"),
                            oMInvoice.Select(x => x.Inn).FirstOrDefault(),
                            oMInvoice.Select(x => x.Kpp).FirstOrDefault(),
                            oMInvoice.Select(x => x.BicBank).FirstOrDefault(),
                            BankLineBreak(oMInvoice.Select(x => x.BankName).FirstOrDefault(), 16, "г."),
                            oMInvoice.Select(x => x.RachAcc).FirstOrDefault(),
                            oMInvoice.Select(x => x.NumInvoice).FirstOrDefault(),
                            oMInvoice.Select(x => x.dateInvoice).FirstOrDefault()?.ToShortDateString(),
                            oMInvoice.Sum(x => x.SumOpl)?.ToString("n2", CultureRu),
                            GetNumInvoice(oMInvoice.Select(x => x.NumInvoice).FirstOrDefault())
                            );
                }
            }
            else
            {
                foreach (var oMInvoice in oMInvoices)
                {
                    dataTable.Rows.Add(
                            BankLineBreak(oMInvoice.SubjectName, 22, "л/с"),
                            oMInvoice.Inn,
                            oMInvoice.Kpp,
                            oMInvoice.BicBank,
                            BankLineBreak(oMInvoice.BankName, 16, "г."),
                            oMInvoice.NumCard,
                            oMInvoice.NumInvoice,
                            oMInvoice.dateInvoice?.ToShortDateString(),
                            oMInvoice.SumOpl?.ToString("n2", CultureRu),
                            GetNumInvoice(oMInvoice.NumInvoice)
                            );
                }
            }

            DataView dv = dataTable.DefaultView;
            dv.Sort = "Subject";
            dataTable = dv.ToTable();

            if (oMInvoices.Count == 0)
            {
                dataTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
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
            string signHead = !noSign ? "/" + sep[0].ToString().Trim() + "/" : "/                    /",
                   departmentHead = !noSign ? AddLineBreak(sep[1].ToString().Trim(), "городского") : string.Empty;

            podpisant = GetQueryParam<string>("Podpisant", query);
            noSign = GetQueryParam<bool>("NoSign", query);
            sep = podpisant.Split('-');
            string sign = !noSign ? "/" + sep[0].ToString().Trim() + "/" : "/                    /",
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

        private string GetNumInvoice(string numInvoice)
        {
            if (numInvoice != null && numInvoice.IndexOf("ЖП") != -1)
                numInvoice = numInvoice.Substring(numInvoice.IndexOf("ЖП")).Replace("ЖП", "");
            if (numInvoice != null && numInvoice.IndexOf("ОИ") != -1)
                numInvoice = numInvoice.Substring(numInvoice.IndexOf("ОИ")).Replace("ОИ", "");

            return numInvoice;
        }

        public string AddLineBreak(string text, string checkText)
        {
            if (text.IndexOf(checkText) > 0)
                return text.Replace(" " + checkText, Environment.NewLine + checkText);
            return text;
        }

        public string BankLineBreak(string subjectName, int length, string checkWord)
        {
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