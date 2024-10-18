using System;
using LunatiaProject.Core;
using LunatiaProject.ItemAndInventory;
namespace LunatiaProject.Interfaces
{
	public interface IHaveRecipe
	{
		// Recipes list in string format
		string RecipesList { get; }
		// Method to get recipe informaiton by name
		string GetRecipeByName(string name);
		// Method to add recipe(s) to a list
		void AddRecipe(Recipe recipe);
		void AddAllRecipe(List<Recipe> recipes);
		// Method to locate and return Recipe
        Recipe Locate(string id);

    }
}

