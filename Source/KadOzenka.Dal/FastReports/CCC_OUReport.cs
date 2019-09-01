using Core.Shared.Extensions;
using Core.Shared.Misc;
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
    public class CCC_OUReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query) { { return "CCC_OUReport"; } }

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

                OMInvoice oMInvoice = OMInvoice.Where(x => x.EmpId == ObjectId).SelectAll().Execute().FirstOrDefault();

                if (oMInvoice != null)
                {
                    OMDamage rowOMDamage = OMDamage.Where(x => x.EmpId == oMInvoice.LinkDamage).SelectAll().Execute().FirstOrDefault();

                    if (rowOMDamage != null)
                    {
                        docNumber = rowOMDamage.NomDoc;
                        List<OMInvoice> oMInvoices = OMInvoice.Where(x => x.LinkDamage == rowOMDamage.EmpId).SelectAll().OrderBy(x => x.EmpId).Execute();

                        foreach (OMInvoice row in oMInvoices)
                        {
                            if (oMInvoices.Count > 1)
                                i += 1;
                            if (row.Sort == null || row.dateInvoice == null)
                                GenerateNumDateZacluch(row, docNumber, i);
                        }
                    }
                }
            }
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            DataSet dataSet = new DataSet();
            OMInvoice oMInvoice = new OMInvoice();

            #region variables
            string mainAgreementFio = string.Empty,
                   agreementDepFio = string.Empty,
                   agreementDepFio2 = string.Empty,
                   dateFillMain = string.Empty,
                   docNumber = string.Empty,
                   dateInput = string.Empty,
                   insurOrg = string.Empty,
                   damageInput = string.Empty,
                   insurNom = string.Empty,
                   ndog = string.Empty,
                   ndogdat = string.Empty,
                   insurWorker = string.Empty,
                   sumDamageText = string.Empty,
                   agreementDep = string.Empty,
                   agreementFio = string.Empty,
                   dateFill1 = string.Empty,
                   agreementFio2 = "/                                 /",
                   dateFill2 = string.Empty,
                   paymentInsurText = string.Empty,
                   reject = string.Empty,
                   signatureFio = "/                                  /",
                   signatureDep = string.Empty,
                   signatureDate = string.Empty,
                   org = string.Empty,
                   aok = string.Empty,
                   bank = string.Empty,
                   bankLS = string.Empty,
                   doplataText = string.Empty,
                   subsidyText = string.Empty,
                   st4Text = string.Empty,
                   st4Text_1 = string.Empty,
                   mainAgreementId = string.Empty,
                   rejectNextRow = string.Empty,
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
                   ss1 = string.Empty,
                   ss2 = string.Empty,
                   ss3 = string.Empty,
                   ss4 = string.Empty,
                   st1 = string.Empty,
                   st2 = string.Empty,
                   st3 = string.Empty,
                   st4 = string.Empty,
                   podtext = "административный округ, муниципальный район, адрес,";

            decimal doplataDec = 0;

            DataTable dtEmpty = new DataTable("EMPTY");
            dtEmpty.Columns.Add("EMPTYTITLE");
            dtEmpty.Columns.Add("EMPTYTEXT");

            DataTable dataTable = new DataTable("COMMON");
            dataTable.Columns.Add("DocNumber");
            dataTable.Columns.Add("DateInput");
            dataTable.Columns.Add("InsurOrg");
            dataTable.Columns.Add("DamageInput");
            dataTable.Columns.Add("InsurNom");
            dataTable.Columns.Add("Ndog");
            dataTable.Columns.Add("Ndogdat");
            dataTable.Columns.Add("InsurWorker");                    
            dataTable.Columns.Add("PODPISANTHEAD");
            dataTable.Columns.Add("DEPTHEAD");
            dataTable.Columns.Add("Year");

            DataTable dtInsur = new DataTable("INSUR");
            dtInsur.Columns.Add("Bank1");
            dtInsur.Columns.Add("Bank2");
            dtInsur.Columns.Add("Bank3");
            dtInsur.Columns.Add("SS1");
            dtInsur.Columns.Add("SS2");
            dtInsur.Columns.Add("SS3");
            dtInsur.Columns.Add("SS4");
            dtInsur.Columns.Add("ST1");
            dtInsur.Columns.Add("ST4TEXT");
            dtInsur.Columns.Add("ST4TEXT_1");
            dtInsur.Columns.Add("ST2");
            dtInsur.Columns.Add("ST3");
            dtInsur.Columns.Add("ST4");

            DataTable dtTitle = new DataTable("TITLE");
            dtTitle.Columns.Add("Title");

            DataTable dtRaschet = new DataTable("RASCHET");
            dtRaschet.Columns.Add("SumDamage");
            dtRaschet.Columns.Add("SumDamageText");
            dtRaschet.Columns.Add("Doplata");
            dtRaschet.Columns.Add("DoplataText");
            dtRaschet.Columns.Add("PartTown");
            dtRaschet.Columns.Add("AgreementDepFio");
            dtRaschet.Columns.Add("AgreementDepFio2");
            dtRaschet.Columns.Add("AgreementFio2");
            dtRaschet.Columns.Add("PaymentInsur");
            dtRaschet.Columns.Add("PaymentInsurText");
            dtRaschet.Columns.Add("Reject");

            DataTable dtReject = new DataTable("REJECT");
            dtReject.Columns.Add("RejectNextRow");

            DataTable dtSign = new DataTable("SIGN");
            dtSign.Columns.Add("SignatureFio");
            dtSign.Columns.Add("SignatureDep");
            #endregion

            try
            {
                string podpisant = GetQueryParam<string>("PodpisantHead", query);
                bool noSign = GetQueryParam<bool>("NoSignHead", query);
                string podpisantRep = GetQueryParam<string>("Podpisant", query);
                bool noSignRep = GetQueryParam<bool>("NoSign", query);
                int? year = GetQueryParam<int?>("Year", query);
                DateTime? dateSost = GetQueryParam<DateTime?>("DateSost", query);
                DateTime? dateRaschProizv = GetQueryParam<DateTime?>("dateFill", query);

                string signHead = string.Empty;
                string departmentHead = string.Empty;

                if (!noSign && podpisant.IsNotEmpty())
                {
                    string[] sep = podpisant.Split('-');
                    signHead = "/" + sep[0].ToString().Trim() + "/";
                    departmentHead = sep[1].ToString().Trim()
                       .Replace(" ГБУ", Environment.NewLine + "ГБУ")
                       .Replace(" и жилищного", Environment.NewLine + "и жилищного");
                }
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

                oMInvoice = OMInvoice.Where(x => x.EmpId == ObjectId).SelectAll().Select(x => x.ParentGbuNoPayReason.Reason).Select(x => x.ParentGbuNoPayReason.ShortExplanation).OrderBy(x => x.Sort).Execute().FirstOrDefault();
                if (oMInvoice != null)
                {
                    OMDamage rowOMDamage = OMDamage.Where(x => x.EmpId == oMInvoice.LinkDamage).SelectAll().Execute().FirstOrDefault();

                    if (rowOMDamage != null)
                    {
                        dateInput = oMInvoice.DataZakluchenia.HasValue ? oMInvoice.DataZakluchenia.Value.ToShortDateString() : (dateSost.HasValue && !bInit
                            ? dateSost.Value.ToShortDateString()
                            : rowOMDamage.NomDate.HasValue ? rowOMDamage.NomDate.Value.ToShortDateString() : string.Empty);

                        if (rowOMDamage.InsurOrgId.HasValue)
                        {
                            OMInsuranceOrganization oMInsuranceOrganization = OMInsuranceOrganization.Where(x => x.Id == rowOMDamage.InsurOrgId.Value).SelectAll().Execute().FirstOrDefault();
                            insurOrg = oMInsuranceOrganization?.FullName;
                        }

                        damageInput = rowOMDamage.InsurDate.HasValue ? rowOMDamage.InsurDate.Value.ToShortDateString() : string.Empty;
                        insurNom = rowOMDamage.InsurNom;

                        sumDamage = parseCostToString(rowOMDamage.SumDamage);
                        sumDamageText = AmountInWords.RusCurrency.Str((double)Math.Round(rowOMDamage.SumDamage ?? 0, 2), "RURSHORT");

                        partTown = rowOMDamage.PartTown?.ToString("n0");
                        doplataDec = (rowOMDamage.SumDamage ?? 0) * (rowOMDamage.PartTown ?? 0) / 100;
                        doplata = parseCostToString(doplataDec);
                        doplataText = AmountInWords.RusCurrency.Str((double)Math.Round(doplataDec, 2), "RURSHORT");

                        subsidy = parseCostToString(rowOMDamage.Subsidy);
                        subsidyText = AmountInWords.RusCurrency.Str((double)Math.Round(rowOMDamage.Subsidy ?? 0, 2), "RURSHORT");

                        //Расчет произвел
                        dateFill1 = dateRaschProizv.HasValue && !bInit ? dateRaschProizv.Value.ToShortDateString() : rowOMDamage.NomDate.ToString();
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
                    }

                    ndog = oMInvoice.SvidPolycNum;
                    ndogdat = ndogdat = oMInvoice.SvdPolyceDate.HasValue ? oMInvoice.SvdPolyceDate.Value.ToShortDateString() + " г." : null;

                    //Раздел - Заключение о выплате из бюджета города Москвы
                    decimal? paymInsur = oMInvoice.SumOpl;
                    paymentInsur = parseCostToString(paymInsur);
                    paymentInsurText = AmountInWords.RusCurrency.Str((double)Math.Round(paymInsur ?? 0, 2), "RURSHORT");

                    if (oMInvoice.NoteNoPayId != null)
                    {
                        reject = "";
                        rejectNextRow = "";                        

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
                    }

                    OMAllProperty oMAllProperty = OMAllProperty.Where(x => x.Ndog == oMInvoice.SvidPolycNum && x.Ndogdat == oMInvoice.SvdPolyceDate).SelectAll().Execute().FirstOrDefault();
                    if (oMAllProperty != null)
                    {
                        ss1 = parseCostToString(oMAllProperty.Ss1);
                        ss2 = parseCostToString(oMAllProperty.Ss2);
                        ss3 = parseCostToString(oMAllProperty.Ss3);
                        ss4 = parseCostToString(oMAllProperty.Ss1 + oMAllProperty.Ss2 + oMAllProperty.Ss3);
                        st4 = parseCostToString(oMAllProperty.St1 + oMAllProperty.St2 + oMAllProperty.St3);
                        GetTextObjectCost("(" + AmountInWords.RusCurrency.Str((double)Math.Round((oMAllProperty.St1 + oMAllProperty.St2 + oMAllProperty.St3) ?? 0, 2), "RURSHORT") + ")", st4, out st4Text, out st4Text_1);
                        st1 = parseCostToString(oMAllProperty.St1);
                        st2 = parseCostToString(oMAllProperty.St2);
                        st3 = parseCostToString(oMAllProperty.St3);

                    }

                    insurWorker = oMInvoice.SubjectName;
                    bank1 = (oMInvoice.Inn != null ? ("ИНН " + oMInvoice.Inn) : "") + (oMInvoice.Kpp != null ? (" КПП " + oMInvoice.Kpp) : "") + (oMInvoice.BicBank != null ? (" БИК " + oMInvoice.BicBank) : "") + (oMInvoice.KorAcc != null ? (" к /с " + oMInvoice.KorAcc) : "");
                    bank2 = (oMInvoice.RachAcc != null ? ("р/с " + oMInvoice.RachAcc) : "") + (oMInvoice.BankName != null ? (" в " + oMInvoice.BankName) : "");
                    bank3 = oMInvoice.NumCard != null ? "банковская карта № " + oMInvoice.NumCard : "";

                    dataTable.Rows.Add(
                        oMInvoice.NumInvoice,
                        dateInput,
                        insurOrg,
                        damageInput,
                        insurNom,
                        ndog,
                        ndogdat,
                        insurWorker,                        
                        signHead,
                        departmentHead,
                        year + " г.");

                    dtRaschet.Rows.Add(
                        sumDamage,
                        sumDamageText,
                        doplata,
                        doplataText,
                        partTown,
                        agreementDepFio,
                        agreementDepFio2,
                        agreementFio2,
                        paymentInsur,
                        paymentInsurText,
                        reject    
                        );

                    dtInsur.Rows.Add(
                        bank1.Trim().Trim(','),
                        bank2.Trim().Trim(','),
                        bank3.Trim().Trim(','),
                        ss1,
                        ss2,
                        ss3,
                        ss4,
                        st1,
                        st4Text.Trim(),
                        st4Text_1.Trim(),
                        st2,
                        st3,
                        st4
                        );

                    dtSign.Rows.Add(
                        signatureFio,
                        signatureDep
                        );

                    dtTitle.Rows.Add("");

                    if (dtReject.Rows.Count == 0) dtReject.Rows.Add("");

                }
                else
                {
                    dtEmpty.Rows.Add("Отчет не может быть сформирован:", "Вы не ввели информацию по субъектам!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            dataSet.Tables.Add(dtEmpty);
            dataSet.Tables.Add(dataTable);
            dataSet.Tables.Add(NewLineAddress(address, podtext, oMInvoice));
            dataSet.Tables.Add(dtRaschet);
            dataSet.Tables.Add(dtTitle);
            dataSet.Tables.Add(dtInsur);
            dataSet.Tables.Add(dtReject);
            dataSet.Tables.Add(dtSign);
            return dataSet;
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

        private void GetTextObjectCost(string costObjectTest, string costObject, out string st4Text, out string st4Text_1)
        {
            //costObjectTest = "(Пятьдесят девять миллионов пятьсот шестьдесят девять тысяч семьсот двадц ать";
            //costObjectTest = "(Пятьдесят девять миллионов пятьсот шестьдесят девять тысяч семьсот дв адцать";
            //costObjectTest = "(Пятьдесят девять милли онов пятьсот шестьдесят девять тысяч семьсот двадцать два руб. 51 коп.)";
            //costObjectTest = "(Пятьдесят девять миллионов пятьсот шестьдесят девять тысяч семьсот двадцать два руб. 51 коп.)";
            //costObject = "59 569 722 руб. 51 коп.";
            // 46 - длинна строки
            int sLength = 46 - costObject.Length;
            st4Text = "";
            st4Text_1 = "(" + costObjectTest + ")";

            if (costObjectTest.Length > 77)
            {
                Regex regular = new Regex(".{0," + sLength + "}(?=\\s|$)", RegexOptions.Singleline);
                MatchCollection matches = regular.Matches(costObjectTest);
                st4Text = matches[0].Value;
                if (matches.Count > 1)
                {
                    st4Text_1 = "";
                    for (int i = 1; i < matches.Count - 1; i++)
                    {
                        st4Text_1 += matches[i].ToString();
                    }
                }
            }
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
            for (int i = 0; i < addressSplit.Length; i++)
            {
                addressText += addressSplit[i] + (i != addressSplit.Length - 1 ? "," : "");
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

        private string parseCostToString(decimal? cost)
        {
            string costToString = "0 руб.";
            string[] splitSum;

            splitSum = cost.HasValue ? cost.Value.ToString("n2").Replace(".", ", ").Split(",") : null;
            costToString = splitSum != null ? string.Format("{0} руб. {1} коп.", splitSum[0], splitSum[1] != null ? splitSum[1] : "00") : null;

            return costToString;
        }

        private void GenerateNumDateZacluch(OMInvoice oMInvoice, string docNumber, int i)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                if (oMInvoice.Sort == null)
                {
                    oMInvoice.NumInvoice = docNumber + (i != 0 ? "-" + i : "");
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
    }
}