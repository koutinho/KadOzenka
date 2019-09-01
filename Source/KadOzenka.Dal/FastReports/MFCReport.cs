using Core.Shared.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Platform.Reports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CIPJS.DAL.FastReports
{
    public class MFCReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query) { return "MFCReport"; }

        private string ReportNumber { get; set; }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        public override void SetReportNumber(ref string reportNumber, DateTime reportDate, NameValueCollection query)
        {
            string insurComp = GetQueryParam<string>("SK", query);
            DateTime? dateRep = GetQueryParam<DateTime?>("DateRep", query);

            reportNumber = insurComp + "_" + dateRep?.ToString("Y");
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            string insurComp = GetQueryParam<string>("SK", query);
            DateTime? dateRep = GetQueryParam<DateTime?>("DateRep", query);
            DateTime? datePodpisant = GetQueryParam<DateTime?>("DatePodpisant", query);

            string sDatePodpisant = "«____»  " + DateTime.Now.ToString("Y").ToLower() + " г.";
            if (datePodpisant.HasValue)
                sDatePodpisant = string.Format("«{0}» {1} {2} г.", datePodpisant.Value.ToString("dd"), GetMonthPadej(datePodpisant.Value.ToString("MMMM", CultureInfo.CurrentCulture).ToLower()), datePodpisant.Value.ToString("yyyy"));

            string sign = "/                      /";
            string department = string.Empty;
            string podpisant = GetQueryParam<string>("Podpisant", query);
            bool noSign = GetQueryParam<bool>("NoSign", query);
            if (!noSign && podpisant.IsNotEmpty())
            {
                string[] sep = podpisant.Split('-');
                string[] signSplit = sep[0].ToString().Trim().Split(' ');
                sign = "/" + signSplit[1].Trim() + " " + signSplit[0].Trim() + "/";


                if (sep[1].IndexOf("ГБУ") > -1)
                {
                    string[] splitDep = sep[1].Trim().Split("ГБУ");
                    department = "ГБУ " + splitDep[1].Trim().Replace(" и жилищного", Environment.NewLine + "и жилищного") + Environment.NewLine + Environment.NewLine;
                    department += splitDep[0].Trim().Replace("обязанности генерального", "обязанности" + Environment.NewLine + "генерального");
                    //Исполняющий обязанности генерального директора ГБУ «Центр имущественных платежей и жилищного страхования»
                }
                else if (sep[1].IndexOf("Департамента") > -1)
                {
                    string[] splitDep = sep[1].Trim().Split("Департамента");
                    department = "Департамента " + splitDep[1].Trim().Replace(" города", Environment.NewLine + "города") + Environment.NewLine + Environment.NewLine;
                    department += splitDep[0].Trim();
                    //Первый заместитель руководителя Департамента городского имущества города Москвы
                }
                else
                    department = sep[1].Trim();
            }

            DataSet dataSet = new DataSet();
            DataTable dtOplata = new DataTable("INSUR");
            dtOplata.Columns.Add("COL100MINUS");
            dtOplata.Columns.Add("COL100");
            dtOplata.Columns.Add("COL100PLUS");
            dtOplata.Columns.Add("COL0");
            dtOplata.Columns.Add("ALL_COL");
            dtOplata.Columns.Add("ALL_SUM");
            dtOplata.Columns.Add("COL100MINUS2");
            dtOplata.Columns.Add("COL1002");
            dtOplata.Columns.Add("COL100PLUS2");
            dtOplata.Columns.Add("COL02");
            dtOplata.Columns.Add("ALL_COL2");
            dtOplata.Columns.Add("ALL_SUM2");
            dtOplata.Columns.Add("SUM7");
            dtOplata.Columns.Add("IDAO");
            dtOplata.Columns.Add("AO");

            DataTable dtMKD = new DataTable("MKD");
            dtMKD.Columns.Add("ALLMKD");
            dtMKD.Columns.Add("INMKD");
            dtMKD.Columns.Add("OUTMKD");
            dtMKD.Columns.Add("AO");

            DataTable dtFlat = new DataTable("FLAT");
            dtFlat.Columns.Add("ALLFLAT");
            dtFlat.Columns.Add("INFLAT");
            dtFlat.Columns.Add("OUTFLAT");
            dtFlat.Columns.Add("AO");

            DataTable dtCommon = new DataTable("COMMON");
            dtCommon.Columns.Add("DateRep");
            dtCommon.Columns.Add("SysDate");
            dtCommon.Columns.Add("Dep");
            dtCommon.Columns.Add("FIO");
            dtCommon.Columns.Add("DATEPODPISANT");

            dtCommon.Rows.Add(
                dateRep?.ToString("Y").ToUpper() + " года",
                sDatePodpisant,
                department,
                "_____________ " + sign
                );

            string getSql = String.Format(@"select t.* from insur_okrug t, insur_insurance_organization o 
                                                where o.short_name = '{0}' and t.insurance_company_id = o.id
                                                order by t.id", insurComp);
            DbCommand getCommand = DBMngr.Realty.GetSqlStringCommand(getSql);
            DataTable dtSK = DBMngr.Realty.ExecuteDataSet(getCommand, null, false).Tables[0];
            string okrugIds = string.Join(",", dtSK.Rows.OfType<DataRow>().Select(x => string.Join(", ", x.Field<long>("Id").ToString())));
            string orgId = dtSK.Rows[0].Field<long>("insurance_company_id").ToString();

            #region Запрос по Начислениям и оплате для 3 страницы
            getSql = String.Format(@"with --начисление
                                        s as (
                                        select first_value(ir.value) over (
                                         order by ir.date_begin desc
                                            rows between unbounded preceding and unbounded  following
                                        ) tariff
                                        from INSUR_TARIFF ir 
                                        where ir.date_begin <= to_date('{1}','dd.mm.yyyy')
                                        ), s1 as (
                                        select max(tariff) tariff from s
                                        ), s2 as (
                                        select io.id okrug_id,
                                         count(case when iin.sum_nach > 0 and iin.sum_nach < round(iin.opl * s1.tariff,2) then 1 end) as col100minus,
                                         count(case when iin.sum_nach > 0 and iin.sum_nach = round(iin.opl * s1.tariff,2) then 1 end) as col100,
                                         count(case when iin.sum_nach > 0 and iin.sum_nach > round(iin.opl * s1.tariff,2) then 1 end) as col100plus,
                                            count(case when iin.sum_nach = 0 then 1 end) as col0,
                                            coalesce(count(*), 0) as all_col, 
                                         coalesce(sum(iin.sum_nach), 0) as all_sum,
                                            0 as SUM7
                                        from insur_okrug io
                                        join insur_district id on id.okrug_id = io.id
                                        join insur_input_file iif on iif.district_id = id.id and iif.period_reg_date = to_date('{1}','dd.mm.yyyy')
                                        join insur_input_nach iin on iin.link_id_file = iif.emp_id
                                        join s1 on 1=1
                                        where  io.id in ({0})
                                        group by io.id)
                                        , --оплата
                                        sz as (
                                        select io.id okrug_id, iip.kodpl, iip.sum_opl,
                                                                     row_number() over(partition by iip.kodpl order by iip.pmt_date) rn
                                                                    from insur_okrug io
                                                                    join insur_district id on id.okrug_id = io.id
                                                                    join insur_input_file iif on iif.district_id = id.id
                                                                    join insur_input_plat iip on iip.link_id_file = iif.emp_id
                                                                    where iip.sum_opl > 0 and iip.status_identif_code in (10000107, 10000109)
                                                                    and iif.period_reg_date = to_date('{1}','dd.mm.yyyy')
                                                                    and io.id in ({0})
                                                                    ), sn as (
                                                                    select io.id okrug_id, iin.kodpl, iin.sum_nach,
                                                                     row_number() over(partition by iin.kodpl order by iin.emp_id) rn
                                                                    from insur_okrug io
                                                                    join insur_district id on id.okrug_id = io.id
                                                                    join insur_input_file iif on iif.district_id = id.id
                                                                    join insur_input_nach iin on iin.link_id_file = iif.emp_id
                                                                    where iin.sum_nach > 0 
                                                                    and iif.period_reg_date = to_date('{1}','dd.mm.yyyy')
                                                                    and io.id in ({0}))
                                        , s4 as (
                                        select sz.okrug_id,
                                         count(case when sz.sum_opl < coalesce(sn.sum_nach,0) then 1 END) as col100minus,
                                         count(case when sz.sum_opl = coalesce(sn.sum_nach,0) then 1 END) as col100,
                                         count(case when sz.sum_opl > coalesce(sn.sum_nach,0) then 1 END) as col100plus,
                                            0 as col0,
                                            coalesce(count(*), 0) as all_col, 
                                         coalesce(sum(sz.sum_opl), 0) as all_sum,
                                            coalesce(round(sum(sz.sum_opl) * 0.07,2), 0) as SUM7 
                                        from sz 
                                        left join sn on sn.kodpl = sz.kodpl and sn.rn = sz.rn and sn.okrug_id = sz.okrug_id
                                        group by sz.okrug_id)
                                        select i.id, i.name, 
                                        coalesce(s2.col100minus, 0) col100minus, coalesce(s2.col100, 0) col100, coalesce(s2.col100plus, 0) col100plus, 
                                        coalesce(s2.col0, 0) col0, coalesce(s2.all_col, 0) all_col, coalesce(s2.all_sum, 0) all_sum,
                                        coalesce(s4.col100minus, 0) col100minus2, coalesce(s4.col100, 0) col1002, coalesce(s4.col100plus, 0) col100plus2, 
                                        coalesce(s4.col0, 0) col02, coalesce(s4.all_col, 0) all_col2, coalesce(s4.all_sum, 0) all_sum2, coalesce(s2.SUM7 + s4.SUM7, 0) as SUM7
                                        from insur_okrug i
                                                left join s2 on i.id = s2.okrug_id
                                                left join s4 on i.id = s4.okrug_id
                                        where  i.id in ({0}) --and s2.okrug_id = s4.okrug_id
                                        order by i.name",
                                            okrugIds, dateRep?.ToShortDateString() ?? DateTime.MinValue.ToShortDateString());
            #endregion


            //getSql = String.Format(@"select 7 id,  5 as name, 1 col100minus, 2 col100, 3 col100plus, 4 col0, 5 all_col, 6 all_sum, 7 as SUM7,
            //                        8 as col100minus2, 9 as col1002, 10 as col100plus2, 
            //                        11 as col02, 12 as all_col2, 13 as all_sum2");

            getCommand = DBMngr.Realty.GetSqlStringCommand(getSql);
            DataTable dtOpl = DBMngr.Realty.ExecuteDataSet(getCommand, null, false).Tables[0];

            foreach (DataRow row in dtOpl.Rows)
            {
                dtOplata.Rows.Add(
                    row["COL100MINUS"].ParseToDecimal().ToString("n0"),
                    row["COL100"].ParseToDecimal().ToString("n0"),
                    row["COL100PLUS"].ParseToDecimal().ToString("n0"),
                    row["COL0"].ParseToDecimal().ToString("n0"),
                    row["ALL_COL"].ParseToDecimal().ToString("n0"),
                    row["ALL_SUM"].ParseToDecimal().ToString("n2"),
                    row["COL100MINUS2"].ParseToDecimal().ToString("n0"),
                    row["COL1002"].ParseToDecimal().ToString("n0"),
                    row["COL100PLUS2"].ParseToDecimal().ToString("n0"),
                    row["COL02"].ParseToDecimal().ToString("n0"),
                    row["ALL_COL2"].ParseToDecimal().ToString("n0"),
                    row["ALL_SUM2"].ParseToDecimal().ToString("n2"),
                    row["SUM7"].ParseToDecimal().ToString("n2"),
                    row["ID"],
                    row["NAME"].ToString().Length > 0 ? FirstUpper(row["NAME"].ToString()) : ""
                    );
            }

            //статистика по МКД   и статистика по ЖП 
            getSql = String.Format(@"with s2 as (
                                                select coalesce(t1.all_mkd, 0) as all_mkd, coalesce(t1.out_mkd, 0) as out_mkd, coalesce(t1.in_mkd, 0) as in_mkd, 
                                                       coalesce(t.all_flat, 0) as all_flat, coalesce(t.in_flat, 0) as in_flat, coalesce(t.out_flat, 0) as out_flat, 
                                                       t.okrug_id
                                                from v_mfc_otch_flat_stat t, v_mfc_otch_mkd_stat t1  
                                                where t.date_period = to_date('{1}', 'dd.mm.yyyy') and t.okrug_id in 
                                                      (select ii.okrug_id from ref_addr_okrug ii where ii.insurance_company_id in ({0}))
                                                  and t1.date_period = to_date('{1}', 'dd.mm.yyyy') and t1.okrug_id in 
                                                      (select ii.okrug_id from ref_addr_okrug ii where ii.insurance_company_id in ({0}))
                                                  and t.okrug_id = t1.okrug_id )
                                                select i.okrug_id, i.full_name as NAME, 
                                                coalesce(s2.all_mkd, 0) as all_mkd, coalesce(s2.out_mkd, 0) as out_mkd, coalesce(s2.in_mkd, 0) as in_mkd, 
                                                       coalesce(s2.all_flat, 0) as all_flat, coalesce(s2.in_flat, 0) as in_flat, coalesce(s2.out_flat, 0) as out_flat
                                                from ref_addr_okrug i
                                                     left join s2 on i.okrug_id = s2.okrug_id
                                                where i.insurance_company_id in ({0})
                                                order by i.full_name",
                                   orgId, dateRep?.ToShortDateString() ?? DateTime.MinValue.ToShortDateString());

            getCommand = DBMngr.Realty.GetSqlStringCommand(getSql);
            DataTable dtRows = DBMngr.Realty.ExecuteDataSet(getCommand, null, false).Tables[0];

            foreach (DataRow row in dtRows.Rows)
            {
                dtMKD.Rows.Add(
                    row["ALL_MKD"].ParseToDecimal().ToString("n0"),
                    row["IN_MKD"].ParseToDecimal().ToString("n0"),
                    row["OUT_MKD"].ParseToDecimal().ToString("n0"),
                    row["NAME"].ToString().Length > 0 ? FirstUpper(row["NAME"].ToString()) : ""
                    );

                dtFlat.Rows.Add(
                    row["ALL_FLAT"].ParseToDecimal().ToString("n0"),
                    row["IN_FLAT"].ParseToDecimal().ToString("n0"),
                    row["OUT_FLAT"].ParseToDecimal().ToString("n0"),
                    row["NAME"].ToString().Length > 0 ? FirstUpper(row["NAME"].ToString()) : ""
                    );
            }

            if (dtOplata.Rows.Count == 0)
                dtOplata.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (dtMKD.Rows.Count == 0) dtMKD.Rows.Add("", "", "", "");
            if (dtFlat.Rows.Count == 0) dtFlat.Rows.Add("", "", "", "");

            dataSet.Tables.Add(dtOplata);
            dataSet.Tables.Add(dtCommon);
            dataSet.Tables.Add(dtMKD);
            dataSet.Tables.Add(dtFlat);
            return dataSet;
        }

        public static string FirstUpper(string str)
        {
            return str.Substring(0, 1).ToUpper() + (str.Length > 1 ? str.Substring(1).ToLower() : "");
        }

        public static string GetMonthPadej(string month)
        {
            switch (month)
            {
                case "январь": return "января";
                case "февраль": return "февраля";
                case "март": return "марта";
                case "апрель": return "апреля";
                case "май": return "мая";
                case "июнь": return "июня";
                case "июль": return "июля";
                case "август": return "августа";
                case "сентябрь": return "сентября";
                case "октябрь": return "октября";
                case "ноябрь": return "ноября";
                case "декабрь": return "декабря";
                default: return null;
            }
        }
    }
}
