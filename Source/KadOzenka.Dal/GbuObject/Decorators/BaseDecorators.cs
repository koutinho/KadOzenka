using System;
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


	    public virtual List<T> GetItems()
	    {
		    throw new NotImplementedException();
	    }

		public virtual List<T> GetItems(int packageIndex, int packageSize)
	    {
		    throw new NotImplementedException();
	    }

		public virtual int GetItemsCount()
		{
			throw new NotImplementedException();
		}
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

        public override List<T> GetItems(int packageIndex, int packageSize)
        {
	        return ItemsGetter != null ? ItemsGetter.GetItems(packageIndex, packageSize) : new List<T>();
        }

        public override int GetItemsCount()
        {
			return ItemsGetter?.GetItemsCount() ?? 0;
		}
    }


    public class ItemBase
    {
	    public long Id { get; set; }
	    public long ObjectId { get; set; }
    }
}
