using CIPJS.DAL.DamageAnalysis;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Reports;
using Core.UI.Registers.Reports.Model;
using ObjectModel.Bti;
using ObjectModel.Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Insur;
using Platform.Reports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;

namespace CIPJS.DAL.FastReports
{
    public class CCCReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query) { { return "CCCReport"; } }

        private bool bInit;

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        //override -> new  сделано ради компиляции
        public new void InitializeFilterValues(long objId, string senderName, bool initialisation)
        {
            if (initialisation)
            {
                string docNumber = "";
                string numDela = string.Empty;
                int i = 0;
                bInit = true;

                OMInvoice oMInvoice = OMInvoice.Where(x => x.EmpId == objId && x.Status_Code != InvoiceStatus.ErrorInDetails).SelectAll().Execute().FirstOrDefault();

                OMDamage rowOMDamage = oMInvoice != null ? OMDamage.Where(x => x.EmpId == oMInvoice.LinkDamage).SelectAll().Execute().FirstOrDefault() : null;
                docNumber = rowOMDamage != null ? rowOMDamage.NomDoc : null;

                if (rowOMDamage != null)
                {
                    List<OMInvoice> oMInvoices = OMInvoice.Where(x => x.LinkDamage == rowOMDamage.EmpId && x.Status_Code != InvoiceStatus.ErrorInDetails).SelectAll().Execute();

                    List<OMInvoice> oMInvoiceWithoutDenied = oMInvoices.Where(x => x.Status_Code == InvoiceStatus.Denied || x.Status_Code == InvoiceStatus.DeniedAgreed).OrderBy(x => x.EmpId).ToList();
                    List<OMInvoice> oMInvoiceFull = oMInvoices.Where(x => x.Status_Code != InvoiceStatus.DeniedAgreed && x.Status_Code != InvoiceStatus.Denied).OrderBy(x => x.EmpId).ToList();
                    oMInvoiceFull.AddRange(oMInvoiceWithoutDenied);

                    foreach (var oMInvoiceRow in oMInvoiceFull)
                    {
                        if (oMInvoices.Count > 1)
                            i += 1;
                        if (oMInvoiceRow.Sort == null || oMInvoiceRow.dateInvoice == null)
                           GenerateNumDateZacluch(oMInvoiceRow, docNumber, i);
                    }
                }
            }
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            DataSet dataSet = new DataSet();

            #region variables
            OMInvoice oMInvoice = new OMInvoice();

            string mainAgreementFio = string.Empty,
                   dateFillMain = string.Empty,
                   docNumber = string.Empty,
                   dateInput = string.Empty,
                   insurOrg = string.Empty,
                   damageInput = string.Empty,
                   insurNom = string.Empty,
                   ndog = string.Empty,
                   ndogdat = string.Empty,
                   insurSumText = string.Empty,
                   insurWorker = string.Empty,
                   estimatedValueText = string.Empty,
                   sumDamageText = string.Empty,
                   agreementDepFio = string.Empty,
                   agreementDepFio2 = string.Empty,
                   agreementFio2 = "/                                  /",
                   paymentInsurText = string.Empty,
                   reject = string.Empty,
                   rejectNextRow = string.Empty,
                   signatureFio = "/                                  /",
                   signatureDep = string.Empty,
                   org = string.Empty,
                   doplataText = string.Empty,
                   subsidyText = string.Empty,
                   mainAgreementId = string.Empty,
                   doplata = string.Empty,
                   sumDamage = string.Empty,
                   insurSum = string.Empty,
                   paymentInsur = string.Empty,
                   estimatedValue = string.Empty,
                   subsidy = string.Empty,
                   partTown = string.Empty,
                   address = string.Empty,
                   bank1 = string.Empty,
                   bank2 = string.Empty,
                   bank3 = string.Empty,
                   podtext = "административный округ, муниципальный район, адрес,";

            DateTime? dateFill1 = null;

            decimal doplataDec = 0,
                    insurSumCalc = 0;

            DataTable dtEmpty = new DataTable("EMPTY");
            dtEmpty.Columns.Add("EMPTYTITLE");
            dtEmpty.Columns.Add("EMPTYTEXT");

            DataTable dataTable = new DataTable("COMMON");
            dataTable.Columns.Add("MainAgreementFIO");
            dataTable.Columns.Add("Year");
            dataTable.Columns.Add("DocNumber");
            dataTable.Columns.Add("DateInput");
            dataTable.Columns.Add("InsurOrg");
            dataTable.Columns.Add("DamageInput");
            dataTable.Columns.Add("InsurNom");
            dataTable.Columns.Add("Ndog");
            dataTable.Columns.Add("Ndogdat");
            dataTable.Columns.Add("InsurWorker");
            dataTable.Columns.Add("Org");
            dataTable.Columns.Add("Subsidy");
            dataTable.Columns.Add("SubsidyText");
            dataTable.Columns.Add("PODPISANTHEAD");
            dataTable.Columns.Add("DEPTHEAD");

            DataTable dtDamageSum = new DataTable("DAMAGESUM");
            dtDamageSum.Columns.Add("SumDamage");

            DataTable dtDamageDopSum = new DataTable("DAMAGEDOPSUM");
            dtDamageDopSum.Columns.Add("SumDamage");

            DataTable dtDamage = new DataTable("DAMAGE");
            dtDamage.Columns.Add("SumDamageText");
            dtDamage.Columns.Add("Doplata");
            dtDamage.Columns.Add("DoplataText");
            dtDamage.Columns.Add("AgreementDepFio");
            dtDamage.Columns.Add("AgreementDepFio2");
            dtDamage.Columns.Add("DateFill1");
            dtDamage.Columns.Add("AgreementFio2");
            dtDamage.Columns.Add("PartTown");

            DataTable dtTitle = new DataTable("TITLE");
            dtTitle.Columns.Add("Title");

            DataTable dtRejectOwn = new DataTable("REJECTOWN");
            dtRejectOwn.Columns.Add("PaymentInsur");
            dtRejectOwn.Columns.Add("PaymentInsurText");
            dtRejectOwn.Columns.Add("Reject");

            DataTable dtInsur = new DataTable("INSUR");
            dtInsur.Columns.Add("Bank1");
            dtInsur.Columns.Add("Bank2");
            dtInsur.Columns.Add("Bank3");
            dtInsur.Columns.Add("EstimatedValue");
            dtInsur.Columns.Add("EstimatedValueText");
            dtInsur.Columns.Add("InsurSum");
            dtInsur.Columns.Add("InsurSumText");

            DataTable dtSign = new DataTable("SIGN");
            dtSign.Columns.Add("SignatureFio");
            dtSign.Columns.Add("SignatureDep");

            DataTable dtReject = new DataTable("REJECT");
            dtReject.Columns.Add("RejectNextRow");
            #endregion

            try
            {
                DateTime? dateSost = GetQueryParam<DateTime?>("DateSost", query);
                DateTime? dateRaschProizv = GetQueryParam<DateTime?>("dateFill", query);
                int? year = GetQueryParam<int?>("Year", query);


                string signHead = "";
                string departmentHead = string.Empty;
                string podpisant = GetQueryParam<string>("PodpisantHead", query);
                bool noSign = GetQueryParam<bool>("NoSignHead", query);
                if (!noSign && podpisant.IsNotEmpty())
                {
                    string[] sep = podpisant.Split('-');
                    signHead = sep[0].ToString().Trim();
                    departmentHead = sep[1].ToString().Trim()
                        .Replace(" ГБУ", Environment.NewLine + "ГБУ")
                        .Replace(" и жилищного", Environment.NewLine + "и жилищного");
                }

                //Расчет согласовал -это выводить в подписанта(в самом конце отчета - последний на второй странице)
                string podpisantRep = GetQueryParam<string>("Podpisant", query);
                bool noSignRep = GetQueryParam<bool>("NoSign", query);
                if (!noSignRep && podpisantRep.IsNotEmpty())
                {
                    string[] sep = podpisantRep.Split('-');
                    signatureFio = "/" + sep[0].ToString().Trim() + "/";
                    signatureDep = sep[1].ToString().Trim().Replace(" ГБУ", Environment.NewLine + "ГБУ").Replace(" и жилищного", Environment.NewLine + "и жилищного");
                }

                string Proveryushi = GetQueryParam<string>("Proveryushi", query);
                bool NoSignProv = GetQueryParam<bool>("NoSignProv", query);
                if (!NoSignProv && Proveryushi.IsNotEmpty())
                {
                    string[] sep = Proveryushi.Split('-');
                    agreementFio2 = "/" + sep[0].ToString().Trim() + "/";
                }

                oMInvoice = OMInvoice.Where(x => x.EmpId == ObjectId).SelectAll().Select(x => x.ParentGbuNoPayReason.Reason).Select(x => x.ParentGbuNoPayReason.ShortExplanation).Execute().FirstOrDefault();
                if (oMInvoice != null && oMInvoice.Status_Code != InvoiceStatus.ErrorInDetails)
                {
                    reject = "";
                    rejectNextRow = "";

                    //Раздел - Заключение о выплате из бюджета города Москвы
                    decimal? paymInsur = (oMInvoice.Status_Code == InvoiceStatus.Denied || oMInvoice.Status_Code == InvoiceStatus.DeniedAgreed) ? 0 : oMInvoice.SumOpl;
                    paymentInsur = parseCostToString(paymInsur);
                    paymentInsurText = AmountInWords.RusCurrency.Str((double)Math.Round(paymInsur ?? 0, 2), "RURSHORT");

                    if (oMInvoice.ParentGbuNoPayReason != null && (oMInvoice.Status_Code == InvoiceStatus.Denied || oMInvoice.Status_Code == InvoiceStatus.DeniedAgreed))
                    {
                        string value = oMInvoice.ParentGbuNoPayReason.ShortExplanation + " (" + oMInvoice.ParentGbuNoPayReason.Reason + ")";
                        Regex regular = new Regex(".{0,50}(?=\\s|$)", RegexOptions.Singleline);
                        MatchCollection matches = regular.Matches(value);
                        if (matches.Count > 1)
                        {
                            reject = matches[0].Value;
                            rejectNextRow = value.Substring(reject.Length).Trim();
                            regular = new Regex(".{0,78}(?=\\s|$)", RegexOptions.Singleline);
                            matches = regular.Matches(rejectNextRow);
                            for (int i = 0; i < matches.Count - 1; i++)
                            {
                                dtReject.Rows.Add(matches[i].ToString());
                            }
                        }
                        else
                        {
                            reject = matches[0].Value;
                        }
                    }

                    insurWorker = oMInvoice.SubjectName;
                    bank1 = (oMInvoice.InnBank != null ? ("ИНН " + oMInvoice.InnBank + ",") : "") + (oMInvoice.KppBank != null ? (" КПП " + oMInvoice.KppBank + ",") : "") + (oMInvoice.BicBank != null ? (" БИК " + oMInvoice.BicBank + ",") : "") + (oMInvoice.KorAcc != null ? (" к /с " + oMInvoice.KorAcc) : "");
                    bank2 = oMInvoice.BankName != null ? ("в " + oMInvoice.BankName) : "";
                    bank3 = (oMInvoice.RachAcc != null ? ("р/с " + oMInvoice.RachAcc + ",") : "") + (oMInvoice.NumCard != null ? " банковская карта № " + oMInvoice.NumCard : "");
                    ndogdat = oMInvoice.SvdPolyceDate.HasValue ? oMInvoice.SvdPolyceDate.Value.ToShortDateString() + " г." : null;
                    ndog = oMInvoice.SvidPolycNum;

                    OMDamage rowOMDamage = OMDamage.Where(x => x.EmpId == oMInvoice.LinkDamage).SelectAll().Execute().FirstOrDefault();
                    if (rowOMDamage != null)
                    {
                        //Если номер Заключения не успел сохраниться, то сгенерим его в отчете    
                        List<OMInvoice> oMInvoices = OMInvoice.Where(x => x.LinkDamage == rowOMDamage.EmpId && x.Status_Code != InvoiceStatus.ErrorInDetails).SelectAll().Execute();
                        if (oMInvoice.Sort == null)
                        {
                            docNumber = oMInvoice.NumInvoice;
                            if (docNumber == null)
                            {
                                int i = 0;
                                string nomDoc = rowOMDamage.NomDoc;
                                List<OMInvoice> oMInvoiceWithoutDenied = oMInvoices.Where(x => x.Status_Code == InvoiceStatus.Denied || x.Status_Code == InvoiceStatus.DeniedAgreed).OrderBy(x => x.EmpId).ToList();
                                List<OMInvoice> oMInvoiceFull = oMInvoices.Where(x => x.Status_Code != InvoiceStatus.DeniedAgreed && x.Status_Code != InvoiceStatus.Denied).OrderBy(x => x.EmpId).ToList();
                                oMInvoiceFull.AddRange(oMInvoiceWithoutDenied);

                                foreach (var oMInvoiceRow in oMInvoiceFull)
                                {
                                    if (oMInvoices.Count > 1)
                                        i += 1;
                                    docNumber = nomDoc + (i != 0 ? " (" + i + ")" : "");

                                    if (oMInvoice.EmpId == oMInvoiceRow.EmpId) break;
                                }
                            }
                        }
                        else
                        {
                            docNumber = rowOMDamage.NomDoc + ((oMInvoice.Sort == null || oMInvoice.Sort == 0 || oMInvoices.Count == 1) ? "" : (" (" + oMInvoice.Sort + ")"));
                        }

                        //Шапка, Раздел после шапки - Реквизиты
                        dateInput = oMInvoice.DataZakluchenia.HasValue ? oMInvoice.DataZakluchenia.Value.ToShortDateString() :( dateSost.HasValue && !bInit
                            ? dateSost.Value.ToShortDateString()
                            : rowOMDamage.NomDate.HasValue ? rowOMDamage.NomDate.Value.ToShortDateString() : string.Empty);

                        if (rowOMDamage.InsurOrgId.HasValue)
                        {
                            OMInsuranceOrganization oMInsuranceOrganization = OMInsuranceOrganization.Where(x => x.Id == rowOMDamage.InsurOrgId.Value).SelectAll().Execute().FirstOrDefault();
                            insurOrg = oMInsuranceOrganization?.FullName;
                        }

                        damageInput = rowOMDamage.InsurDate.HasValue ? rowOMDamage.InsurDate.Value.ToShortDateString() : string.Empty;
                        insurNom = rowOMDamage.InsurNom;

                        estimatedValue = parseCostToString(oMInvoice.InsurSymmaOtchet ?? rowOMDamage.EstimatedValue);
                        estimatedValueText = AmountInWords.RusCurrency.Str((double)Math.Round(oMInvoice.InsurSymmaOtchet ?? rowOMDamage.EstimatedValue ?? 0, 2), "RURSHORT");

                        sumDamage = parseCostToString(rowOMDamage.SumDamage);
                        if (rowOMDamage.NomDoc.Length > 5 && rowOMDamage.NomDoc.Substring(rowOMDamage.NomDoc.Length-5, 5).IndexOf('-') > -1)
                            dtDamageDopSum.Rows.Add(sumDamage);
                        else
                            dtDamageSum.Rows.Add(sumDamage);                        
                        sumDamageText = AmountInWords.RusCurrency.Str((double)Math.Round(rowOMDamage.SumDamage ?? 0, 2), "RURSHORT");

                        partTown = rowOMDamage.PartTown != null ? rowOMDamage.PartTown.Value.ToString("n0", CultureRu) : null;
                        doplataDec = (rowOMDamage.SumDamage ?? 0) * (rowOMDamage.PartTown ?? 0) / 100;
                        doplata = parseCostToString(doplataDec);
                        doplataText = AmountInWords.RusCurrency.Str((double)Math.Round(doplataDec, 2), "RURSHORT");

                        subsidy = parseCostToString(rowOMDamage.Subsidy);
                        subsidyText = AmountInWords.RusCurrency.Str((double)Math.Round(rowOMDamage.Subsidy ?? 0, 2), "RURSHORT");

                        //Расчет произвел
                        dateFill1 = dateRaschProizv.HasValue && !bInit
                            ? dateRaschProizv
                            : rowOMDamage.NomDate;
                        OMUser user = OMUser.Where(x => x.Id == rowOMDamage.AgreementId1).Select(x => x.FullNameForDoc).Select(x => x.Position).Execute().FirstOrDefault();
                        Agreement(user?.Position + " " + user?.FullNameForDoc, out agreementDepFio, out agreementDepFio2);

                        //Проверил
                        user = OMUser.Where(x => x.Id == rowOMDamage.AgreementId2).Select(x => x.FullNameForDoc).Select(x => x.Position).Execute().FirstOrDefault();

                        switch (rowOMDamage.ObjReestrId)
                        {
                            case 316:
                                address = GetAddress(rowOMDamage.ObjId);
                                break;
                            case 317:
                                OMFlat buildingID = OMFlat.Where(x => x.EmpId == rowOMDamage.ObjId).SelectAll().Execute().FirstOrDefault();
                                if (buildingID != null)
                                    address = GetAddress(buildingID.LinkObjectMkd) + ", кв. " + buildingID.Kvnom;
                                break;
                            default:
                                address = "";
                                break;
                        }

                        insurSumCalc = (oMInvoice.InsurSymmaOtchet ?? rowOMDamage.EstimatedValue ?? 0) * (rowOMDamage.PartInsur ?? 0) / 100;
                        insurSum = parseCostToString(insurSumCalc);
                        insurSumText = AmountInWords.RusCurrency.Str((double)Math.Round(insurSumCalc, 2), "RURSHORT");
                    }


                    dataTable.Rows.Add(
                        mainAgreementFio,
                        year + " г.",
                        docNumber, //oMInvoice != null ? oMInvoice.NumInvoice : null,
                        dateInput,
                        insurOrg,
                        damageInput,
                        insurNom,
                        oMInvoice != null ? oMInvoice.SvidPolycNum : null,
                        ndogdat,
                        insurWorker,
                        org,
                        subsidy,
                        subsidyText,
                        signHead,
                        departmentHead
                        );

                    dtDamage.Rows.Add(
                        sumDamageText,
                        doplata,
                        doplataText,
                        agreementDepFio,
                        agreementDepFio2,
                        dateFill1.HasValue ? dateFill1.Value.ToString("D").ToLower() : "",
                        agreementFio2,
                        partTown
                    );

                    dtInsur.Rows.Add(
                        bank1.Trim().Trim(','),
                        bank2.Trim().Trim(','),
                        bank3.Trim().Trim(','),
                        estimatedValue,
                        estimatedValueText,
                        insurSum,
                        insurSumText
                        );

                    dtSign.Rows.Add(
                        signatureFio,
                        signatureDep
                        );

                    dtTitle.Rows.Add("");

                    if (dtReject.Rows.Count == 0) dtReject.Rows.Add("");

                    dtRejectOwn.Rows.Add(
                        paymentInsur,
                        paymentInsurText,
                        reject
                        );
                }
                else
                {
                    if (oMInvoice == null)
                        dtEmpty.Rows.Add("Отчет не может быть сформирован:", "Вы не ввели информацию по субъектам!");
                    else
                        dtEmpty.Rows.Add("Отчет не может быть сформирован:", "Счет в статусе: ошибка в реквизитах!");
                }
            }
            catch (Exception ex)

            {
                throw;
            }

            dataSet.Tables.Add(dtEmpty);
            dataSet.Tables.Add(dataTable);
            dataSet.Tables.Add(NewLineAddress(address, podtext, oMInvoice));
            dataSet.Tables.Add(dtDamageSum);
            dataSet.Tables.Add(dtDamageDopSum);
            dataSet.Tables.Add(dtDamage);
            dataSet.Tables.Add(dtTitle);
            dataSet.Tables.Add(dtInsur);
            dataSet.Tables.Add(dtRejectOwn);
            dataSet.Tables.Add(dtReject);
            dataSet.Tables.Add(dtSign);
            return dataSet;
        }

        private string parseCostToString(decimal? cost)
        {
            string costToString = "0 руб.";
            string[] splitSum;

            
            splitSum = cost.HasValue ? cost.Value.ToString("n2", CultureRu).Replace(".", ", ").Split(",") : null;
            costToString = splitSum != null ? string.Format("{0} руб. {1} коп.", splitSum[0], splitSum[1] != null ? splitSum[1] : "00") : null;

            return costToString;
        }

        private void GenerateNumDateZacluch(OMInvoice oMInvoice, string docNumber, int i)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                if (oMInvoice.Sort == null)
                {
                    oMInvoice.NumInvoice = docNumber + (i != 0 ? " (" + i + ")" : "");
                    oMInvoice.Sort = i;
                }
                if (oMInvoice.dateInvoice == null)
                    oMInvoice.dateInvoice = DateTime.Today;                
                oMInvoice.Save();

                ts.Complete();
            }
        }

        private string GetAddress(long? buildingId)
        {
            string address = "";

            OMBuilding oMBuilding = OMBuilding.Where(x => x.EmpId == buildingId).Select(x => x.AddressId).Select(x => x.OkrugId).Select(x => x.DistrictId).Execute().FirstOrDefault();
            if (oMBuilding != null)
            {
                OMBtiOkrug okrug = OMBtiOkrug.Where(x => x.Id == oMBuilding.OkrugId).Select(x => x.ShortName).Execute().FirstOrDefault();
                address = okrug != null && okrug.ShortName != "" ? okrug.ShortName.Trim() + ", " : "";
                OMBtiDistrict distr = OMBtiDistrict.Where(x => x.Id == oMBuilding.DistrictId).Select(x => x.ShortName).Execute().FirstOrDefault();
                address += distr != null && distr.ShortName != "" ? "район " + distr.ShortName.Trim() + ", " : "";
                OMAddress oMAddress = OMAddress.Where(x => x.EmpId == oMBuilding.AddressId).Select(x => x.FullAddress).Execute().FirstOrDefault();
                if (oMAddress != null && oMAddress.FullAddress != "")
                {
                    //address += oMAddress.
                    string[] addressSplit = oMAddress.FullAddress.Split(',').ToArray();
                    Regex regex = new Regex(@"\b\d{6}\b");

                    foreach (string row in addressSplit)
                    {
                        if (row.ToLower().IndexOf("москва") == -1 && !regex.IsMatch(row))
                            address += row.Trim() + ", ";
                    }
                    address = address.Trim().TrimEnd(',');
                }
            }

            return address;
        }

        private DataTable NewLineAddress(string address, string podtext, OMInvoice oMInvoice)
        {
            DataTable dtAddress = new DataTable("ADDRESS");
            dtAddress.Columns.Add("Address");
            dtAddress.Columns.Add("Podtext");
            decimal widthText = 78;

            if (oMInvoice == null) return dtAddress;

            if (address.Length == 0 || address.IndexOf(',') == -1)
            {
                dtAddress.Rows.Add(address, podtext);
                return dtAddress;
            }

            string addressText = string.Empty;
            string[] addressSplit = address.Split(',');
            bool k = false;
            for(int i = 0; i < addressSplit.Length; i++)
            {
                addressText += addressSplit[i] + (i != addressSplit.Length-1 ? "," : "");
                if (addressText.Length >= widthText && addressText.Length % widthText <= addressSplit[i].Length)
                {
                    dtAddress.Rows.Add(addressText.Trim(), k == false ? podtext : "");
                    addressText = string.Empty;
                    k = true;
                }
            }

            if (addressText.Length < widthText && addressText.Length > 0) dtAddress.Rows.Add(addressText.Trim(), k == false ? podtext : "");

            return dtAddress;
        }

        private void Agreement(string agreement, out string agreementDepFio, out string agreementDepFio2)
        {
            Regex regular = new Regex(".{0,72}(?=\\s|$)", RegexOptions.Singleline);
            MatchCollection matches = regular.Matches(agreement);
            agreementDepFio = matches[0].Value;
            agreementDepFio2 = "";
            if (matches.Count > 1)
            {
                for (int i = 1; i < matches.Count - 1; i++)
                {
                    agreementDepFio2 += matches[i].ToString();
                }
            }
        }
    }
}