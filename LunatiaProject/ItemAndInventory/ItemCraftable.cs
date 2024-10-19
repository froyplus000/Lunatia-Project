using System;
using LunatiaProject.Interfaces;
namespace LunatiaProject.ItemAndInventory
{
	public class ItemCraftable : Item, ICraftable
	{
		// Field
		private Recipe _craftingRecipe;

		// Property
		public Recipe CraftingRecipe
		{
			get { return _craftingRecipe; }
		}

		// Constructor
		public ItemCraftable(string[] ids, string name, string desc, Recipe craftingRecipe) : base(ids, name, desc)
        {
			_craftingRecipe = craftingRecipe;
		}

		// Method

		public string GetRecipe()
		{
			return CraftingRecipe.GetRecipe();
		}
	}
}

