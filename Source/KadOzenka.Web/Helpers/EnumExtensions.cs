using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Helpers
{
    public static class EnumExtensions
    {
        public static List<SelectListItem> GetSelectList(Type enumType, long[] filterValues = null, bool withEmpty = false)
        {
            var values = Enum.GetValues(enumType);
            var selectListItems = new List<SelectListItem>(values.Length);

            foreach (var curValue in Enum.GetValues(enumType))
            {
                var selectItem = new SelectListItem();
                var name = Enum.GetName(enumType, curValue);
                var desc = name;
                var field = enumType.GetField(name);
                var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var result = attributes.Length == 0 ? desc : ((DescriptionAttribute)attributes[0]).Description;
                var value = Convert.ChangeType(curValue, Enum.GetUnderlyingType(enumType)).ToString();
                if (withEmpty && value == "0")
                {
                    selectItem.Text = string.Empty;
                    selectItem.Value = string.Empty;
                }
                else
                {
                    selectItem.Text = result;
                    selectItem.Value = value;
                }

                selectListItems.Add(selectItem);
            }

            if (filterValues != null)
                selectListItems = selectListItems.Where(x => filterValues.Contains(Convert.ToInt64(x.Value))).ToList();

            return selectListItems;
        }
    }
}
