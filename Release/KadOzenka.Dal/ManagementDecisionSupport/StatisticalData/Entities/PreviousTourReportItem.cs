﻿using System.Collections.Generic;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities
{
    public class PreviousTourReportItem : InfoFromTourSettings
    {
        public OMTour Tour { get; set; }

        //From Unit
        public string CadastralNumber { get; set; }
        public decimal? Square { get; set; }
        public decimal? CadastralCost { get; set; }

        //From Rosreestr
        public string OksName { get; set; }
        public string ZuName { get; set; }
        public string ResultName { get; set; }
        public string BuildingPurpose { get; set; }
        public string PlacementPurpose { get; set; }
        public string ConstructionPurpose { get; set; }
        public string ResultPurpose { get; set; }
        public string PermittedUse { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ParentCadastralNumberForOks { get; set; }
        public string BuildYear { get; set; }
        public string CommissioningYear { get; set; }
        public string FloorsNumber { get; set; }
        public string UndergroundFloorsNumber { get; set; }
        public string WallMaterial { get; set; }

        //From Tour Settings

        //Factors from Model
        public List<FactorsService.PricingFactor> Factors { get; set; }
    }
}