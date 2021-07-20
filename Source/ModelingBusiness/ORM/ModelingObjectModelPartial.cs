using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;

namespace ObjectModel.KO
{
    /// <summary>
    /// 206 Модель
    /// </summary>
    public partial class OMModel
    {


        /// <summary>
        /// Ссылка на (210 Факторы модели)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMModelFactor> ModelFactor { get; set; }

        /// <summary>
        /// Ссылка на (223 Картинки с результатами обучения модели)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMModelTrainingResultImages> ModelTrainingResultImages { get; set; }
        public OMModel()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            ModelFactor = new List<ObjectModel.KO.OMModelFactor>();

            ModelTrainingResultImages = new List<ObjectModel.KO.OMModelTrainingResultImages>();

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
    /// 223 Картинки с результатами обучения модели
    /// </summary>
    public partial class OMModelTrainingResultImages
    {

        public OMModelTrainingResultImages()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMModelTrainingResultImages(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 264 Моделирование. Справочники
    /// </summary>
    public partial class OMModelingDictionary
    {


        /// <summary>
        /// Ссылка на (210 Факторы модели)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMModelFactor> ModelFactor { get; set; }

        /// <summary>
        /// Ссылка на (265 Моделирование. Значения справочников)
        /// </summary>
        [Reference]
        public List<ObjectModel.KO.OMModelingDictionariesValues> ModelingDictionariesValues { get; set; }
        public OMModelingDictionary()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            ModelFactor = new List<ObjectModel.KO.OMModelFactor>();

            ModelingDictionariesValues = new List<ObjectModel.KO.OMModelingDictionariesValues>();

        }
        public OMModelingDictionary(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 265 Моделирование. Значения справочников
    /// </summary>
    public partial class OMModelingDictionariesValues
    {

        public OMModelingDictionariesValues()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMModelingDictionariesValues(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Modeling
{
    /// <summary>
    /// 702 Связь модели и объектов аналогов
    /// </summary>
    public partial class OMModelToMarketObjects
    {

        public OMModelToMarketObjects()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMModelToMarketObjects(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}