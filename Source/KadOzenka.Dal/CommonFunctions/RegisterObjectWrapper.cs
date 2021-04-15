using System;
using System.Collections.Generic;
using Core.Register;
using Core.Register.RegisterEntities;

namespace KadOzenka.Dal.CommonFunctions
{
	public class RegisterObjectWrapper : IRegisterObjectWrapper
    {
        public RegisterAttributeValue SetAttributeValue(RegisterObject obj, long attributeId, object value)
        {
            return obj.SetAttributeValue(attributeId, value);
        }

        public int Save(RegisterObject registerObject)
        {
            return RegisterStorage.Save(registerObject);
        }
    }



	public interface IRegisterObjectWrapper
    {
        RegisterAttributeValue SetAttributeValue(RegisterObject obj, long attributeId, object value);

        int Save(RegisterObject registerObject);
    }
}
