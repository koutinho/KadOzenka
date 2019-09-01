using ObjectModel.Insur;
using System.Linq;

namespace CIPJS.DAL.DocBaseType
{
    public class DocBaseTypeService
    {
        private DocBaseTypeDto CreateEmpty()
        {
            return new DocBaseTypeDto
            {
                Id = -1
            };
        }

        public DocBaseTypeDto Get(long? id)
        {
            if (id.HasValue)
            {
                OMDocBaseType docBaseType = OMDocBaseType.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
                if (docBaseType != null)
                {
                    return new DocBaseTypeDto
                    {
                        Id = docBaseType.Id,
                        DocumentBase = docBaseType.DocumentBase,
                        Type = docBaseType.Type
                    };
                }
            }

            return CreateEmpty();
        }
    }
}
