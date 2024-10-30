using System;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.LivingObject;

namespace LunatiaProject.Command
{
    public class CraftCommand : Command
    {
        // Construtor
        public CraftCommand() : base(new string[] { "craft" }) { }

        public override string Execute(Player p, string[] text)
        {
            int textLength = text.Length; // Text length
            for (int i = 0; i < textLength; i++)  // Ensure all input will be in Lowercase
            {
                text[i] = text[i].ToLower();
            }
            RecipeBook? recipeBook = p.Inventory.Fetch("recipebook") as RecipeBook;

            if (recipeBook == null)
            {
                return "You don't have Recipe Book in your inventory";
            }

            // Default Error message
            string errormsg = string.Format("What do you want to {0}?", text[0]);

            switch (textLength)
            {
                case (1):
                    return errormsg;
                case (2):
                    if (text.Last() == "a" | text.Last() == "an" | text.Last() == "the" | text.Last() == "that")
                    {
                        return errormsg;
                    }
                    return CraftProcess(recipeBook, text, p);
                default:

                    return CraftProcess(recipeBook, text, p);
            }
        }
        private string CraftProcess(RecipeBook recipeBook, string[] text, Player p)
        {
            string joinedWord = string.Join("", text.Skip(1)); // Skip first word

            // 1. Find recipe from Item Name
            Recipe recipe = recipeBook.LocateByName(joinedWord);

            // 2. Find recipe from Item Id
            if (recipe == null)
            {
                recipe = recipeBook.LocateById(joinedWord);
            }
            // 3. Find recipe from Recipe Id
            if (recipe == null)
            {
                recipe = recipeBook.Locate(joinedWord);
            }

            if (recipe != null)
            {
                string checkIngredient = recipe.CheckIngredient(p);
                if (checkIngredient != null)
                {
                    return checkIngredient;
                }
                p.Craft(recipe);
                return string.Format("{0} is crafted and added to your inventory", recipe.ItemName);
            }
            else
            {
                return string.Format("Recipe for {0} is not exist.", joinedWord);
            }
        }
    }
}

