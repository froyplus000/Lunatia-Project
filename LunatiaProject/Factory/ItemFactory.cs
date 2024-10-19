using System;
using LunatiaProject.Interfaces;
using LunatiaProject.ItemAndInventory;
namespace LunatiaProject.Factory
{

	public class ItemFactory
	{
		// Constructor
		public ItemFactory() 
		{
		}

		// Method
		public List<Item> CreateItems(string id, string name, string desc, int amount)
		{
			List<Item> items = new List<Item>();
			for (int i = 0; i < amount; i++)
			{
				Item item = new Item(new string[] {id}, name, desc);
				items.Add(item);
			}
			return items;
        }
	}
}

