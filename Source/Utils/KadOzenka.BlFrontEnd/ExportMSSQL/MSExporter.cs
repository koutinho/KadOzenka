using Core.Register;
using Core.Shared.Extensions;
using DevExpress.CodeParser;
using ObjectModel.Core.Shared;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Gbu;
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
        public static bool DBToBoolean(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return false;
            else
                return Convert.ToString(value).ToUpper() == "ДА";
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
        public static long? DBToProcent(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return null;
            else
            {
                string proc = Convert.ToString(value);
                if (proc != string.Empty) return proc.ParseToLongNullable();
                else
                    return null;
            }
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
        public static decimal? DBToDecimalNull(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return null;
            else
                if (value.ToString() == "-")
                return null;
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
            if (tour == null)
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
        public static void DoLoadBd2018ModelAdd()
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id
                };
                task.Save();
            }

            LoadUnitTaskOKS_2016(task.Id, tour.Id, PropertyTypes.Pllacement);
        }
        public static void DoLoadBd2016Unit_FlatSkip()
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2016, 1, 1),
                    DocumentId = inputDoc.Id
                };
                task.Save();
            }

            LoadUnitTaskOKS_2016_Skip(task.Id, tour.Id, PropertyTypes.Pllacement);
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id
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
                    NoteType_Code = KoNoteType.Initial,
                    Status_Code = KoTaskStatus.InWork,
                    TourId = tour.Id,
                    CreationDate = new DateTime(2018, 1, 1),
                    DocumentId = inputDoc.Id
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
                myOleDbCommand.CommandText = "select * from sprFACTORMETKA where value_factor<>'' and metka_factor>-10000";
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
                myOleDbCommand.CommandText = "select * from sprFACTORMETKA where value_factor<>'' and metka_factor>-10000";
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
                        A0ForExponential = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        A0ForMultiplicative = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        Formula = NullConvertor.ToString(myOleDbDataReader["sub_formula"]),
                        AlgoritmType_Code = (KoAlgoritmType)NullConvertor.DBToInt64(myOleDbDataReader["type_formula"]),
                        Description = "-",
                        Name = "-"
                    };
                    koGroup.Save();
                    LoadGroupParcelModelFactor_2016(NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]), koGroup);
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
                        A0ForExponential = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        A0ForMultiplicative = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        Formula = NullConvertor.ToString(myOleDbDataReader["sub_formula"]),
                        AlgoritmType_Code = (KoAlgoritmType)NullConvertor.DBToInt64(myOleDbDataReader["type_formula"]),
                        Description = "-",
                        Name = "-"
                    };
                    koGroup.Save();
                    LoadGroupOKSModelFactor_2016(NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]), koGroup);
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupParcelModelFactor_2016(long ms_id_subgroup, ObjectModel.KO.OMModel model)
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
                        ModelId = model.Id,
                        Weight = NullConvertor.DBToDecimal(myOleDbDataReader["WEIGHT_FACTOR"]),
                        SignAdd = NullConvertor.DBToInt64(myOleDbDataReader["PRADD"]) == 1,
                        SignDiv = NullConvertor.DBToInt64(myOleDbDataReader["PRDIV"]) == 1,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["PR_METKA"]) == 1,
                        MarkerId = -1,
                        AlgorithmType_Code = model.AlgoritmType_Code,
                        AlgorithmType = model.AlgoritmType,
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupOKSModelFactor_2016(long ms_id_subgroup, ObjectModel.KO.OMModel model)
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
                        ModelId = model.Id,
                        Weight = NullConvertor.DBToDecimal(myOleDbDataReader["WEIGHT_FACTOR"]),
                        SignAdd = NullConvertor.DBToInt64(myOleDbDataReader["PRADD"]) == 1,
                        SignDiv = NullConvertor.DBToInt64(myOleDbDataReader["PRDIV"]) == 1,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["PR_METKA"]) == 1,
                        MarkerId = -1,
                        AlgorithmType_Code = model.AlgoritmType_Code,
                        AlgorithmType = model.AlgoritmType,
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
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorParcel_2018,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["IS_METKA"]) == 1
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
                        FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorOKS_2018,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["IS_METKA"]) == 1
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
                myOleDbCommand.CommandText = "select * from sprFACTORMETKA where value_factor<>'' and metka_factor>-10000";
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
                myOleDbCommand.CommandText = "select * from sprFACTORMETKA where value_factor<>'' and metka_factor>-10000";
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
                        A0ForExponential = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        A0ForMultiplicative = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        Formula = NullConvertor.ToString(myOleDbDataReader["sub_formula"]),
                        AlgoritmType_Code = (KoAlgoritmType)NullConvertor.DBToInt64(myOleDbDataReader["type_formula"]),
                        Description = "-",
                        Name = "-"
                    };
                    koGroup.Save();
                    LoadGroupParcelModelFactor_2018(NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]), koGroup);
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
                        A0ForExponential = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        A0ForMultiplicative = NullConvertor.DBToDecimal(myOleDbDataReader["A0"]),
                        Formula = NullConvertor.ToString(myOleDbDataReader["sub_formula"]),
                        AlgoritmType_Code = (KoAlgoritmType)NullConvertor.DBToInt64(myOleDbDataReader["type_formula"]),
                        Description = "-",
                        Name = "-"
                    };
                    koGroup.Save();
                    LoadGroupOKSModelFactor_2018(NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]), koGroup);
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupParcelModelFactor_2018(long ms_id_subgroup, ObjectModel.KO.OMModel model)
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
                        ModelId = model.Id,
                        Weight = NullConvertor.DBToDecimal(myOleDbDataReader["WEIGHT_FACTOR"]),
                        SignAdd = NullConvertor.DBToInt64(myOleDbDataReader["PRADD"]) == 1,
                        SignDiv = NullConvertor.DBToInt64(myOleDbDataReader["PRDIV"]) == 1,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["PR_METKA"]) == 1,
                        MarkerId = -1,
                        AlgorithmType_Code = model.AlgoritmType_Code,
                        AlgorithmType = model.AlgoritmType,
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupOKSModelFactor_2018(long ms_id_subgroup, ObjectModel.KO.OMModel model)
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
                        ModelId = model.Id,
                        Weight = NullConvertor.DBToDecimal(myOleDbDataReader["WEIGHT_FACTOR"]),
                        SignAdd = NullConvertor.DBToInt64(myOleDbDataReader["PRADD"]) == 1,
                        SignDiv = NullConvertor.DBToInt64(myOleDbDataReader["PRDIV"]) == 1,
                        SignMarket = NullConvertor.DBToInt64(myOleDbDataReader["PR_METKA"]) == 1,
                        AlgorithmType_Code = model.AlgoritmType_Code,
                        AlgorithmType = model.AlgoritmType,
                    };
                    koGroup.Save();
                    count++;
                    Console.WriteLine(count);
                }
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadGroupExplication_2018()
        {
            //using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            //{
            //    connection.Open();

            //    SqlCommand myOleDbCommand = connection.CreateCommand();
            //    myOleDbCommand.CommandTimeout = 300;
            //    myOleDbCommand.CommandType = System.Data.CommandType.Text;
            //    myOleDbCommand.CommandText = "select e.*, b.kn_object from tbExplication e, tbObjectBuild b where b.ID_OBJECT=e.ID_OBJECT and e.TYPE_OBJECT=2 and b.change_object=0 and b.status_object=0";
            //    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            //    long count = 0;
            //    List<ObjectModel.KO.OMExplication> Items = new List<ObjectModel.KO.OMExplication>();
            //    while (myOleDbDataReader.Read())
            //    {
            //        string kn = NullConvertor.ToString(myOleDbDataReader["kn_object"]);

            //        ObjectModel.KO.OMExplication koGroup = new ObjectModel.KO.OMExplication
            //        {
            //            Id = -1,
            //            GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
            //            Kc = NullConvertor.DBToDecimalNull(myOleDbDataReader["kc"]),
            //            NameAnalog = NullConvertor.ToString(myOleDbDataReader["NAME_ANALOG"]),
            //            FactorId = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) * 100 + OffsetFactorParcel_2018,
            //            ValueFactor = NullConvertor.ToString(myOleDbDataReader["VALUE_FACTOR"]),
            //            MetkaFactor = NullConvertor.DBToDecimal(myOleDbDataReader["METKA_FACTOR"])

            //        };
            //        count++;
            //        Items.Add(koGroup);
            //        if (Items.Count == 50)
            //        {
            //            ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
            //            Items.Clear();
            //            Console.WriteLine(count);
            //        }
            //    }
            //    ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMMarkCatalog>(Items, x => x.Save());
            //    Console.WriteLine(count);

            //    myOleDbDataReader.Close();
            //    connection.Close();
            //}
        }
        public static void LoadGroupEtalonParcel_2018()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;
                myOleDbCommand.CommandText = "select * from sprEtalon e";
                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMEtalon> Items = new List<ObjectModel.KO.OMEtalon>();
                while (myOleDbDataReader.Read())
                {
                    ObjectModel.KO.OMEtalon koGroup = new ObjectModel.KO.OMEtalon
                    {
                        Id = -1,
                        GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2018,
                        Cadastraldistrict = NullConvertor.ToString(myOleDbDataReader["KR"]),
                        Cadastralnumber = NullConvertor.ToString(myOleDbDataReader["KN"])
                    };
                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 50)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMEtalon>(Items, x => x.Save());
                        Items.Clear();
                        Console.WriteLine(count);
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMEtalon>(Items, x => x.Save());
                Console.WriteLine(count);

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
                        ParentCalcType_Code = KoParentCalcType.None,
                        ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
                    };

                    if (objtype == PropertyTypes.UncompletedBuilding)
                    {
                        koGroup.DegreeReadiness = NullConvertor.DBToProcent(myOleDbDataReader["procent"]);
                    }

                    count++;
                    Items.Add(koGroup);
                    if (Items.Count == 40)
                    {
                        ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x =>
                        {
                            // TODO: Евгению проверить, что так можно
                            //ObjectModel.KO.OMUnit existsOmUnit = ObjectModel.KO.OMUnit.Where(y => y.CadastralNumber == x.CadastralNumber && y.TourId == x.TourId && y.TaskId == x.TaskId).ExecuteFirstOrDefault();

                            //if (existsOmUnit != null) return;

                            x.SaveAndCreate();
                            LoadUnitFactorOKS_2016(x.OldId, x.Id, x.PropertyType_Code);
                        });
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
                        ParentCalcType_Code = KoParentCalcType.None
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

        public static void LoadUnitTaskOKS_2016_Skip(long id_task, long id_tour, PropertyTypes objtype)
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
                    string kn = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]);
                    ObjectModel.KO.OMUnit koGroup = ObjectModel.KO.OMUnit.Where(x => x.TourId == id_tour && x.TaskId == id_task && x.CadastralNumber == kn).SelectAll().ExecuteFirstOrDefault();
                    if (koGroup == null)
                    {
                        koGroup = new ObjectModel.KO.OMUnit
                        {
                            Id = -1,
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
                            CadastralNumber = kn,
                            CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                            Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                            PropertyType_Code = objtype,
                            StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
                            StatusResultCalc_Code = KoStatusResultCalc.None,
                            ParentCalcType_Code = KoParentCalcType.None,
                            ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
                        };

                        if (objtype == PropertyTypes.UncompletedBuilding)
                        {
                            koGroup.DegreeReadiness = NullConvertor.DBToProcent(myOleDbDataReader["procent"]);
                        }
                        Items.Add(koGroup);
                    }
                    count++;
                    if (count % 100 == 0) Console.WriteLine(count);

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
                        StatusResultCalc_Code = KoStatusResultCalc.None,
                        ParentCalcType_Code = KoParentCalcType.None,
                        ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
                    };
                    if (objtype == PropertyTypes.UncompletedBuilding)
                    {
                        koGroup.DegreeReadiness = NullConvertor.DBToProcent(myOleDbDataReader["procent"]);
                    }
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
            int ms_type_object = 0;
            switch (objtype)
            {
                case PropertyTypes.Building:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Building_2018"];
                    ms_type_object = 2;
                    break;
                case PropertyTypes.Pllacement:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Flat_2018"];
                    ms_type_object = 3;
                    break;
                case PropertyTypes.Construction:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Constr_2018"];
                    ms_type_object = 4;
                    break;
                case PropertyTypes.UncompletedBuilding:
                    conn = ConfigurationManager.AppSettings["SQL_connection_OKS_Uncomp_2018"];
                    ms_type_object = 5;
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


                using (SqlConnection connectionMain = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
                {
                    connectionMain.Open();

                    SqlCommand myOleDbCommandMain = connectionMain.CreateCommand();
                    myOleDbCommandMain.CommandTimeout = 600;
                    myOleDbCommandMain.CommandType = System.Data.CommandType.Text;


                    myOleDbCommandMain.CommandText = "select * from tbExplication where id_object=" + ms_id_object.ToString() + " and type_object=" + ms_type_object.ToString(); ;
                    myOleDbDataReader = myOleDbCommandMain.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        ObjectModel.KO.OMExplication exp = new ObjectModel.KO.OMExplication
                        {
                            Id = -1,
                            ObjectId = id_unit,
                            GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                            Square = NullConvertor.DBToDecimalNull(myOleDbDataReader["square"]),
                            Upks = NullConvertor.DBToDecimalNull(myOleDbDataReader["upks"]),
                            Kc = NullConvertor.DBToDecimalNull(myOleDbDataReader["kc"]),
                            NameAnalog = NullConvertor.ToString(myOleDbDataReader["calc_parent"]),
                            UpksAnalog = NullConvertor.DBToDecimalNull(myOleDbDataReader["upks_parent"]),
                        };
                        exp.Save();
                    }
                    myOleDbDataReader.Close();
                    connectionMain.Close();
                }

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
                        StatusResultCalc_Code = KoStatusResultCalc.None,
                        UseAsPrototype = NullConvertor.DBToBoolean(myOleDbDataReader["PROCENT"]),
                        ParentCalcType_Code = KoParentCalcType.None,
                        ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
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
            GetFormula(7977502);
        }
        public static void GetCalcGroup()
        {
            ObjectModel.KO.KOCalcSettings ks = new ObjectModel.KO.KOCalcSettings();
            ks.CalcParcel = true;
            ks.CalcStage1 = true;
            ks.CalcStage2 = true;
            ks.CalcStage3 = true;
            ks.IdTour = 2018;
            ks.CalcAllGroups = false;
            ks.CalcGroups = new List<long>();
            ks.CalcGroups.Add(200031);//200003  100044
            ks.TaskFilter = new List<long>();
            ks.TaskFilter.Add(36743917);//36661332  3663016

            ObjectModel.KO.OMGroup.CalculateSelectGroup(ks);
        }
        public static void ImportXml2016(long idTour, long idTask)
        {
            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == idTask).SelectAll().ExecuteFirstOrDefault();
            if (task != null)
            {
                {
                    string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["XML_Path_2016"], "*.xml", SearchOption.AllDirectories);

                    int countAll = files.Length;
                    int count = 0;
                    foreach (string file in files)
                    {
                        count++;
                        FileStream fileStream = new FileStream(file, FileMode.Open);
                        new Dal.DataImport.DataImporterGkn().ImportDataGknFromXml(fileStream, ConfigurationManager.AppSettings["Schema_Path_2016"], task);
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
                    string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["XML_Path_2018"], "*.xml", SearchOption.AllDirectories);

                    int countAll = files.Length;
                    int count = 0;
                    foreach (string file in files)
                    {
                        count++;
                        FileStream fileStream = new FileStream(file, FileMode.Open);
                        new Dal.DataImport.DataImporterGkn().ImportDataGknFromXml(fileStream, ConfigurationManager.AppSettings["Schema_Path_2018"], task);
                        Console.WriteLine(count.ToString() + " из " + countAll.ToString());
                    }
                }
            }
        }





        public static void DoLoadBd2018Unit_Parcel_VUON()
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

            LoadUnitTaskParcel_2018_VUON(tour.Id);
        }
        public static void DoLoadBd2018Unit_Build_VUON()
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

            LoadUnitTaskBuild_2018_VUON(tour.Id);
        }
        public static void DoLoadBd2018Unit_Construction_VUON()
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

            LoadUnitTaskConstruction_2018_VUON(tour.Id);
        }
        public static void DoLoadBd2018Unit_Flat_VUON()
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

            LoadUnitTaskFlat_2018_VUON(tour.Id);
        }
        public static void DoLoadBd2018Unit_Uncomplited_VUON()
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

            LoadUnitTaskUncomplited_2018_VUON(tour.Id);
        }

        public static void LoadUnitTaskParcel_2018_VUON(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectParcel o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 1 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {

                    int change_object = NullConvertor.DBToInt(myOleDbDataReader["change_object"]);
                    KoUnitStatus koUnitStatus = KoUnitStatus.None;
                    switch (change_object)
                    {
                        case 0:
                            koUnitStatus = KoUnitStatus.Initial;
                            break;
                        case 1:
                            koUnitStatus = KoUnitStatus.New;
                            break;
                        case 2:
                            koUnitStatus = KoUnitStatus.Recalculated;
                            break;
                        case 3:
                            koUnitStatus = KoUnitStatus.Annual;
                            break;
                        default:
                            break;
                    }

                    int status_calc = NullConvertor.DBToInt(myOleDbDataReader["status_calc"]);
                    KoStatusResultCalc koStatusResultCalc = KoStatusResultCalc.None;
                    switch (status_calc)
                    {
                        case 0:
                            koStatusResultCalc = KoStatusResultCalc.CostChanged;
                            break;
                        case 1:
                            koStatusResultCalc = KoStatusResultCalc.CostNotChanged;
                            break;
                        case 2:
                            koStatusResultCalc = KoStatusResultCalc.ErrorInData;
                            break;
                        case 3:
                            koStatusResultCalc = KoStatusResultCalc.ErrorTechnical;
                            break;
                        default:
                            break;
                    }

                    int status_object = NullConvertor.DBToInt(myOleDbDataReader["status_object"]);
                    KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.None;
                    switch (status_object)
                    {
                        case 0:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Initial;
                            break;
                        case 2:
                            koStatusRepeatCalc = KoStatusRepeatCalc.RepeatedInitial;
                            break;
                        case 3:
                            koStatusRepeatCalc = KoStatusRepeatCalc.New;
                            break;
                        case 4:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Repeated;
                            break;
                        default:
                            break;
                    }

                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    Int64 id_outputDoc = NullConvertor.DBToInt(myOleDbDataReader["id_outputdoc"]);

                    int doc_in_status = NullConvertor.DBToInt(myOleDbDataReader["doc_in_status"]);
                    KoNoteType koNoteType = KoNoteType.Initial;
                    switch (doc_in_status)
                    {
                        case 0:
                            koNoteType = KoNoteType.Day;
                            break;
                        case 1:
                            koNoteType = KoNoteType.Petition;
                            break;
                        case 2:
                            koNoteType = KoNoteType.Year;
                            break;
                        default:
                            break;
                    }




                    OMInstance inputDoc = null;
                    OMInstance outputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);

                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 100000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 100000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }
                    if (id_outputDoc > 0)
                    {
                        string outRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_out_num"]);
                        DateTime outCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_date"]);
                        DateTime outApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_app"]);
                        string outDescription = NullConvertor.ToString(myOleDbDataReader["doc_out_name"]);

                        outputDoc = OMInstance.Where(x => x.Id == (id_outputDoc + 100000000)).SelectAll().ExecuteFirstOrDefault();
                        if (outputDoc == null)
                        {
                            outputDoc = OMInstance.Where(x => x.RegNumber == outRegNumber && x.CreateDate == outCreateDate && x.ApproveDate == outApproveDate && x.Description == outDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (outputDoc == null)
                        {
                            outputDoc = new OMInstance
                            {
                                Id = id_outputDoc + 100000000,
                                RegNumber = outRegNumber,
                                CreateDate = outCreateDate,
                                ApproveDate = outApproveDate,
                                Description = outDescription,
                            };
                            outputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        ObjectModel.KO.OMTask task = null;
                        task = ObjectModel.KO.OMTask.Where(x => x.DocumentId == inputDoc.Id).SelectAll().ExecuteFirstOrDefault();

                        if (task == null)
                        {
                            task = new ObjectModel.KO.OMTask
                            {
                                Id = -1,
                                NoteType_Code = koNoteType,
                                Status_Code = KoTaskStatus.InWork,
                                TourId = id_tour,
                                CreationDate = inputDoc.CreateDate,
                                DocumentId = inputDoc.Id,
                                EstimationDate = inputDoc.ApproveDate
                            };
                            task.Save();
                        }








                        ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                        {
                            Id = -1,
                            CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["KC_OBJECT"]),
                            CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                            Upks = NullConvertor.DBToDecimal(myOleDbDataReader["UPKSZ_OBJECT"]),
                            UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                            GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupParcel_2018,
                            OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                            TourId = id_tour,
                            TaskId = task.Id,
                            Status_Code = koUnitStatus,
                            ObjectId = -1,
                            CreationDate = task.EstimationDate,
                            CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                            CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                            Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                            PropertyType_Code = PropertyTypes.Stead,
                            StatusRepeatCalc_Code = koStatusRepeatCalc,
                            StatusResultCalc_Code = koStatusResultCalc,
                            UseAsPrototype = NullConvertor.DBToBoolean(myOleDbDataReader["PROCENT"]),
                            ResponseDocId = ((outputDoc == null) ? -1 : outputDoc.Id),
                            ParentCalcType_Code = KoParentCalcType.None,
                            ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
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
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorParcel_2018(x.OldId, x.Id); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskBuild_2018_VUON(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectBuild o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 2 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {

                    int change_object = NullConvertor.DBToInt(myOleDbDataReader["change_object"]);
                    KoUnitStatus koUnitStatus = KoUnitStatus.None;
                    switch (change_object)
                    {
                        case 0:
                            koUnitStatus = KoUnitStatus.Initial;
                            break;
                        case 1:
                            koUnitStatus = KoUnitStatus.New;
                            break;
                        case 2:
                            koUnitStatus = KoUnitStatus.Recalculated;
                            break;
                        case 3:
                            koUnitStatus = KoUnitStatus.Annual;
                            break;
                        default:
                            break;
                    }

                    int status_calc = NullConvertor.DBToInt(myOleDbDataReader["status_calc"]);
                    KoStatusResultCalc koStatusResultCalc = KoStatusResultCalc.None;
                    switch (status_calc)
                    {
                        case 0:
                            koStatusResultCalc = KoStatusResultCalc.CostChanged;
                            break;
                        case 1:
                            koStatusResultCalc = KoStatusResultCalc.CostNotChanged;
                            break;
                        case 2:
                            koStatusResultCalc = KoStatusResultCalc.ErrorInData;
                            break;
                        case 3:
                            koStatusResultCalc = KoStatusResultCalc.ErrorTechnical;
                            break;
                        default:
                            break;
                    }

                    int status_object = NullConvertor.DBToInt(myOleDbDataReader["status_object"]);
                    KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.None;
                    switch (status_object)
                    {
                        case 0:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Initial;
                            break;
                        case 2:
                            koStatusRepeatCalc = KoStatusRepeatCalc.RepeatedInitial;
                            break;
                        case 3:
                            koStatusRepeatCalc = KoStatusRepeatCalc.New;
                            break;
                        case 4:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Repeated;
                            break;
                        default:
                            break;
                    }

                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    Int64 id_outputDoc = NullConvertor.DBToInt(myOleDbDataReader["id_outputdoc"]);
                    int doc_in_status = NullConvertor.DBToInt(myOleDbDataReader["doc_in_status"]);
                    KoNoteType koNoteType = KoNoteType.Initial;
                    switch (doc_in_status)
                    {
                        case 0:
                            koNoteType = KoNoteType.Day;
                            break;
                        case 1:
                            koNoteType = KoNoteType.Petition;
                            break;
                        case 2:
                            koNoteType = KoNoteType.Year;
                            break;
                        default:
                            break;
                    }




                    OMInstance inputDoc = null;
                    OMInstance outputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);
                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 200000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }
                    if (id_outputDoc > 0)
                    {
                        string outRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_out_num"]);
                        DateTime outCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_date"]);
                        DateTime outApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_app"]);
                        string outDescription = NullConvertor.ToString(myOleDbDataReader["doc_out_name"]);
                        outputDoc = OMInstance.Where(x => x.Id == (id_outputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (outputDoc == null)
                        {
                            outputDoc = OMInstance.Where(x => x.RegNumber == outRegNumber && x.CreateDate == outCreateDate && x.ApproveDate == outApproveDate && x.Description == outDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (outputDoc == null)
                        {
                            outputDoc = new OMInstance
                            {
                                Id = id_outputDoc + 200000000,
                                RegNumber = outRegNumber,
                                CreateDate = outCreateDate,
                                ApproveDate = outApproveDate,
                                Description = outDescription,
                            };
                            outputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        ObjectModel.KO.OMTask task = null;
                        task = ObjectModel.KO.OMTask.Where(x => x.DocumentId == inputDoc.Id).SelectAll().ExecuteFirstOrDefault();

                        if (task == null)
                        {
                            task = new ObjectModel.KO.OMTask
                            {
                                Id = -1,
                                NoteType_Code = koNoteType,
                                Status_Code = KoTaskStatus.InWork,
                                TourId = id_tour,
                                CreationDate = inputDoc.CreateDate,
                                DocumentId = inputDoc.Id,
                                EstimationDate = inputDoc.ApproveDate
                            };
                            task.Save();
                        }








                        ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                        {

                            Id = -1,
                            CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["KC_OBJECT"]),
                            CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                            Upks = NullConvertor.DBToDecimal(myOleDbDataReader["UPKSZ_OBJECT"]),
                            UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                            GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                            OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                            TourId = id_tour,
                            TaskId = task.Id,
                            Status_Code = koUnitStatus,
                            ObjectId = -1,
                            CreationDate = task.EstimationDate,
                            CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                            CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                            Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                            PropertyType_Code = PropertyTypes.Building,
                            StatusRepeatCalc_Code = koStatusRepeatCalc,
                            StatusResultCalc_Code = koStatusResultCalc,
                            ResponseDocId = ((outputDoc == null) ? -1 : outputDoc.Id),
                            ParentCalcType_Code = KoParentCalcType.None,
                            ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
                        };
                        count++;
                        Items.Add(koGroup);
                        if (Items.Count == 25)
                        {
                            ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, PropertyTypes.Building); });
                            Items.Clear();
                            Console.WriteLine(count);
                        }
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, PropertyTypes.Building); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskConstruction_2018_VUON(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectConstruction o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 4 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {

                    int change_object = NullConvertor.DBToInt(myOleDbDataReader["change_object"]);
                    KoUnitStatus koUnitStatus = KoUnitStatus.None;
                    switch (change_object)
                    {
                        case 0:
                            koUnitStatus = KoUnitStatus.Initial;
                            break;
                        case 1:
                            koUnitStatus = KoUnitStatus.New;
                            break;
                        case 2:
                            koUnitStatus = KoUnitStatus.Recalculated;
                            break;
                        case 3:
                            koUnitStatus = KoUnitStatus.Annual;
                            break;
                        default:
                            break;
                    }

                    int status_calc = NullConvertor.DBToInt(myOleDbDataReader["status_calc"]);
                    KoStatusResultCalc koStatusResultCalc = KoStatusResultCalc.None;
                    switch (status_calc)
                    {
                        case 0:
                            koStatusResultCalc = KoStatusResultCalc.CostChanged;
                            break;
                        case 1:
                            koStatusResultCalc = KoStatusResultCalc.CostNotChanged;
                            break;
                        case 2:
                            koStatusResultCalc = KoStatusResultCalc.ErrorInData;
                            break;
                        case 3:
                            koStatusResultCalc = KoStatusResultCalc.ErrorTechnical;
                            break;
                        default:
                            break;
                    }

                    int status_object = NullConvertor.DBToInt(myOleDbDataReader["status_object"]);
                    KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.None;
                    switch (status_object)
                    {
                        case 0:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Initial;
                            break;
                        case 2:
                            koStatusRepeatCalc = KoStatusRepeatCalc.RepeatedInitial;
                            break;
                        case 3:
                            koStatusRepeatCalc = KoStatusRepeatCalc.New;
                            break;
                        case 4:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Repeated;
                            break;
                        default:
                            break;
                    }

                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    Int64 id_outputDoc = NullConvertor.DBToInt(myOleDbDataReader["id_outputdoc"]);
                    int doc_in_status = NullConvertor.DBToInt(myOleDbDataReader["doc_in_status"]);
                    KoNoteType koNoteType = KoNoteType.Initial;
                    switch (doc_in_status)
                    {
                        case 0:
                            koNoteType = KoNoteType.Day;
                            break;
                        case 1:
                            koNoteType = KoNoteType.Petition;
                            break;
                        case 2:
                            koNoteType = KoNoteType.Year;
                            break;
                        default:
                            break;
                    }




                    OMInstance inputDoc = null;
                    OMInstance outputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);

                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 200000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }
                    if (id_outputDoc > 0)
                    {
                        string outRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_out_num"]);
                        DateTime outCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_date"]);
                        DateTime outApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_app"]);
                        string outDescription = NullConvertor.ToString(myOleDbDataReader["doc_out_name"]);

                        outputDoc = OMInstance.Where(x => x.Id == (id_outputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (outputDoc == null)
                        {
                            outputDoc = OMInstance.Where(x => x.RegNumber == outRegNumber && x.CreateDate == outCreateDate && x.ApproveDate == outApproveDate && x.Description == outDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (outputDoc == null)
                        {
                            outputDoc = new OMInstance
                            {
                                Id = id_outputDoc + 200000000,
                                RegNumber = outRegNumber,
                                CreateDate = outCreateDate,
                                ApproveDate = outApproveDate,
                                Description = outDescription,
                            };
                            outputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        ObjectModel.KO.OMTask task = null;
                        task = ObjectModel.KO.OMTask.Where(x => x.DocumentId == inputDoc.Id).SelectAll().ExecuteFirstOrDefault();

                        if (task == null)
                        {
                            task = new ObjectModel.KO.OMTask
                            {
                                Id = -1,
                                NoteType_Code = koNoteType,
                                Status_Code = KoTaskStatus.InWork,
                                TourId = id_tour,
                                CreationDate = inputDoc.CreateDate,
                                DocumentId = inputDoc.Id,
                                EstimationDate = inputDoc.ApproveDate
                            };
                            task.Save();
                        }








                        ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                        {

                            Id = -1,
                            CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["KC_OBJECT"]),
                            CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                            Upks = NullConvertor.DBToDecimal(myOleDbDataReader["UPKSZ_OBJECT"]),
                            UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                            GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                            OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                            TourId = id_tour,
                            TaskId = task.Id,
                            Status_Code = koUnitStatus,
                            ObjectId = -1,
                            CreationDate = task.EstimationDate,
                            CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                            CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                            Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                            PropertyType_Code = PropertyTypes.Construction,
                            StatusRepeatCalc_Code = koStatusRepeatCalc,
                            StatusResultCalc_Code = koStatusResultCalc,
                            ResponseDocId = ((outputDoc == null) ? -1 : outputDoc.Id),
                            ParentCalcType_Code = KoParentCalcType.None,
                            ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
                        };
                        count++;
                        Items.Add(koGroup);
                        if (Items.Count == 25)
                        {
                            ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, PropertyTypes.Construction); });
                            Items.Clear();
                            Console.WriteLine(count);
                        }
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, PropertyTypes.Construction); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskFlat_2018_VUON(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectFlat o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 3 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {

                    int change_object = NullConvertor.DBToInt(myOleDbDataReader["change_object"]);
                    KoUnitStatus koUnitStatus = KoUnitStatus.None;
                    switch (change_object)
                    {
                        case 0:
                            koUnitStatus = KoUnitStatus.Initial;
                            break;
                        case 1:
                            koUnitStatus = KoUnitStatus.New;
                            break;
                        case 2:
                            koUnitStatus = KoUnitStatus.Recalculated;
                            break;
                        case 3:
                            koUnitStatus = KoUnitStatus.Annual;
                            break;
                        default:
                            break;
                    }

                    int status_calc = NullConvertor.DBToInt(myOleDbDataReader["status_calc"]);
                    KoStatusResultCalc koStatusResultCalc = KoStatusResultCalc.None;
                    switch (status_calc)
                    {
                        case 0:
                            koStatusResultCalc = KoStatusResultCalc.CostChanged;
                            break;
                        case 1:
                            koStatusResultCalc = KoStatusResultCalc.CostNotChanged;
                            break;
                        case 2:
                            koStatusResultCalc = KoStatusResultCalc.ErrorInData;
                            break;
                        case 3:
                            koStatusResultCalc = KoStatusResultCalc.ErrorTechnical;
                            break;
                        default:
                            break;
                    }

                    int status_object = NullConvertor.DBToInt(myOleDbDataReader["status_object"]);
                    KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.None;
                    switch (status_object)
                    {
                        case 0:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Initial;
                            break;
                        case 2:
                            koStatusRepeatCalc = KoStatusRepeatCalc.RepeatedInitial;
                            break;
                        case 3:
                            koStatusRepeatCalc = KoStatusRepeatCalc.New;
                            break;
                        case 4:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Repeated;
                            break;
                        default:
                            break;
                    }

                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    Int64 id_outputDoc = NullConvertor.DBToInt(myOleDbDataReader["id_outputdoc"]);
                    int doc_in_status = NullConvertor.DBToInt(myOleDbDataReader["doc_in_status"]);
                    KoNoteType koNoteType = KoNoteType.Initial;
                    switch (doc_in_status)
                    {
                        case 0:
                            koNoteType = KoNoteType.Day;
                            break;
                        case 1:
                            koNoteType = KoNoteType.Petition;
                            break;
                        case 2:
                            koNoteType = KoNoteType.Year;
                            break;
                        default:
                            break;
                    }




                    OMInstance inputDoc = null;
                    OMInstance outputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);
                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 200000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }
                    if (id_outputDoc > 0)
                    {
                        string outRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_out_num"]);
                        DateTime outCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_date"]);
                        DateTime outApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_app"]);
                        string outDescription = NullConvertor.ToString(myOleDbDataReader["doc_out_name"]);
                        outputDoc = OMInstance.Where(x => x.Id == (id_outputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (outputDoc == null)
                        {
                            outputDoc = OMInstance.Where(x => x.RegNumber == outRegNumber && x.CreateDate == outCreateDate && x.ApproveDate == outApproveDate && x.Description == outDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (outputDoc == null)
                        {
                            outputDoc = new OMInstance
                            {
                                Id = id_outputDoc + 200000000,
                                RegNumber = outRegNumber,
                                CreateDate = outCreateDate,
                                ApproveDate = outApproveDate,
                                Description = outDescription,
                            };
                            outputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        ObjectModel.KO.OMTask task = null;
                        task = ObjectModel.KO.OMTask.Where(x => x.DocumentId == inputDoc.Id).SelectAll().ExecuteFirstOrDefault();

                        if (task == null)
                        {
                            task = new ObjectModel.KO.OMTask
                            {
                                Id = -1,
                                NoteType_Code = koNoteType,
                                Status_Code = KoTaskStatus.InWork,
                                TourId = id_tour,
                                CreationDate = inputDoc.CreateDate,
                                DocumentId = inputDoc.Id,
                                EstimationDate = inputDoc.ApproveDate

                            };
                            task.Save();
                        }








                        ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                        {

                            Id = -1,
                            CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["KC_OBJECT"]),
                            CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                            Upks = NullConvertor.DBToDecimal(myOleDbDataReader["UPKSZ_OBJECT"]),
                            UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                            GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                            OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                            TourId = id_tour,
                            TaskId = task.Id,
                            Status_Code = koUnitStatus,
                            ObjectId = -1,
                            CreationDate = task.EstimationDate,
                            CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                            CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                            Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                            PropertyType_Code = PropertyTypes.Pllacement,
                            StatusRepeatCalc_Code = koStatusRepeatCalc,
                            StatusResultCalc_Code = koStatusResultCalc,
                            ResponseDocId = ((outputDoc == null) ? -1 : outputDoc.Id),
                            ParentCalcType_Code = KoParentCalcType.None,
                            ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
                        };
                        count++;
                        Items.Add(koGroup);
                        if (Items.Count == 25)
                        {
                            ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, PropertyTypes.Pllacement); });
                            Items.Clear();
                            Console.WriteLine(count);
                        }
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, PropertyTypes.Pllacement); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskUncomplited_2018_VUON(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectUnderconstruction o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 5 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {

                    int change_object = NullConvertor.DBToInt(myOleDbDataReader["change_object"]);
                    KoUnitStatus koUnitStatus = KoUnitStatus.None;
                    switch (change_object)
                    {
                        case 0:
                            koUnitStatus = KoUnitStatus.Initial;
                            break;
                        case 1:
                            koUnitStatus = KoUnitStatus.New;
                            break;
                        case 2:
                            koUnitStatus = KoUnitStatus.Recalculated;
                            break;
                        case 3:
                            koUnitStatus = KoUnitStatus.Annual;
                            break;
                        default:
                            break;
                    }

                    int status_calc = NullConvertor.DBToInt(myOleDbDataReader["status_calc"]);
                    KoStatusResultCalc koStatusResultCalc = KoStatusResultCalc.None;
                    switch (status_calc)
                    {
                        case 0:
                            koStatusResultCalc = KoStatusResultCalc.CostChanged;
                            break;
                        case 1:
                            koStatusResultCalc = KoStatusResultCalc.CostNotChanged;
                            break;
                        case 2:
                            koStatusResultCalc = KoStatusResultCalc.ErrorInData;
                            break;
                        case 3:
                            koStatusResultCalc = KoStatusResultCalc.ErrorTechnical;
                            break;
                        default:
                            break;
                    }

                    int status_object = NullConvertor.DBToInt(myOleDbDataReader["status_object"]);
                    KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.None;
                    switch (status_object)
                    {
                        case 0:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Initial;
                            break;
                        case 2:
                            koStatusRepeatCalc = KoStatusRepeatCalc.RepeatedInitial;
                            break;
                        case 3:
                            koStatusRepeatCalc = KoStatusRepeatCalc.New;
                            break;
                        case 4:
                            koStatusRepeatCalc = KoStatusRepeatCalc.Repeated;
                            break;
                        default:
                            break;
                    }

                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    Int64 id_outputDoc = NullConvertor.DBToInt(myOleDbDataReader["id_outputdoc"]);
                    int doc_in_status = NullConvertor.DBToInt(myOleDbDataReader["doc_in_status"]);
                    KoNoteType koNoteType = KoNoteType.Initial;
                    switch (doc_in_status)
                    {
                        case 0:
                            koNoteType = KoNoteType.Day;
                            break;
                        case 1:
                            koNoteType = KoNoteType.Petition;
                            break;
                        case 2:
                            koNoteType = KoNoteType.Year;
                            break;
                        default:
                            break;
                    }




                    OMInstance inputDoc = null;
                    OMInstance outputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);
                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 200000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }
                    if (id_outputDoc > 0)
                    {
                        string outRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_out_num"]);
                        DateTime outCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_date"]);
                        DateTime outApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_out_app"]);
                        string outDescription = NullConvertor.ToString(myOleDbDataReader["doc_out_name"]);
                        outputDoc = OMInstance.Where(x => x.Id == (id_outputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (outputDoc == null)
                        {
                            outputDoc = OMInstance.Where(x => x.RegNumber == outRegNumber && x.CreateDate == outCreateDate && x.ApproveDate == outApproveDate && x.Description == outDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (outputDoc == null)
                        {
                            outputDoc = new OMInstance
                            {
                                Id = id_outputDoc + 200000000,
                                RegNumber = outRegNumber,
                                CreateDate = outCreateDate,
                                ApproveDate = outApproveDate,
                                Description = outDescription,
                            };
                            outputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        ObjectModel.KO.OMTask task = null;
                        task = ObjectModel.KO.OMTask.Where(x => x.DocumentId == inputDoc.Id).SelectAll().ExecuteFirstOrDefault();

                        if (task == null)
                        {
                            task = new ObjectModel.KO.OMTask
                            {
                                Id = -1,
                                NoteType_Code = koNoteType,
                                Status_Code = KoTaskStatus.InWork,
                                TourId = id_tour,
                                CreationDate = inputDoc.CreateDate,
                                DocumentId = inputDoc.Id,
                                EstimationDate = inputDoc.ApproveDate

                            };
                            task.Save();
                        }








                        ObjectModel.KO.OMUnit koGroup = new ObjectModel.KO.OMUnit
                        {

                            Id = -1,
                            CadastralCost = NullConvertor.DBToDecimal(myOleDbDataReader["KC_OBJECT"]),
                            CadastralCostPre = NullConvertor.DBToDecimal(myOleDbDataReader["NKC_OBJECT"]),
                            Upks = NullConvertor.DBToDecimal(myOleDbDataReader["UPKSZ_OBJECT"]),
                            UpksPre = NullConvertor.DBToDecimal(myOleDbDataReader["NUPKSZ_OBJECT"]),
                            GroupId = NullConvertor.DBToInt64(myOleDbDataReader["id_subgroup"]) + OffsetSubGroupOKS_2018,
                            OldId = NullConvertor.DBToInt64(myOleDbDataReader["id_object"]),
                            TourId = id_tour,
                            TaskId = task.Id,
                            Status_Code = koUnitStatus,
                            ObjectId = -1,
                            CreationDate = task.EstimationDate,
                            CadastralNumber = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]),
                            CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]),
                            Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]),
                            PropertyType_Code = PropertyTypes.UncompletedBuilding,
                            StatusRepeatCalc_Code = koStatusRepeatCalc,
                            StatusResultCalc_Code = koStatusResultCalc,
                            ResponseDocId = ((outputDoc == null) ? -1 : outputDoc.Id),
                            ParentCalcType_Code = KoParentCalcType.None,
                            ParentCalcNumber = NullConvertor.ToString(myOleDbDataReader["CALC_PARENT"]),
                        };
                        koGroup.DegreeReadiness = NullConvertor.DBToProcent(myOleDbDataReader["procent"]);
                        count++;
                        Items.Add(koGroup);
                        if (Items.Count == 25)
                        {
                            ParallelLoopResult resultSucces1 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, PropertyTypes.UncompletedBuilding); });
                            Items.Clear();
                            Console.WriteLine(count);
                        }
                    }
                }
                ParallelLoopResult resultSucces2 = Parallel.ForEach<ObjectModel.KO.OMUnit>(Items, x => { x.SaveAndCreate(); LoadUnitFactorOKS_2018(x.OldId, x.Id, PropertyTypes.UncompletedBuilding); });
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }


        public static void DoLoadBd2018Unit_Parcel_VUON_GKN()
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

            LoadUnitTaskParcel_2018_VUON_GKN(tour.Id);
        }
        public static void DoLoadBd2018Unit_Build_VUON_GKN()
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

            LoadUnitTaskBuild_2018_VUON_GKN(tour.Id);
        }
        public static void DoLoadBd2018Unit_Construction_VUON_GKN()
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

            LoadUnitTaskConstruction_2018_VUON_GKN(tour.Id);
        }
        public static void DoLoadBd2018Unit_Uncomplited_VUON_GKN()
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

            LoadUnitTaskUncomplited_2018_VUON_GKN(tour.Id);
        }
        public static void DoLoadBd2018Unit_Flat_VUON_GKN()
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

            LoadUnitTaskFlat_2018_VUON_GKN(tour.Id);
        }

        public static void LoadUnitTaskParcel_2018_VUON_GKN(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_Parcel_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectParcel o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 1 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.Gbu.OMMainObject> Items = new List<ObjectModel.Gbu.OMMainObject>();

                while (myOleDbDataReader.Read())
                {
                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);

                    OMInstance inputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);

                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 100000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 100000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        string kn = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]);
                        decimal Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]);
                        string CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]);
                        string Name = NullConvertor.ToString(myOleDbDataReader["name_object"]);
                        string Place = NullConvertor.ToString(myOleDbDataReader["place_object"]);
                        string Adress = NullConvertor.ToString(myOleDbDataReader["adress_object"]);
                        string Category = NullConvertor.ToString(myOleDbDataReader["key_parametr"]);
                        string ByDoc = NullConvertor.ToString(myOleDbDataReader["use_object"]);

                        ObjectModel.Gbu.OMMainObject obj = ObjectModel.Gbu.OMMainObject.Where(x => x.CadastralNumber == kn).SelectAll().ExecuteFirstOrDefault();
                        if (obj != null)
                        {
                            count++;

                            #region Сохранение данных ГКН
                            //Наименование участка
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(1, Name, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Площадь
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(2, Square, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Тип объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(26, "Земельный участок", obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Местоположение
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(8, Place, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Адрес
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(600, Adress, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Кадастровый квартал
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(601, CadastralBlock, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Категория земель
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(3, Category, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Вид использования по документам
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(4, ByDoc, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            #endregion




                            Items.Add(obj);
                            if (count % 25 == 0)
                            {
                                Console.WriteLine(count);
                            }
                        }
                    }
                }
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskBuild_2018_VUON_GKN(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectBuild o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 2 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.Gbu.OMMainObject> Items = new List<ObjectModel.Gbu.OMMainObject>();

                while (myOleDbDataReader.Read())
                {
                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    OMInstance inputDoc = null;
                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);
                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 200000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        string kn = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]);
                        string zu = NullConvertor.ToString(myOleDbDataReader["KN_ZU"]);
                        decimal Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]);
                        string CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]);
                        string Name = NullConvertor.ToString(myOleDbDataReader["name_object"]);
                        string Place = NullConvertor.ToString(myOleDbDataReader["place_object"]);
                        string Adress = NullConvertor.ToString(myOleDbDataReader["adress_object"]);
                        string Category = NullConvertor.ToString(myOleDbDataReader["key_parametr"]);
                        string ByDoc = NullConvertor.ToString(myOleDbDataReader["use_object"]);
                        string wall = NullConvertor.ToString(myOleDbDataReader["wall"]);
                        string floor = NullConvertor.ToString(myOleDbDataReader["floors"]);
                        string unfloor = NullConvertor.ToString(myOleDbDataReader["under_floors"]);
                        string built = NullConvertor.ToString(myOleDbDataReader["year_built"]);
                        string used = NullConvertor.ToString(myOleDbDataReader["year_used"]);

                        ObjectModel.Gbu.OMMainObject obj = ObjectModel.Gbu.OMMainObject.Where(x => x.CadastralNumber == kn).SelectAll().ExecuteFirstOrDefault();
                        if (obj != null)
                        {
                            count++;

                            #region Сохранение данных ГКН
                            //Площадь
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(2, Square, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Назначение здания
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(14, ByDoc, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Наименование объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(19, Name, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Количество этажей
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(17, floor, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Количество подземных этажей
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(18, unfloor, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Год ввода в эксплуатацию
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(16, used, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Год постройки
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(15, built, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Тип объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(26, "Здание", obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Материал стен
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(21, wall, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Местоположение
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(8, Place, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Адрес
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(600, Adress, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Кадастровый квартал
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(601, CadastralBlock, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Земельный участок
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(602, zu, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            #endregion




                            Items.Add(obj);
                            if (count % 25 == 0)
                            {
                                Console.WriteLine(count);
                            }
                        }
                    }
                }
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskConstruction_2018_VUON_GKN(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectConstruction o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 4 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.Gbu.OMMainObject> Items = new List<ObjectModel.Gbu.OMMainObject>();

                while (myOleDbDataReader.Read())
                {
                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    OMInstance inputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);

                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 200000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        string kn = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]);
                        string zu = NullConvertor.ToString(myOleDbDataReader["KN_ZU"]);
                        decimal Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]);
                        string CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]);
                        string Name = NullConvertor.ToString(myOleDbDataReader["name_object"]);
                        string Place = NullConvertor.ToString(myOleDbDataReader["place_object"]);
                        string Adress = NullConvertor.ToString(myOleDbDataReader["adress_object"]);
                        string Category = NullConvertor.ToString(myOleDbDataReader["key_parametr"]);
                        string ByDoc = NullConvertor.ToString(myOleDbDataReader["use_object"]);
                        string wall = NullConvertor.ToString(myOleDbDataReader["wall"]);
                        string floor = NullConvertor.ToString(myOleDbDataReader["floors"]);
                        string unfloor = NullConvertor.ToString(myOleDbDataReader["under_floors"]);
                        string built = NullConvertor.ToString(myOleDbDataReader["year_built"]);
                        string used = NullConvertor.ToString(myOleDbDataReader["year_used"]);

                        ObjectModel.Gbu.OMMainObject obj = ObjectModel.Gbu.OMMainObject.Where(x => x.CadastralNumber == kn).SelectAll().ExecuteFirstOrDefault();
                        if (obj != null)
                        {
                            count++;

                            #region Сохранение данных ГКН
                            //Площадь
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(44, Category, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Назначение сооружения
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(22, ByDoc, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Наименование объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(19, Name, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);

                            //Количество этажей
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(17, floor, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Количество подземных этажей
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(18, unfloor, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Год ввода в эксплуатацию
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(16, used, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Год постройки
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(15, built, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Тип объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(26, "Сооружение", obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Местоположение
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(8, Place, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Адрес
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(600, Adress, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Кадастровый квартал
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(601, CadastralBlock, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Земельный участок
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(602, zu, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            #endregion



                            Items.Add(obj);
                            if (count % 25 == 0)
                            {
                                Console.WriteLine(count);
                            }
                        }
                    }
                }
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskUncomplited_2018_VUON_GKN(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectUnderconstruction o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 5 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;

                while (myOleDbDataReader.Read())
                {
                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    OMInstance inputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);
                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 200000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }

                    if (inputDoc != null)
                    {
                        string kn = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]);
                        string zu = NullConvertor.ToString(myOleDbDataReader["KN_ZU"]);
                        decimal Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]);
                        string CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]);
                        string Name = NullConvertor.ToString(myOleDbDataReader["name_object"]);
                        string Place = NullConvertor.ToString(myOleDbDataReader["place_object"]);
                        string Adress = NullConvertor.ToString(myOleDbDataReader["adress_object"]);
                        string Category = NullConvertor.ToString(myOleDbDataReader["key_parametr"]);
                        string ByDoc = NullConvertor.ToString(myOleDbDataReader["use_object"]);
                        string wall = NullConvertor.ToString(myOleDbDataReader["wall"]);
                        string floor = NullConvertor.ToString(myOleDbDataReader["floors"]);
                        string unfloor = NullConvertor.ToString(myOleDbDataReader["under_floors"]);
                        string built = NullConvertor.ToString(myOleDbDataReader["year_built"]);
                        string used = NullConvertor.ToString(myOleDbDataReader["year_used"]);
                        string proc = NullConvertor.ToString(myOleDbDataReader["procent"]);

                        ObjectModel.Gbu.OMMainObject obj = ObjectModel.Gbu.OMMainObject.Where(x => x.CadastralNumber == kn).SelectAll().ExecuteFirstOrDefault();
                        if (obj != null)
                        {
                            count++;
                            #region Сохранение данных ГКН
                            //Процент готовности
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(46, (proc == string.Empty) ? (decimal?)null : proc.ParseToDecimalNullable(), obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Площадь
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(44, Category, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Наименование объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(19, ByDoc, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Тип объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(26, "ОНС", obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Местоположение
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(8, Place, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Адрес
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(600, Adress, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Кадастровый квартал
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(601, CadastralBlock, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Земельный участок
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(602, zu, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            #endregion

                            if (count % 25 == 0)
                            {
                                Console.WriteLine(count);
                            }
                        }
                    }
                }
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }
        public static void LoadUnitTaskFlat_2018_VUON_GKN(long id_tour)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_OKS_2018"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                myOleDbCommand.CommandText = "select o.*, c.id_inputdoc, c.id_outputdoc, dIn.name_doc as doc_in_name, dIn.date_doc as doc_in_date, dIn.num_doc as doc_in_num, dIn.date_app as doc_in_app, dIn.status_doc as doc_in_status, dOut.name_doc as doc_out_name, dOut.date_doc as doc_out_date, dOut.num_doc as doc_out_num, dOut.date_app as doc_out_app, dOut.status_doc as doc_out_status, dOut.name_out, dOut.num_out, dOut.date_out from tbObjectFlat o, tbCalcObject c left join tbDocObject dIn on dIn.id_doc = c.id_inputdoc and dIn.type_doc = 0 left join tbDocObject dOut on dOut.id_doc = c.id_outputdoc and dOut.type_doc = 1 where o.ID_OBJECT = c.id_object and c.type_object = 3 and o.change_object > 0";

                SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                long count = 0;
                List<ObjectModel.KO.OMUnit> Items = new List<ObjectModel.KO.OMUnit>();

                while (myOleDbDataReader.Read())
                {
                    Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_inputdoc"]);
                    OMInstance inputDoc = null;

                    if (id_inputDoc > 0)
                    {
                        string inRegNumber = NullConvertor.ToString(myOleDbDataReader["doc_in_num"]);
                        DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_date"]);
                        DateTime inApproveDate = NullConvertor.DBToDateTime(myOleDbDataReader["doc_in_app"]);
                        string inDescription = NullConvertor.ToString(myOleDbDataReader["doc_in_name"]);
                        inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 200000000)).SelectAll().ExecuteFirstOrDefault();
                        if (inputDoc == null)
                        {
                            inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.ApproveDate == inApproveDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                        }
                        if (inputDoc == null)
                        {
                            inputDoc = new OMInstance
                            {
                                Id = id_inputDoc + 200000000,
                                RegNumber = inRegNumber,
                                CreateDate = inCreateDate,
                                ApproveDate = inApproveDate,
                                Description = inDescription,
                            };
                            inputDoc.Save();
                        }
                    }
                    if (inputDoc != null)
                    {
                        string kn = NullConvertor.ToString(myOleDbDataReader["KN_OBJECT"]);
                        string zu = NullConvertor.ToString(myOleDbDataReader["KN_ZU"]);
                        string parent = NullConvertor.ToString(myOleDbDataReader["KN_PARENT"]);
                        decimal Square = NullConvertor.DBToDecimal(myOleDbDataReader["SQUARE_OBJECT"]);
                        string CadastralBlock = NullConvertor.ToString(myOleDbDataReader["KN_KK"]);
                        string Name = NullConvertor.ToString(myOleDbDataReader["name_object"]);
                        string Place = NullConvertor.ToString(myOleDbDataReader["place_object"]);
                        string Adress = NullConvertor.ToString(myOleDbDataReader["adress_object"]);
                        string Category = NullConvertor.ToString(myOleDbDataReader["key_parametr"]);
                        string ByDoc = NullConvertor.ToString(myOleDbDataReader["use_object"]);
                        string wall = NullConvertor.ToString(myOleDbDataReader["wall"]);
                        string floor = NullConvertor.ToString(myOleDbDataReader["floors"]);
                        string unfloor = NullConvertor.ToString(myOleDbDataReader["under_floors"]);
                        string built = NullConvertor.ToString(myOleDbDataReader["year_built"]);
                        string used = NullConvertor.ToString(myOleDbDataReader["year_used"]);
                        string proc = NullConvertor.ToString(myOleDbDataReader["procent"]);
                        string level = NullConvertor.ToString(myOleDbDataReader["level_flat"]);

                        ObjectModel.Gbu.OMMainObject obj = ObjectModel.Gbu.OMMainObject.Where(x => x.CadastralNumber == kn).SelectAll().ExecuteFirstOrDefault();
                        if (obj != null)
                        {
                            count++;
                            #region Сохранение данных ГКН
                            //Площадь
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(2, Square, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Назначение помещения
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(23, ByDoc, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Наименование объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(19, Name, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);

                            //Количество этажей
                            if (floor != string.Empty)
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(17, floor, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Количество подземных этажей
                            if (unfloor != string.Empty)
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(18, unfloor, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Год ввода в эксплуатацию
                            if (used != string.Empty)
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(16, used, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Год постройки
                            if (built != string.Empty)
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(15, built, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);

                            //Тип объекта
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(26, "Помещение", obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Материал стен
                            if (wall != string.Empty)
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(21, wall, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Местоположение
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(8, Place, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Адрес
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(600, Adress, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            //Кадастровый квартал
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(601, CadastralBlock, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);


                            //Кадастровый номер здания или сооружения, в котором расположено помещение
                            KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(604, parent, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);

                            if (level != string.Empty)
                            {
                                //Номер этажа
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(24, level, obj.Id, inputDoc.Id, inputDoc.ApproveDate.Value, inputDoc.ApproveDate.Value, Core.SRD.SRDSession.Current.UserID, inputDoc.ApproveDate.Value);
                            }



                            #endregion

                            if (count % 25 == 0)
                            {
                                Console.WriteLine(count);
                            }
                        }
                    }
                }
                Console.WriteLine(count);
                myOleDbDataReader.Close();
                connection.Close();
            }
        }

        public static void DoLoadBd2018Unit_Unit_GBU_TEXT(PropertyTypes types)
        {
            List<ObjectModel.Gbu.OMMainObject> objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == types).SelectAll().Execute();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;
                foreach (ObjectModel.Gbu.OMMainObject obj in objs)
                {

                    myOleDbCommand.CommandText = "select ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorTextValue ft, tbFactor f, tbDocument d " +
                                                 "where o.kn_object = '" + obj.CadastralNumber + "' and o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 1 and ft.id_factor<=594 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };
                                inputDoc.Save();
                            }
                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]);
                            string value = NullConvertor.ToString(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);


                            if (obj != null)
                            {
                                #region Сохранение данных ГКН
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(id_factor, value, obj.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                //Площадь
                                //KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(2, Square, obj.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                #endregion
                            }
                        }
                    }
                    myOleDbDataReader.Close();

                    count++;
                    if (count % 25 == 0) Console.WriteLine(count);
                }
                Console.WriteLine(count);



                connection.Close();
            }
        }
        public static void DoLoadBd_Parcel_GBU_TEXT()
        {
            DoLoadBd2018Unit_Unit_GBU_TEXT(PropertyTypes.Stead);
        }
        public static void DoLoadBd_Build_GBU_TEXT()
        {
            DoLoadBd2018Unit_Unit_GBU_TEXT(PropertyTypes.Building);
        }
        public static void DoLoadBd_Construction_GBU_TEXT()
        {
            DoLoadBd2018Unit_Unit_GBU_TEXT(PropertyTypes.Construction);
        }
        public static void DoLoadBd_Uncomplited_GBU_TEXT()
        {
            DoLoadBd2018Unit_Unit_GBU_TEXT(PropertyTypes.UncompletedBuilding);
        }
        public static void DoLoadBd_Flat_GBU_TEXT()
        {
            DoLoadBd2018Unit_Unit_GBU_TEXT(PropertyTypes.Parking);
            DoLoadBd2018Unit_Unit_GBU_TEXT(PropertyTypes.Pllacement);
        }

        public static void DoLoadBd2018Unit_Unit_GBU_Numeric(PropertyTypes types)
        {
            List<ObjectModel.Gbu.OMMainObject> objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == types).SelectAll().Execute();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;
                foreach (ObjectModel.Gbu.OMMainObject obj in objs)
                {

                    myOleDbCommand.CommandText = "select ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorDoubleValue ft, tbFactor f, tbDocument d " +
                                                 "where o.kn_object = '" + obj.CadastralNumber + "' and o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 3 and ft.id_factor<=594 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };
                                inputDoc.Save();
                            }
                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]);
                            decimal? value = NullConvertor.DBToDecimalNull(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);


                            if (obj != null)
                            {
                                #region Сохранение данных ГКН
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(id_factor, value, obj.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                //Площадь
                                //KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(2, Square, obj.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                #endregion
                            }
                        }
                    }
                    myOleDbDataReader.Close();

                    count++;
                    if (count % 25 == 0) Console.WriteLine(count);
                }
                Console.WriteLine(count);



                connection.Close();
            }
        }
        public static void DoLoadBd_Parcel_GBU_Numeric()
        {
            DoLoadBd2018Unit_Unit_GBU_Numeric(PropertyTypes.Stead);
        }
        public static void DoLoadBd_Build_GBU_Numeric()
        {
            DoLoadBd2018Unit_Unit_GBU_Numeric(PropertyTypes.Building);
        }
        public static void DoLoadBd_Construction_GBU_Numeric()
        {
            DoLoadBd2018Unit_Unit_GBU_Numeric(PropertyTypes.Construction);
        }
        public static void DoLoadBd_Uncomplited_GBU_Numeric()
        {
            DoLoadBd2018Unit_Unit_GBU_Numeric(PropertyTypes.UncompletedBuilding);
        }
        public static void DoLoadBd_Flat_GBU_Numeric()
        {
            DoLoadBd2018Unit_Unit_GBU_Numeric(PropertyTypes.Parking);
            DoLoadBd2018Unit_Unit_GBU_Numeric(PropertyTypes.Pllacement);
        }

        public static void DoLoadBd2018Unit_Unit_GBU_Date(PropertyTypes types)
        {
            List<ObjectModel.Gbu.OMMainObject> objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == types).SelectAll().Execute();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;
                foreach (ObjectModel.Gbu.OMMainObject obj in objs)
                {

                    myOleDbCommand.CommandText = "select ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorDateValue ft, tbFactor f, tbDocument d " +
                                                 "where o.kn_object = '" + obj.CadastralNumber + "' and o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 2 and ft.id_factor<=594 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };
                                inputDoc.Save();
                            }
                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]);
                            DateTime value = NullConvertor.DBToDateTime(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);


                            if (obj != null)
                            {
                                #region Сохранение данных ГКН
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Date(id_factor, value, obj.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                //Площадь
                                //KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(2, Square, obj.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                #endregion
                            }
                        }
                    }
                    myOleDbDataReader.Close();

                    count++;
                    if (count % 25 == 0) Console.WriteLine(count);
                }
                Console.WriteLine(count);



                connection.Close();
            }
        }
        public static void DoLoadBd_Parcel_GBU_Date()
        {
            DoLoadBd2018Unit_Unit_GBU_Date(PropertyTypes.Stead);
        }
        public static void DoLoadBd_Build_GBU_Date()
        {
            DoLoadBd2018Unit_Unit_GBU_Date(PropertyTypes.Building);
        }
        public static void DoLoadBd_Construction_GBU_Date()
        {
            DoLoadBd2018Unit_Unit_GBU_Date(PropertyTypes.Construction);
        }
        public static void DoLoadBd_Uncomplited_GBU_Date()
        {
            DoLoadBd2018Unit_Unit_GBU_Date(PropertyTypes.UncompletedBuilding);
        }
        public static void DoLoadBd_Flat_GBU_Date()
        {
            DoLoadBd2018Unit_Unit_GBU_Date(PropertyTypes.Parking);
            DoLoadBd2018Unit_Unit_GBU_Date(PropertyTypes.Pllacement);
        }


        public static void DoLoadBd2018Unit_Dop_TEXT_B()
        {
            DoLoadBd2018Unit_Dop_TEXT_Obj(PropertyTypes.Building);
        }
        public static void DoLoadBd2018Unit_Dop_TEXT_C()
        {
            DoLoadBd2018Unit_Dop_TEXT_Obj(PropertyTypes.Construction);
        }
        public static void DoLoadBd2018Unit_Dop_TEXT_U()
        {
            DoLoadBd2018Unit_Dop_TEXT_Obj(PropertyTypes.UncompletedBuilding);
        }
        public static void DoLoadBd2018Unit_Dop_TEXT_F()
        {
            DoLoadBd2018Unit_Dop_TEXT_Obj(PropertyTypes.Pllacement);
        }
        public static void DoLoadBd2018Unit_Dop_TEXT_P()
        {
            DoLoadBd2018Unit_Dop_TEXT_Obj(PropertyTypes.Stead);
        }

        public static void DoLoadBd2018Unit_Dop_TEXT_Obj(PropertyTypes type_code)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;
                List<ObjectModel.Gbu.OMMainObject> records = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == type_code).SelectAll().Execute();
                List<OMInstance> docs = new List<OMInstance>();
                foreach (OMMainObject record in records)
                {

                    myOleDbCommand.CommandText = "select o.kn_object, ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorTextValue ft, tbFactor f, tbDocument d " +
                                                 "where o.kn_object='" + record.CadastralNumber + "' and o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 1 and ft.id_factor>594 and ft.id_factor<=716 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        string kn_obj = NullConvertor.ToString(myOleDbDataReader["kn_object"]);
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = docs.Find(x => x.Id == (id_inputDoc + 300000000));
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                                if (inputDoc != null) docs.Add(inputDoc);
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                                if (inputDoc != null)
                                {
                                    inputDoc.Id = (id_inputDoc + 300000000);
                                    docs.Add(inputDoc);
                                }
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };
                                inputDoc.Save();
                                docs.Add(inputDoc);
                            }

                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) + 1000;
                            string value = NullConvertor.ToString(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);

                            {
                                #region Сохранение данных ГКН
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(id_factor, value, record.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                #endregion
                            }
                        }
                    }
                    count++;
                    if (count % 25 == 0) Console.WriteLine(count);
                    myOleDbDataReader.Close();

                }
                Console.WriteLine(count);
                connection.Close();
            }
        }



        public static void DoLoadBd2018Unit_Dop_NUM_B()
        {
            DoLoadBd2018Unit_Dop_NUM_Obj(PropertyTypes.Building);
        }
        public static void DoLoadBd2018Unit_Dop_NUM_C()
        {
            DoLoadBd2018Unit_Dop_NUM_Obj(PropertyTypes.Construction);
        }
        public static void DoLoadBd2018Unit_Dop_NUM_U()
        {
            DoLoadBd2018Unit_Dop_NUM_Obj(PropertyTypes.UncompletedBuilding);
        }
        public static void DoLoadBd2018Unit_Dop_NUM_F()
        {
            DoLoadBd2018Unit_Dop_NUM_Obj(PropertyTypes.Pllacement);
        }
        public static void DoLoadBd2018Unit_Dop_NUM_P()
        {
            DoLoadBd2018Unit_Dop_NUM_Obj(PropertyTypes.Stead);
        }



        public static void DoLoadBd2018Unit_Dop_NUM_Obj(PropertyTypes type_code)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;
                List<ObjectModel.Gbu.OMMainObject> records = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == type_code).SelectAll().Execute();
                List<OMInstance> docs = new List<OMInstance>();
                foreach (OMMainObject record in records)
                {

                    myOleDbCommand.CommandText = "select o.kn_object, ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorDoubleValue ft, tbFactor f, tbDocument d " +
                                                 "where o.kn_object='" + record.CadastralNumber + "' and o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 3 and ft.id_factor>594 and ft.id_factor<=716 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        string kn_obj = NullConvertor.ToString(myOleDbDataReader["kn_object"]);
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = docs.Find(x => x.Id == (id_inputDoc + 300000000));
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                                if (inputDoc != null) docs.Add(inputDoc);
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                                if (inputDoc != null)
                                {
                                    inputDoc.Id = (id_inputDoc + 300000000);
                                    docs.Add(inputDoc);
                                }
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };
                                inputDoc.Save();
                                docs.Add(inputDoc);
                            }

                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) + 1000;
                            decimal? value = NullConvertor.DBToDecimalNull(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);

                            {
                                #region Сохранение данных ГКН
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(id_factor, value, record.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                #endregion
                            }
                        }
                    }
                    count++;
                    if (count % 25 == 0) Console.WriteLine(count);
                    myOleDbDataReader.Close();

                }
                Console.WriteLine(count);
                connection.Close();
            }
        }


        public static void DoLoadBd2018Unit_Dop_Data_B()
        {
            DoLoadBd2018Unit_Dop_Data_Obj(PropertyTypes.Building);
        }
        public static void DoLoadBd2018Unit_Dop_Data_C()
        {
            DoLoadBd2018Unit_Dop_Data_Obj(PropertyTypes.Construction);
        }
        public static void DoLoadBd2018Unit_Dop_Data_U()
        {
            DoLoadBd2018Unit_Dop_Data_Obj(PropertyTypes.UncompletedBuilding);
        }
        public static void DoLoadBd2018Unit_Dop_Data_F()
        {
            DoLoadBd2018Unit_Dop_Data_Obj(PropertyTypes.Pllacement);
        }
        public static void DoLoadBd2018Unit_Dop_Data_P()
        {
            DoLoadBd2018Unit_Dop_Data_Obj(PropertyTypes.Stead);
        }


        public static void DoLoadBd2018Unit_Dop_Data_Obj(PropertyTypes type_code)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;
                List<ObjectModel.Gbu.OMMainObject> records = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == type_code).SelectAll().Execute();
                List<OMInstance> docs = new List<OMInstance>();
                foreach (OMMainObject record in records)
                {

                    myOleDbCommand.CommandText = "select o.kn_object, ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorDateValue ft, tbFactor f, tbDocument d " +
                                                 "where o.kn_object='" + record.CadastralNumber + "' and o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 2 and ft.id_factor>594 and ft.id_factor<=716 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        string kn_obj = NullConvertor.ToString(myOleDbDataReader["kn_object"]);
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = docs.Find(x => x.Id == (id_inputDoc + 300000000));
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                                if (inputDoc != null) docs.Add(inputDoc);
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                                if (inputDoc != null)
                                {
                                    inputDoc.Id = (id_inputDoc + 300000000);
                                    docs.Add(inputDoc);
                                }
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };

                                try
                                {
                                    inputDoc.Save();
                                }
                                catch
                                {
                                    string error = String.Format("Ошибка при записи документа.Id ={0}, RegNumber={1}, CreateDate={2}, ApproveDate={3}, Description={4}", inputDoc.Id, inputDoc.RegNumber, inputDoc.CreateDate.ToString(), inputDoc.ApproveDate.Value.ToString(), inputDoc.Description);
                                    Console.WriteLine(error);
                                    throw new SystemException(error);
                                }

                                docs.Add(inputDoc);
                            }

                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) + 1000;
                            DateTime value = NullConvertor.DBToDateTime(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);

                            {
                                #region Сохранение данных ГКН
                                try
                                {
                                    KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Date(id_factor, value, record.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                }
                                catch
                                {
                                    string error = String.Format(String.Format("Ошибка при записи значения фактора. Id={0}, Value={1}, Object_id={2}, Document_id={3}, sDate={4}, otDate={5}", id_factor.ToString(), value.ToString(), record.Id.ToString(), inputDoc.Id.ToString(), date.ToString(), inputDoc.CreateDate.ToString()));
                                    Console.WriteLine(error);
                                    throw new SystemException(error);
                                }
                                #endregion
                            }
                        }
                    }
                    count++;
                    if (count % 25 == 0) Console.WriteLine(count);
                    myOleDbDataReader.Close();

                }
                Console.WriteLine(count);
                connection.Close();
            }
        }



        public static void DoLoadBd2018Unit_Dop_Data()
        {
            List<ObjectModel.Gbu.OMMainObject> Records = new List<ObjectModel.Gbu.OMMainObject>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;
                {
                    myOleDbCommand.CommandText = "select o.kn_object, ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorDateValue ft, tbFactor f, tbDocument d " +
                                                 "where o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 2 and ft.id_factor>594 and ft.id_factor<=716 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        string kn_obj = NullConvertor.ToString(myOleDbDataReader["kn_object"]);
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };
                                inputDoc.Save();
                            }
                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) + 1000;
                            DateTime value = NullConvertor.DBToDateTime(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);

                            OMMainObject cur = Records.Find(x => x.CadastralNumber == kn_obj);
                            if (cur == null)
                            {
                                cur = OMMainObject.Where(x => x.CadastralNumber == kn_obj).SelectAll().ExecuteFirstOrDefault();
                                if (cur != null)
                                    Records.Add(cur);
                            }
                            if (cur != null)
                            {
                                #region Сохранение данных ГКН
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Date(id_factor, value, cur.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                #endregion
                            }
                        }
                        count++;
                        if (count % 25 == 0) Console.WriteLine(count);
                    }
                    myOleDbDataReader.Close();

                }
                Console.WriteLine(count);
                connection.Close();
            }
        }
        public static void DoLoadBd2018Unit_Dop_Numeric()
        {
            List<ObjectModel.Gbu.OMMainObject> Records = new List<ObjectModel.Gbu.OMMainObject>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;
                {
                    myOleDbCommand.CommandText = "select o.kn_object, ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorDoubleValue ft, tbFactor f, tbDocument d " +
                                                 "where o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 3 and ft.id_factor>594 and ft.id_factor<=716 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        string kn_obj = NullConvertor.ToString(myOleDbDataReader["kn_object"]);
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };
                                inputDoc.Save();
                            }
                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) + 1000;
                            decimal? value = NullConvertor.DBToDecimalNull(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);

                            OMMainObject cur = Records.Find(x => x.CadastralNumber == kn_obj);
                            if (cur == null)
                            {
                                cur = OMMainObject.Where(x => x.CadastralNumber == kn_obj).SelectAll().ExecuteFirstOrDefault();
                                if (cur != null)
                                    Records.Add(cur);
                            }
                            if (cur != null)
                            {
                                #region Сохранение данных ГКН
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_Numeric(id_factor, value, cur.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                #endregion
                            }
                        }
                        count++;
                        if (count % 25 == 0) Console.WriteLine(count);
                    }
                    myOleDbDataReader.Close();

                }
                Console.WriteLine(count);
                connection.Close();
            }
        }
        public static void DoLoadBd2018Unit_Dop_TEXT1()
        {
            List<ObjectModel.Gbu.OMMainObject> Records = new List<ObjectModel.Gbu.OMMainObject>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["SQL_connection_GBU"]))
            {
                connection.Open();

                SqlCommand myOleDbCommand = connection.CreateCommand();
                myOleDbCommand.CommandTimeout = 300;
                myOleDbCommand.CommandType = System.Data.CommandType.Text;

                long count = 0;

                {

                    myOleDbCommand.CommandText = "select o.kn_object, ft.id_factor, ft.id_document, ft.value, ft.date_value, d.num_document, d.date_document, d.name_document from tbObject o, tbFactorTextValue ft, tbFactor f, tbDocument d " +
                                                 "where o.id_object = ft.id_object and f.id_har = ft.id_factor and f.type_har = 1 and ft.id_factor>594 and ft.id_factor<=716 and f.id_source <> 2 and d.id_document = ft.id_document  order by id_factor";

                    SqlDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        string kn_obj = NullConvertor.ToString(myOleDbDataReader["kn_object"]);
                        Int64 id_inputDoc = NullConvertor.DBToInt64(myOleDbDataReader["id_document"]);
                        OMInstance inputDoc = null;
                        if (id_inputDoc > 0)
                        {
                            string inRegNumber = NullConvertor.ToString(myOleDbDataReader["num_document"]);
                            DateTime inCreateDate = NullConvertor.DBToDateTime(myOleDbDataReader["date_document"]);
                            string inDescription = NullConvertor.ToString(myOleDbDataReader["name_document"]);

                            inputDoc = OMInstance.Where(x => x.Id == (id_inputDoc + 300000000)).SelectAll().ExecuteFirstOrDefault();
                            if (inputDoc == null)
                            {
                                inputDoc = OMInstance.Where(x => x.RegNumber == inRegNumber && x.CreateDate == inCreateDate && x.Description == inDescription).SelectAll().ExecuteFirstOrDefault();
                            }
                            if (inputDoc == null)
                            {
                                inputDoc = new OMInstance
                                {
                                    Id = id_inputDoc + 300000000,
                                    RegNumber = inRegNumber,
                                    CreateDate = inCreateDate,
                                    ApproveDate = inCreateDate,
                                    Description = inDescription,
                                };
                                inputDoc.Save();
                            }
                        }

                        if (inputDoc != null)
                        {
                            long id_factor = NullConvertor.DBToInt64(myOleDbDataReader["id_factor"]) + 1000;
                            string value = NullConvertor.ToString(myOleDbDataReader["value"]);
                            DateTime date = NullConvertor.DBToDateTime(myOleDbDataReader["date_value"]);

                            OMMainObject cur = Records.Find(x => x.CadastralNumber == kn_obj);
                            if (cur == null)
                            {
                                cur = OMMainObject.Where(x => x.CadastralNumber == kn_obj).SelectAll().ExecuteFirstOrDefault();
                                if (cur != null)
                                    Records.Add(cur);
                            }
                            if (cur != null)
                            {
                                #region Сохранение данных ГКН
                                KadOzenka.Dal.DataImport.DataImporterGkn.SetAttributeValue_String(id_factor, value, cur.Id, inputDoc.Id, date, inputDoc.CreateDate, Core.SRD.SRDSession.Current.UserID, date);
                                #endregion
                            }
                        }
                        count++;
                        if (count % 25 == 0) Console.WriteLine(count);
                    }
                    myOleDbDataReader.Close();

                }
                Console.WriteLine(count);
                connection.Close();
            }
        }


    }
}
