using Core.ConfigParam;
using Core.Shared.Extensions;
using FastReport;
using FastReport.Export.Csv;
using FastReport.Export.Html;
using FastReport.Export.Image;
using FastReport.Export.Mht;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using FastReport.Export.RichText;
using FastReport.Export.Text;
//using Ionic.Zip;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DebugApplication.Report
{
    public abstract class FastReportBase : FastReport.Report
    {
        protected CultureInfo CultureRu = new CultureInfo("ru-RU");

        public ReportType ReportType { get; set; }

        public bool IsAlreadyCreated { get { return Report.PreparedPages != null; } set { } }

        public DateTime? FillDate { get; set; }

        public NameValueCollection Query { get; set; }

        public FastReportBase()
        {
            //FastReport.Utils.Config.ReportSettings.ShowProgress = false;
        }

        public virtual string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        public void Update(NameValueCollection query)
        {
            CreateDocument(TemplateName(query), query);
            IsAlreadyCreated = true;

            Dictionary.Clear();

            DataSet dataSet = GetData(query);

            if (ReportType.FilterValues.Any())
            {
                DataTable dataTable = dataSet.Tables.Add("Filter");
                ReportType.FilterValues.ForEach(x => {
                    //колонки со дополнительным значением должность
                    if (x.ParamName == "Signer" || x.ParamName == "Approved")
                    {
                        dataTable.Columns.Add(string.Format("{0}Post", x.ParamName));
                    }

                    dataTable.Columns.Add(x.ParamName);
                });

                DataRow dataRow = dataTable.NewRow();
                ReportType.FilterValues.ForEach(x =>
                {
                    string value = query[x.ParamName];

                    //колонки со дополнительным значением должность
                    if (x.ParamName == "Signer" || x.ParamName == "Approved")
                    {
                        int postDelimiterIndex = value.IndexOf('-');
                        string postValue = string.Empty;

                        if (postDelimiterIndex >= 0)
                        {
                            int postLength = value.Length - postDelimiterIndex;

                            //должность (+- 1 удаляет разделитель)
                            postValue = value.Substring(postDelimiterIndex + 1, postLength - 1)
                                .Trim();

                            value = value.Substring(0, postDelimiterIndex).Trim();
                        }

                        dataRow.SetField(string.Format("{0}Post", x.ParamName), postValue);
                    }

                    dataRow.SetField(x.ParamName, value);
                });
                dataTable.Rows.Add(dataRow);
            }

            // добавление каждой таблицы из DataSet как источника данных
            foreach (DataTable table in dataSet.Tables)
            {
                string tableName = table.TableName;
                Dictionary.Report.RegisterData(dataSet, tableName);
                GetDataSource(tableName).Enabled = true;

                // в файле *.frx банд должен называться Band<название_таблицы_в_DataSet>, например BandItem
                DataBand dataBand = FindObject("Band" + tableName) as DataBand;
                if (dataBand != null) dataBand.DataSource = GetDataSource(tableName);
            }

            Compressed = true;
            Prepare();

            bool firstAndLastPages = query["FirstAndLastPages"].ParseToBoolean();
            bool withoutFirstAndLastPages = query["WithoutFirstAndLastPages"].ParseToBoolean();

            if (firstAndLastPages)
            {
                while (PreparedPages.Count > 2)
                {
                    PreparedPages.RemovePage(1);
                }
            }

            if (withoutFirstAndLastPages)
            {
                if (PreparedPages.Count > 1) PreparedPages.RemovePage(0);
                if (PreparedPages.Count > 1) PreparedPages.RemovePage(PreparedPages.Count - 1);
            }
        }

        public void ExportToPdf(string path)
        {
            Prepare();
            Export(new PDFExport(), path);
        }

        /*public void ExportToPdf(string path, int maxSizeFile)
        {
            int sizeEmptyPdfFileInKb = 98;
            double sizeOnePagePdfInKb = 12;
            int countPagesInOnePdfFile = Math.Max(1, (int)((maxSizeFile - sizeEmptyPdfFileInKb) / sizeOnePagePdfInKb));
            PDFExport export = new PDFExport();

            if (PreparedPages.Count <= countPagesInOnePdfFile)
            {
                Export(export, path);
            }
            else
            {
                export.PageRange = PageRange.PageNumbers;

                using (var zip = new ZipFile())
                {
                    int countFiles = (int)Math.Round((float)PreparedPages.Count / countPagesInOnePdfFile);

                    for (int i = 0; i < countFiles; i++)
                    {
                        MemoryStream stream = new MemoryStream();
                        int firstPageNumber = (i * countPagesInOnePdfFile) + 1;
                        int endPageNumber = Math.Min((i * countPagesInOnePdfFile) + countPagesInOnePdfFile, PreparedPages.Count);

                        export.PageNumbers = string.Format("{0}-{1}", firstPageNumber, endPageNumber);
                        Export(export, stream);
                        stream.Position = 0;
                        zip.AddEntry(string.Format("report_{0}.pdf", i + 1), stream);
                    }

                    zip.Save(path.Replace(".pdf", ".zip"));
                }
            }
        }*/

        public void ExportToXls(string path)
        {
            Export(new Excel2007Export { Wysiwyg = false }, path);
        }

        public void ExportToXlsx(string path)
        {
            Export(new Excel2007Export { Wysiwyg = false }, path);
        }

        public void ExportToRtf(string path)
        {
            Export(new RTFExport(), path);
        }

        public void ExportToMht(string path)
        {
            Export(new MHTExport(), path);
        }

        public void ExportToHtml(string path)
        {
            Export(new HTMLExport(), path);
        }

        public void ExportToText(string path)
        {
            Export(new TextExport(), path);
        }

        public void ExportToCsv(string path)
        {
            Export(new CSVExport(), path);
        }

        public void ExportToImage(string path)
        {
            Export(new ImageExport(), path);
        }

        public string GetReportName()
        {
            return string.Empty;
        }

        protected virtual DataSet GetData(NameValueCollection query) { return null; }

        protected virtual string TemplateName(NameValueCollection query) { return string.Empty; }

        protected void CreateDocument(string templateName, NameValueCollection query)
        {
            MemoryStream stream = new MemoryStream();
            string filePath = Configuration.GetFullPath(string.Format(@"\Reports\{0}.frx", templateName));

            using (Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                Load(fileStream);
            }
        }

        public virtual long? ObjectId(NameValueCollection query) { return null; }

        protected T GetQueryParam<T>(string paramName, NameValueCollection query)
        {
            string obj = query[paramName];
            FilterValue filter = ReportType.FilterValues.FirstOrDefault(x => x.ParamName == paramName);

            if (filter != null && filter.ParamType == DataType.ObjectsList)
            {
                return obj.DeserializeFromXml<T>();
            }

            return !string.IsNullOrWhiteSpace(obj) ? obj.ParseTo<T>() : default(T);
        }
    }
}
