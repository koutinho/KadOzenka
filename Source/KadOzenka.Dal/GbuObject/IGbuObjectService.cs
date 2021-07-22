using Core.Register;
using KadOzenka.Web.Models.GbuObject;
using System;
using System.Collections.Generic;
using CommonSdks;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Core.Register;
using KadOzenka.Dal.GbuObject.Entities;
using ObjectModel.Directory;


namespace KadOzenka.Dal.GbuObject
{
	public interface IGbuObjectService
	{
		List<GbuObjectAttribute> GetAllAttributes(long objectId, List<long> sources = null,
			List<long> attributes = null, DateTime? dateS = null, DateTime? dateOt = null,
			List<GbuColumnsToDownload> attributesToDownload = null);

		List<GbuObjectAttribute> GetAllAttributes(List<long> objectIds, List<long> sources = null,
			List<long> inputAttributes = null, DateTime? dateS = null, DateTime? dateOt = null,
			List<GbuColumnsToDownload> attributesToDownload = null);

		List<AllDataTreeDto> GetAllDataTree(long objectId, string parentNodeId, long nodeLevel);

		List<GbuAttributeValueObjectsCountDto> GetAttributeValueKoObjectsCount(long attributeId, KoUnitStatus koUnitStatus, 
			DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo, QueryManager queryManager);

		List<long> GetGbuRegistersIds();

		List<OMAttribute> GetGbuAttributes();

		int AddNewVirtualAttribute(string attributeName, long registerId, RegisterAttributeType type);
	}
}
