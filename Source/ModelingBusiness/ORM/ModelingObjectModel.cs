using System;
using Core.ObjectModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Extensions;
using ObjectModel.Directory;
namespace ObjectModel.KO
{
    /// <summary>
    /// 206 Модель (KO_MODEL)
    /// </summary>
    [RegisterInfo(RegisterID = 206)]
    [Serializable]
    public partial class OMModel : OMBaseClass<OMModel>
    {

        private long _id;
        /// <summary>
        /// 20600100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _groupid;
        /// <summary>
        /// 20600200 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600200)]
        public long? GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private string _name;
        /// <summary>
        /// 20600300 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600300)]
        public string Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private string _description;
        /// <summary>
        /// 20600400 Описание (DESCRIPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600400)]
        public string Description
        {
            get
            {
                CheckPropertyInited("Description");
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }


        private string _formula;
        /// <summary>
        /// 20600500 Формула (FORMULA)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600500)]
        public string Formula
        {
            get
            {
                CheckPropertyInited("Formula");
                return _formula;
            }
            set
            {
                _formula = value;
                NotifyPropertyChanged("Formula");
            }
        }


        private string _algoritmtype;
        /// <summary>
        /// 20600600 Алгоритм расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20600600)]
        public string AlgoritmType
        {
            get
            {
                CheckPropertyInited("AlgoritmType");
                return _algoritmtype;
            }
            set
            {
                _algoritmtype = value;
                NotifyPropertyChanged("AlgoritmType");
            }
        }


        private KoAlgoritmType _algoritmtype_Code;
        /// <summary>
        /// 20600600 Алгоритм расчета (справочный код) (ALGORITM_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600600)]
        public KoAlgoritmType AlgoritmType_Code
        {
            get
            {
                CheckPropertyInited("AlgoritmType_Code");
                return this._algoritmtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_algoritmtype))
                    {
                         _algoritmtype = descr;
                    }
                }
                else
                {
                     _algoritmtype = descr;
                }

