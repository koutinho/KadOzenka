using System.Collections.Generic;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.GbuObject.Decorators
{
	public abstract class AItemsGetter<T> where T : ItemBase
	{
		protected ILogger Logger { get; set; }

		protected AItemsGetter(ILogger logger)
        {
	        Logger = logger;
        }


	    public abstract List<T> GetItems();
    }


    public abstract class ADecorator<T> : AItemsGetter<T> where T : ItemBase
    {
        protected AItemsGetter<T> ItemsGetter;
        protected GbuObjectService GbuObjectService { get; }
        protected RosreestrRegisterService RosreestrRegisterService { get; }

        protected ADecorator(AItemsGetter<T> itemsGetter, ILogger logger): base(logger)
        {
	        ItemsGetter = itemsGetter;
	        RosreestrRegisterService = new RosreestrRegisterService();
	        GbuObjectService = new GbuObjectService();
        }

        public override List<T> GetItems()
        {
	        return ItemsGetter != null ? ItemsGetter.GetItems() : new List<T>();
        }
    }


    public class ItemBase
    {
	    public long Id { get; set; }
	    public long ObjectId { get; set; }
    }
}
