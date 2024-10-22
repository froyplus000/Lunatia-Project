using System;
using LunatiaProject.LivingObject;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.Interfaces;
using LunatiaProject.Core;


namespace LunatiaProject.Command
{
	public class LookCommand : Command
	{
		// Constructor
		public LookCommand() : base (new string[] { "look" }) { }

		// Methods
        public override string Execute(Player p, string[] text)
		{
            int textLength = text.Length; // variable to store input length
            // Update : Ensure all input will be in Lowercase
            for (int i = 0; i < textLength; i++)
            {
                text[i] = text[i].ToLower();
            }
            IHaveInventory container; // variable to store a container

			// Default - If input lenght isn't 3 or 5, return error massage
			switch (textLength)
			{
                case (2):
                    // If 3rd word is here, return full description of player's current location
                    if (text[1] == "here" && text[1] == "around" && text[0] == "look")
                    {
                        return p.Location.FullDescription;
                    }
                    return "I don't know how to look like that";

                case (3):
					// 1st word must be "look"
                    if (text[0] != "look")
                    {
                        return "Error in look input";
                    }
                   
                    // 2nd word must be "at"
                    if (text[1] != "at")
                    {
                        return "What do you want to look at?";
                    }

                    // If 3rd word is here, return full description of player's current location
                    if (text[2] == "here" )
                    {
                        return p.Location.FullDescription;
                    }

                    // There are 3 elements in command, so player is container
                    container = p;
                    return LookAtIn(text[2], container);

                case (5):
                    // 1st word must be "look"
                    if (text[0] != "look")
                    {
                        return "Error in look input";
                    }
                    // 2nd word must be "at"
                    if (text[1] != "at")
                    {
                        return "What do you want to look at?";
                    }
                    // 4th word must be "in"
                    if (text[3] != "in")
                    {
                        return "What do you want to look in?";
                    }
                    // Locate container in Player Inventory, assign it as container
                    container = FetchContainer(p, text[4]);
                    if (container == null)
                    {
                        return string.Format("I can't find the {0}", text[4]);
                    }


                    return LookAtIn(text[2], container);

				default: // Default code for assigning error message and Break
                    return "I don't know how to look like that";
            }
		}

        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            // Fixed test in iteration 4, allow "look at gem in inventory"
            if (p.AreYou(containerId))
            {
                return p;
            }
            // First, check if player's current location is where player want search item in. (e.g. Forest)
            // If user inputs a location, then return player's location as a container to find item in next step
            else if (p.Location.AreYou(containerId))
            {
                return p.Location;
            }
            // Second, check if player have container in their inventory. (e.g. Bag)
            // If it's not player's current location, then it need to be some container item in the player's inventory, return that container
            else if (p.Inventory.HasItem(containerId))
            {
                return p.Inventory.Fetch(containerId) as IHaveInventory;
            }
            else
            {
                return null;
            }
        }

        private string LookAtIn(string itemId, IHaveInventory container)
        {
            // First, check if the item exists in the container (e.g., player inventory)
            GameObject item = container.Locate(itemId);

            // If the item is a RecipeBook, return the recipe list
            if (item is RecipeBook recipebook)
            {
                return recipebook.RecipesList;
            }

            if (item != null)
            {
                return item.FullDescription;
            }

            // If not found, check if the container is the player and they have a recipebook
            if (container is Player player)
            {
                // Assuming player has a method to get their RecipeBook from the inventory
                RecipeBook recipeBook = player.Inventory.Fetch("recipebook") as RecipeBook;

                if (recipeBook != null)
                {
                    // Try to find the recipe in the recipebook
                    Recipe recipe = recipeBook.Locate(itemId);

                    if (recipe != null)
                    {
                        return recipe.GetRecipe();
                    }
                }

                Map.Path path = player.Location.LocatePath(itemId);
                if (path != null)
                {
                    if (path.IsLocked == true)
                    {
                        return string.Format("{0} Path, {1} that requires {2} to travel through.", path.Name, path.FullDescription, path.Key.Name);
                    }
                    else
                    {
                        return string.Format("{0} Path, {1}.", path.Name, path.FullDescription);
                    }

                }
            }
            return string.Format("I can't find the {0}", itemId);
        }
    }
}

