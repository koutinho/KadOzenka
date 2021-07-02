using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Tasks.Dto;
using KadOzenka.Dal.Tasks.Exceptions;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Dto;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Repositories;
using KadOzenka.Dal.Tasks.Resources;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings
{
	public class InheritanceInheritanceFactorSettingsService : IInheritanceFactorSettingsService
	{
		private static readonly ILogger Logger = Log.ForContext<InheritanceInheritanceFactorSettingsService>();
		public IFactorSettingsRepository FactorSettingsRepository { get; set; }
		public IRegisterCacheWrapper RegisterCacheWrapper { get; set; }



		public InheritanceInheritanceFactorSettingsService(IFactorSettingsRepository factorSettingsRepository,
			IRegisterCacheWrapper registerCacheWrapper)
		{
			FactorSettingsRepository = factorSettingsRepository;
			RegisterCacheWrapper = registerCacheWrapper;
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

		public int Add(InheritanceFactorSettingDto inheritanceFactor)
		{
			ValidateFactor(inheritanceFactor);

			var newFactor = new OMFactorSettings
			{
				FactorId = inheritanceFactor.FactorId,
				Inheritance_Code = inheritanceFactor.FactorInheritance,
				Source = inheritanceFactor.Source,
				CorrectFactorId = inheritanceFactor.CorrectFactorId
			};

			return FactorSettingsRepository.Save(newFactor);
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

			var isFactorExists = FactorSettingsRepository.IsFactorExists(inheritanceFactor.FactorId);
			if (isFactorExists)
				throw new InheritanceFactorAlreadyExistsException(RegisterCacheWrapper.GetAttributeData(inheritanceFactor.FactorId).Name);

			var isCorrectFactorExists = FactorSettingsRepository.IsFactorExists(inheritanceFactor.CorrectFactorId);
			if (isCorrectFactorExists)
				throw new InheritanceCorrectingFactorAlreadyExistsException(RegisterCacheWrapper.GetAttributeData(inheritanceFactor.CorrectFactorId).Name);

			//todo both oks\zu
		}

		#endregion
	}
}
