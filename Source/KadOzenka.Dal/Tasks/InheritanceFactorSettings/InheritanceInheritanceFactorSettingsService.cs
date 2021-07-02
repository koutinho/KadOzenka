using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Dto;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Exceptions;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Repositories;
using KadOzenka.Dal.Tasks.Resources;
using KadOzenka.Dal.Tours;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings
{
	public class InheritanceInheritanceFactorSettingsService : IInheritanceFactorSettingsService
	{
		private static readonly ILogger Logger = Log.ForContext<InheritanceInheritanceFactorSettingsService>();
		public IFactorSettingsRepository FactorSettingsRepository { get; set; }
		public IRegisterCacheWrapper RegisterCacheWrapper { get; set; }
		public ITourFactorService TourFactorService { get; set; }



		public InheritanceInheritanceFactorSettingsService(IFactorSettingsRepository factorSettingsRepository,
			IRegisterCacheWrapper registerCacheWrapper, ITourFactorService tourFactorService)
		{
			FactorSettingsRepository = factorSettingsRepository;
			RegisterCacheWrapper = registerCacheWrapper;
			TourFactorService = tourFactorService;
		}



		public List<InheritanceFactorSettingDto> Get(List<long> tourAttributes)
		{
			if (tourAttributes.IsEmpty())
				return new List<InheritanceFactorSettingDto>();

			var factors = OMFactorSettings.Where(x => tourAttributes.Contains((long) x.FactorId) || tourAttributes.Contains((long) x.CorrectFactorId)).SelectAll().Execute();
			
			return factors.Select(x => new InheritanceFactorSettingDto
			{
				Id = x.Id,
				FactorName = GetAttributeNameSafety(x.FactorId),
				FactorInheritance = x.Inheritance_Code,
				Source = x.Source,
				CorrectFactorName = GetAttributeNameSafety(x.CorrectFactorId)
			}).ToList();
		}

		public OMFactorSettings GetById(long? settingId)
		{
			if (settingId == null)
				throw new Exception("Не передан ИД настройки факторов");

			var setting = FactorSettingsRepository.GetById(settingId.Value, null);
			if (setting == null)
				throw new InheritanceFactorNotFoundException(settingId.Value);

			return setting;
		}


		public int Add(InheritanceFactorSettingDto settingDto)
		{
			ValidateFactor(settingDto);

			var newFactor = new OMFactorSettings
			{
				FactorId = settingDto.FactorId,
				Inheritance_Code = settingDto.FactorInheritance,
				Source = settingDto.Source,
				CorrectFactorId = settingDto.CorrectFactorId
			};

			return FactorSettingsRepository.Save(newFactor);
		}

		public void Update(InheritanceFactorSettingDto settingDto)
		{
			ValidateFactor(settingDto);

			var setting = GetById(settingDto.Id);
			setting.FactorId = settingDto.FactorId;
			setting.Inheritance_Code = settingDto.FactorInheritance;
			setting.Source = settingDto.Source;
			setting.CorrectFactorId = settingDto.CorrectFactorId;

			FactorSettingsRepository.Save(setting);
		}

		public void Delete(long? settingId)
		{
			var setting = GetById(settingId);
			setting.Destroy();
		}


		#region Support Methods

		//В БД некоторые ИД при конвертации были забиты как "-1"
		private string GetAttributeNameSafety(long? attributeId)
		{
			try
			{
				return RegisterCache.GetAttributeData(attributeId.GetValueOrDefault()).Name;
			}
			catch (Exception e)
			{
				Logger.Warning(e, "В кеше не найден атрибут с ИД {AttributeId}", attributeId);
			}

			return string.Empty;
		}

		private void ValidateFactor(InheritanceFactorSettingDto inheritanceFactor)
		{
			if (inheritanceFactor.FactorId == 0)
				throw new InheritanceEmptyFactorException(Messages.InheritanceFactorIsEmpty);
			if (inheritanceFactor.CorrectFactorId == 0)
				throw new InheritanceEmptyFactorException(Messages.InheritanceCorrectingFactorIsEmpty);

			if (inheritanceFactor.FactorId == inheritanceFactor.CorrectFactorId)
				throw new InheritanceFactorInSettingAreTheSameException();

			var isFactorExists = FactorSettingsRepository.IsFactorExists(inheritanceFactor.Id, inheritanceFactor.FactorId);
			if (isFactorExists)
				throw new InheritanceFactorAlreadyExistsException(RegisterCacheWrapper.GetAttributeData(inheritanceFactor.FactorId).Name);

			var isCorrectFactorExists = FactorSettingsRepository.IsFactorExists(inheritanceFactor.Id, inheritanceFactor.CorrectFactorId);
			if (isCorrectFactorExists)
				throw new InheritanceCorrectingFactorAlreadyExistsException(RegisterCacheWrapper.GetAttributeData(inheritanceFactor.CorrectFactorId).Name);

			var tourFactors = TourFactorService.GetAllTourAttributes(inheritanceFactor.TourId);
			var oksIds = tourFactors.Oks.Select(x => x.Id).ToList();
			var zuIds = tourFactors.Zu.Select(x => x.Id).ToList();
			if ((oksIds.Contains(inheritanceFactor.FactorId) && zuIds.Contains(inheritanceFactor.CorrectFactorId)) ||
			    oksIds.Contains(inheritanceFactor.CorrectFactorId) && zuIds.Contains(inheritanceFactor.FactorId))
				throw new InheritanceFactorsTypeMismatchException();
		}

		#endregion
	}
}
