﻿using System;
using LunatiaProject.Core;
using LunatiaProject.Interfaces;

namespace LunatiaProject.ItemAndInventory
{
	public class RecipeBook : Item, IHaveRecipe
	{
        // Fields
        private static RecipeBook _instance; // Singleton instance
		private List<Recipe> _recipes;

        // Propety

        public string RecipesList
        {
            get
            {
                string recipeList = "A List of All Recipe:\n";
                int index = 1;
                foreach (var recipe in _recipes)
                {
                    recipeList += string.Format("\t{0}. {1}, {2} ({3})\n", index, recipe.Name, recipe.FullDescription, recipe.FirstId);
                    index++;
                }

                return recipeList;
            }
        }

        // Static property to access the singleton instance
        public static RecipeBook Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecipeBook();
                }
                return _instance;
            }
        }

        // Private Constructor (Singleton)
        private RecipeBook() : base(new string[] { "recipebook", "recipe book"}, "Recipe Book", "a book that contains all recipes using for crafting purpose") 
        {
            _recipes = new List<Recipe>();
        }

        // Methods

        public void AddRecipe(Recipe recipe)
        {
            _recipes.Add(recipe);
        }

        public void AddAllRecipe(List<Recipe> recipes)
        {
            foreach (Recipe recipe in recipes)
            {
                this.AddRecipe(recipe);
            }
        }

        public string GetRecipeByName(string name)
        {

            foreach (var recipe in _recipes)
            {
                if (recipe.Name == name || recipe.ItemName == name || recipe.FirstId == name)
                {
                    return recipe.GetRecipe();
                }
            }

            return string.Format("No recipe found for {0}\nTry enter either name of item you want to craft or recipe name itself\n", name);
        }

        public Recipe Locate(string id)
        {
            foreach (Recipe recipe in _recipes)
            {
                if (recipe.AreYou(id))
                {
                    return recipe;
                }
            }
            return null;
        }

        // Methods for CraftCommand
        public Recipe LocateByName(string itemName)
        {
            foreach (Recipe recipe in _recipes)
            {
                if (recipe.ItemId == itemName)
                {
                    return recipe;
                }
            }
            return null;
        }

        public Recipe LocateById(string itemId)
        {
            foreach (Recipe recipe in _recipes)
            {
                if (recipe.AreYou(itemId))
                {
                    return recipe;
                }
            }
            return null;
        }
    }
}

