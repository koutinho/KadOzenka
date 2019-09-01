using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPJS.DAL.Building;
using Core.ErrorManagment;
using Core.Register.DAL;
using Core.Register.LongProcessManagment;
using Core.SessionManagment;
using Core.Shared.Misc;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Core.Register;
using ObjectModel.Insur;

namespace CIPJS.Controllers
{
    public class FlagInsurController : BaseController
    {
        public ActionResult Copy()
        {
            int count = Core.UI.Registers.CoreUI.Registers.RegistersVariables.CurrentList != null ? Core.UI.Registers.CoreUI.Registers.RegistersVariables.CurrentList.Count : 0;
            ViewBag.UniqueSessionKey = HttpContextHelper.HttpContext.Request.Query["UniqueSessionKey"];

            return View(count);
        }

		public ActionResult Check(long insurBuildingId)
		{
			string message;
            StringBuilder response = new StringBuilder();

			bool flagInsur = BuildingService.CalculateFlagInsur(insurBuildingId, out message);
            
        
			if(flagInsur == true)
			{
                response.AppendLine("Признак: Подлежит страхованию");
			}
			else
			{
                response.AppendLine($"Признак: Не подлежит страхованию. Причина: {message}.");
			}

            var buildingFlagInsur = OMBuilding.Where(x => x.EmpId == insurBuildingId && x.SpecialActual == 1).Select(x => x.FlagInsurCalculated).ExecuteFirstOrDefault()?.FlagInsurCalculated;
            if (buildingFlagInsur != null)
            {
                if (buildingFlagInsur != flagInsur)
                {
                    response.Append(Environment.NewLine);
                    response.AppendLine("Внимание, найдено несоответствие признаков! Необходимо пересчитать признак в МКД!");
                }

            }

            ViewBag.message = response.ToString();
            return View();
		}

		public ActionResult FlagInsurFillSelected()
		{
			int count = Core.UI.Registers.CoreUI.Registers.RegistersVariables.CurrentList != null ? Core.UI.Registers.CoreUI.Registers.RegistersVariables.CurrentList.Count : 0;
			ViewBag.UniqueSessionKey = HttpContextHelper.HttpContext.Request.Query["UniqueSessionKey"];

			return View(count);
		}
		
		public static SessionVariable<int> ProcessedCountVariable = new SessionVariable<int>();

        /// <summary>
        /// Копирование признака в мкд
        /// </summary>
        /// <param name="isGPCopyCheck">Производить копирование признака в ЖП</param>
        /// <returns></returns>
        public ActionResult DoCopy(bool? isGPCopyCheck = false)
        {
            List<long> currentList = Core.UI.Registers.CoreUI.Registers.RegistersVariables.CurrentList.ToList();
            string sessionKey = SessionKey;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    int processedCount = 0;

					List<OMBuilding> buildings = OMBuilding.Where(w => currentList.Contains(w.EmpId)).Select(x => x.FlagInsur).Select(x => x.FlagInsurCalculated).Execute().ToList();

                    for (int i = 0; i < buildings.Count; i++)
                    {
						OMBuilding building = buildings[i];

						if (building.FlagInsurCalculated != null && 
						    building.FlagInsur != building.FlagInsurCalculated)
						{
                            if (isGPCopyCheck.HasValue && isGPCopyCheck.Value)
                            {
                                List<OMFlat> flats = OMFlat.Where(x => x.LinkObjectMkd == building.EmpId && x.SpecialActual == 1).Execute();
                                foreach (var flat in flats)
                                {
                                    flat.FlagInsur = building.FlagInsurCalculated;
                                    flat.Save();
                                }
                            }

							building.FlagInsur = building.FlagInsurCalculated;
							building.Save();
						}
						
                        processedCount++;
                        new SessionManager(sessionKey).Set(ProcessedCountVariable, processedCount);
                    }
                }
                catch(Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            }, TaskCreationOptions.RunContinuationsAsynchronously);

            return Ok();
        }

		public int GetCopyProgress()
		{
			return SessionManager.Get(ProcessedCountVariable);
		}

		public ActionResult DoFlagInsurFillSelected()
		{
			long listId = RegisterListDAL.CreateTempList(OMBuilding.GetRegisterId(), 
				Core.UI.Registers.CoreUI.Registers.RegistersVariables.CurrentList, 
				"TenamentsInsurFlagRecalculation", 
				$"Перерасчет признака Подлежит страхованию для (кол-во объектов: {Core.UI.Registers.CoreUI.Registers.RegistersVariables.CurrentList.Count})");

			LongProcessManager.AddTaskToQueue("FlagInsurFiller", OMList.GetRegisterId(), listId);

			return Ok();
		}
    }
}