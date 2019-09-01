using System;
using ObjectModel.Insur;

namespace CIPJS.DAL.InsuranceOrganization
{
    public class LinkStrahPostDto
    {
        public long Id { get; set; }
        public DateTime DateStart { get; set; }
        public long LinkCompany { get; set; }
        public string LinkCompanyName { get; set; }
        public bool StatusNew { get; set; }
        public long KodPost { get; set; }

        public static LinkStrahPostDto FromOMObject(OMLinkStrahPost obj) =>
            new LinkStrahPostDto
            {
                Id = obj.Id,
                DateStart = obj.DateStart,
                KodPost = obj.KodPost,
                LinkCompany = obj.LinkCompany,
                LinkCompanyName = obj.ParentInsuranceOrganization.FullName,
                StatusNew = obj.StatusNew
            };

        public OMLinkStrahPost ToOMObject() =>
            new OMLinkStrahPost
            {
                Id = this.Id,
                DateStart = this.DateStart,
                KodPost = this.KodPost,
                LinkCompany = this.LinkCompany,
                StatusNew = this.StatusNew
            };
    }
}
