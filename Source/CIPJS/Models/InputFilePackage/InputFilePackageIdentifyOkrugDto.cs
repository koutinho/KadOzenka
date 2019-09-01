using Core.Shared.Extensions;
using ObjectModel.Insur;
using ObjectModel.Directory;

namespace CIPJS.Models.InputFilePackage
{
    public class InputFilePackageIdentifyOkrugDto
    {
        public long Id { get; set; }

        public long PlatCount { get; set; }

        public static InputFilePackageIdentifyOkrugDto OMMap(long inputFilePackageId)
        {
            return new InputFilePackageIdentifyOkrugDto
            {
                Id = inputFilePackageId,
                PlatCount = OMInputPlat.Where(x => x.ParentInputFile.LinkPackage == inputFilePackageId
                    && (x.StatusIdentif_Code == StatusIdentifikacii.None || x.StatusIdentif_Code == StatusIdentifikacii.NotIdentified))
                    .GetCountQuery()
                    .ExecuteQuery()
                    .Rows[0]["Count"].ParseToLong()
            };
        }
    }
}
