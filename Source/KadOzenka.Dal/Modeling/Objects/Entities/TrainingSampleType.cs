using System.ComponentModel;

namespace KadOzenka.Dal.Modeling.Objects.Entities
{
	public enum TrainingSampleType
	{
		[Description("Контрольная выборка")]
		Control,
		[Description("Обучающая выборка")]
		Training
	}
}
