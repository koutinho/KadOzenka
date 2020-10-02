using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.ObjectsCharacteristics;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Controllers;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.Gbu.ExportAttribute;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Task
{
	#region Entities

	public class OksAdditionalFilters
	{
		public bool IsBuildings { get; set; }
		public bool IsPlacements { get; set; }
		public bool IsUncompletedBuildings { get; set; }
		public bool IsConstructions { get; set; }

		[Display(Name = "Жилые")]
		public bool IsLivePlacement { get; set; }
		[Display(Name = "Нежилые")]
		public bool IsNotLivePlacement { get; set; }

		public PlacementPurpose PlacementPurpose
		{
			get
			{
				if (IsLivePlacement)
					return PlacementPurpose.Live;
				if(IsNotLivePlacement)
					return PlacementPurpose.NotLive;

				return PlacementPurpose.None;
			}
		}
	}

	#endregion

    public class ExportAttributesModel : IValidatableObject
    {
        [Display(Name = "Задания на оценку")]
        public List<long> TaskFilter { get; set; }
        [Display(Name = "Объект капитального строительства")]
        public bool IsOks { get; set; }
        [Display(Name = "Земельный участок")]
        public bool IsZu { get; set; }
        public ObjectTypeExtended ObjType
        {
            get
            {
                if (IsOks && IsZu)
                    return ObjectTypeExtended.Both;

                return IsOks 
                    ? ObjectTypeExtended.Oks 
                    : ObjectTypeExtended.Zu;
            }
        }

        public OksAdditionalFilters OksAdditionalFilters { get; set; }

        public long RatingTour { get; set; }

		public bool CreateAttributes { get; set; }

        public string MethodToSaveTemplate => CreateAttributes
            ? nameof(TaskController.SaveTemplateForTransferAttributesWithCreate)
            : nameof(TaskController.SaveTemplateForTransferAttributesWithoutCreate);
        public string MethodToGetTemplate => nameof(TaskController.GetTemplateForTransferAttributes);

        [Control("KO", 1)]
        public long? IdAttributeKO1 { get; set; }

        [Control("KO", 2)]
		public long? IdAttributeKO2 { get; set; }

		[Control("KO", 3)]
		public long? IdAttributeKO3 { get; set; }

		[Control("KO", 4)]
		public long? IdAttributeKO4 { get; set; }

		[Control("KO", 5)]
		public long? IdAttributeKO5 { get; set; }

		[Control("KO", 6)]
		public long? IdAttributeKO6 { get; set; }

		[Control("KO", 7)]
		public long? IdAttributeKO7 { get; set; }

		[Control("KO", 8)]
		public long? IdAttributeKO8 { get; set; }

		[Control("KO", 9)]
		public long? IdAttributeKO9 { get; set; }

		[Control("KO", 10)]
		public long? IdAttributeKO10 { get; set; }

		[Control("GBU", 1)]
		public long? IdAttributeGBU1 { get; set; }

		[Control("GBU", 2)]
		public long? IdAttributeGBU2 { get; set; }

		[Control("GBU", 3)]
		public long? IdAttributeGBU3 { get; set; }

		[Control("GBU", 4)]
		public long? IdAttributeGBU4 { get; set; }

		[Control("GBU", 5)]
		public long? IdAttributeGBU5 { get; set; }

		[Control("GBU", 6)]
		public long? IdAttributeGBU6 { get; set; }

		[Control("GBU", 7)]
		public long? IdAttributeGBU7 { get; set; }

		[Control("GBU", 8)]
		public long? IdAttributeGBU8 { get; set; }

		[Control("GBU", 9)]
		public long? IdAttributeGBU9 { get; set; }

		[Control("GBU", 10)]
		public long? IdAttributeGBU10 { get; set; }

		public List<PartialExportAttributesRowModel> ExportAttribute { get; set; }

        private TourFactorService TourFactorService { get; set; }

        public ExportAttributesModel()
        {
            TourFactorService = new TourFactorService();
            IsOks = true;
            OksAdditionalFilters = new OksAdditionalFilters();
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if(RatingTour == 0)
                errors.Add(new ValidationResult("Не выбран тур"));

            if (TaskFilter == null || TaskFilter.Count == 0)
                errors.Add(new ValidationResult("Не выбрано задание на оценку"));

            return errors;
        }

        public GbuExportAttributeSettings ToGbuExportAttributeSettings()
		{
			return new GbuExportAttributeSettings
			{
				TaskFilter = TaskFilter,
				Attributes = GetAttributeItems()
			};
		}

		public GbuExportAttributeSettings ToGbuExportAndCreateAttributeSettings()
		{
			return new GbuExportAttributeSettings
			{
				TaskFilter = TaskFilter,
				Attributes = CreateAttributeItems()
			};
		}

		#region Методы для получения атрибутов

		private List<ExportAttributeItem> GetAttributeItems()
		{
			var res = new List<ExportAttributeItem>();

			foreach (var prop in GetType().GetProperties())
			{
				Control attr = GetAttribute(prop);
				if (attr != null && attr.Name == "KO")
				{
					if (!GetPropertyByNameAttribute("GBU", attr.NumberControl).GetValue(this, null).TryParseToDecimal(out var idGbu))
						continue;

					if(!prop.GetValue(this, null).TryParseToDecimal(out var idKo))
						continue;

					res.Add(new ExportAttributeItem
					{
						IdAttributeKO = (long)idKo,
						IdAttributeGBU = (long)idGbu
					});
                }
            }

			if (ExportAttribute != null && ExportAttribute.Count > 0)
			{
				foreach (var attribute in ExportAttribute)
				{
					if(!attribute.IdAttributeGbu.TryParseToDecimal(out var idGbu)) continue;

					if (!attribute.IdAttributeKO.TryParseToDecimal(out var idKo)) continue;

					res.Add(new ExportAttributeItem
					{
						IdAttributeGBU = (long)idGbu,
						IdAttributeKO = (long)idKo
                    });
				}
			}

			return res;
		}

		private List<ExportAttributeItem> CreateAttributeItems()
		{
			var res = new List<ExportAttributeItem>();

            foreach (var prop in GetType().GetProperties())
			{
				Control attr = GetAttribute(prop);
                if (attr != null && attr.Name == "GBU")
                {
                    if (!GetPropertyByNameAttribute("GBU", attr.NumberControl).GetValue(this, null).TryParseToDecimal(out var idGbu))
                        continue;

                    FillAttributesInfo(idGbu, res);
                }
            }

			if (ExportAttribute != null && ExportAttribute.Count > 0)
			{
				foreach (var attribute in ExportAttribute)
				{
					if (!attribute.IdAttributeGbu.TryParseToDecimal(out var idGbu)) continue;

                    FillAttributesInfo(idGbu, res);
                }
			}

			return res;
		}

        private void FillAttributesInfo(decimal idGbu, List<ExportAttributeItem> res)
        {
            if (ObjType == ObjectTypeExtended.Both)
            {
                var idKoForOks = CreateKoAttribute((long)idGbu, RatingTour, true);
                var idKoForZu = CreateKoAttribute((long)idGbu, RatingTour, false);

                res.Add(new ExportAttributeItem
                {
                    IdAttributeKO = idKoForOks,
                    IdAttributeGBU = (long)idGbu
                });
                res.Add(new ExportAttributeItem
                {
                    IdAttributeKO = idKoForZu,
                    IdAttributeGBU = (long)idGbu
                });
            }
            else
            {
                var idKo = CreateKoAttribute((long)idGbu, RatingTour, ObjType == ObjectTypeExtended.Oks);

                res.Add(new ExportAttributeItem
                {
                    IdAttributeKO = idKo,
                    IdAttributeGBU = (long)idGbu
                });
            }
        }

        private long CreateKoAttribute(long idGbu, long tourId, bool isOks)
        {
            OMTransferAttributes transferAttribute = OMTransferAttributes
                .Where(x => x.TourId == tourId && x.IsOks == isOks && x.GbuId == idGbu)
                .Select(x => x.KoId)
                .ExecuteFirstOrDefault();

            if (transferAttribute != null)
            {
                return transferAttribute.KoId;
            }

            var existedTourFactorRegister = !isOks
                ? OMTourFactorRegister
                    .Where(x => x.TourId == tourId && x.ObjectType_Code == PropertyTypes.Stead)
                    .SelectAll().ExecuteFirstOrDefault()
                : OMTourFactorRegister
                    .Where(x => x.TourId == tourId && x.ObjectType_Code != PropertyTypes.Stead)
                    .SelectAll().ExecuteFirstOrDefault();

            var registerId = existedTourFactorRegister == null
                ? TourFactorService.CreateTourFactorRegister(tourId, !isOks).RegisterId
                : existedTourFactorRegister.RegisterId.GetValueOrDefault();

            OMAttribute attributeGbu = OMAttribute.Where(x => x.Id == idGbu).SelectAll().ExecuteFirstOrDefault();
            CharacteristicDto characteristicDto = new CharacteristicDto
            {
                Name = attributeGbu.Name,
                RegisterId = registerId,
                Type = (Core.Register.RegisterAttributeType)attributeGbu.Type,
                ReferenceId = attributeGbu.ReferenceId
            };

            long idKo = new ObjectsCharacteristicsService().AddCharacteristic(characteristicDto, true);

            //запомнить соответствие
            OMTransferAttributes newTransferAttribute = new OMTransferAttributes
            {
                TourId = tourId,
                IsOks = isOks,
                KoId = idKo,
                GbuId = idGbu
            };
            newTransferAttribute.Save();

            return idKo;
        }

        private static Control GetAttribute(PropertyInfo prop)
		{
			object[] attrs = prop.GetCustomAttributes(true);
			if (attrs.Length > 0)
			{
				return attrs[0] as Control;
			}

			return null;
		}

		private PropertyInfo GetPropertyByNameAttribute(string name, int number)
		{
			PropertyInfo[] props = GetType().GetProperties();
			foreach (var prop in props)
			{
				object[] attrs = prop.GetCustomAttributes(true);
				if (attrs.Length == 0)
				{
					continue;
				}

				if ((attrs[0] as Control)?.Name == name && (attrs[0] as Control)?.NumberControl == number)
				{
					return prop;
				}

			}
			return null;
		}

		#endregion
    }

    public class Control : Attribute
    {
	    public string Name;
	    public int NumberControl;

	    public Control(string name, int number)
	    {
		    Name = name;
		    NumberControl = number;
	    }
    }
}