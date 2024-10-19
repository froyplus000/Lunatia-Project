using System;
using LunatiaProject.ItemAndInventory;
namespace LunatiaProject.Interfaces
{
	public interface ICraftable
	{
		string GetRecipe();
		Recipe CraftingRecipe { get; }
	}
}

