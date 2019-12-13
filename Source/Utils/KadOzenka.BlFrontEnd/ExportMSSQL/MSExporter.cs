using Core.Register;
using Core.Shared.Extensions;
using ObjectModel.Core.Shared;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;



namespace KadOzenka.BlFrontEnd.ExportMSSQL
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

    public static class MSExporter
    {
        private static long OffsetGroupOKS_2018 = 10000;
        private static long OffsetSubGroupOKS_2018 = 100000;
        private static long OffsetGroupParcel_2018 = 20000;
        private static long OffsetSubGroupParcel_2018 = 200000;

        private static long OffsetGroupOKS_2016 = 30000;
        private static long OffsetSubGroupOKS_2016 = 300000;
        private static long OffsetGroupParcel_2016 = 40000;
        private static long OffsetSubGroupParcel_2016 = 400000;

        private static long OffsetFactorParcel_2018 = 25100000;
        private static long OffsetFactorOKS_2018 = 25000000;
        private static long OffsetFactorParcel_2016 = 25300000;
        private static long OffsetFactorOKS_2016 = 25200000;

        public static void DoLoadBd2016Model()
        {
            long curYear = 2016;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour==null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            LoadGroupOKS_2016(tour.Id);
            LoadGroupParcel_2016(tour.Id);
            //LoadGroupOKSFactor_2016();
            //LoadGroupParcelFactor_2016();
            LoadGroupParcelFactorMetka_2016();
            LoadGroupOKSFactorMetka_2016();
            LoadGroupParcelModel_2016();
            LoadGroupOKSModel_2016();
        }
        public static void DoLoadBd2018Model()
        {
            long curYear = 2018;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            LoadGroupOKS_2018(tour.Id);
            LoadGroupParcel_2018(tour.Id);
            LoadGroupOKSFactor_2018();
            LoadGroupParcelFactor_2018();
            LoadGroupParcelFactorMetka_2018();
            LoadGroupOKSFactorMetka_2018();
            LoadGroupParcelModel_2018();
            LoadGroupOKSModel_2018();
        }

        public static void DoLoadBd2016Unit_Uncomplited()
        {
            long curYear = 2016;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 2;


            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task==null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2016, 1, 1),
                        ApproveDate = new DateTime(2016, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }
                task = new ObjectModel.KO.OMTask
                {
                    Id=curTask,
                    NoteType_Code=KoNoteType.None,
                    Status_Code=KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate=new DateTime(2016,1,1),
                    DocumentId= inputDoc.Id,
                    ResponseDocId=-1
                };
                task.Save();
            }

            LoadUnitTaskOKS_2016(task.Id, tour.Id, PropertyTypes.UncompletedBuilding);
        }
        public static void DoLoadBd2016Unit_Building()
        {
            long curYear = 2016;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 2;

            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2016, 1, 1),
                        ApproveDate = new DateTime(2016, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }
                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }

            LoadUnitTaskOKS_2016(task.Id, tour.Id, PropertyTypes.Building);
        }
        public static void DoLoadBd2016Unit_Construction()
        {
            long curYear = 2016;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 2;

            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2016, 1, 1),
                        ApproveDate = new DateTime(2016, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }
                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }

            LoadUnitTaskOKS_2016(task.Id, tour.Id, PropertyTypes.Construction);
        }
        public static void DoLoadBd2016Unit_Parcel()
        {
            long curYear = 2016;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 2;

            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2016, 1, 1),
                        ApproveDate = new DateTime(2016, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }
                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }

            LoadUnitTaskParcel_2016(task.Id, tour.Id);
        }
        public static void DoLoadBd2016Unit_Flat()
        {
            long curYear = 2016;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 2;

            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2016, 1, 1),
                        ApproveDate = new DateTime(2016, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }
                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }

            LoadUnitTaskOKS_2016(task.Id, tour.Id, PropertyTypes.Pllacement);
        }

        public static void DoLoadBd2018Unit_Uncomplited()
        {
            long curYear = 2018;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 1;


            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2018, 1, 1),
                        ApproveDate = new DateTime(2018, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }

                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }



            LoadUnitTaskOKS_2018(task.Id, tour.Id, PropertyTypes.UncompletedBuilding);
        }
        public static void DoLoadBd2018Unit_Building()
        {
            long curYear = 2018;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 1;


            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2018, 1, 1),
                        ApproveDate = new DateTime(2018, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }

                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }



            LoadUnitTaskOKS_2018(task.Id, tour.Id, PropertyTypes.Building);
        }
        public static void DoLoadBd2018Unit_Construction()
        {
            long curYear = 2018;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 1;


            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2018, 1, 1),
                        ApproveDate = new DateTime(2018, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }

                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }



            LoadUnitTaskOKS_2018(task.Id, tour.Id, PropertyTypes.Construction);
        }
        public static void DoLoadBd2018Unit_Parcel()
        {
            long curYear = 2018;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 1;


            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2018, 1, 1),
                        ApproveDate = new DateTime(2018, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }

                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }

            LoadUnitTaskParcel_2018(task.Id, tour.Id);
        }
        public static void DoLoadBd2018Unit_Flat()
        {
            long curYear = 2018;
            ObjectModel.KO.OMTour tour = ObjectModel.KO.OMTour.Where(x => x.Year == curYear).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                tour = new ObjectModel.KO.OMTour
                {
                    Year = curYear,
                    Id = curYear,
                };
                tour.Save();
            }

            long curTask = 1;


            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == curTask).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                OMInstance inputDoc = null;
                if (inputDoc == null)
                {
                    inputDoc = new OMInstance
                    {
                        Id = -1,
                        RegNumber = "б/н",
                        CreateDate = new DateTime(2018, 1, 1),
                        ApproveDate = new DateTime(2018, 1, 1),
                        Description = "Перечень объектов оценки",
                    };
                    inputDoc.Save();
                }

                task = new ObjectModel.KO.OMTask
                {
                    Id = curTask,
                    NoteType_Code = KoNoteType.None,
                    Status_Code = KoTaskStatus.None,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id,
                    ResponseDocId = -1
                };
                task.Save();
            }



            LoadUnitTaskOKS_2018(task.Id, tour.Id, PropertyTypes.Pllacement);
        }





        public static void DoLoadXml2016()
        {
            ImportXml2016(2016, 2);
        }
        public static void DoLoadXml2018()
        {
            ImportXml2018(2018, 1);
        }

        #region БД 2016 Model
        public static void LoadGroupOKS_2016(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2016"]))
            {
                connection.Open();

                Dictionary<long, long> newIds = new Dictionary<long, long>();


                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprGroup where id_group>0";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.KO.OMGroup> Items = new List<ObjectModel.KO.OMGroup>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroup koGroup = new ObjectModel.KO.OMGroup
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupOKS_2016,
                        GroupAlgoritm_Code = ObjectModel.Directory.KoGroupAlgoritm.MainOKS,
                        GroupName = NullConvertor.ToString(myOleDbDataReader["num_group"]) + ". " + NullConvertor.ToString(myOleDbDataReader["desc_group"]),
                        ParentId = -1
                    };
                    koGroup.Save();
                    ObjectModel.KO.OMTourGroup tg = new ObjectModel.KO.OMTourGroup
                    {
                        GroupId = koGroup.Id,
                        TourId = id_tour
                    };
                    tg.Save();


                    newIds.Add(NullConvertor.DBToInt64(myOleDbDataReader["id_group"]), koGroup.Id);
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();

                myOleDbCommand.CommandText = "select * from sprSubGroup";
                myOleDbDataReader = myOleDbCommand.ExecuteReader();
                count = 0;
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroup koGroup = new ObjectModel.KO.OMGroup
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2016,
                        GroupAlgoritm_Code = (ObjectModel.Directory.KoGroupAlgoritm)NullConvertor.DBToInt64(myOleDbDataReader["type_group"]),
                        GroupName = NullConvertor.ToString(myOleDbDataReader["num_group"]) + ". " + NullConvertor.ToString(myOleDbDataReader["name_group"]),
                        ParentId = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupOKS_2016
                    };
                    koGroup.Save();
                    ObjectModel.KO.OMTourGroup tg = new ObjectModel.KO.OMTourGroup
                    {
                        GroupId = koGroup.Id,
                        TourId = id_tour
                    };
                    tg.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();



                connection.Close();
            }
        }
        public static void LoadGroupParcel_2016(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprGroup where id_group>0";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.KO.OMGroup> Items = new List<ObjectModel.KO.OMGroup>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroup koGroup = new ObjectModel.KO.OMGroup
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupParcel_2016,
                        GroupAlgoritm_Code = ObjectModel.Directory.KoGroupAlgoritm.MainParcel,
                        GroupName = NullConvertor.ToString(myOleDbDataReader["num_group"]) + ". " + NullConvertor.ToString(myOleDbDataReader["desc_group"]),
                        ParentId = -1
                    };
                    koGroup.Save();
                    ObjectModel.KO.OMTourGroup tg = new ObjectModel.KO.OMTourGroup
                    {
                        GroupId = koGroup.Id,
                        TourId = id_tour
                    };
                    tg.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();

                myOleDbCommand.CommandText = "select * from sprSubGroup";
                myOleDbDataReader = myOleDbCommand.ExecuteReader();
                count = 0;
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroup koGroup = new ObjectModel.KO.OMGroup
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2016,
                        GroupName = NullConvertor.ToString(myOleDbDataReader["num_group"]) + ". " + NullConvertor.ToString(myOleDbDataReader["name_group"]),
                        ParentId = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupParcel_2016
                    };
                    long tpgr = NullConvertor.DBToInt64(myOleDbDataReader["type_group"]);
                    if (tpgr == 0) tpgr = 13;
                    koGroup.GroupAlgoritm_Code = (ObjectModel.Directory.KoGroupAlgoritm)tpgr;

                    koGroup.Save();
                    ObjectModel.KO.OMTourGroup tg = new ObjectModel.KO.OMTourGroup
                    {
                        GroupId = koGroup.Id,
                        TourId = id_tour
                    };
                    tg.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();



                connection.Close();
            }
        }
        public static void LoadGroupOKSFactor_2016()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from lnkFactorGroup";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.KO.OMGroup> Items = new List<ObjectModel.KO.OMGroup>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroupFactor koGroup = new ObjectModel.KO.OMGroupFactor
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupOKS_2016,
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorOKS_2016
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();

                connection.Close();
            }
        }
        public static void LoadGroupParcelFactor_2016()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from lnkFactorGroup";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.KO.OMGroup> Items = new List<ObjectModel.KO.OMGroup>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroupFactor koGroup = new ObjectModel.KO.OMGroupFactor
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupParcel_2016,
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorParcel_2016
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();

                connection.Close();
            }
        }
        public static void LoadGroupParcelFactorMetka_2016()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprFACTORMETKA";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMMarkCatalog> Items = new List<ObjectModel.KO.OMMarkCatalog>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMMarkCatalog koGroup = new ObjectModel.KO.OMMarkCatalog
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2016,
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorParcel_2016,
                        ValueFactor = NullConvertor.ToString(myOleDbDataReader["VALUE_FACTOR"]),
                        MetkaFactor = NullConvertor.DBToDecimal(myOleDbDataReader["METKA_FACTOR"])

                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
                Console.WriteLine(count);

                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupOKSFactorMetka_2016()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprFACTORMETKA";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMMarkCatalog> Items = new List<ObjectModel.KO.OMMarkCatalog>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMMarkCatalog koGroup = new ObjectModel.KO.OMMarkCatalog
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2016,
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorOKS_2016,
                        ValueFactor = NullConvertor.ToString(myOleDbDataReader["VALUE_FACTOR"]),
                        MetkaFactor = NullConvertor.DBToDecimal(myOleDbDataReader["METKA_FACTOR"])

                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
                Console.WriteLine(count);

                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupParcelModel_2016()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSubFormula where ID_SUBGROUP in (select ID_SUBGROUP from sprSUBFORMULAFACTOR)";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMModel> Items = new List<ObjectModel.KO.OMModel>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMModel koGroup = new ObjectModel.KO.OMModel
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2016,
                        A0 = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        Formula = NullConvertor.ToString(myOleDbDataReader["sub_formula"]),
                        AlgoritmType_Code = (KoAlgoritmType)NullConvertor.DBToInt64(myOleDbDataReader["type_formula"]),
                        Description = "-",
                        Name = "-"
                    };
                    koGroup.Save();
                    LoadGroupParcelModelFactor_2016(NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]), koGroup.Id);
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupOKSModel_2016()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSubFormula where ID_SUBGROUP in (select ID_SUBGROUP from sprSUBFORMULAFACTOR)";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMModel> Items = new List<ObjectModel.KO.OMModel>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMModel koGroup = new ObjectModel.KO.OMModel
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2016,
                        A0 = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        Formula = NullConvertor.ToString(myOleDbDataReader["sub_formula"]),
                        AlgoritmType_Code = (KoAlgoritmType)NullConvertor.DBToInt64(myOleDbDataReader["type_formula"]),
                        Description = "-",
                        Name = "-"
                    };
                    koGroup.Save();
                    LoadGroupOKSModelFactor_2016(NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]), koGroup.Id);
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupParcelModelFactor_2016(long ms_id_subgroup, long id_model)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSubFormulaFactor where id_subgroup=" + ms_id_subgroup.ToString();
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMModelFactor> Items = new List<ObjectModel.KO.OMModelFactor>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMModelFactor koGroup = new ObjectModel.KO.OMModelFactor
                    {
                        Id = -1,
                        B0 = NullConvertor.DBToDecimal(myOleDbDataReader["B0"]),
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorParcel_2016,
                        ModelId = id_model,
                        Weight = NullConvertor.DBToDecimal(myOleDbDataReader["WEIGHT_FACTOR"]),
                        SignAdd = NullConvertor.DBToInt64(myOleDbDataReader["PRADD"]) == 1,
                        SignDiv = NullConvertor.DBToInt64(myOleDbDataReader["PRDIV"]) == 1,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["PR_METKA"]) == 1,
                        MarkerId = -1,
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupOKSModelFactor_2016(long ms_id_subgroup, long id_model)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSubFormulaFactor where id_subgroup=" + ms_id_subgroup.ToString();
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMModelFactor> Items = new List<ObjectModel.KO.OMModelFactor>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMModelFactor koGroup = new ObjectModel.KO.OMModelFactor
                    {
                        Id = -1,
                        B0 = NullConvertor.DBToDecimal(myOleDbDataReader["B0"]),
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorOKS_2016,
                        ModelId = id_model,
                        Weight = NullConvertor.DBToDecimal(myOleDbDataReader["WEIGHT_FACTOR"]),
                        SignAdd = NullConvertor.DBToInt64(myOleDbDataReader["PRADD"]) == 1,
                        SignDiv = NullConvertor.DBToInt64(myOleDbDataReader["PRDIV"]) == 1,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["PR_METKA"]) == 1,
                        MarkerId = -1,
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        #endregion

        #region БД 2018 Model
        public static void LoadGroupOKS_2018(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                Dictionary<long, long> newIds = new Dictionary<long, long>();


                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprGroup where id_group>0";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.KO.OMGroup> Items = new List<ObjectModel.KO.OMGroup>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroup koGroup = new ObjectModel.KO.OMGroup
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupOKS_2018,
                        GroupAlgoritm_Code = ObjectModel.Directory.KoGroupAlgoritm.MainOKS,
                        GroupName = NullConvertor.ToString(myOleDbDataReader["num_group"]) + ". " + NullConvertor.ToString(myOleDbDataReader["desc_group"]),
                        ParentId = -1
                    };
                    koGroup.Save();
                    ObjectModel.KO.OMTourGroup tg = new ObjectModel.KO.OMTourGroup
                    {
                        GroupId = koGroup.Id,
                        TourId = id_tour
                    };
                    tg.Save();


                    newIds.Add(NullConvertor.DBToInt64(myOleDbDataReader["id_group"]), koGroup.Id);
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();

                myOleDbCommand.CommandText = "select * from sprSubGroup";
                myOleDbDataReader = myOleDbCommand.ExecuteReader();
                count = 0;
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroup koGroup = new ObjectModel.KO.OMGroup
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                        GroupAlgoritm_Code = (ObjectModel.Directory.KoGroupAlgoritm)NullConvertor.DBToInt64(myOleDbDataReader["type_group"]),
                        GroupName = NullConvertor.ToString(myOleDbDataReader["num_group"]) + ". " + NullConvertor.ToString(myOleDbDataReader["name_group"]),
                        ParentId = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupOKS_2018
                    };
                    koGroup.Save();
                    ObjectModel.KO.OMTourGroup tg = new ObjectModel.KO.OMTourGroup
                    {
                        GroupId = koGroup.Id,
                        TourId = id_tour
                    };
                    tg.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();



                connection.Close();
            }
        }
        public static void LoadGroupParcel_2018(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprGroup where id_group>0";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.KO.OMGroup> Items = new List<ObjectModel.KO.OMGroup>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroup koGroup = new ObjectModel.KO.OMGroup
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupParcel_2018,
                        GroupAlgoritm_Code = ObjectModel.Directory.KoGroupAlgoritm.MainParcel,
                        GroupName = NullConvertor.ToString(myOleDbDataReader["num_group"]) + ". " + NullConvertor.ToString(myOleDbDataReader["desc_group"]),
                        ParentId = -1
                    };
                    koGroup.Save();
                    ObjectModel.KO.OMTourGroup tg = new ObjectModel.KO.OMTourGroup
                    {
                        GroupId = koGroup.Id,
                        TourId = id_tour
                    };
                    tg.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();

                myOleDbCommand.CommandText = "select * from sprSubGroup";
                myOleDbDataReader = myOleDbCommand.ExecuteReader();
                count = 0;
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroup koGroup = new ObjectModel.KO.OMGroup
                    {
                        Id = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2018,
                        GroupName = NullConvertor.ToString(myOleDbDataReader["num_group"]) + ". " + NullConvertor.ToString(myOleDbDataReader["name_group"]),
                        ParentId = NullConvertor.DBToInt64(myOleDbDataReader["id_group"]) + OffsetGroupParcel_2018
                    };
                    long tpgr = NullConvertor.DBToInt64(myOleDbDataReader["type_group"]);
                    if (tpgr == 0) tpgr = 13;
                    koGroup.GroupAlgoritm_Code = (ObjectModel.Directory.KoGroupAlgoritm)tpgr;

                    koGroup.Save();
                    ObjectModel.KO.OMTourGroup tg = new ObjectModel.KO.OMTourGroup
                    {
                        GroupId = koGroup.Id,
                        TourId = id_tour
                    };
                    tg.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();



                connection.Close();
            }
        }
        public static void LoadGroupParcelFactor_2018()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSUBGROUPKOEFF";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.KO.OMGroup> Items = new List<ObjectModel.KO.OMGroup>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroupFactor koGroup = new ObjectModel.KO.OMGroupFactor
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2018,
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorParcel_2018
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();

                connection.Close();
            }
        }
        public static void LoadGroupOKSFactor_2018()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSUBGROUPKOEFF";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int count = 0;
                List<ObjectModel.KO.OMGroup> Items = new List<ObjectModel.KO.OMGroup>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMGroupFactor koGroup = new ObjectModel.KO.OMGroupFactor
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorOKS_2018
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();

                connection.Close();
            }
        }
        public static void LoadGroupParcelFactorMetka_2018()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprFACTORMETKA";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMMarkCatalog> Items = new List<ObjectModel.KO.OMMarkCatalog>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMMarkCatalog koGroup = new ObjectModel.KO.OMMarkCatalog
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2018,
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorParcel_2018,
                        ValueFactor = NullConvertor.ToString(myOleDbDataReader["VALUE_FACTOR"]),
                        MetkaFactor = NullConvertor.DBToDecimal(myOleDbDataReader["METKA_FACTOR"])

                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
                Console.WriteLine(count);

                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupOKSFactorMetka_2018()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprFACTORMETKA";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMMarkCatalog> Items = new List<ObjectModel.KO.OMMarkCatalog>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMMarkCatalog koGroup = new ObjectModel.KO.OMMarkCatalog
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorOKS_2018,
                        ValueFactor = NullConvertor.ToString(myOleDbDataReader["VALUE_FACTOR"]),
                        MetkaFactor = NullConvertor.DBToDecimal(myOleDbDataReader["METKA_FACTOR"])

                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
                Console.WriteLine(count);

                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupParcelModel_2018()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSubFormula where ID_SUBGROUP in (select ID_SUBGROUP from sprSUBFORMULAFACTOR)";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMModel> Items = new List<ObjectModel.KO.OMModel>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMModel koGroup = new ObjectModel.KO.OMModel
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2018,
                        A0 = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        Formula = NullConvertor.ToString(myOleDbDataReader["sub_formula"]),
                        AlgoritmType_Code = (KoAlgoritmType)NullConvertor.DBToInt64(myOleDbDataReader["type_formula"]),
                        Description = "-",
                        Name = "-"
                    };
                    koGroup.Save();
                    LoadGroupParcelModelFactor_2018(NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]), koGroup.Id);
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupOKSModel_2018()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSubFormula where ID_SUBGROUP in (select ID_SUBGROUP from sprSUBFORMULAFACTOR)";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMModel> Items = new List<ObjectModel.KO.OMModel>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMModel koGroup = new ObjectModel.KO.OMModel
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                        A0 = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        Formula = NullConvertor.ToString(myOleDbDataReader["sub_formula"]),
                        AlgoritmType_Code = (KoAlgoritmType)NullConvertor.DBToInt64(myOleDbDataReader["type_formula"]),
                        Description = "-",
                        Name = "-"
                    };
                    koGroup.Save();
                    LoadGroupOKSModelFactor_2018(NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]), koGroup.Id);
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupParcelModelFactor_2018(long ms_id_subgroup, long id_model)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSubFormulaFactor where id_subgroup=" + ms_id_subgroup.ToString();
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMModelFactor> Items = new List<ObjectModel.KO.OMModelFactor>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMModelFactor koGroup = new ObjectModel.KO.OMModelFactor
                    {
                        Id = -1,
                        B0 = NullConvertor.DBToDecimal(myOleDbDataReader["B0"]),
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorParcel_2018,
                        ModelId = id_model,
                        Weight = NullConvertor.DBToDecimal(myOleDbDataReader["WEIGHT_FACTOR"]),
                        SignAdd = NullConvertor.DBToInt64(myOleDbDataReader["PRADD"]) == 1,
                        SignDiv = NullConvertor.DBToInt64(myOleDbDataReader["PRDIV"]) == 1,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["PR_METKA"]) == 1,
                        MarkerId = -1,
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupOKSModelFactor_2018(long ms_id_subgroup, long id_model)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprSubFormulaFactor where id_subgroup=" + ms_id_subgroup.ToString();
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMModelFactor> Items = new List<ObjectModel.KO.OMModelFactor>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMModelFactor koGroup = new ObjectModel.KO.OMModelFactor
                    {
                        Id = -1,
                        B0 = NullConvertor.DBToDecimal(myOleDbDataReader["B0"]),
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorOKS_2018,
                        ModelId = id_model,
                        Weight = NullConvertor.DBToDecimal(myOleDbDataReader["WEIGHT_FACTOR"]),
                        SignAdd = NullConvertor.DBToInt64(myOleDbDataReader["PRADD"]) == 1,
                        SignDiv = NullConvertor.DBToInt64(myOleDbDataReader["PRDIV"]) == 1,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["PR_METKA"]) == 1,
                        MarkerId = -1,
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        #endregion

        #region БД 2016 Factor
        public static void LoadUnitTaskOKS_2016(long id_task, long id_tour, PropertyTypes objtype)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 600;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                switch (objtype)
                {
                    case PropertyTypes.None:
                        break;
                    case PropertyTypes.Stead:
                        myOleDbCommand.CommandText = "select * from tbObjectParcel";
                        break;
                    case PropertyTypes.Building:
                        myOleDbCommand.CommandText = "select * from tbObjectBuild";
                        break;
                    case PropertyTypes.Pllacement:
                        myOleDbCommand.CommandText = "select * from tbObjectFlat";
                        break;
                    case PropertyTypes.Construction:
                        myOleDbCommand.CommandText = "select * from tbObjectConstruction";
                        break;
                    case PropertyTypes.UncompletedBuilding:
                        myOleDbCommand.CommandText = "select * from tbObjectUnderConstruction";
                        break;
                    case PropertyTypes.Company:
                        break;
                    case PropertyTypes.UnitedPropertyComplex:
                        break;
                    case PropertyTypes.Parking:
                        myOleDbCommand.CommandText = "select * from tbObjectFlat";
                        break;
                    case PropertyTypes.Other:
                        break;
                    default:
                        break;
                }

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                    {
                        Id = -1,
                        ModelId = -1,
                        CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                        CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                        Upks = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                        UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2016,
                        OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                        TourId = id_tour,
                        TaskId = id_task,
                        Status_Code = KoUnitStatus.Initial,
                        ObjectId = -1,
                        CreationDate = new DateTime(2016, 1, 1),
                        CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                        CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                        Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                        PropertyType_Code = objtype,
                        StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
                        StatusResultCalc_Code = KoStatusResultCalc.None,
                        ParentCalcType_Code=KoParentCalcType.None
                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 40)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2016(x.OldId, x.Id, x.PropertyType_Code); });
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2016(x.OldId, x.Id, x.PropertyType_Code); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitFactorOKS_2016(long ms_id_object, long id_unit, PropertyTypes objtype)
        {
            string conn = string.Empty;
            switch (objtype)
            {
                case PropertyTypes.Building:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Building_2016"];
                    break;
                case PropertyTypes.Pllacement:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Flat_2016"];
                    break;
                case PropertyTypes.Construction:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Constr_2016"];
                    break;
                case PropertyTypes.UncompletedBuilding:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Uncomp_2016"];
                    break;
                default:
                    break;
            }

            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbFactorValue where id_object=" + ms_id_object.ToString();
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                //long count = 0;

                RegisterObject registerObject = new RegisterObject(252, (int)id_unit);

                List<ObjectModel.KO.OMUnitParamsOks2016> Items = new List<ObjectModel.KO.OMUnitParamsOks2016>();
                while (myOleDbDataReader.Read())
                {
                    long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]);
                    object value = NullConvertor.ToString(myOleDbDataReader["factor_value"]);


                    var attributeData = RegisterCache.GetAttributeData((int)(id_factor * 100 + OffsetFactorOKS_2016));
                    int referenceItemId = -1;
                    if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
                    {
                        OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == value.ToString()).ExecuteFirstOrDefault();
                        if (item != null) referenceItemId = (int)item.ItemId;
                    }

                    switch (attributeData.Type)
                    {
                        case RegisterAttributeType.INTEGER:
                            value = value.ParseToLongNullable();
                            break;
                        case RegisterAttributeType.DECIMAL:
                            value = value.ParseToDecimalNullable();
                            break;
                        case RegisterAttributeType.BOOLEAN:
                            value = value.ParseToBooleanNullable();
                            break;
                        case RegisterAttributeType.STRING:
                            value = value.ToString();
                            break;
                        case RegisterAttributeType.DATE:
                            value = value.ParseToDateTimeNullable();
                            break;
                    }

                    registerObject.SetAttributeValue((int)(id_factor * 100 + OffsetFactorOKS_2016), value, referenceItemId);
                    //count++;
                    //Console.WriteLine(count);
                }
                RegisterStorage.Save(registerObject);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskParcel_2016(long id_task, long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2016"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select * from tbObjectParcel";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                    {
                        Id = -1,
                        ModelId = -1,
                        CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                        CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                        Upks = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                        UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2016,
                        OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                        TourId = id_tour,
                        TaskId = id_task,
                        Status_Code = KoUnitStatus.Initial,
                        ObjectId = -1,
                        CreationDate = new DateTime(2016, 1, 1),
                        CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                        CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                        Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                        PropertyType_Code = PropertyTypes.Stead,
                        StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
                        StatusResultCalc_Code = KoStatusResultCalc.None,
                        ParentCalcType_Code=KoParentCalcType.None
                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 40)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorParcel_2016(x.OldId, x.Id); });
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorParcel_2016(x.OldId, x.Id); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitFactorParcel_2016(long ms_id_object, long id_unit)
        {
            string conn = ConfigurationManager.AppSettings["SQL_connection_Parcel_Parcel_2016"];

            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbFactorValue where id_object=" + ms_id_object.ToString();
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                //long count = 0;

                RegisterObject registerObject = new RegisterObject(253, (int)id_unit);

                List<ObjectModel.KO.OMUnitParamsZu2016> Items = new List<ObjectModel.KO.OMUnitParamsZu2016>();
                while (myOleDbDataReader.Read())
                {
                    long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]);
                    object value = NullConvertor.ToString(myOleDbDataReader["factor_value"]);


                    var attributeData = RegisterCache.GetAttributeData((int)(id_factor * 100 + OffsetFactorParcel_2016));
                    int referenceItemId = -1;
                    if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
                    {
                        OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == value.ToString()).ExecuteFirstOrDefault();
                        if (item != null) referenceItemId = (int)item.ItemId;
                    }

                    switch (attributeData.Type)
                    {
                        case RegisterAttributeType.INTEGER:
                            value = value.ParseToLongNullable();
                            break;
                        case RegisterAttributeType.DECIMAL:
                            value = value.ParseToDecimalNullable();
                            break;
                        case RegisterAttributeType.BOOLEAN:
                            value = value.ParseToBooleanNullable();
                            break;
                        case RegisterAttributeType.STRING:
                            value = value.ToString();
                            break;
                        case RegisterAttributeType.DATE:
                            value = value.ParseToDateTimeNullable();
                            break;
                    }

                    registerObject.SetAttributeValue((int)(id_factor * 100 + OffsetFactorParcel_2016), value, referenceItemId);
                    //count++;
                    //Console.WriteLine(count);
                }
                RegisterStorage.Save(registerObject);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        #endregion

        #region БД 2018 Factor
        public static void LoadUnitTaskOKS_2018(long id_task, long id_tour, PropertyTypes objtype)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 600;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                switch (objtype)
                {
                    case PropertyTypes.None:
                        break;
                    case PropertyTypes.Stead:
                        myOleDbCommand.CommandText = "select * from tbObjectParcel where change_object=0";
                        break;
                    case PropertyTypes.Building:
                        myOleDbCommand.CommandText = "select * from tbObjectBuild where change_object=0";
                        break;
                    case PropertyTypes.Pllacement:
                        myOleDbCommand.CommandText = "select * from tbObjectFlat where change_object=0";
                        break;
                    case PropertyTypes.Construction:
                        myOleDbCommand.CommandText = "select * from tbObjectConstruction where change_object=0";
                        break;
                    case PropertyTypes.UncompletedBuilding:
                        myOleDbCommand.CommandText = "select * from tbObjectUnderConstruction where change_object=0";
                        break;
                    case PropertyTypes.Company:
                        break;
                    case PropertyTypes.UnitedPropertyComplex:
                        break;
                    case PropertyTypes.Parking:
                        myOleDbCommand.CommandText = "select * from tbObjectFlat where change_object=0";
                        break;
                    case PropertyTypes.Other:
                        break;
                    default:
                        break;
                }

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                    {
                        Id = -1,
                        ModelId = -1,
                        CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["KC_OBJECT"]),
                        CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                        Upks = NullConvertor.DBToDecimal(myOleDbDataReader["UPKSZ_OBJECT"]),
                        UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                        OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                        TourId = id_tour,
                        TaskId = id_task,
                        Status_Code = KoUnitStatus.Initial,
                        ObjectId = -1,
                        CreationDate = new DateTime(2018, 1, 1),
                        CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                        CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                        Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                        PropertyType_Code = objtype,
                        StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
                        StatusResultCalc_Code = KoStatusResultCalc.None
                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, x.PropertyType_Code); });
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, x.PropertyType_Code); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitFactorOKS_2018(long ms_id_object, long id_unit, PropertyTypes objtype)
        {
            string conn = string.Empty;
            switch (objtype)
            {
                case PropertyTypes.Building:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Building_2018"];
                    break;
                case PropertyTypes.Pllacement:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Flat_2018"];
                    break;
                case PropertyTypes.Construction:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Constr_2018"];
                    break;
                case PropertyTypes.UncompletedBuilding:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Uncomp_2018"];
                    break;
                default:
                    break;
            }
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbFactorValue where id_object=" + ms_id_object.ToString();
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                //long count = 0;

                RegisterObject registerObject = new RegisterObject(250, (int)id_unit);

                List<ObjectModel.KO.OMUnitParamsOks2018> Items = new List<ObjectModel.KO.OMUnitParamsOks2018>();
                while (myOleDbDataReader.Read())
                {
                    long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]);
                    object value = NullConvertor.ToString(myOleDbDataReader["factor_value"]);


                    var attributeData = RegisterCache.GetAttributeData((int)(id_factor * 100 + OffsetFactorOKS_2018));
                    int referenceItemId = -1;
                    if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
                    {
                        OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == value.ToString()).ExecuteFirstOrDefault();
                        if (item != null) referenceItemId = (int)item.ItemId;
                    }

                    switch (attributeData.Type)
                    {
                        case RegisterAttributeType.INTEGER:
                            value = value.ParseToLongNullable();
                            break;
                        case RegisterAttributeType.DECIMAL:
                            value = value.ParseToDecimalNullable();
                            break;
                        case RegisterAttributeType.BOOLEAN:
                            value = value.ParseToBooleanNullable();
                            break;
                        case RegisterAttributeType.STRING:
                            value = value.ToString();
                            break;
                        case RegisterAttributeType.DATE:
                            value = value.ParseToDateTimeNullable();
                            break;
                    }

                    registerObject.SetAttributeValue((int)(id_factor * 100 + OffsetFactorOKS_2018), value, referenceItemId);
                    //count++;
                    //Console.WriteLine(count);
                }
                RegisterStorage.Save(registerObject);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskParcel_2018(long id_task, long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select * from tbObjectParcel where change_object=0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                    {
                        Id = -1,
                        ModelId = -1,
                        CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["KC_OBJECT"]),
                        CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                        Upks = NullConvertor.DBToDecimal(myOleDbDataReader["UPKSZ_OBJECT"]),
                        UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2018,
                        OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                        TourId = id_tour,
                        TaskId = id_task,
                        Status_Code = KoUnitStatus.Initial,
                        ObjectId = -1,
                        CreationDate = new DateTime(2018, 1, 1),
                        CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                        CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                        Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                        PropertyType_Code = PropertyTypes.Stead,
                        StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
                        StatusResultCalc_Code = KoStatusResultCalc.None
                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 25)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorParcel_2018(x.OldId, x.Id); });
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorParcel_2018(x.OldId, x.Id); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitFactorParcel_2018(long ms_id_object, long id_unit)
        {
            string conn = ConfigurationManager.AppSettings["SQL_connection_Parcel_Parcel_2018"];

            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from tbFactorValue where id_object=" + ms_id_object.ToString();
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                //long count = 0;

                RegisterObject registerObject = new RegisterObject(251, (int)id_unit);

                List<ObjectModel.KO.OMUnitParamsZu2018> Items = new List<ObjectModel.KO.OMUnitParamsZu2018>();
                while (myOleDbDataReader.Read())
                {
                    long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]);
                    object value = NullConvertor.ToString(myOleDbDataReader["factor_value"]);


                    var attributeData = RegisterCache.GetAttributeData((int)(id_factor * 100 + OffsetFactorParcel_2018));
                    int referenceItemId = -1;
                    if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
                    {
                        OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == value.ToString()).ExecuteFirstOrDefault();
                        if (item != null) referenceItemId = (int)item.ItemId;
                    }

                    switch (attributeData.Type)
                    {
                        case RegisterAttributeType.INTEGER:
                            value = value.ParseToLongNullable();
                            break;
                        case RegisterAttributeType.DECIMAL:
                            value = value.ParseToDecimalNullable();
                            break;
                        case RegisterAttributeType.BOOLEAN:
                            value = value.ParseToBooleanNullable();
                            break;
                        case RegisterAttributeType.STRING:
                            value = value.ToString();
                            break;
                        case RegisterAttributeType.DATE:
                            value = value.ParseToDateTimeNullable();
                            break;
                    }

                    registerObject.SetAttributeValue((int)(id_factor * 100 + OffsetFactorParcel_2018), value, referenceItemId);
                    //count++;
                    //Console.WriteLine(count);
                }
                RegisterStorage.Save(registerObject);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        #endregion

        public static void GetFormula(long idModel)//8225691 модель 300003 группа
        {
            ObjectModel.KO.OMModel model = ObjectModel.KO.OMModel.Where(x => x.Id == idModel).SelectAll().ExecuteFirstOrDefault();
            if (model != null)
            {
                Console.WriteLine("=========================");
                Console.WriteLine(model.GetFormulaMain(true));
                Console.WriteLine("=========================");
                Console.WriteLine(model.GetFormulaFull(true));
            }
        }
        public static void GetFormulaText()
        {
            GetFormula(8225691);
        }
        public static void GetCalcGroup()
        {
            ObjectModel.KO.OMGroup group = ObjectModel.KO.OMGroup.Where(x=>x.Id== 300003).SelectAll().ExecuteFirstOrDefault();
            if (group!=null)
            {
                List<ObjectModel.KO.OMUnit> units = ObjectModel.KO.OMUnit.Where(x=>x.GroupId==group.Id && x.Status_Code==KoUnitStatus.Initial).SelectAll().Execute();
                group.Calculate(units);
            }
        }

        public static void ImportXml2016(long idTour, long idTask)
        {
            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == idTask).SelectAll().ExecuteFirstOrDefault();
            if (task != null)
            {
                {
                    string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["XML_Path_2016"], " *.xml", SearchOption.AllDirectories);

                    int countAll = files.Length;
                    int count = 0;
                    foreach (string file in files)
                    {
                        count++;
                        Dal.DataImport.DataImporterGkn.ImportDataGknFromXml(file, ConfigurationManager.AppSettings["Schema_Path_2016"], task.CreationDate.Value, idTour, idTask, task.CreationDate.Value, task.CreationDate.Value, 63);
                        Console.WriteLine(count.ToString() + " из " + countAll.ToString());
                    }
                }
            }
        }
        public static void ImportXml2018(long idTour, long idTask)
        {
            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == idTask).SelectAll().ExecuteFirstOrDefault();
            if (task != null)
            {
                {
                    string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["XML_Path_2018"], " *.xml", SearchOption.AllDirectories);

                    int countAll = files.Length;
                    int count = 0;
                    foreach (string file in files)
                    {
                        count++;
                        Dal.DataImport.DataImporterGkn.ImportDataGknFromXml(file, ConfigurationManager.AppSettings["Schema_Path_2018"], task.CreationDate.Value, idTour, idTask, task.CreationDate.Value, task.CreationDate.Value, 63);
                        Console.WriteLine(count.ToString() + " из " + countAll.ToString());
                    }
                }
            }
        }
    }
}
