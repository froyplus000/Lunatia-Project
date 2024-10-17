using System;
using LunatiaProject.ItemAndInventory;
namespace LunatiaProject.Factory
{
	public interface IRecipeFactory
	{
		List<Recipe> CreateRecipe();
	}
}