                this._algoritmtype_Code = value;
                NotifyPropertyChanged("AlgoritmType");
                NotifyPropertyChanged("AlgoritmType_Code");
            }
        }


        private decimal? _a0;
        /// <summary>
        /// 20600700 Cвободный член в формуле для Линейного алгоритма (A0)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600700)]
        public decimal? A0
        {
            get
            {
                CheckPropertyInited("A0");
                return _a0;
            }
            set
            {
                _a0 = value;
                NotifyPropertyChanged("A0");
            }
        }


        private string _calculationtype;
        /// <summary>
        /// 20600800 Тип расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20600800)]
        public string CalculationType
        {
            get
            {
                CheckPropertyInited("CalculationType");
                return _calculationtype;
            }
            set
            {
                _calculationtype = value;
                NotifyPropertyChanged("CalculationType");
            }
        }


        private KoCalculationType _calculationtype_Code;
        /// <summary>
        /// 20600800 Тип расчета (справочный код) (CALCULATION_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600800)]
        public KoCalculationType CalculationType_Code
        {
            get
            {
                CheckPropertyInited("CalculationType_Code");
                return this._calculationtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_calculationtype))
                    {
                         _calculationtype = descr;
                    }
                }
                else
                {
                     _calculationtype = descr;
                }

                this._calculationtype_Code = value;
                NotifyPropertyChanged("CalculationType");
                NotifyPropertyChanged("CalculationType_Code");
            }
        }


        private string _calculationmethod;
        /// <summary>
        /// 20600900 Метод расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20600900)]
        public string CalculationMethod
        {
            get
            {
                CheckPropertyInited("CalculationMethod");
                return _calculationmethod;
            }
            set
            {
                _calculationmethod = value;
                NotifyPropertyChanged("CalculationMethod");
            }
        }


        private KoCalculationMethod _calculationmethod_Code;
        /// <summary>
        /// 20600900 Метод расчета (справочный код) (CALCULATION_METHOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600900)]
        public KoCalculationMethod CalculationMethod_Code
        {
            get
            {
                CheckPropertyInited("CalculationMethod_Code");
                return this._calculationmethod_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_calculationmethod))
                    {
                         _calculationmethod = descr;
                    }
                }
                else
                {
                     _calculationmethod = descr;
                }

                this._calculationmethod_Code = value;
                NotifyPropertyChanged("CalculationMethod");
                NotifyPropertyChanged("CalculationMethod_Code");
            }
        }


        private string _lineartrainingresult;
        /// <summary>
        /// 20601100 Результат обучения по линейной формуле (LINEAR_TRAINING_RESULT)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601100)]
        public string LinearTrainingResult
        {
            get
            {
                CheckPropertyInited("LinearTrainingResult");
                return _lineartrainingresult;
            }
            set
            {
                _lineartrainingresult = value;
                NotifyPropertyChanged("LinearTrainingResult");
            }
        }


        private string _exponentialtrainingresult;
        /// <summary>
        /// 20601200 Результат обучения по экспоненциальной формуле (EXPONENTIAL_TRAINING_RESULT)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601200)]
        public string ExponentialTrainingResult
        {
            get
            {
                CheckPropertyInited("ExponentialTrainingResult");
                return _exponentialtrainingresult;
            }
            set
            {
                _exponentialtrainingresult = value;
                NotifyPropertyChanged("ExponentialTrainingResult");
            }
        }


        private string _multiplicativetrainingresult;
        /// <summary>
        /// 20601300 Результат обучения по мультипликативной формуле (MULTIPLICATIVE_TRAINING_RESULT)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601300)]
        public string MultiplicativeTrainingResult
        {
            get
            {
                CheckPropertyInited("MultiplicativeTrainingResult");
                return _multiplicativetrainingresult;
            }
            set
            {
                _multiplicativetrainingresult = value;
                NotifyPropertyChanged("MultiplicativeTrainingResult");
            }
        }


        private bool? _isoksobjecttype;
        /// <summary>
        /// 20601400 Тип объекта (IS_OKS_OBJECT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601400)]
        public bool? IsOksObjectType
        {
            get
            {
                CheckPropertyInited("IsOksObjectType");
                return _isoksobjecttype;
            }
            set
            {
                _isoksobjecttype = value;
                NotifyPropertyChanged("IsOksObjectType");
            }
        }


        private string _type;
        /// <summary>
        /// 20601500 Тип модели ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20601500)]
        public string Type
        {
            get
            {
                CheckPropertyInited("Type");
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }


        private KoModelType _type_Code;
        /// <summary>
        /// 20601500 Тип модели (справочный код) (type)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601500)]
        public KoModelType Type_Code
        {
            get
            {
                CheckPropertyInited("Type_Code");
                return this._type_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_type))
                    {
                         _type = descr;
                    }
                }
                else
                {
                     _type = descr;
                }

                this._type_Code = value;
                NotifyPropertyChanged("Type");
                NotifyPropertyChanged("Type_Code");
            }
        }


        private decimal? _a0forexponential;
        /// <summary>
        /// 20601600 Cвободный член в формуле для Экспоненциального алгоритма (A0_exponential)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601600)]
        public decimal? A0ForExponential
        {
            get
            {
                CheckPropertyInited("A0ForExponential");
                return _a0forexponential;
            }
            set
            {
                _a0forexponential = value;
                NotifyPropertyChanged("A0ForExponential");
            }
        }


        private decimal? _a0formultiplicative;
        /// <summary>
        /// 20601700 Cвободный член в формуле для Мультипликативного алгоритма (A0_multiplicative)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601700)]
        public decimal? A0ForMultiplicative
        {
            get
            {
                CheckPropertyInited("A0ForMultiplicative");
                return _a0formultiplicative;
            }
            set
            {
                _a0formultiplicative = value;
                NotifyPropertyChanged("A0ForMultiplicative");
            }
        }


        private string _objectsstatistic;
        /// <summary>
        /// 20602100 Статистика по объектам модели (objects_statistic)
        /// </summary>
        [RegisterAttribute(AttributeID = 20602100)]
        public string ObjectsStatistic
        {
            get
            {
                CheckPropertyInited("ObjectsStatistic");
                return _objectsstatistic;
            }
            set
            {
                _objectsstatistic = value;
                NotifyPropertyChanged("ObjectsStatistic");
            }
        }


        private bool? _isactive;
        /// <summary>
        /// 20602200 Признак выбора модели для расчета (IS_ACTIVE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20602200)]
        public bool? IsActive
        {
            get
            {
                CheckPropertyInited("IsActive");
                return _isactive;
            }
            set
            {
                _isactive = value;
                NotifyPropertyChanged("IsActive");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 210 Факторы модели (KO_MODEL_FACTOR)
    /// </summary>
    [RegisterInfo(RegisterID = 210)]
    [Serializable]
    public partial class OMModelFactor : OMBaseClass<OMModelFactor>
    {

        private long _id;
        /// <summary>
        /// 21000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _modelid;
        /// <summary>
        /// 21000200 Идентификатор модели (MODEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000200)]
        public long? ModelId
        {
            get
            {
                CheckPropertyInited("ModelId");
                return _modelid;
            }
            set
            {
                _modelid = value;
                NotifyPropertyChanged("ModelId");
            }
        }


        private long? _factorid;
        /// <summary>
        /// 21000300 Идентификатор фактора (FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000300)]
        public long? FactorId
        {
            get
            {
                CheckPropertyInited("FactorId");
                return _factorid;
            }
            set
            {
                _factorid = value;
                NotifyPropertyChanged("FactorId");
            }
        }


        private long? _markerid;
        /// <summary>
        /// 21000400 Идентификатор метки (MARKER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000400)]
        public long? MarkerId
        {
            get
            {
                CheckPropertyInited("MarkerId");
                return _markerid;
            }
            set
            {
                _markerid = value;
                NotifyPropertyChanged("MarkerId");
            }
        }


        private decimal _correction;
        /// <summary>
        /// 21000500 Поправка (CORRECTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000500)]
        public decimal Correction
        {
            get
            {
                CheckPropertyInited("Correction");
                return _correction;
            }
            set
            {
                _correction = value;
                NotifyPropertyChanged("Correction");
            }
        }


        private decimal _coefficientforlinear;
        /// <summary>
        /// 21000600 Коэффициент для линейного алгоритма (COEFFICIENT_FOR_LINEAR)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000600)]
        public decimal CoefficientForLinear
        {
            get
            {
                CheckPropertyInited("CoefficientForLinear");
                return _coefficientforlinear;
            }
            set
            {
                _coefficientforlinear = value;
                NotifyPropertyChanged("CoefficientForLinear");
            }
        }


        private bool _signmarket;
        /// <summary>
        /// 21000900 Признак использования метки (SIGN_MARKET)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000900)]
        public bool SignMarket
        {
            get
            {
                CheckPropertyInited("SignMarket");
                return _signmarket;
            }
            set
            {
                _signmarket = value;
                NotifyPropertyChanged("SignMarket");
            }
        }


        private long? _dictionaryid;
        /// <summary>
        /// 21001000 Идентификатор словаря (DICTIONARY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001000)]
        public long? DictionaryId
        {
            get
            {
                CheckPropertyInited("DictionaryId");
                return _dictionaryid;
            }
            set
            {
                _dictionaryid = value;
                NotifyPropertyChanged("DictionaryId");
            }
        }


        private string _algorithmtype;
        /// <summary>
        /// 21001100  Алгоритм рассчёта ()
        /// </summary>
        [RegisterAttribute(AttributeID = 21001100)]
        public string AlgorithmType
        {
            get
            {
                CheckPropertyInited("AlgorithmType");
                return _algorithmtype;
            }
            set
            {
                _algorithmtype = value;
                NotifyPropertyChanged("AlgorithmType");
            }
        }


        private KoAlgoritmType _algorithmtype_Code;
        /// <summary>
        /// 21001100  Алгоритм рассчёта (справочный код) (algorithm_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001100)]
        public KoAlgoritmType AlgorithmType_Code
        {
            get
            {
                CheckPropertyInited("AlgorithmType_Code");
                return this._algorithmtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_algorithmtype))
                    {
                         _algorithmtype = descr;
                    }
                }
                else
                {
                     _algorithmtype = descr;
                }

                this._algorithmtype_Code = value;
                NotifyPropertyChanged("AlgorithmType");
                NotifyPropertyChanged("AlgorithmType_Code");
            }
        }


        private bool? _isactive;
        /// <summary>
        /// 21001300 Использовать в моделировании (is_active)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001300)]
        public bool? IsActive
        {
            get
            {
                CheckPropertyInited("IsActive");
                return _isactive;
            }
            set
            {
                _isactive = value;
                NotifyPropertyChanged("IsActive");
            }
        }


        private string _marktype;
        /// <summary>
        /// 21001500 Тип метки (mark_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001500)]
        public string MarkType
        {
            get
            {
                CheckPropertyInited("MarkType");
                return _marktype;
            }
            set
            {
                _marktype = value;
                NotifyPropertyChanged("MarkType");
            }
        }


        private ObjectModel.Directory.Ko.MarkType _marktype_Code;
        /// <summary>
        /// 21001500 Тип метки (справочный код) (mark_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001500)]
        public ObjectModel.Directory.Ko.MarkType MarkType_Code
        {
            get
            {
                CheckPropertyInited("MarkType_Code");
                return this._marktype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_marktype))
                    {
                         _marktype = descr;
                    }
                }
                else
                {
                     _marktype = descr;
                }

                this._marktype_Code = value;
                NotifyPropertyChanged("MarkType");
                NotifyPropertyChanged("MarkType_Code");
            }
        }


        private decimal? _correctingterm;
        /// <summary>
        /// 21001600 Корректирующее слагаемое (correcting_term)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001600)]
        public decimal? CorrectingTerm
        {
            get
            {
                CheckPropertyInited("CorrectingTerm");
                return _correctingterm;
            }
            set
            {
                _correctingterm = value;
                NotifyPropertyChanged("CorrectingTerm");
            }
        }


        private decimal? _k;
        /// <summary>
        /// 21001700 K (k)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001700)]
        public decimal? K
        {
            get
            {
                CheckPropertyInited("K");
                return _k;
            }
            set
            {
                _k = value;
                NotifyPropertyChanged("K");
            }
        }


        private decimal _coefficientforexponential;
        /// <summary>
        /// 21001800 Коэффициент для экспоненциального алгоритма (COEFFICIENT_FOR_EXPONENTIAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001800)]
        public decimal CoefficientForExponential
        {
            get
            {
                CheckPropertyInited("CoefficientForExponential");
                return _coefficientforexponential;
            }
            set
            {
                _coefficientforexponential = value;
                NotifyPropertyChanged("CoefficientForExponential");
            }
        }


        private decimal _coefficientformultiplicative;
        /// <summary>
        /// 21001900 Коэффициент для мультипликативного алгоритма (COEFFICIENT_FOR_MULTIPLICATIVE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001900)]
        public decimal CoefficientForMultiplicative
        {
            get
            {
                CheckPropertyInited("CoefficientForMultiplicative");
                return _coefficientformultiplicative;
            }
            set
            {
                _coefficientformultiplicative = value;
                NotifyPropertyChanged("CoefficientForMultiplicative");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 223 Картинки с результатами обучения модели (ko_model_training_result_images)
    /// </summary>
    [RegisterInfo(RegisterID = 223)]
    [Serializable]
    public partial class OMModelTrainingResultImages : OMBaseClass<OMModelTrainingResultImages>
    {

        private long _id;
        /// <summary>
        /// 22300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 22300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _modelid;
        /// <summary>
        /// 22300200 ИД модели (model_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 22300200)]
        public long ModelId
        {
            get
            {
                CheckPropertyInited("ModelId");
                return _modelid;
            }
            set
            {
                _modelid = value;
                NotifyPropertyChanged("ModelId");
            }
        }


        private byte[] _scatter;
        /// <summary>
        /// 22300300 Разброс (scatter)
        /// </summary>
        [RegisterAttribute(AttributeID = 22300300)]
        public byte[] Scatter
        {
            get
            {
                CheckPropertyInited("Scatter");
                return _scatter;
            }
            set
            {
                _scatter = value;
                NotifyPropertyChanged("Scatter");
            }
        }


        private string _algorithmtype;
        /// <summary>
        /// 22300400 Алгоритм расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 22300400)]
        public string AlgorithmType
        {
            get
            {
                CheckPropertyInited("AlgorithmType");
                return _algorithmtype;
            }
            set
            {
                _algorithmtype = value;
                NotifyPropertyChanged("AlgorithmType");
            }
        }


        private KoAlgoritmType _algorithmtype_Code;
        /// <summary>
        /// 22300400 Алгоритм расчета (справочный код) (algorithm_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 22300400)]
        public KoAlgoritmType AlgorithmType_Code
        {
            get
            {
                CheckPropertyInited("AlgorithmType_Code");
                return this._algorithmtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_algorithmtype))
                    {
                         _algorithmtype = descr;
                    }
                }
                else
                {
                     _algorithmtype = descr;
                }

                this._algorithmtype_Code = value;
                NotifyPropertyChanged("AlgorithmType");
                NotifyPropertyChanged("AlgorithmType_Code");
            }
        }


        private byte[] _correlation;
        /// <summary>
        /// 22300500 Корреляция (correlation)
        /// </summary>
        [RegisterAttribute(AttributeID = 22300500)]
        public byte[] Correlation
        {
            get
            {
                CheckPropertyInited("Correlation");
                return _correlation;
            }
            set
            {
                _correlation = value;
                NotifyPropertyChanged("Correlation");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 264 Моделирование. Справочники (KO_MODELING_DICTIONARIES)
    /// </summary>
    [RegisterInfo(RegisterID = 264)]
    [Serializable]
    public partial class OMModelingDictionary : OMBaseClass<OMModelingDictionary>
    {

        private long _id;
        /// <summary>
        /// 26400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _name;
        /// <summary>
        /// 26400200 Имя (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 26400200)]
        public string Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private string _type;
        /// <summary>
        /// 26400300 Тип (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26400300)]
        public string Type
        {
            get
            {
                CheckPropertyInited("Type");
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }


        private ObjectModel.Directory.KO.ModelDictionaryType _type_Code;
        /// <summary>
        /// 26400300 Тип (справочный код) (TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26400300)]
        public ObjectModel.Directory.KO.ModelDictionaryType Type_Code
        {
            get
            {
                CheckPropertyInited("Type_Code");
                return this._type_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_type))
                    {
                         _type = descr;
                    }
                }
                else
                {
                     _type = descr;
                }

                this._type_Code = value;
                NotifyPropertyChanged("Type");
                NotifyPropertyChanged("Type_Code");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 265 Моделирование. Значения справочников (KO_MODELING_DICTIONARIES_VALUES)
    /// </summary>
    [RegisterInfo(RegisterID = 265)]
    [Serializable]
    public partial class OMModelingDictionariesValues : OMBaseClass<OMModelingDictionariesValues>
    {

        private long _id;
        /// <summary>
        /// 26500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _dictionaryid;
        /// <summary>
        /// 26500200 ИД справочника (DICTIONARY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26500200)]
        public long DictionaryId
        {
            get
            {
                CheckPropertyInited("DictionaryId");
                return _dictionaryid;
            }
            set
            {
                _dictionaryid = value;
                NotifyPropertyChanged("DictionaryId");
            }
        }


        private string _value;
        /// <summary>
        /// 26500300 Значение (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26500300)]
        public string Value
        {
            get
            {
                CheckPropertyInited("Value");
                return _value;
            }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }


        private decimal _calculationvalue;
        /// <summary>
        /// 26500400 Значение для расчета (CALCULATION_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26500400)]
        public decimal CalculationValue
        {
            get
            {
                CheckPropertyInited("CalculationValue");
                return _calculationvalue;
            }
            set
            {
                _calculationvalue = value;
                NotifyPropertyChanged("CalculationValue");
            }
        }

    }
}

namespace ObjectModel.Modeling
{
    /// <summary>
    /// 702 Связь модели и объектов аналогов (MODELING_MODEL_TO_MARKET_OBJECTS)
    /// </summary>
    [RegisterInfo(RegisterID = 702)]
    [Serializable]
    public partial class OMModelToMarketObjects : OMBaseClass<OMModelToMarketObjects>
    {

        private long _id;
        /// <summary>
        /// 70200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 70200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _marketobjectinfo;
        /// <summary>
        /// 70200200 Описание объекта-аналога (market_object_info)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200200)]
        public string MarketObjectInfo
        {
            get
            {
                CheckPropertyInited("MarketObjectInfo");
                return _marketobjectinfo;
            }
            set
            {
                _marketobjectinfo = value;
                NotifyPropertyChanged("MarketObjectInfo");
            }
        }


        private decimal _price;
        /// <summary>
        /// 70200300 Цена ОА (PRICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200300)]
        public decimal Price
        {
            get
            {
                CheckPropertyInited("Price");
                return _price;
            }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
            }
        }


        private bool? _isexcluded;
        /// <summary>
        /// 70200400 Признак исключения из расчета (IS_EXCLUDED)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200400)]
        public bool? IsExcluded
        {
            get
            {
                CheckPropertyInited("IsExcluded");
                return _isexcluded;
            }
            set
            {
                _isexcluded = value;
                NotifyPropertyChanged("IsExcluded");
            }
        }


        private long _modelid;
        /// <summary>
        /// 70200500 Идентификатор модели (MODEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200500)]
        public long ModelId
        {
            get
            {
                CheckPropertyInited("ModelId");
                return _modelid;
            }
            set
            {
                _modelid = value;
                NotifyPropertyChanged("ModelId");
            }
        }


        private string _coefficients;
        /// <summary>
        /// 70200600 Рассчитанные коэффициенты для объекта (COEFFICIENTS)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200600)]
        public string Coefficients
        {
            get
            {
                CheckPropertyInited("Coefficients");
                return _coefficients;
            }
            set
            {
                _coefficients = value;
                NotifyPropertyChanged("Coefficients");
            }
        }


        private bool? _isfortraining;
        /// <summary>
        /// 70200700 Признак выбора аналога в обучающую модель (IS_FOR_TRAINING)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200700)]
        public bool? IsForTraining
        {
            get
            {
                CheckPropertyInited("IsForTraining");
                return _isfortraining;
            }
            set
            {
                _isfortraining = value;
                NotifyPropertyChanged("IsForTraining");
            }
        }


        private decimal? _pricefrommodel;
        /// <summary>
        /// 70200800 Спрогнозированная цена (PRICE_FROM_MODEL)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200800)]
        public decimal? PriceFromModel
        {
            get
            {
                CheckPropertyInited("PriceFromModel");
                return _pricefrommodel;
            }
            set
            {
                _pricefrommodel = value;
                NotifyPropertyChanged("PriceFromModel");
            }
        }


        private bool? _isforcontrol;
        /// <summary>
        /// 70200900 Признак выбора аналога в контрольную модель (IS_FOR_CONTROL)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200900)]
        public bool? IsForControl
        {
            get
            {
                CheckPropertyInited("IsForControl");
                return _isforcontrol;
            }
            set
            {
                _isforcontrol = value;
                NotifyPropertyChanged("IsForControl");
            }
        }


        private long? _marketobjectid;
        /// <summary>
        /// 70201000 ИД объекта-аналога (market_object_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 70201000)]
        public long? MarketObjectId
        {
            get
            {
                CheckPropertyInited("MarketObjectId");
                return _marketobjectid;
            }
            set
            {
                _marketobjectid = value;
                NotifyPropertyChanged("MarketObjectId");
            }
        }


        private long? _unitid;
        /// <summary>
        /// 70201100 ИД юнита, свзяанного с объектом-аналогом (unit_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 70201100)]
        public long? UnitId
        {
            get
            {
                CheckPropertyInited("UnitId");
                return _unitid;
            }
            set
            {
                _unitid = value;
                NotifyPropertyChanged("UnitId");
            }
        }


        private string _unitpropertytype;
        /// <summary>
        /// 70201200 Тип объекта ЕО (UNIT_PROPERTY_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 70201200)]
        public string UnitPropertyType
        {
            get
            {
                CheckPropertyInited("UnitPropertyType");
                return _unitpropertytype;
            }
            set
            {
                _unitpropertytype = value;
                NotifyPropertyChanged("UnitPropertyType");
            }
        }


        private PropertyTypes _unitpropertytype_Code;
        /// <summary>
        /// 70201200 Тип объекта ЕО (справочный код) (UNIT_PROPERTY_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 70201200)]
        public PropertyTypes UnitPropertyType_Code
        {
            get
            {
                CheckPropertyInited("UnitPropertyType_Code");
                return this._unitpropertytype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_unitpropertytype))
                    {
                         _unitpropertytype = descr;
                    }
                }
                else
                {
                     _unitpropertytype = descr;
                }

                this._unitpropertytype_Code = value;
                NotifyPropertyChanged("UnitPropertyType");
                NotifyPropertyChanged("UnitPropertyType_Code");
            }
        }

    }
}
