using ObjectModel.Insur;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;

namespace CIPJS.DAL.InsuranceOrganization
{
    public class InsuranceOrganizationService
    {
        private InsuranceOrganizationDto CreateEmpty()
        {
            return new InsuranceOrganizationDto
            {
                Id = -1
            };
        }

        public InsuranceOrganizationDto Get(long? id)
        {
            if (id.HasValue)
            {
                OMInsuranceOrganization insuranceOrganization = OMInsuranceOrganization.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
                if (insuranceOrganization != null)
                {
                    return new InsuranceOrganizationDto
                    {
                        Id = insuranceOrganization.Id,
                        FullName = insuranceOrganization.FullName,
                        ShortName = insuranceOrganization.ShortName,
                        Code = insuranceOrganization.Code
                    };
                }
            }

            return CreateEmpty();
        }

        public List<InsuranceOrganizationDto> GetByName(string name)
        {
            var query = name.IsNullOrEmpty()
                ? OMInsuranceOrganization.Where(x => true)
                : OMInsuranceOrganization.Where(x => x.ShortName.ToLower().Contains(name.ToLower()));
            List<InsuranceOrganizationDto> result = query
                .SelectAll()
                .OrderBy(x => x.FullName)
                .Execute()
                .Select(x => new InsuranceOrganizationDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    ShortName = x.ShortName,
                    Code = x.Code
                }).ToList();

            return result;
        }

        public int InsertLinkStrahPost(LinkStrahPostDto dto) => dto.ToOMObject().Save();

        public void UpdateLinkStrahPost(LinkStrahPostDto dto) => dto.ToOMObject().Save();

        public void DeleteLinkStrahPost(LinkStrahPostDto dto) => dto.ToOMObject().Destroy();

    }
}
