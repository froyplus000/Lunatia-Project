using System;
using System.Linq;
namespace LunatiaProject.ItemAndInventory
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
                // Check if there are no items in the inventory
                if (_items == null || !_items.Any())
                {
                    return "\tNo Item\n";  // Return "No Item" if the list is empty
                }

                // Group items from List of items
                var groupedItems = _items 
                    .GroupBy(item => item.ShortDescription) // Group by item that have same short description
                    // This will create anonymous object for each group
                    .Select(itemGroup => new
                    {
                        // Each group have ShortDescription as a Key
                        ShortDescription = itemGroup.Key,
                        Count = itemGroup.Count() // Count number of items in the group
                    });

                // Build the formatted string for the inventory
                string itemList = "";
                foreach (var itemGroup in groupedItems)
                {
                    itemList += string.Format("\t{0} x {1}\n", itemGroup.Count, itemGroup.ShortDescription);
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
       
        public bool HasItem(string id) // Refactored using Linq
        {
            // Return True, if any item in the collection satisfies AreYou condition
            // Means return true if Item exist in the Collection
            return _items.Any(item => item.AreYou(id)); // Linq, Any()
        }

        // Counts how many item in the collection satisfies AreYou condition.
        public int GetItemCount(string id)
        {
            return _items.Count(item => item.AreYou(id)); // Linq, Count()
        }

        public void Put(Item itm)
        {
            _items.Add(itm);
        }

        public void PutMultipleItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                _items.Add(item);
            }
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

