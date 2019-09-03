using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;

namespace ObjectModel.Market
{
    /// <summary>
    /// 100 Объект аналог
    /// </summary>
    public partial class OMCoreObject
    {

        public OMCoreObject()
        {

            Uid = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMCoreObject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}