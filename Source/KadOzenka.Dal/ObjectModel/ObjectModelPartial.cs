using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;

namespace ObjectModel.Market
{
    /// <summary>
    /// 100 Таблица, содержащая объекты аналоги
    /// </summary>
    public partial class OMCoreObject
    {


        /// <summary>
        /// Ссылка на (101 Таблица, содержащая объекты полученные с ЦИАНа)
        /// </summary>
        [Reference]
        public List<ObjectModel.Market.OMCianObject> CianObject { get; set; }

        /// <summary>
        /// Ссылка на (102 Таблица, содержащая объекты полученные с авито)
        /// </summary>
        [Reference]
        public List<ObjectModel.Market.OMAvitoObject> AvitoObject { get; set; }
        public OMCoreObject()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            CianObject = new List<ObjectModel.Market.OMCianObject>();

            AvitoObject = new List<ObjectModel.Market.OMAvitoObject>();

        }
        public OMCoreObject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 101 Таблица, содержащая объекты полученные с ЦИАНа
    /// </summary>
    public partial class OMCianObject
    {

        public OMCianObject()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMCianObject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 102 Таблица, содержащая объекты полученные с авито
    /// </summary>
    public partial class OMAvitoObject
    {

        public OMAvitoObject()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMAvitoObject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 103 Таблица, содержащая настройки модуля
    /// </summary>
    public partial class OMSettings
    {

        public OMSettings()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMSettings(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Cld
{
    /// <summary>
    /// 304 Организации
    /// </summary>
    public partial class OMSubject
    {


        /// <summary>
        /// Ссылка на (941 Подразделение в организации пользователя системы)
        /// </summary>
        [Reference]
        public List<ObjectModel.Core.SRD.OMDepartment> Department { get; set; }
        public OMSubject()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Department = new List<ObjectModel.Core.SRD.OMDepartment>();

        }
        public OMSubject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}