using Core.ErrorManagment;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ObjectModel.Directory.Sud;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KadOzenka.BlFrontEnd.ExportSud
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
        public static bool DBToInt(object value, out int result)
        {
            result = DBToInt(value);
            return (result >= 0);
        }
        public static bool DBToInt64(object value, out long result)
        {
            result = DBToInt64(value);
            return (result >= 0);
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

    public static class SudExporter
    {
        private static string FilePath = "C:\\Work\\Загрузка (рассмотренные).xlsx";

        public static void ExportExcel()
        {
            string filename = @"C:\Work\12345.xlsx";
            Stream resultFile = KadOzenka.Dal.DataExport.DataExporterSud.ExportAllDataToExcel();
            using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
            {
                resultFile.CopyTo(output);
            }
        }
        public static void ExportXml()
        {
            string filename = @"C:\Work\12345.xml";
            Stream resultFile = KadOzenka.Dal.DataExport.DataExporterSud.ExportDataToXml();
            using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
            {
                resultFile.CopyTo(output);
            }
        }
        public static void ExportStatExcel()
        {
            string filename = @"C:\Work\12345.xlsx";
            Stream resultFile = KadOzenka.Dal.DataExport.DataExporterSud.ExportStatistic();
            using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
            {
                resultFile.CopyTo(output);
            }
        }
        public static void ExportStatObjectExcel()
        {
            string filename = @"C:\Work\12345.xlsx";
            Stream resultFile = KadOzenka.Dal.DataExport.DataExporterSud.ExportStatisticObject();
            using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
            {
                resultFile.CopyTo(output);
            }
        }
        public static void ExportStatCheckExcel()
        {
            string filename = @"C:\Work\12345.xlsx";
            Stream resultFile = KadOzenka.Dal.DataExport.DataExporterSud.ExportStatisticCheck();
            using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
            {
                resultFile.CopyTo(output);
            }
        }

        public static long GetNewIdUser(long oldId)
        {
            switch (oldId)
            {
                case 1: return 2;
                case 2: return 7110;
                case 3: return 7113;
                case 4: return 7116;
                case 5: return 7119;
                case 6: return 7122;
                case 7: return 7125;
                case 8: return 7128;
                case 9: return 7131;
                case 10: return 7134;
                case 11: return 7137;
                case 12: return 7140;
                case 13: return 7143;
                case 14: return 7146;
                case 15: return 7149;
                case 16: return 7152;
                case 17: return 7155;
                case 18: return 7158;
                case 19: return 7161;
                case 20: return 7164;
                case 21: return 7167;
                case 22: return 7170;
                case 23: return 7173;
                case 24: return 7176;
                case 25: return 7179;
                case 26: return 7182;
                case 27: return 7185;
                case 28: return 7188;
                case 29: return 7191;
                case 30: return 7194;
                case 31: return 7197;
                case 32: return 7200;
                case 33: return 7203;
                case 34: return 7206;
                case 35: return 7209;
                default: return oldId;
            }
        }


        public static void DoLoadBd()
		{
            LoadDict();
            LoadObject();
            LoadObjectStatus();
            LoadDRS();
            LoadSud();
            LoadSudStatus();
            LoadSudLink();
            LoadSudLinkStatus();
            LoadOtchet();
            LoadOtchetStatus();
            LoadOtchetLink();
            LoadOtchetLinkStatus();
            LoadZak();
            LoadZakStatus();
            LoadZakLink();
            LoadZakLinkStatus();
            LoadLog();
            LoadParam();
        }
        public static void DoLoadExcel()
        {
            ImportDataSudFromExcel();
        }

        public static void LoadObject()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbObject";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMObject> Items = new List<ObjectModel.Sud.OMObject>();
                while (myOleDbDataReader.Read())
                {
                    long id = NullConvertor.DBToInt64(myOleDbDataReader["id"]);

                    ObjectModel.Sud.OMObject sudObject = new ObjectModel.Sud.OMObject
                    {
                        Id = id,
                        Typeobj_Code = (SudObjectType)NullConvertor.DBToInt(myOleDbDataReader["typeobj"], 0),
                        Workstat_Code = (ProcessingStatus)NullConvertor.DBToInt(myOleDbDataReader["workstat"], 0),
                        Kn = NullConvertor.ToString(myOleDbDataReader["kn"]),
                        Date = NullConvertor.DBToDateTime(myOleDbDataReader["date"]),

                        NameCenter = NullConvertor.ToString(myOleDbDataReader["name_center"]),
                        StatDgi = NullConvertor.ToString(myOleDbDataReader["stat_dgi"]),
                        Owner = NullConvertor.ToString(myOleDbDataReader["owner"]),
                        Adres = NullConvertor.ToString(myOleDbDataReader["adres"])
                    };

                    bool notEmptySQ = (NullConvertor.DBToDecimal(myOleDbDataReader["square"], out decimal square));
                    if (notEmptySQ)
                        sudObject.Square = square;
                    bool notEmptyKC = (NullConvertor.DBToDecimal(myOleDbDataReader["kc"], out decimal kc));

                    if (notEmptyKC)
                        sudObject.Kc = kc;



                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMObject>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMObject>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadObjectStatus()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbObjectStatus";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMObjectStatus> Items = new List<ObjectModel.Sud.OMObjectStatus>();
                while (myOleDbDataReader.Read())
                {
                    long id = NullConvertor.DBToInt64(myOleDbDataReader["id"]);

                    ObjectModel.Sud.OMObjectStatus sudObject = new ObjectModel.Sud.OMObjectStatus
                    {
                        Id = id,
                        Typeobj = NullConvertor.DBToInt(myOleDbDataReader["typeobj"], 0)==1
                    };

                    if (NullConvertor.DBToInt(myOleDbDataReader["kn"], out int kn)) sudObject.Kn = kn==1;
                    if (NullConvertor.DBToInt(myOleDbDataReader["date"], out int date)) sudObject.Date = date==1;
                    if (NullConvertor.DBToInt(myOleDbDataReader["square"], out int square)) sudObject.Square = square == 1;
                    if (NullConvertor.DBToInt(myOleDbDataReader["kc"], out int kc)) sudObject.Kc = kc == 1;

                    if (NullConvertor.DBToInt(myOleDbDataReader["name_center"], out int nameCenter)) sudObject.NameCenter = nameCenter == 1;
                    if (NullConvertor.DBToInt(myOleDbDataReader["stat_dgi"], out int statDgi)) sudObject.StatDgi = statDgi == 1;
                    if (NullConvertor.DBToInt(myOleDbDataReader["owner"], out int owner)) sudObject.Owner = owner == 1;
                    if (NullConvertor.DBToInt(myOleDbDataReader["adres"], out int adres)) sudObject.Adres = adres == 1;
                    if (NullConvertor.DBToInt(myOleDbDataReader["status"], out int status)) sudObject.Status = status == 1;



                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMObjectStatus>(Items, x=>x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMObjectStatus>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadDRS()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbDRS";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMDRS> Items = new List<ObjectModel.Sud.OMDRS>();
                while (myOleDbDataReader.Read())
                {

                    ObjectModel.Sud.OMDRS sudObject = new ObjectModel.Sud.OMDRS
                    {
                        IdObject = NullConvertor.DBToInt64(myOleDbDataReader["id_object"])
                    };

                    if (NullConvertor.ToString(myOleDbDataReader["drs_group"], out string drs_group)) sudObject.DrsGroup = drs_group;

                    if (NullConvertor.ToString(myOleDbDataReader["drs_sost"], out string drs_sost)) sudObject.DrsSost = drs_sost;
                    if (NullConvertor.ToString(myOleDbDataReader["drs_prichin"], out string drs_prichin)) sudObject.DrsPrichin = drs_prichin;
                    if (NullConvertor.ToString(myOleDbDataReader["drs_owner"], out string drs_owner)) sudObject.DrsOwner = drs_owner;

                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq1"], out decimal drs_sq1)) sudObject.DrsSq1 = drs_sq1;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq2"], out decimal drs_sq2)) sudObject.DrsSq2 = drs_sq2;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq3"], out decimal drs_sq3)) sudObject.DrsSq3 = drs_sq3;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq4"], out decimal drs_sq4)) sudObject.DrsSq4 = drs_sq4;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq5"], out decimal drs_sq5)) sudObject.DrsSq5 = drs_sq5;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq6"], out decimal drs_sq6)) sudObject.DrsSq6 = drs_sq6;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq7"], out decimal drs_sq7)) sudObject.DrsSq7 = drs_sq7;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq8"], out decimal drs_sq8)) sudObject.DrsSq8 = drs_sq8;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_sq9"], out decimal drs_sq9)) sudObject.DrsSq9 = drs_sq9;

                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_updrs"], out decimal drs_updrs)) sudObject.DrsUpdrs = drs_updrs;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["drs_drs"], out decimal drs_drs)) sudObject.DrsDrs = drs_drs;

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMDRS>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMDRS>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadDict()
        {
            int i = 1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbDict";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMDict> Items = new List<ObjectModel.Sud.OMDict>();
                while (myOleDbDataReader.Read())
                {
                    long id = NullConvertor.DBToInt64(myOleDbDataReader["id"]);
                    long id_parent = NullConvertor.DBToInt64(myOleDbDataReader["id_parent"]);
                    string name = NullConvertor.ToString(myOleDbDataReader["name"]);
                    long type = NullConvertor.DBToInt(myOleDbDataReader["type"]);
                    if (name == string.Empty) name = "-";
                    ObjectModel.Sud.OMDict sudObject = new ObjectModel.Sud.OMDict
                    {
                        Id = id,
                        IdParent = id_parent,
                        Name = name,
                        Type = type
                    };
                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMDict>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMDict>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadSud()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbSud";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMSud> Items = new List<ObjectModel.Sud.OMSud>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMSud sudObject = new ObjectModel.Sud.OMSud
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        Name = NullConvertor.ToString(myOleDbDataReader["name"]),
                        Number = NullConvertor.ToString(myOleDbDataReader["number"]),
                        Status = NullConvertor.DBToInt(myOleDbDataReader["status"])
                    };

                    if (NullConvertor.DBToDateTime(myOleDbDataReader["date"], out DateTime date)) sudObject.Date = date;
                    if (NullConvertor.DBToDateTime(myOleDbDataReader["sud_date"], out DateTime sud_date)) sudObject.SudDate = sud_date;

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMSud>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMSud>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadSudStatus()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbSudStatus";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMSudStatus> Items = new List<ObjectModel.Sud.OMSudStatus>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMSudStatus sudObject = new ObjectModel.Sud.OMSudStatus
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        Name = NullConvertor.DBToInt(myOleDbDataReader["name"]),
                        Number = NullConvertor.DBToInt(myOleDbDataReader["number"]),
                        Date = NullConvertor.DBToInt(myOleDbDataReader["date"]),
                        SudDate = NullConvertor.DBToInt(myOleDbDataReader["sud_date"]),
                        Status = NullConvertor.DBToInt(myOleDbDataReader["status"]),
                        Astatus = NullConvertor.DBToInt(myOleDbDataReader["status"])
                    };
                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMSudStatus>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMSudStatus>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadSudLink()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbSudLink";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMSudLink> Items = new List<ObjectModel.Sud.OMSudLink>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMSudLink sudObject = new ObjectModel.Sud.OMSudLink
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        IdObject = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                        IdSud = NullConvertor.DBToInt64(myOleDbDataReader["id_sud"]),
                        Use = NullConvertor.ToString(myOleDbDataReader["use"]),
                        Descr = NullConvertor.ToString(myOleDbDataReader["descr"])
                    };

                    if (NullConvertor.DBToDecimal(myOleDbDataReader["rs"], out decimal rs)) sudObject.Rs = rs;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["uprs"], out decimal uprs)) sudObject.Uprs = uprs;

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMSudLink>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMSudLink>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadSudLinkStatus()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbSudLinkStatus";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMSudLinkStatus> Items = new List<ObjectModel.Sud.OMSudLinkStatus>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMSudLinkStatus sudObject = new ObjectModel.Sud.OMSudLinkStatus
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        IdObject = NullConvertor.DBToInt(myOleDbDataReader["id_object"]),
                        IdSud = NullConvertor.DBToInt(myOleDbDataReader["id_sud"]),
                        Use = NullConvertor.DBToInt(myOleDbDataReader["use"]),
                        Descr = NullConvertor.DBToInt(myOleDbDataReader["descr"]),
                        Rs = NullConvertor.DBToInt(myOleDbDataReader["rs"]),
                        Uprs = NullConvertor.DBToInt(myOleDbDataReader["uprs"]),
                        Status = NullConvertor.DBToInt(myOleDbDataReader["status"])
                    };
                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMSudLinkStatus>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMSudLinkStatus>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadOtchet()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbOtchet";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMOtchet> Items = new List<ObjectModel.Sud.OMOtchet>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMOtchet sudObject = new ObjectModel.Sud.OMOtchet
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        Number = NullConvertor.ToString(myOleDbDataReader["number"]),
                        Org = NullConvertor.ToString(myOleDbDataReader["org"]),
                        Fio = NullConvertor.ToString(myOleDbDataReader["fio"]),
                        Sro = NullConvertor.ToString(myOleDbDataReader["sro"]),
                        Jalob= NullConvertor.DBToInt(myOleDbDataReader["jalob"])
                    };

                    if (NullConvertor.DBToDateTime(myOleDbDataReader["date"], out DateTime date)) sudObject.Date = date;
                    if (NullConvertor.DBToDateTime(myOleDbDataReader["date_in"], out DateTime date_in)) sudObject.DateIn = date_in;

                    if (NullConvertor.DBToInt64(myOleDbDataReader["id_org"], out long id_org)) sudObject.IdOrg = id_org;
                    if (NullConvertor.DBToInt64(myOleDbDataReader["id_fio"], out long id_fio)) sudObject.IdFio = id_fio;
                    if (NullConvertor.DBToInt64(myOleDbDataReader["id_sro"], out long id_sro)) sudObject.IdSro = id_sro;

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMOtchet>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMOtchet>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadOtchetStatus()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbOtchetStatus";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMOtchetStatus> Items = new List<ObjectModel.Sud.OMOtchetStatus>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMOtchetStatus sudObject = new ObjectModel.Sud.OMOtchetStatus
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        Number = NullConvertor.DBToInt(myOleDbDataReader["number"]),
                        IdOrg = NullConvertor.DBToInt(myOleDbDataReader["id_org"]),
                        IdFio = NullConvertor.DBToInt(myOleDbDataReader["id_fio"]),
                        IdSro = NullConvertor.DBToInt(myOleDbDataReader["id_sro"]),
                        Jalob = NullConvertor.DBToInt(myOleDbDataReader["jalob"]),
                        Date = NullConvertor.DBToInt(myOleDbDataReader["date"]),
                        DateIn = NullConvertor.DBToInt(myOleDbDataReader["date_in"]),
                        Status = NullConvertor.DBToInt(myOleDbDataReader["status"])
                    };

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMOtchetStatus>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMOtchetStatus>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadOtchetLink()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbOtchetLink";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMOtchetLink> Items = new List<ObjectModel.Sud.OMOtchetLink>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMOtchetLink sudObject = new ObjectModel.Sud.OMOtchetLink
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        IdObject = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                        IdOtchet = NullConvertor.DBToInt64(myOleDbDataReader["id_otchet"]),
                        Use = NullConvertor.ToString(myOleDbDataReader["use"]),
                        Descr = NullConvertor.ToString(myOleDbDataReader["descr"])
                    };

                    if (NullConvertor.DBToDecimal(myOleDbDataReader["rs"], out decimal rs)) sudObject.Rs = rs;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["uprs"], out decimal uprs)) sudObject.Uprs = uprs;

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMOtchetLink>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMOtchetLink>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadOtchetLinkStatus()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbOtchetLinkStatus";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMOtchetLinkStatus> Items = new List<ObjectModel.Sud.OMOtchetLinkStatus>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMOtchetLinkStatus sudObject = new ObjectModel.Sud.OMOtchetLinkStatus
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        IdObject = NullConvertor.DBToInt(myOleDbDataReader["id_object"]),
                        IdOtchet = NullConvertor.DBToInt(myOleDbDataReader["id_otchet"]),
                        Use = NullConvertor.DBToInt(myOleDbDataReader["use"]),
                        Descr = NullConvertor.DBToInt(myOleDbDataReader["descr"]),
                        Rs = NullConvertor.DBToInt(myOleDbDataReader["rs"]),
                        Uprs = NullConvertor.DBToInt(myOleDbDataReader["uprs"]),
                        Status = NullConvertor.DBToInt(myOleDbDataReader["status"])
                    };

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMOtchetLinkStatus>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMOtchetLinkStatus>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadZak()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbZak";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMZak> Items = new List<ObjectModel.Sud.OMZak>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMZak sudObject = new ObjectModel.Sud.OMZak
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        Number = NullConvertor.ToString(myOleDbDataReader["number"]),

                        Org = NullConvertor.ToString(myOleDbDataReader["org"]),
                        Fio = NullConvertor.ToString(myOleDbDataReader["fio"]),
                        Sro = NullConvertor.ToString(myOleDbDataReader["sro"]),
                        RecUser = NullConvertor.ToString(myOleDbDataReader["rec_user"]),
                        RecLetter = NullConvertor.ToString(myOleDbDataReader["rec_letter"]),
                        RecBefore = NullConvertor.DBToInt(myOleDbDataReader["rec_before"]),
                        RecAfter = NullConvertor.DBToInt(myOleDbDataReader["rec_after"]),
                        RecSoglas = NullConvertor.DBToInt(myOleDbDataReader["rec_soglas"])
                    };

                    if (NullConvertor.DBToDateTime(myOleDbDataReader["date"], out DateTime date)) sudObject.Date = date;
                    if (NullConvertor.DBToDateTime(myOleDbDataReader["rec_date"], out DateTime rec_date)) sudObject.RecDate = rec_date;

                    if (NullConvertor.DBToInt64(myOleDbDataReader["id_org"], out long id_org)) sudObject.IdOrg = id_org;
                    if (NullConvertor.DBToInt64(myOleDbDataReader["id_fio"], out long id_fio)) sudObject.IdFio = id_fio;
                    if (NullConvertor.DBToInt64(myOleDbDataReader["id_sro"], out long id_sro)) sudObject.IdSro = id_sro;

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMZak>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMZak>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadZakStatus()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbZakStatus";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMZakStatus> Items = new List<ObjectModel.Sud.OMZakStatus>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMZakStatus sudObject = new ObjectModel.Sud.OMZakStatus
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        Number = NullConvertor.DBToInt(myOleDbDataReader["number"]),
                        RecUser = NullConvertor.DBToInt(myOleDbDataReader["rec_user"]),
                        RecLetter = NullConvertor.DBToInt(myOleDbDataReader["rec_letter"]),
                        RecBefore = NullConvertor.DBToInt(myOleDbDataReader["rec_before"]),
                        RecAfter = NullConvertor.DBToInt(myOleDbDataReader["rec_after"]),
                        RecSoglas = NullConvertor.DBToInt(myOleDbDataReader["rec_soglas"]),

                        IdOrg = NullConvertor.DBToInt(myOleDbDataReader["id_org"]),
                        IdFio = NullConvertor.DBToInt(myOleDbDataReader["id_fio"]),
                        IdSro = NullConvertor.DBToInt(myOleDbDataReader["id_sro"]),

                        Date = NullConvertor.DBToInt(myOleDbDataReader["date"]),
                        RecDate = NullConvertor.DBToInt(myOleDbDataReader["rec_date"]),
                        Status = NullConvertor.DBToInt(myOleDbDataReader["status"])
                    };

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMZakStatus>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMZakStatus>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadZakLink()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbZakLink";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMZakLink> Items = new List<ObjectModel.Sud.OMZakLink>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMZakLink sudObject = new ObjectModel.Sud.OMZakLink
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        IdObject = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                        IdZak = NullConvertor.DBToInt64(myOleDbDataReader["id_zak"]),
                        Use = NullConvertor.ToString(myOleDbDataReader["use"]),
                        Descr = NullConvertor.ToString(myOleDbDataReader["descr"])
                    };

                    if (NullConvertor.DBToDecimal(myOleDbDataReader["rs"], out decimal rs)) sudObject.Rs = rs;
                    if (NullConvertor.DBToDecimal(myOleDbDataReader["uprs"], out decimal uprs)) sudObject.Uprs = uprs;

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMZakLink>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMZakLink>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadZakLinkStatus()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbZakLinkStatus";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMZakLinkStatus> Items = new List<ObjectModel.Sud.OMZakLinkStatus>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMZakLinkStatus sudObject = new ObjectModel.Sud.OMZakLinkStatus
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        IdObject = NullConvertor.DBToInt(myOleDbDataReader["id_object"]),
                        IdZak = NullConvertor.DBToInt(myOleDbDataReader["id_zak"]),
                        Use = NullConvertor.DBToInt(myOleDbDataReader["use"]),
                        Descr = NullConvertor.DBToInt(myOleDbDataReader["descr"]),

                        Rs = NullConvertor.DBToInt(myOleDbDataReader["rs"]),
                        Uprs = NullConvertor.DBToInt(myOleDbDataReader["uprs"]),
                        Status = NullConvertor.DBToInt(myOleDbDataReader["status"]),
                    };

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMZakLinkStatus>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMZakLinkStatus>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadParam()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbParam";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMParam> Items = new List<ObjectModel.Sud.OMParam>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMParam sudObject = new ObjectModel.Sud.OMParam
                    {
                        Pid = NullConvertor.DBToInt64(myOleDbDataReader["pid"]),
                        IdTable = NullConvertor.DBToInt64(myOleDbDataReader["id_table"]),
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        ParamName = NullConvertor.ToString(myOleDbDataReader["param_name"]),

                        IdUser = GetNewIdUser(NullConvertor.DBToInt64(myOleDbDataReader["id_user"])),
                        DateUser = NullConvertor.DBToDateTime(myOleDbDataReader["date_user"]),
                        ParamStatus_Code = (NullConvertor.DBToInt(myOleDbDataReader["param_status"]) == 1) ? ProcessingStatus.Processed : ProcessingStatus.InWork
                    };

                    if (NullConvertor.DBToDecimal(myOleDbDataReader["param_double"], out decimal paramDouble)) sudObject.ParamDouble = paramDouble;
                    if (NullConvertor.DBToDateTime(myOleDbDataReader["param_date"], out DateTime paramDate)) sudObject.ParamDate = paramDate;
                    if (NullConvertor.ToString(myOleDbDataReader["param_char"], out string paramChar)) sudObject.ParamChar = paramChar;
                    if (NullConvertor.DBToInt(myOleDbDataReader["param_int"], out int paramInt)) sudObject.ParamInt = paramInt;

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMParam>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMParam>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadLog()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_SUD"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbLog";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.Sud.OMLog> Items = new List<ObjectModel.Sud.OMLog>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.Sud.OMLog sudObject = new ObjectModel.Sud.OMLog
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id"]),
                        IdRecord = NullConvertor.DBToInt64(myOleDbDataReader["id_record"]),
                        IdTable = NullConvertor.DBToInt64(myOleDbDataReader["id_table"]),
                        NameTable = NullConvertor.ToString(myOleDbDataReader["name_table"]),
                        TypeOper = NullConvertor.ToString(myOleDbDataReader["type_oper"]),
                        XmlData = NullConvertor.ToString(myOleDbDataReader["xml_data"]),

                        IdUser = GetNewIdUser(NullConvertor.DBToInt64(myOleDbDataReader["id_user"])),
                        DateOper = NullConvertor.DBToDateTime(myOleDbDataReader["date_oper"]),
                    };

                    count++;
                    Items.Add(sudObject);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.Sud.OMLog>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                Console.WriteLine(count);
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.Sud.OMLog>(Items, x => x.Save());
                myOleDbDataReader.Close();
                connection.Close();
            }
        }


        class ErrorRow
        {
            public int rowIndex = 0;
            public bool allerror = false;
            public List<int> colIndex;
            public ErrorRow(int index)
            {
                rowIndex = index;
                colIndex = new List<int>();
            }
        }

        public static Stream ImportDataSudFromExcel()
        {
            ExcelFile excelFile = ExcelFile.Load(FilePath, new XlsxLoadOptions());


            List<ErrorRow> errorrowMain = new List<ErrorRow>();
            List<ErrorRow> errorrowOtcher = new List<ErrorRow>();
            List<ErrorRow> errorrowSud = new List<ErrorRow>();
            List<ErrorRow> errorrowZak = new List<ErrorRow>();
            int counterr = 0;


            var mainWorkSheet = excelFile.Worksheets[0];

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };

            int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
            int countall = 0;
            Parallel.ForEach(mainWorkSheet.Rows, options, row =>
            {
                bool ReadyImport = false;
                ErrorRow errorMain = new ErrorRow(row.Index);
                ErrorRow errorOtchet = new ErrorRow(row.Index);
                ErrorRow errorSud = new ErrorRow(row.Index);
                ErrorRow errorZak = new ErrorRow(row.Index);
                string errortext = string.Empty;
                try
                {
                    if (row.Index != 0) //все, кроме заголовков
                    {

                        string cKn = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
                        DateTime? cDate = mainWorkSheet.Rows[row.Index].Cells[4].Value.ParseToDateTimeNullable();
                        ObjectModel.Sud.OMObject sud_object = ObjectModel.Sud.OMObject.Where(x => x.Kn == cKn && x.Date == cDate).SelectAll().ExecuteFirstOrDefault();


                        decimal? cSq = mainWorkSheet.Rows[row.Index].Cells[3].Value.ParseToDecimalNullable();
                        decimal? cKC = mainWorkSheet.Rows[row.Index].Cells[5].Value.ParseToDecimalNullable();
                        string cName_Center = mainWorkSheet.Rows[row.Index].Cells[6].Value.ParseToString();
                        string cStat_Dgi = mainWorkSheet.Rows[row.Index].Cells[7].Value.ParseToString();
                        string cOwner = mainWorkSheet.Rows[row.Index].Cells[8].Value.ParseToString();
                        string cAdres = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
                        ObjectModel.Directory.Sud.SudObjectType cType_code = ObjectModel.Directory.Sud.SudObjectType.None;
                        string cType = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
                        if (cType.ToUpper() == "ЗДАНИЕ") cType_code = ObjectModel.Directory.Sud.SudObjectType.Building;
                        if (cType.ToUpper() == "ПОМЕЩЕНИЕ") cType_code = ObjectModel.Directory.Sud.SudObjectType.Room;
                        if (cType.ToUpper() == "УЧАСТОК") cType_code = ObjectModel.Directory.Sud.SudObjectType.Site;
                        if (cType.ToUpper() == "СООРУЖЕНИЕ") cType_code = ObjectModel.Directory.Sud.SudObjectType.Construction;
                        if (cType.ToUpper() == "ОНС") cType_code = ObjectModel.Directory.Sud.SudObjectType.Ons;
                        if (cType.ToUpper() == "МАШИНОМЕСТО") cType_code = ObjectModel.Directory.Sud.SudObjectType.ParkingPlace;

                        bool newobj = false;

                        if (sud_object == null)
                        {
                            if (cKn != null && cDate != null && cSq != null && cKC != null && cType_code != ObjectModel.Directory.Sud.SudObjectType.None)
                            {
                                sud_object = new ObjectModel.Sud.OMObject
                                {
                                    Kn = cKn,
                                    Date = cDate,
                                    Square = cSq,
                                    Kc = cKC,
                                    NameCenter = cName_Center,
                                    StatDgi = cStat_Dgi,
                                    Owner = cOwner,
                                    Adres = cAdres,
                                    Typeobj_Code = cType_code,
                                    Workstat_Code = ObjectModel.Directory.Sud.ProcessingStatus.Processed
                                };
                                newobj = true;
                                sud_object.SaveAndCheckParam();
                                ReadyImport = true;
                            }
                            else
                            {
                                ReadyImport = false;
                                errortext = "Отсутствует тип объекта/площадь/оспариваемая стоимость";
                                errorMain.allerror = true;
                                if (cSq == null)
                                {
                                    errorMain.colIndex.Add(3);
                                }
                                if (cKC == null)
                                {
                                    errorMain.colIndex.Add(5);
                                }
                            }

                        }
                        else
                        {
                            cSq = (cSq == null) ? sud_object.Square : cSq;
                            cKC = (cKC == null) ? sud_object.Kc : cKC;
                            cName_Center = (cName_Center == string.Empty) ? sud_object.NameCenter : cName_Center;
                            cStat_Dgi = (cStat_Dgi == string.Empty) ? sud_object.StatDgi : cStat_Dgi;
                            cOwner = (cOwner == string.Empty) ? sud_object.Owner : cOwner;
                            cAdres = (cAdres == string.Empty) ? sud_object.Adres : cAdres;
                            cType_code = (cType_code == ObjectModel.Directory.Sud.SudObjectType.None) ? sud_object.Typeobj_Code : cType_code;
                            ReadyImport = true;

                            if (cType_code != sud_object.Typeobj_Code && cType_code != ObjectModel.Directory.Sud.SudObjectType.None)
                            {
                                errorMain.colIndex.Add(1);
                            }
                            if (cAdres.ToUpper() != sud_object.Adres.ToUpper() && cAdres != string.Empty)
                            {
                                errorMain.colIndex.Add(2);
                            }
                            if (cSq != sud_object.Square && cSq != null)
                            {
                                errorMain.colIndex.Add(3);
                            }
                            if (cKC != sud_object.Kc && cKC != null)
                            {
                                errorMain.colIndex.Add(5);
                            }
                            if (cName_Center.ToUpper() != sud_object.NameCenter.ToUpper() && cName_Center != string.Empty)
                            {
                                errorMain.colIndex.Add(6);
                            }
                            if (cStat_Dgi.ToUpper() != sud_object.StatDgi.ToUpper() && cStat_Dgi != string.Empty)
                            {
                                errorMain.colIndex.Add(7);
                            }
                            if (cOwner.ToUpper() != sud_object.Owner.ToUpper() && cOwner != string.Empty)
                            {
                                errorMain.colIndex.Add(8);
                            }

                            sud_object.Kn = cKn;
                            sud_object.Date = cDate;
                            sud_object.Square = cSq;
                            sud_object.Kc = cKC;
                            sud_object.NameCenter = cName_Center;
                            sud_object.StatDgi = cStat_Dgi;
                            sud_object.Owner = cOwner;
                            sud_object.Adres = cAdres;
                            sud_object.Typeobj_Code = cType_code;

                            sud_object.SaveAndCheckParam();
                        }

                        if (ReadyImport)
                        {
                            #region Отчеты
                            string oNumber = mainWorkSheet.Rows[row.Index].Cells[9].Value.ParseToString();
                            DateTime? oDate = mainWorkSheet.Rows[row.Index].Cells[10].Value.ParseToDateTimeNullable();
                            DateTime? oDateIn = mainWorkSheet.Rows[row.Index].Cells[17].Value.ParseToDateTimeNullable();
                            string oOrgName = mainWorkSheet.Rows[row.Index].Cells[12].Value.ParseToString();
                            string oFioName = mainWorkSheet.Rows[row.Index].Cells[13].Value.ParseToString();
                            string oSroName = mainWorkSheet.Rows[row.Index].Cells[14].Value.ParseToString();
                            string oJalob = mainWorkSheet.Rows[row.Index].Cells[18].Value.ParseToString();

                            ObjectModel.Sud.OMOtchet sud_otchet = ObjectModel.Sud.OMOtchet.Where(x => x.Number == oNumber).SelectAll().ExecuteFirstOrDefault();
                            ObjectModel.Sud.OMDict oOrg = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == oOrgName.ToUpper() && x.Type == 1).SelectAll().ExecuteFirstOrDefault();
                            if (oOrg == null && oOrgName != string.Empty)
                            {
                                oOrg = new ObjectModel.Sud.OMDict
                                {
                                    Id = -1,
                                    Name = oOrgName,
                                    Type = 1,
                                    IdParent = -1,
                                };
                                oOrg.Save();
                            }
                            ObjectModel.Sud.OMDict oFio = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == oFioName.ToUpper() && x.Type == 3).SelectAll().ExecuteFirstOrDefault();
                            if (oFio == null && oFioName != string.Empty)
                            {
                                oFio = new ObjectModel.Sud.OMDict
                                {
                                    Id = -1,
                                    Name = oFioName,
                                    Type = 3,
                                    IdParent = -1,
                                };
                                oFio.Save();
                            }
                            ObjectModel.Sud.OMDict oSro = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == oSroName.ToUpper() && x.Type == 2).SelectAll().ExecuteFirstOrDefault();
                            if (oSro == null && oSroName != string.Empty)
                            {
                                oSro = new ObjectModel.Sud.OMDict
                                {
                                    Id = -1,
                                    Name = oSroName,
                                    Type = 2,
                                    IdParent = -1,
                                };
                                oSro.Save();
                            }


                            bool newotch = false;

                            if (sud_otchet == null)
                            {
                                if (oNumber != string.Empty)
                                {
                                    sud_otchet = new ObjectModel.Sud.OMOtchet
                                    {
                                        Id = -1,
                                        Number = oNumber,
                                        Date = oDate,
                                        DateIn = oDateIn,
                                        Jalob = (oJalob.ToUpper() == "ДА") ? 1 : 0,
                                        Fio = (oFio == null) ? string.Empty : oFio.Name,
                                        IdFio = (oFio == null) ? (long?)null : oFio.Id,
                                        Org = (oOrg == null) ? string.Empty : oOrg.Name,
                                        IdOrg = (oOrg == null) ? (long?)null : oOrg.Id,
                                        Sro = (oSro == null) ? string.Empty : oSro.Name,
                                        IdSro = (oSro == null) ? (long?)null : oSro.Id,
                                    };
                                    sud_otchet.SaveAndCheckParam();
                                    newotch = true;
                                }
                            }
                            if (sud_otchet != null)
                            {
                                if (!newotch)
                                {

                                    oDate = (oDate == null) ? sud_otchet.Date : oDate;
                                    oDateIn = (oDateIn == null) ? sud_otchet.DateIn : oDateIn;
                                    long? lJalob = (oJalob == string.Empty) ? sud_otchet.Jalob : ((oJalob.ToUpper() == "ДА") ? 1 : 0);

                                    long? oSroId = (oSro == null) ? sud_otchet.IdSro : oSro.Id;
                                    oSroName = (oSro == null) ? sud_otchet.Sro : oSro.Name;

                                    long? oFioId = (oFio == null) ? sud_otchet.IdFio : oFio.Id;
                                    oFioName = (oFio == null) ? sud_otchet.Fio : oFio.Name;

                                    long? oOrgId = (oOrg == null) ? sud_otchet.IdOrg : oOrg.Id;
                                    oOrgName = (oOrg == null) ? sud_otchet.Org : oOrg.Name;

                                    if ((sud_otchet.Date != oDate) && (oDate != null))
                                    {
                                        errorOtchet.colIndex.Add(10);
                                    }
                                    if (sud_otchet.DateIn != oDateIn && oDateIn != null)
                                    {
                                        errorOtchet.colIndex.Add(17);
                                    }
                                    if (sud_otchet.Jalob != ((oJalob.ToUpper() == "ДА") ? 1 : 0) && oJalob.ToUpper() != string.Empty)
                                    {
                                        errorOtchet.colIndex.Add(18);
                                    }
                                    if (sud_otchet.Org.ToUpper() != oOrgName.ToUpper() && oOrgName != string.Empty)
                                    {
                                        errorOtchet.colIndex.Add(12);
                                    }
                                    if (sud_otchet.Sro.ToUpper() != oSroName.ToUpper() && oSroName != string.Empty)
                                    {
                                        errorOtchet.colIndex.Add(14);
                                    }
                                    if (sud_otchet.Fio.ToUpper() != oFioName.ToUpper() && oFioName != string.Empty)
                                    {
                                        errorOtchet.colIndex.Add(13);
                                    }

                                    sud_otchet.Number = oNumber;
                                    sud_otchet.Date = oDate;
                                    sud_otchet.DateIn = oDateIn;
                                    sud_otchet.Jalob = lJalob;
                                    sud_otchet.Fio = oFioName;
                                    sud_otchet.IdFio = oFioId;
                                    sud_otchet.Org = oOrgName;
                                    sud_otchet.IdOrg = oOrgId;
                                    sud_otchet.Sro = oSroName;
                                    sud_otchet.IdSro = oSroId;

                                    sud_otchet.SaveAndCheckParam();
                                }


                                {
                                    decimal? cRc = mainWorkSheet.Rows[row.Index].Cells[15].Value.ParseToDecimalNullable();
                                    decimal? cUc = mainWorkSheet.Rows[row.Index].Cells[16].Value.ParseToDecimalNullable();
                                    string cUse = mainWorkSheet.Rows[row.Index].Cells[11].Value.ParseToString();

                                    if (cRc != null || cUc != null)
                                    {
                                        ObjectModel.Sud.OMOtchetLink sud_otchet_link = ObjectModel.Sud.OMOtchetLink.Where(x => x.IdObject == sud_object.Id && x.IdOtchet == sud_otchet.Id).SelectAll().ExecuteFirstOrDefault();
                                        if (sud_otchet_link == null)
                                        {
                                            sud_otchet_link = new ObjectModel.Sud.OMOtchetLink
                                            {
                                                Id = -1,
                                                IdObject = sud_object.Id,
                                                IdOtchet = sud_otchet.Id,
                                                Use = cUse,
                                                Rs = cRc,
                                                Uprs = cUc,
                                            };
                                        }
                                        else
                                        {

                                            cUse = (cUse == string.Empty) ? sud_otchet_link.Use : cUse;
                                            cRc = (cRc == null) ? sud_otchet_link.Rs : cRc;
                                            cUc = (cUc == null) ? sud_otchet_link.Uprs : cUc;



                                            if ((sud_otchet_link.Use.ToUpper() != cUse.ToUpper()) && (cUse != string.Empty))
                                            {
                                                errorOtchet.colIndex.Add(11);
                                            }
                                            if ((sud_otchet_link.Rs != cRc) && (cRc != null))
                                            {
                                                errorOtchet.colIndex.Add(15);
                                            }
                                            if ((sud_otchet_link.Uprs != cUc) && (cUc != null))
                                            {
                                                errorOtchet.colIndex.Add(16);
                                            }

                                            sud_otchet_link.Use = cUse;
                                            sud_otchet_link.Rs = cRc;
                                            sud_otchet_link.Uprs = cUc;

                                            sud_otchet_link.SaveAndCheckParam();
                                        }
                                    }
                                    else
                                    {
                                        errorOtchet.colIndex.Add(15);
                                        errorOtchet.colIndex.Add(16);
                                    }
                                }
                            }
                            else
                            {
                                if (oNumber == string.Empty && (mainWorkSheet.Rows[row.Index].Cells[10].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[11].Value.ParseToString() != string.Empty || oOrgName != string.Empty || oFioName != string.Empty || oSroName != string.Empty || mainWorkSheet.Rows[row.Index].Cells[15].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[16].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[17].Value.ParseToString() != string.Empty || oJalob != string.Empty))
                                {
                                    errorOtchet.colIndex.Add(9);
                                }
                            }
                            #endregion

                            #region Судебные решения
                            string sName = mainWorkSheet.Rows[row.Index].Cells[19].Value.ParseToString();
                            string sStatus = mainWorkSheet.Rows[row.Index].Cells[20].Value.ParseToString();
                            string sNumber = mainWorkSheet.Rows[row.Index].Cells[21].Value.ParseToString();
                            DateTime? sDate = mainWorkSheet.Rows[row.Index].Cells[22].Value.ParseToDateTimeNullable();
                            DateTime? sDateAct = mainWorkSheet.Rows[row.Index].Cells[23].Value.ParseToDateTimeNullable();
                            long? lStatus = -1;
                            switch (sStatus.ToLower())
                            {
                                case "без статуса":
                                    lStatus = 0;
                                    break;
                                case "удовлетворено":
                                    lStatus = 1;
                                    break;
                                case "отказано":
                                    lStatus = 2;
                                    break;
                                case "приостановлено":
                                    lStatus = 3;
                                    break;
                                case "частично удовлетворено":
                                    lStatus = 4;
                                    break;
                            }

                            ObjectModel.Sud.OMSud sud_sud = ObjectModel.Sud.OMSud.Where(x => x.Number == sNumber).SelectAll().ExecuteFirstOrDefault();

                            bool newsud = false;
                            if (sud_sud == null)
                            {
                                if (sNumber != string.Empty)
                                {
                                    sud_sud = new ObjectModel.Sud.OMSud
                                    {
                                        Id = -1,
                                        Date = sDate,
                                        Name = sName,
                                        Number = sNumber,
                                        Status = (lStatus == -1) ? 0 : lStatus,
                                        SudDate = sDateAct,
                                    };
                                    sud_sud.SaveAndCheckParam();
                                    newsud = true;
                                }
                            }
                            if (sud_sud != null)
                            {
                                if (!newsud)
                                {
                                    sName = (sName == string.Empty) ? sud_sud.Name : sName;
                                    sNumber = (sNumber == string.Empty) ? sud_sud.Number : sNumber;
                                    sDate = (sDate == null) ? sud_sud.Date : sDate;
                                    sDateAct = (sDateAct == null) ? sud_sud.SudDate : sDateAct;
                                    lStatus = (lStatus == -1) ? sud_sud.Status : lStatus;

                                    if ((sud_sud.Date != sDate) && (sDate != null))
                                    {
                                        errorSud.colIndex.Add(22);
                                    }
                                    if ((sud_sud.SudDate != sDateAct) && (sDateAct != null))
                                    {
                                        errorSud.colIndex.Add(23);
                                    }
                                    if (sud_sud.Name.ToUpper() != sName.ToUpper() && (sName != string.Empty))
                                    {
                                        errorSud.colIndex.Add(19);
                                    }
                                    if ((sud_sud.Status != lStatus) && (lStatus != -1))
                                    {
                                        errorSud.colIndex.Add(20);
                                    }

                                    sud_sud.Date = sDate;
                                    sud_sud.Name = sName;
                                    sud_sud.Number = sNumber;
                                    sud_sud.Status = lStatus;
                                    sud_sud.SudDate = sDateAct;
                                    sud_sud.SaveAndCheckParam();
                                }

                                {
                                    decimal? sRc = mainWorkSheet.Rows[row.Index].Cells[24].Value.ParseToDecimalNullable();
                                    string sUse = mainWorkSheet.Rows[row.Index].Cells[25].Value.ParseToString();
                                    string sDesc = mainWorkSheet.Rows[row.Index].Cells[26].Value.ParseToString();

                                    ObjectModel.Sud.OMSudLink sud_sud_link = ObjectModel.Sud.OMSudLink.Where(x => x.IdObject == sud_object.Id && x.IdSud == sud_sud.Id).SelectAll().ExecuteFirstOrDefault();
                                    if (sud_sud_link == null)
                                    {
                                        sud_sud_link = new ObjectModel.Sud.OMSudLink
                                        {
                                            Id = -1,
                                            Descr = sDesc,
                                            IdObject = sud_object.Id,
                                            IdSud = sud_sud.Id,
                                            Rs = sRc,
                                            Use = sUse,
                                        };
                                        sud_sud_link.SaveAndCheckParam();
                                    }
                                    else
                                    {
                                        sUse = (sUse == string.Empty) ? sud_sud_link.Use : sUse;
                                        sDesc = (sDesc == string.Empty) ? sud_sud_link.Descr : sDesc;
                                        sRc = (sRc == null) ? sud_sud_link.Rs : sRc;


                                        if ((sud_sud_link.Use.ToUpper() != sUse.ToUpper()) && (sUse != string.Empty))
                                        {
                                            errorSud.colIndex.Add(25);
                                        }
                                        if ((sud_sud_link.Rs != sRc) && (sRc != null))
                                        {
                                            errorSud.colIndex.Add(24);
                                        }

                                        sud_sud_link.Use = sUse;
                                        sud_sud_link.Rs = sRc;
                                        sud_sud_link.Descr = sDesc;
                                        sud_sud_link.SaveAndCheckParam();
                                    }
                                }
                            }
                            else
                            {
                                if (mainWorkSheet.Rows[row.Index].Cells[19].Value.ParseToString() == string.Empty && (mainWorkSheet.Rows[row.Index].Cells[20].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[21].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[22].Value.ParseToString() != null || mainWorkSheet.Rows[row.Index].Cells[23].Value.ParseToString() != null || mainWorkSheet.Rows[row.Index].Cells[24].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[25].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[26].Value.ParseToString() != string.Empty))
                                {
                                    errorSud.colIndex.Add(21);
                                }
                            }
                            #endregion

                            #region Заключения
                            string zNumber = mainWorkSheet.Rows[row.Index].Cells[27].Value.ParseToString();
                            DateTime? zDate = mainWorkSheet.Rows[row.Index].Cells[28].Value.ParseToDateTimeNullable();
                            DateTime? zRecDate = mainWorkSheet.Rows[row.Index].Cells[37].Value.ParseToDateTimeNullable();
                            string zOrgName = mainWorkSheet.Rows[row.Index].Cells[30].Value.ParseToString();
                            string zFioName = mainWorkSheet.Rows[row.Index].Cells[31].Value.ParseToString();
                            string zSroName = mainWorkSheet.Rows[row.Index].Cells[32].Value.ParseToString();
                            string zRecUser = mainWorkSheet.Rows[row.Index].Cells[38].Value.ParseToString();
                            string zRecLetter = mainWorkSheet.Rows[row.Index].Cells[39].Value.ParseToString();
                            string zBefore = mainWorkSheet.Rows[row.Index].Cells[35].Value.ParseToString();
                            string zAfter = mainWorkSheet.Rows[row.Index].Cells[36].Value.ParseToString();
                            string zSoglas = mainWorkSheet.Rows[row.Index].Cells[41].Value.ParseToString();

                            ObjectModel.Sud.OMZak sud_zak = ObjectModel.Sud.OMZak.Where(x => x.Number == zNumber).SelectAll().ExecuteFirstOrDefault();
                            ObjectModel.Sud.OMDict zOrg = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == zOrgName.ToUpper() && x.Type == 1).SelectAll().ExecuteFirstOrDefault();
                            if (zOrg == null && zOrgName != string.Empty)
                            {
                                zOrg = new ObjectModel.Sud.OMDict
                                {
                                    Id = -1,
                                    Name = zOrgName,
                                    Type = 1,
                                    IdParent = -1,
                                };
                                zOrg.Save();
                            }
                            ObjectModel.Sud.OMDict zFio = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == zFioName.ToUpper() && x.Type == 3).SelectAll().ExecuteFirstOrDefault();
                            if (zFio == null && zFioName != string.Empty)
                            {
                                zFio = new ObjectModel.Sud.OMDict
                                {
                                    Id = -1,
                                    Name = zFioName,
                                    Type = 3,
                                    IdParent = -1,
                                };
                                zFio.Save();
                            }
                            ObjectModel.Sud.OMDict zSro = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == zSroName.ToUpper() && x.Type == 2).SelectAll().ExecuteFirstOrDefault();
                            if (zSro == null && zSroName != string.Empty)
                            {
                                zSro = new ObjectModel.Sud.OMDict
                                {
                                    Id = -1,
                                    Name = zSroName,
                                    Type = 2,
                                    IdParent = -1,
                                };
                                zSro.Save();
                            }
                            bool newzak = false;

                            if (sud_zak == null)
                            {
                                if (zNumber != string.Empty)
                                {
                                    sud_zak = new ObjectModel.Sud.OMZak
                                    {
                                        Id = -1,
                                        Number = zNumber,
                                        Date = zDate,
                                        Fio = (oFio == null) ? string.Empty : oFio.Name,
                                        IdFio = (oFio == null) ? (long?)null : oFio.Id,
                                        Org = (oOrg == null) ? string.Empty : oOrg.Name,
                                        IdOrg = (oOrg == null) ? (long?)null : oOrg.Id,
                                        Sro = (oSro == null) ? string.Empty : oSro.Name,
                                        IdSro = (oSro == null) ? (long?)null : oSro.Id,
                                        RecAfter = (zAfter.ToUpper() == "ДА") ? 1 : 0,
                                        RecBefore = (zBefore.ToUpper() == "ДА") ? 1 : 0,
                                        RecSoglas = (zSoglas.ToUpper() == "ДА") ? 1 : 0,
                                        RecDate = zRecDate,
                                        RecUser = zRecUser,
                                        RecLetter = zRecLetter,
                                    };
                                    sud_zak.SaveAndCheckParam();
                                    newzak = true;
                                }
                            }
                            if (sud_zak != null)
                            {
                                if (!newzak)
                                {
                                    zDate = (zDate == null) ? sud_zak.Date : zDate;
                                    zRecDate = (zRecDate == null) ? sud_zak.RecDate : zRecDate;
                                    zRecUser = (zRecUser == string.Empty) ? sud_zak.RecUser : zRecUser;
                                    zRecLetter = (zRecLetter == string.Empty) ? sud_zak.RecLetter : zRecLetter;

                                    long? lBefore = (zBefore == string.Empty) ? sud_zak.RecBefore : ((zBefore.ToUpper() == "ДА") ? 1 : 0);
                                    long? lAfter = (zAfter == string.Empty) ? sud_zak.RecAfter : ((zAfter.ToUpper() == "ДА") ? 1 : 0);
                                    long? lSoglas = (zSoglas == string.Empty) ? sud_zak.RecSoglas : ((zSoglas.ToUpper() == "ДА") ? 1 : 0);

                                    long? zSroId = (zSro == null) ? sud_zak.IdSro : zSro.Id;
                                    zSroName = (zSro == null) ? sud_zak.Sro : zSro.Name;

                                    long? zFioId = (zFio == null) ? sud_zak.IdFio : zFio.Id;
                                    zFioName = (zFio == null) ? sud_zak.Fio : zFio.Name;

                                    long? zOrgId = (zOrg == null) ? sud_zak.IdOrg : zOrg.Id;
                                    zOrgName = (zOrg == null) ? sud_zak.Org : zOrg.Name;




                                    if ((sud_zak.Date != zDate) && (zDate != null))
                                    {
                                        errorZak.colIndex.Add(28);
                                    }
                                    if ((sud_zak.RecDate != zRecDate) && (zRecDate != null))
                                    {
                                        errorZak.colIndex.Add(37);
                                    }
                                    if ((sud_zak.RecBefore != ((zBefore.ToUpper() == "ДА") ? 1 : 0)) && (zBefore != string.Empty))
                                    {
                                        errorZak.colIndex.Add(35);
                                    }
                                    if ((sud_zak.RecAfter != ((zAfter.ToUpper() == "ДА") ? 1 : 0)) && (zAfter != string.Empty))
                                    {
                                        errorZak.colIndex.Add(36);
                                    }
                                    if ((sud_zak.RecSoglas != ((zSoglas.ToUpper() == "ДА") ? 1 : 0)) && (zSoglas != string.Empty))
                                    {
                                        errorZak.colIndex.Add(41);
                                    }
                                    if ((sud_zak.RecUser.ToUpper() != zRecLetter.ToUpper()) && (zRecLetter != string.Empty))
                                    {
                                        errorZak.colIndex.Add(38);
                                    }
                                    if ((sud_zak.Org.ToUpper() != zOrgName.ToUpper()) && (zOrgName != string.Empty))
                                    {
                                        errorZak.colIndex.Add(30);
                                    }
                                    if ((sud_zak.Sro.ToUpper() != zSroName.ToUpper()) && (zSroName != string.Empty))
                                    {
                                        errorZak.colIndex.Add(32);
                                    }
                                    if ((sud_zak.Fio.ToUpper() != zFioName.ToUpper()) && (zFioName != string.Empty))
                                    {
                                        errorZak.colIndex.Add(31);
                                    }

                                    sud_zak.Date = zDate;
                                    sud_zak.RecDate = zRecDate;
                                    sud_zak.RecUser = zRecUser;
                                    sud_zak.RecLetter = zRecLetter;
                                    sud_zak.RecBefore = lBefore;
                                    sud_zak.RecAfter = lAfter;
                                    sud_zak.RecSoglas = lSoglas;
                                    sud_zak.IdSro = zSroId;
                                    sud_zak.Sro = zSroName;
                                    sud_zak.IdFio = zFioId;
                                    sud_zak.Fio = zFioName;
                                    sud_zak.IdOrg = zOrgId;
                                    sud_zak.Org = zOrgName;
                                    sud_zak.SaveAndCheckParam();
                                }
                                {
                                    decimal? zRc = mainWorkSheet.Rows[row.Index].Cells[33].Value.ParseToDecimalNullable();
                                    decimal? zUc = mainWorkSheet.Rows[row.Index].Cells[34].Value.ParseToDecimalNullable();
                                    string zUse = mainWorkSheet.Rows[row.Index].Cells[29].Value.ParseToString();
                                    string zDesc = mainWorkSheet.Rows[row.Index].Cells[40].Value.ParseToString();

                                    if (zRc > 0 || zUc > 0)
                                    {
                                        ObjectModel.Sud.OMZakLink sud_zak_link = ObjectModel.Sud.OMZakLink.Where(x => x.IdObject == sud_object.Id && x.IdZak == sud_zak.Id).SelectAll().ExecuteFirstOrDefault();
                                        if (sud_zak_link == null)
                                        {
                                            sud_zak_link = new ObjectModel.Sud.OMZakLink
                                            {
                                                Id = -1,
                                                Descr = zDesc,
                                                IdObject = sud_object.Id,
                                                IdZak = sud_zak.Id,
                                                Rs = zRc,
                                                Uprs = zUc,
                                                Use = zUse,
                                            };
                                            sud_zak_link.SaveAndCheckParam();
                                        }
                                        else
                                        {
                                            zUse = (zUse == string.Empty) ? sud_zak_link.Use : zUse;
                                            zDesc = (zDesc == string.Empty) ? sud_zak_link.Descr : zDesc;
                                            zRc = (zRc == null) ? sud_zak_link.Rs : zRc;
                                            zUc = (zUc == null) ? sud_zak_link.Uprs : zUc;


                                            if ((sud_zak_link.Use.ToUpper() != zUse.ToUpper()) && (zUse != string.Empty))
                                            {
                                                errorZak.colIndex.Add(29);
                                            }
                                            if ((sud_zak_link.Rs != zRc) && (zRc != null))
                                            {
                                                errorZak.colIndex.Add(33);
                                            }
                                            if ((sud_zak_link.Uprs != zUc) && (zUc != null))
                                            {
                                                errorZak.colIndex.Add(34);
                                            }

                                            sud_zak_link.Use = zUse;
                                            sud_zak_link.Descr = zDesc;
                                            sud_zak_link.Rs = zRc;
                                            sud_zak_link.Uprs = zUc;
                                            sud_zak_link.SaveAndCheckParam();
                                        }
                                    }
                                    else
                                    {
                                        errorZak.colIndex.Add(33);
                                        errorZak.colIndex.Add(34);
                                    }
                                }
                            }
                            else
                            {
                                if (mainWorkSheet.Rows[row.Index].Cells[27].Value.ParseToString() == string.Empty && (mainWorkSheet.Rows[row.Index].Cells[28].Value.ParseToString() != string.Empty ||
                                mainWorkSheet.Rows[row.Index].Cells[29].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[30].Value.ParseToString() != string.Empty ||
                                mainWorkSheet.Rows[row.Index].Cells[31].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[32].Value.ParseToString() != string.Empty ||
                                mainWorkSheet.Rows[row.Index].Cells[33].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[34].Value.ParseToString() != string.Empty ||
                                mainWorkSheet.Rows[row.Index].Cells[35].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[36].Value.ParseToString() != string.Empty ||
                                mainWorkSheet.Rows[row.Index].Cells[37].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[38].Value.ParseToString() != string.Empty ||
                                mainWorkSheet.Rows[row.Index].Cells[39].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[40].Value.ParseToString() != string.Empty ||
                                mainWorkSheet.Rows[row.Index].Cells[41].Value.ParseToString() != string.Empty))
                                {
                                    errorZak.colIndex.Add(27);
                                }
                            }
                            #endregion

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
                        else
                        {
                            try
                            {
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Ошибка");
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
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
                    counterr++;
                    errorMain.allerror = true;
                }

                if (errorMain.allerror || errorMain.colIndex.Count > 0)
                    errorrowMain.Add(errorMain);
                if (errorOtchet.colIndex.Count > 0)
                    errorrowOtcher.Add(errorOtchet);
                if (errorSud.colIndex.Count > 0)
                    errorrowSud.Add(errorSud);
                if (errorZak.colIndex.Count > 0)
                    errorrowZak.Add(errorZak);


                countall++;
                if (countall % 20 == 0)
                    Console.WriteLine(countall.ToString());

            });

            foreach (ErrorRow ind in errorrowMain)
            {
                if (ind.rowIndex > 0)
                {
                    for (int i = 0; i < 42; i++)
                        mainWorkSheet.Rows[ind.rowIndex].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
                }
                foreach (int indX in ind.colIndex)
                {
                    mainWorkSheet.Rows[ind.rowIndex].Cells[indX].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
                }
            }
            foreach (ErrorRow ind in errorrowOtcher)
            {
                if (ind.rowIndex > 0)
                {
                    for (int i = 9; i < 19; i++)
                        mainWorkSheet.Rows[ind.rowIndex].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 200, 255));
                }
                foreach (int indX in ind.colIndex)
                {
                    mainWorkSheet.Rows[ind.rowIndex].Cells[indX].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
                }
            }
            foreach (ErrorRow ind in errorrowSud)
            {
                if (ind.rowIndex > 0)
                {
                    for (int i = 19; i < 27; i++)
                        mainWorkSheet.Rows[ind.rowIndex].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 255));
                }
                foreach (int indX in ind.colIndex)
                {
                    mainWorkSheet.Rows[ind.rowIndex].Cells[indX].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
                }
            }
            foreach (ErrorRow ind in errorrowZak)
            {
                if (ind.rowIndex > 0)
                {
                    for (int i = 27; i < 42; i++)
                        mainWorkSheet.Rows[ind.rowIndex].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 255, 200));
                }
                foreach (int indX in ind.colIndex)
                {
                    mainWorkSheet.Rows[ind.rowIndex].Cells[indX].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
                }
            }




            MemoryStream stream = new MemoryStream();
            excelFile.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            excelFile.Save(FilePath, SaveOptions.XlsxDefault);
            return stream;
        }


    }
}
