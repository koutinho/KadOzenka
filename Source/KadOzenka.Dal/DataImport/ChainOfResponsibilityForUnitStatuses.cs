using ObjectModel.Directory;

namespace KadOzenka.Dal.DataImport
{
	/// <summary>
	///  Интерфейс Обработчика объявляет методы построения цепочки обработчиков и выполнения запроса.
	/// </summary>
	public interface IHandler
	{
		IHandler SetNext(IHandler handler);

		UnitUpdateStatus Handle(UnitChangedProperties properties);
	}

	public abstract class AbstractHandler : IHandler
	{
		private IHandler _nextHandler;

		public IHandler SetNext(IHandler handler)
		{
			_nextHandler = handler;
			return handler;
		}

		public virtual UnitUpdateStatus Handle(UnitChangedProperties properties)
		{
			return _nextHandler?.Handle(properties) ?? UnitUpdateStatus.WithoutChanges;
		}


		#region Support Methods

		protected bool IsGroupChanged(UnitChangedProperties properties)
		{
			return properties.IsNameChanged || properties.IsPurposeOksChanged || properties.IsTypeOfUserByDocumentsChanged;
		}

		protected bool IsEgrnChanged(UnitChangedProperties properties)
		{
			return properties.IsSquareChanged || properties.IsBuildYearChanged || properties.IsCommissioningYearChanged ||
			       properties.IsFloorsCountChanged || properties.IsUndergroundFloorsCountChanged ||
			       properties.IsWallMaterialChanged || properties.IsZuNumberChanged || properties.IsReadinessPercentageChanged ||
			       properties.IsCharacteristicChanged;
		}

		protected bool IsFsChanged(UnitChangedProperties properties)
		{
			return properties.IsAddressChanged || properties.IsCadasrtalQuartalChanged || properties.IsLocationChanged;
		}

		#endregion
	}

	public class GroupChangesHandler : AbstractHandler
	{
		public override UnitUpdateStatus Handle(UnitChangedProperties properties)
		{
			if (IsGroupChanged(properties))
				return UnitUpdateStatus.GroupChange;

			return base.Handle(properties);
		}
	}

	public class EgrnChangesHandler : AbstractHandler
	{
		public override UnitUpdateStatus Handle(UnitChangedProperties properties)
		{
			if (IsEgrnChanged(properties))
				return UnitUpdateStatus.EgrnChanges;

			return base.Handle(properties);
		}
	}

	public class FsChangesHandler : AbstractHandler
	{
		public override UnitUpdateStatus Handle(UnitChangedProperties properties)
		{
			if (IsFsChanged(properties))
				return UnitUpdateStatus.FsChange;

			return base.Handle(properties);
		}
	}

	public class GroupAndEgrnChangesHandler : AbstractHandler
	{
		public override UnitUpdateStatus Handle(UnitChangedProperties properties)
		{
			if (IsGroupChanged(properties) && IsEgrnChanged(properties))
				return UnitUpdateStatus.GroupAndEgrnChange;

			return base.Handle(properties);
		}
	}

	public class GroupAndFsChangesHandler : AbstractHandler
	{
		public override UnitUpdateStatus Handle(UnitChangedProperties properties)
		{
			if (IsGroupChanged(properties) && IsFsChanged(properties))
				return UnitUpdateStatus.GroupAndFsChange;

			return base.Handle(properties);
		}
	}

	public class GroupAndFsAndCharacteristicChangesHandler : AbstractHandler
	{
		public override UnitUpdateStatus Handle(UnitChangedProperties properties)
		{
			if (IsGroupChanged(properties) && IsFsChanged(properties) && IsEgrnChanged(properties))
				return UnitUpdateStatus.GroupAndFsAndEgrnChanges;

			return base.Handle(properties);
		}
	}

	#region Entities

	public class UnitChangedProperties
	{
		public bool IsNameChanged { get; set; }
		public bool IsPurposeOksChanged { get; set; }
		public bool IsTypeOfUserByDocumentsChanged { get; set; }
		public bool IsSquareChanged { get; set; }
		public bool IsBuildYearChanged { get; set; }
		public bool IsCommissioningYearChanged { get; set; }
		public bool IsFloorsCountChanged { get; set; }
		public bool IsUndergroundFloorsCountChanged { get; set; }
		public bool IsWallMaterialChanged { get; set; }
		public bool IsZuNumberChanged { get; set; }
		public bool IsAddressChanged { get; set; }
		public bool IsCadasrtalQuartalChanged { get; set; }
		public bool IsLocationChanged { get; set; }
		public bool IsReadinessPercentageChanged { get; set; }
		public bool IsCharacteristicChanged { get; set; }
	}

	#endregion
}
