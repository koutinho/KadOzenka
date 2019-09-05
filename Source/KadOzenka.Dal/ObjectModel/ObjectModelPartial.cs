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