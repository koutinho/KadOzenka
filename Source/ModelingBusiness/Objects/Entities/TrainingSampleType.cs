using System.ComponentModel;

namespace ModelingBusiness.Objects.Entities
{
	public enum TrainingSampleType
	{
		[Description("Контрольная выборка")]
		Control,
		[Description("Обучающая выборка")]
		Training
	}
}
