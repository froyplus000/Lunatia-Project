using System;
using LunatiaProject.ItemAndInventory;
namespace LunatiaProject.Interfaces
{
	public interface IItemFactory
	{
        List<Item> CreateItems(string id, string name, string desc, int amount);
	}
}

