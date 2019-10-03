using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;

namespace ObjectModel.Market
{
    /// <summary>
    /// 100 Аналоги
    /// </summary>
    public partial class OMCoreObject
    {

        public OMCoreObject()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

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

namespace ObjectModel.KO
{
    /// <summary>
    /// 200 Объект кадастровой оценки
    /// </summary>
    public partial class OMMainObject
    {


        /// <summary>
        /// Ссылка на (201 Единица кадастровой оценки)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMUnit> Unit { get; set; }
        public OMMainObject()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Unit = new List<ObjectModel.KO.OMUnit>();

        }
        public OMMainObject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 201 Единица кадастровой оценки
    /// </summary>
    public partial class OMUnit
    {

        public OMUnit()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMUnit(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 202 Тур оценки
    /// </summary>
    public partial class OMTour
    {


        /// <summary>
        /// Ссылка на (201 Единица кадастровой оценки)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMUnit> Unit { get; set; }

        /// <summary>
        /// Ссылка на (203 Задание на оценку)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMTask> Task { get; set; }

        /// <summary>
        /// Ссылка на (207 Модель тура)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMTourModel> TourModel { get; set; }
        public OMTour()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Unit = new List<ObjectModel.KO.OMUnit>();

            Task = new List<ObjectModel.KO.OMTask>();

            TourModel = new List<ObjectModel.KO.OMTourModel>();

        }
        public OMTour(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 203 Задание на оценку
    /// </summary>
    public partial class OMTask
    {


        /// <summary>
        /// Ссылка на (201 Единица кадастровой оценки)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMUnit> Unit { get; set; }
        public OMTask()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Unit = new List<ObjectModel.KO.OMUnit>();

        }
        public OMTask(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 204 Документы
    /// </summary>
    public partial class OMDocument
    {


        /// <summary>
        /// Ссылка на (203 Задание на оценку)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMTask> Task { get; set; }
        public OMDocument()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Task = new List<ObjectModel.KO.OMTask>();

        }
        public OMDocument(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 205 Группы/Подгруппы
    /// </summary>
    public partial class OMGroup
    {


        /// <summary>
        /// Ссылка на (201 Единица кадастровой оценки)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMUnit> Unit { get; set; }

        /// <summary>
        /// Ссылка на (206 Модель)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMModel> Model { get; set; }

        /// <summary>
        /// Ссылка на (208 Факторы группы)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMGroupFactor> GroupFactor { get; set; }
        public OMGroup()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Unit = new List<ObjectModel.KO.OMUnit>();

            Model = new List<ObjectModel.KO.OMModel>();

            GroupFactor = new List<ObjectModel.KO.OMGroupFactor>();

        }
        public OMGroup(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 206 Модель
    /// </summary>
    public partial class OMModel
    {


        /// <summary>
        /// Ссылка на (201 Единица кадастровой оценки)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMUnit> Unit { get; set; }

        /// <summary>
        /// Ссылка на (207 Модель тура)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMTourModel> TourModel { get; set; }

        /// <summary>
        /// Ссылка на (210 Факторы модели)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMModelFactor> ModelFactor { get; set; }
        public OMModel()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Unit = new List<ObjectModel.KO.OMUnit>();

            TourModel = new List<ObjectModel.KO.OMTourModel>();

            ModelFactor = new List<ObjectModel.KO.OMModelFactor>();

        }
        public OMModel(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 207 Модель тура
    /// </summary>
    public partial class OMTourModel
    {

        public OMTourModel()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMTourModel(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 208 Факторы группы
    /// </summary>
    public partial class OMGroupFactor
    {

        public OMGroupFactor()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMGroupFactor(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 210 Факторы модели
    /// </summary>
    public partial class OMModelFactor
    {

        public OMModelFactor()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMModelFactor(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 211 Справочник меток
    /// </summary>
    public partial class OMMarkCatalog
    {


        /// <summary>
        /// Ссылка на (210 Факторы модели)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMModelFactor> ModelFactor { get; set; }
        public OMMarkCatalog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            ModelFactor = new List<ObjectModel.KO.OMModelFactor>();

        }
        public OMMarkCatalog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}