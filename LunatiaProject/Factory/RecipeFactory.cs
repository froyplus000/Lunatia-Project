using System;
using System.Text.Json; // Need for working with JSON file
using LunatiaProject.Interfaces;

using LunatiaProject.ItemAndInventory;

namespace LunatiaProject.Factory
{
	public class RecipeFactory : IRecipeFactory
	{
		// Field
		private string _recipeFilePath;

		// Property
		public string RecipeFilePath
		{
			get { return _recipeFilePath; }
		}

		// Construtor
		public RecipeFactory(string recipeFilePath)
		{
			_recipeFilePath = recipeFilePath;
		}

		// Method
		public List<Recipe> CreateRecipe()
		{
            List<Recipe> recipes = new List<Recipe>();

            try
            {
                // Read the JSON file as a string
                string jsonString = File.ReadAllText(_recipeFilePath);

                // Deserialize the JSON data into a list of RecipeJson objects
                var recipeJsonList = JsonSerializer.Deserialize<List<RecipeJson>>(jsonString);

                // Convert each RecipeJSON entry into a Recipe object
                foreach (var recipeJson in recipeJsonList)
                {
                    Recipe recipe = new Recipe(
                        recipeJson.Id,
                        recipeJson.Name,
                        recipeJson.Desc,
                        recipeJson.Ingredients,
                        recipeJson.ItemId,
                        recipeJson.ItemName,
                        recipeJson.ItemDescription
                    );
                    recipes.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading recipes: " + ex.Message);
            }
            return recipes;
        }

        // Method For working with JSON
        private class RecipeJson // Class to store recipe object from JSON
        {
            public string[] Id { get; set; }  // Matches the "id" in JSON (array of strings)
            public string Name { get; set; }   // Matches the "name" in JSON
            public string Desc { get; set; }   // Matches the "desc" in JSON
            public Dictionary<string, int> Ingredients { get; set; }  // Matches "ingredients" in JSON (key-value pairs)
            public string ItemId { get; set; }  // Matches "itemId" in JSON
            public string ItemName { get; set; }  // Matches "itemName" in JSON
            public string ItemDescription { get; set; }  // Matches "itemDescription" in JSON
        }
    }
}

