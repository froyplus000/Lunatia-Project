using System;
namespace Swin_Adventure_Iteration_8
{
	public class Inventory
	{
		// Fields
		private List<Item> _items;

        // Properties
        public string ItemList
        {
            get
            {
                string itemList = "";
                foreach (Item item in _items)
                {
                    // Updated : using \t instead of 4 spaces
                    itemList += string.Format("\t{0}\n", item.ShortDescription);
                }
                return itemList;
            }
        }

        // Constructor
        public Inventory()
		{
            _items = new List<Item>();
        }

        // Methods
        public bool HasItem(string id)
        {
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    return true;
                }
            }
            return false;
        }

        public void Put(Item itm)
        {
            _items.Add(itm);
        }

        public Item Take(string id)
        {
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    _items.Remove(item);
                    return item;
                }
            }
            return null;
        }

        public Item Fetch(string id)
        {
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    return item;
                }
            }
            return null;
        }
    }
}

