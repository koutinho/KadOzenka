using CIPJS.DAL.HierarchicalGridAnalysisDamage.Models;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using Core.SRD;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CIPJS.DAL.Helpers;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTable
{
    abstract class BaseSepTable : IModelBuilder<SepTableEl, SRDUser>
    {
        public abstract List<GroupEl<SepTableEl>> Create(SRDUser objs);

        object IModelBuilder.Create(object objs) => Create((SRDUser)objs);

        //protected List<OMInvoice> GetData(SRDUser user)
        //{
        //    var objs = OMInvoice.Where(x => x.UserId == user.ID)
        //       .Select(x => x.ParentDamage.EmpId)
        //       .Select(x => x.ParentDamage.NomDoc)//№ Дела
        //       .Select(x => x.ParentDamage.NomDate)//Дата создания
        //       .Select(x => x.ParentDamage.DateDocLastGBY)//Дата поступления в ГЦИП и ЖС пакета документов по выплатному делу
        //       .Select(x => x.ParentDamage.DateDocAddGBY)//Дата досылки
        //       .Select(x => x.ParentDamage.DamageAmountStatus_Code)//статус
        //       .Select(s => s.ParentDamage.FlagZakluchReissue)//Перевыпуск заключения
        //       .SelectAll()
        //       .Execute();
            
        //    return objs;
        //}

        protected List<SepTableEl> GetData(string DamageFieldUsId, SRDUser user)
        {
            var filter = DamageFieldUsId == null || user == null ? "" : $"d.{DamageFieldUsId} = {user.ID} and";

            var commandText =
                $@"
                select
                d.flag_zakluch_reissue as FlagZakluchReissue,
                i.status_code as InvoiceStatus,
                d.damage_amount_status_code as DamageStatus,
                i.emp_id as InvoiceId,
                d.emp_id as DamageId,
                i.num_invoice as NumConclusions,
                i.date_zackluchenia as DateIssue,
                i.subject_name as NamePolicyholder,
                d.nom_doc as NumCases,
                d.nom_date as DateCreate,
                d.date_doc_last_gby as DateReceiptGcipAndJc,
                d.date_doc_add_gby as DateShipment,
                c.comment as Comment,
                o.full_name as Performer
                from insur_invoice i
                join insur_damage d on i.link_damage = d.emp_id
                left join insur_comment c on d.emp_id = c.link_object_id and c.link_reestr_id = 313
                left join insur_insurance_organization o on d.insur_org_id = o.id
                where {filter} (c.link_object_id is null or c.date_create = 
                (select
                max(c1.date_create)
                from insur_comment c1 
                where c1.link_reestr_id = 313 and c1.link_object_id = c.link_object_id))";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            var result = DBMngr.Realty.ExecuteDataSet(command).Tables[0];
            var objs = result.GetModelsByDataReader<SepTableEl>();

            return objs;
        }

        protected GroupEl<SepTableEl> GetPerevipuskZakluch(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Перевыпуск заключения";
            var filtered = objs.Where(x => x.FlagZakluchReissue != null && x.FlagZakluchReissue.Value).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }

        protected GroupEl<SepTableEl> GetPeredanoNaViplatu(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Передано на проверку \"На Выплату\"";
            var filtered = objs.Where(x => x.DamageStatus == (long)StatusDamageAmount.SendToCheck
            && x.InvoiceStatus == (long)InvoiceStatus.TransferredPayment).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }

        protected GroupEl<SepTableEl> GetPeredanoNaProvOtkaz(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Передано на проверку \"Отказ в выплате\"";
            var filtered = objs.Where(x => x.DamageStatus == (long)StatusDamageAmount.SendToCheck
            && (x.InvoiceStatus == (long)InvoiceStatus.Denied || x.InvoiceStatus == (long)InvoiceStatus.DeniedAgreed)).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }

        protected GroupEl<SepTableEl> GetProverenoNaViplatu(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Проверено \"На Выплату\"";
            var filtered = objs.Where(x => x.DamageStatus == (long)StatusDamageAmount.Checked
            && x.InvoiceStatus == (long)InvoiceStatus.TransferredPayment).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }

        protected GroupEl<SepTableEl> GetProverenoOtkaz(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Проверено \"Отказ в Выплате\"";
            var filtered = objs.Where(x => x.DamageStatus == (long)StatusDamageAmount.Checked
            && (x.InvoiceStatus == (long)InvoiceStatus.Denied || x.InvoiceStatus == (long)InvoiceStatus.DeniedAgreed)).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }
    }
}
