using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Tasks.Dto;
using KadOzenka.Dal.Tasks.Exceptions;
using KadOzenka.Dal.Tasks.Repositories;
using KadOzenka.Dal.Tasks.Resources;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.Tasks
{
	public class FactorSettingsService : IFactorSettingsService
	{
		private static readonly ILogger Logger = Log.ForContext<FactorSettingsService>();
		public IFactorSettingsRepository FactorSettingsRepository { get; set; }
		public IRegisterCacheWrapper RegisterCacheWrapper { get; set; }



		public FactorSettingsService(IFactorSettingsRepository factorSettingsRepository,
			IRegisterCacheWrapper registerCacheWrapper)
		{
			FactorSettingsRepository = factorSettingsRepository;
			RegisterCacheWrapper = registerCacheWrapper;
		}



		public List<FactorSettingsDto> Get(List<long> tourAttributes)
		{
			if (tourAttributes.IsEmpty())
				return new List<FactorSettingsDto>();

			var factors = OMFactorSettings.Where(x => tourAttributes.Contains((long) x.FactorId) || tourAttributes.Contains((long) x.CorrectFactorId)).SelectAll().Execute();
			
			return factors.Select(x => new FactorSettingsDto
			{
				Id = x.Id,
				FactorName = GetAttributeNameSafety(x.FactorId),
				FactorInheritance = x.Inheritance_Code,
				Source = x.Source,
				CorrectFactorName = GetAttributeNameSafety(x.CorrectFactorId)
			}).ToList();
		}

		public int Add(FactorSettingsDto factor)
		{
			ValidateFactor(factor);

			var newFactor = new OMFactorSettings
			{
				FactorId = factor.FactorId,
				Inheritance_Code = factor.FactorInheritance,
				Source = factor.Source,
				CorrectFactorId = factor.CorrectFactorId
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

		private void ValidateFactor(FactorSettingsDto factor)
		{
			if (factor.FactorId == 0)
				throw new InheritanceEmptyFactorException(Messages.InheritanceFactorIsEmpty);
			if (factor.CorrectFactorId == 0)
				throw new InheritanceEmptyFactorException(Messages.InheritanceCorrectingFactorIsEmpty);

			if (factor.FactorId == factor.CorrectFactorId)
				throw new InheritanceFactorInSettingAreTheSameException();

			var isFactorExists = FactorSettingsRepository.IsFactorExists(factor.FactorId);
			if (isFactorExists)
				throw new InheritanceFactorAlreadyExistsException(RegisterCacheWrapper.GetAttributeData(factor.FactorId).Name);

			var isCorrectFactorExists = FactorSettingsRepository.IsFactorExists(factor.CorrectFactorId);
			if (isCorrectFactorExists)
				throw new InheritanceCorrectingFactorAlreadyExistsException(RegisterCacheWrapper.GetAttributeData(factor.CorrectFactorId).Name);

			//todo both oks\zu
		}

		#endregion
	}
}
