﻿using System;
using System.Text;
using System.Data;
using System.Globalization;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

using Core.Register;
using Core.Numerator;
using Core.Shared.Misc;
using Core.Shared.Extensions;
using Core.Register.RegisterEntities;
using ObjectModel.Gbu.Harmonization;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Простая гармонизация
    /// </summary>
    public class Harmonization
    {
        /// <summary>
        /// Выполнение операции гармонизации
        /// </summary>
        public static void Run(HarmonizationSettings setting)
        {
            //TODO: реализацию надо перенести из старого комплекса
            List<ObjectModel.Gbu.OMMainObject> Objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == setting.PropertyType && x.IsActive == true).SelectAll().Execute();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            Parallel.ForEach(Objs, options, item => { RunOne(item, setting); });
        }

        public static bool GetLevelData(ObjectModel.Gbu.OMMainObject obj, long? idSourceAttrib, long? idResultAttrib, List<GbuObjectAttribute> attribs)
        {
            bool res = false;
            if (idSourceAttrib != null && idResultAttrib != null)
            {
                GbuObjectAttribute attrib = attribs.Find(x => x.AttributeId == idSourceAttrib.Value);
                if (attrib != null)
                {
                    if (attrib.StringValue != string.Empty && attrib.StringValue != null)
                    {
                        res = true;
                        var attributeValue = new GbuObjectAttribute
                        {
                            Id = -1,
                            AttributeId = idResultAttrib.Value,
                            ObjectId = obj.Id,
                            ChangeDocId = attrib.ChangeDocId,
                            S = attrib.S,
                            ChangeUserId = SRDSession.Current.UserID,
                            ChangeDate = DateTime.Now,
                            Ot = attrib.Ot,
                            StringValue = attrib.StringValue,
                        };
                        attributeValue.Save();
                    }
                }
            }
            return res;
        }
        public static void RunOne(ObjectModel.Gbu.OMMainObject obj, HarmonizationSettings setting)
        {
            DateTime dt = (setting.DateActual==null)?DateTime.Now:setting.DateActual.Value;
            List<long> lstIds = new List<long>();
            if (setting.Level1Attribute != null) lstIds.Add(setting.Level1Attribute.Value);
            if (setting.Level2Attribute != null) lstIds.Add(setting.Level2Attribute.Value);
            if (setting.Level3Attribute != null) lstIds.Add(setting.Level3Attribute.Value);
            if (setting.Level4Attribute != null) lstIds.Add(setting.Level4Attribute.Value);
            if (setting.Level5Attribute != null) lstIds.Add(setting.Level5Attribute.Value);
            if (setting.Level6Attribute != null) lstIds.Add(setting.Level6Attribute.Value);
            if (setting.Level7Attribute != null) lstIds.Add(setting.Level7Attribute.Value);
            if (setting.Level8Attribute != null) lstIds.Add(setting.Level8Attribute.Value);
            if (setting.Level9Attribute != null) lstIds.Add(setting.Level9Attribute.Value);
            if (setting.Level10Attribute != null) lstIds.Add(setting.Level10Attribute.Value);

            List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(obj.Id, null, lstIds, dt);

            if (!GetLevelData(obj, setting.Level1Attribute, setting.IdAttributeResult, attribs))
                if (!GetLevelData(obj, setting.Level2Attribute, setting.IdAttributeResult, attribs))
                    if (!GetLevelData(obj, setting.Level3Attribute, setting.IdAttributeResult, attribs))
                        if (!GetLevelData(obj, setting.Level4Attribute, setting.IdAttributeResult, attribs))
                            if (!GetLevelData(obj, setting.Level5Attribute, setting.IdAttributeResult, attribs))
                                if (!GetLevelData(obj, setting.Level6Attribute, setting.IdAttributeResult, attribs))
                                    if (!GetLevelData(obj, setting.Level7Attribute, setting.IdAttributeResult, attribs))
                                        if (!GetLevelData(obj, setting.Level8Attribute, setting.IdAttributeResult, attribs))
                                            if (!GetLevelData(obj, setting.Level9Attribute, setting.IdAttributeResult, attribs))
                                                if (!GetLevelData(obj, setting.Level10Attribute, setting.IdAttributeResult, attribs))
                                                {
                                                    var attributeValue = new GbuObjectAttribute
                                                    {
                                                        Id = -1,
                                                        AttributeId = setting.IdAttributeResult.Value,
                                                        ObjectId = obj.Id,
                                                        ChangeDocId = -1,
                                                        S = dt,
                                                        ChangeUserId = SRDSession.Current.UserID,
                                                        ChangeDate = DateTime.Now,
                                                        Ot = dt,
                                                    };
                                                    attributeValue.Save();

                                                }
        }
    }

}