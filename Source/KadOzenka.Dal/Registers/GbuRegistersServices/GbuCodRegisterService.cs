using Core.Register.RegisterEntities;

namespace KadOzenka.Dal.Registers.GbuRegistersServices
{
	public class GbuCodRegisterService : GbuRegisterService
	{
		protected override string RegisterName => "Источник: ГБУ (ЦОД)";

		/// <summary>
		/// Аттрибут "Кадастровый квартал итоговый"
		/// </summary>
		public RegisterAttribute GetCadastralQuarterFinalAttribute()
		{
			return GetRegisterAttributeByName(RegisterId, "Кадастровый квартал итоговый");
		}
	}
}
