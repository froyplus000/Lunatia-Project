using System;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.Enum;
namespace LunatiaProject.Interfaces
{
	public interface IGatherable
	{
		int ResourceAmount { get; }
		ResourceType ResourceType { get; }
		Item CreateItem();
	}
}

