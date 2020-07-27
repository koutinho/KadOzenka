using System.ComponentModel.DataAnnotations;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
	public class CodJobViewModel
	{
		public long Id { get; set; }

		[Display(Name="Задание ЦОД")]
		public string Name { get; set; }

		[Display(Name = "Результат")]
		public string Result { get; set; }

		public static CodJobViewModel FromEntity(OMCodJob entity)
		{
			if (entity == null)
			{
				return new CodJobViewModel
				{
					Id = -1
				};
			}

			return new CodJobViewModel
			{
				Id = entity.Id,
				Name = entity.NameJob,
				Result = entity.ResultJob
			};
		}

		public static void ToEntity(CodJobViewModel viewModel, ref OMCodJob codJob)
		{
			codJob.NameJob = viewModel.Name;
			codJob.ResultJob = viewModel.Result;
		}
	}
}
