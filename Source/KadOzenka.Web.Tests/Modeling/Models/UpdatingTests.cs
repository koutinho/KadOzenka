using KadOzenka.Web.Models.Modeling;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Modeling.Models
{
	public class UpdatingTests : BaseModelingTests
	{
		[Test]
		public void CanNot_Update_Automatic_Model_If_Model_State_Is_Invalid()
		{
			var controller = ModelingController;
			var method = new ControllerMethod<AutomaticModelingModel>(controller.AutomaticModelCard);
			
			CheckMethodValidateModelState(controller, method);
		}
	}
}