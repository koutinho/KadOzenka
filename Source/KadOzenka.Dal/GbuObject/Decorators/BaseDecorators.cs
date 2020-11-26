using System.Collections.Generic;

namespace KadOzenka.Dal.GbuObject.Decorators
{
	public abstract class AItemsGetter<T> where T : ItemBase
    {
        public abstract List<T> GetItems();
    }


    public abstract class ADecorator<T> : AItemsGetter<T> where T : ItemBase
    {
        protected AItemsGetter<T> ItemsGetter;

        protected ADecorator(AItemsGetter<T> itemsGetter)
        {
	        ItemsGetter = itemsGetter;
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
