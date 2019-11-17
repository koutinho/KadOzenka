using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
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

    public static class SudExporter
    {
        private static string SQL_connection = "server= x1carbon; user id=srodoc; password=Eavp9QcHfFZD4fl6gmznaQ==; database=SUDGBU; connection timeout=30";

        public static void DoLoad()
		{
            //LoadDict();
            //LoadObject();
            //LoadObjectStatus();
            //LoadDRS();
            //LoadSud();
            //LoadSudStatus();
            //LoadSudLink();
            //LoadSudLinkStatus();
            //LoadOtchet();
            //LoadOtchetStatus();
            //LoadOtchetLink();
            //LoadOtchetLinkStatus();
            //LoadZak();
            //LoadZakStatus();
            //LoadZakLink();
            //LoadZakLinkStatus();
            //LoadLog();
            //LoadParam();
        }

        public static void LoadObject()
        {
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
                        Typeobj = NullConvertor.DBToInt(myOleDbDataReader["typeobj"], 0),
                        Workstat = NullConvertor.DBToInt(myOleDbDataReader["workstat"], 0),
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
                        Typeobj = NullConvertor.DBToInt(myOleDbDataReader["typeobj"], 0)
                    };

                    if (NullConvertor.DBToInt(myOleDbDataReader["kn"], out int kn)) sudObject.Kn = kn;
                    if (NullConvertor.DBToInt(myOleDbDataReader["date"], out int date)) sudObject.Date = date;
                    if (NullConvertor.DBToInt(myOleDbDataReader["square"], out int square)) sudObject.Square = square;
                    if (NullConvertor.DBToInt(myOleDbDataReader["kc"], out int kc)) sudObject.Kc = kc;

                    if (NullConvertor.DBToInt(myOleDbDataReader["name_center"], out int nameCenter)) sudObject.NameCenter = nameCenter;
                    if (NullConvertor.DBToInt(myOleDbDataReader["stat_dgi"], out int statDgi)) sudObject.StatDgi = statDgi;
                    if (NullConvertor.DBToInt(myOleDbDataReader["owner"], out int owner)) sudObject.Owner = owner;
                    if (NullConvertor.DBToInt(myOleDbDataReader["adres"], out int adres)) sudObject.Adres = adres;
                    if (NullConvertor.DBToInt(myOleDbDataReader["status"], out int status)) sudObject.Status = status;



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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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
                        ParamName= NullConvertor.ToString(myOleDbDataReader["param_name"]),

                        IdUser = NullConvertor.DBToInt64(myOleDbDataReader["id_user"]),
                        DateUser = NullConvertor.DBToDateTime(myOleDbDataReader["date_user"]),
                        ParamStatus = NullConvertor.DBToInt(myOleDbDataReader["param_status"])
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
            using (SqlConnection connection = new SqlConnection(SQL_connection))
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

                        IdUser = NullConvertor.DBToInt64(myOleDbDataReader["id_user"]),
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

    }
}
