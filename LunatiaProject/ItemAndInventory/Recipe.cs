using System;
using System.Text;
using LunatiaProject.Core;
using LunatiaProject.LivingObject;

namespace LunatiaProject.ItemAndInventory
{
	public class Recipe : GameObject
	{

        // Fields
        private string _itemId; // Name of an item this recipe crafts
        private string _itemName; // Name of an item this recipe crafts
        private string _itemDesciption; // Name of an item this recipe crafts
        private Dictionary<string, int> _ingredients;

        // Property
        public string ItemId
        {
            get { return _itemId; }
        }
        public string ItemName
        {
            get { return _itemName; }
        }
        public string ItemDescription
        {
            get { return _itemDesciption; }
        }
        public Dictionary<string, int> Ingredients
        {
            get { return _ingredients; }
        }
        

        // Constructor
        public Recipe(string[] id, string name, string desc, Dictionary<string, int> ingredients, string itemId, string itemName, string itemDesc) : base(id, name, desc)
		{
            _ingredients = ingredients;
            _itemId = itemId;
            _itemName = itemName;
            _itemDesciption = itemDesc;
		}

        // Methods

        public string GetRecipe()
        {

            string recipeInfo = string.Format("Requires:\n");
            foreach (var ingredient in Ingredients)
            {
                recipeInfo += string.Format("\t{0} x {1}\n", ingredient.Value, ingredient.Key);
            }
            recipeInfo += string.Format("To Craft : {0}\n", ItemName);
            return recipeInfo;
        }

        private string IngredientsAlert(KeyValuePair<string, int> ingredient, int itemCount)
        {
            return string.Format("You need {0} x {1} to craft {2}", ingredient.Value - itemCount, ingredient.Key, ItemName);
        }

        public string CheckIngredient(Player player)
        {
            // Check if player have all items requires to craft in their inventory or not
            foreach (var ingredient in Ingredients)
            {
                // Also get the count of that item
                int itemCount = player.Inventory.GetItemCount(ingredient.Key);
                if (itemCount < ingredient.Value)
                {
                    // If player inventory doesn't have enough amount of that item
                    return IngredientsAlert(ingredient, itemCount); // Alert them
                }
            }
            return null;
        }

        public bool CanCraft(Player player)
        {
            return (CheckIngredient(player) == null) ? true : false;
        }

    }
}

