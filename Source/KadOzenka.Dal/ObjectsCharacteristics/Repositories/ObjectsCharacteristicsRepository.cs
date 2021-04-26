using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.ObjectsCharacteristics.Repositories
{
    public class ObjectsCharacteristicsRepository : GenericRepository<OMObjectsCharacteristicsRegister>, IObjectCharacteristicsRepository
    {
	    protected override QSQuery<OMObjectsCharacteristicsRegister> GetBaseQuery(Expression<Func<OMObjectsCharacteristicsRegister, bool>> whereExpression)
	    {
		    return OMObjectsCharacteristicsRegister.Where(whereExpression);
	    }

	    protected override Expression<Func<OMObjectsCharacteristicsRegister, bool>> GetWhereByIdExpression(long id)
	    {
		    return x => x.Id == id;
	    }

        public void CreateOrUpdateCharacteristicSetting(long attributeId, bool useParentAttributeForLivingPlacement,
            bool useParentAttributeForNotLivingPlacement, bool useParentAttributeForCarPlace, bool disableAttributeEditing)
        {
            var settings = OMAttributeSettings.Where(x => x.AttributeId == attributeId).SelectAll()
                .ExecuteFirstOrDefault();
            if (settings == null)
            {
                settings = new OMAttributeSettings
                {
                    AttributeId = attributeId
                };
            }

            settings.UseParentAttributeForLivingPlacements = useParentAttributeForLivingPlacement;
            settings.UseParentAttributeForNotLivingPlacements = useParentAttributeForNotLivingPlacement;
            settings.UseParentAttributeForCarPlace = useParentAttributeForCarPlace;
            settings.DisableEditing = disableAttributeEditing;
            settings.Save();
        }

        public int GetNumberOfExistingRegistersWithCharacteristics()
        {
            return OMObjectsCharacteristicsRegister.Where(x => true).SelectAll().ExecuteCount();
        }

        public void CreateObjectCharacteristics(long registerId, bool disableAttributeEditing)
        {
            new OMObjectsCharacteristicsRegister
            {
                RegisterId = registerId,
                DisableEditing = disableAttributeEditing
            }.Save();
        }

        public OMAttributeSettings GetRegisterAttributeSettings(long attributeId)
        {
            return OMAttributeSettings.Where(x => x.AttributeId == attributeId).SelectAll().ExecuteFirstOrDefault();
        }

        public bool GetObjectRegisterEditState(long registerId)
        {
            var regSettings = OMObjectsCharacteristicsRegister.Where(x => x.RegisterId == registerId).SelectAll()
                .ExecuteFirstOrDefault();
            return regSettings?.DisableEditing.GetValueOrDefault(false) ?? false;
        }

        public void SaveRegister(OMRegister omRegister, bool? disableAttributeEditing)
        {
            var characteristicsRegister = OMObjectsCharacteristicsRegister
                .Where(x => x.RegisterId == omRegister.RegisterId).SelectAll().ExecuteFirstOrDefault();
            if (characteristicsRegister.DisableEditing != disableAttributeEditing)
            {
                // Сохранение для реестра с источниками
                characteristicsRegister.DisableEditing = disableAttributeEditing;
                characteristicsRegister.Save();

                // Сохранение параметров атрибутов
                var attributes = OMAttribute.Where(x => x.RegisterId == omRegister.RegisterId).Select(x => x.Id)
                    .Execute().Select(x => x.Id).ToList();
                attributes.ForEach(attributeId =>
                {
                    var settings = OMAttributeSettings.Where(x => x.AttributeId == attributeId).SelectAll()
                        .ExecuteFirstOrDefault();
                    if (settings == null)
                    {
                        settings = new OMAttributeSettings
                        {
                            AttributeId = attributeId
                        };
                    }

                    settings.DisableEditing = disableAttributeEditing;
                    settings.Save();
                });
            }

            omRegister.Save();
        }
    }
}