using System;
using LunatiaProject.ItemAndInventory;
namespace LunatiaProject.Interfaces
{
	public interface IItemFactory
	{
		Item CreateItem(string id, string name, string desc);
        List<Item> CreateItems(string id, string name, string desc, int amount);
		List<Item> CreateItemsFromFile();
    }
}

