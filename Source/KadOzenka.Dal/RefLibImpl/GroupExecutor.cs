using Core.RefLib;
using Core.Register.QuerySubsystem;
using ObjectModel.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Text;

namespace KadOzenka.Dal.RefLibImpl
{
	[Serializable]
	[Export(typeof(IReferenceExecutor))]
	public class GroupExecutor : IReferenceExecutor
	{
		public List<OMReferenceItem> CreateItemList(long referenceId, Func<OMReferenceItem, bool> expression = null)
		{
			throw new NotImplementedException();
		}

		public QSColumn GetValueColumnByItemIdColumn(long referenceId, QSColumn itemIdColumn)
		{
			throw new NotImplementedException();
		}
	}
}
