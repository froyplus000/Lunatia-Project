using System;
using LunatiaProject.Map;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.LivingObject;

namespace LunatiaProject.Command
{
	public class PickUpCommand : Command
	{
		// Construtor
		public PickUpCommand() : base(new string[] { "pick","pickup", "collect", "put"}) { }

        public override string Execute(Player p, string[] text)
		{
            int textLength = text.Length; // Text length
            for (int i = 0; i < textLength; i++)  // Ensure all input will be in Lowercase
            {
                text[i] = text[i].ToLower();
            }


            // Default Error message
            string errormsg = string.Format("What do you want to {0}?", text[0]);
            Location location = p.Location;
            Item targetItem;

            switch (textLength)
            {
                case (2):

                    // "pick a", "collect an", "put the", "pick up"
                    if (text.Last() == "a" | text.Last() == "an" | text.Last() == "the" | text.Last() == "up")
                    {
                        return errormsg; // Return default error message
                    }

                    // Find Item in location, assign to variable
                    targetItem = location.Inventory.Fetch(text[1]);
                    return PickUpProcess(targetItem, location, text, p);
                case (3):

                    if (text[1] == "up" || text[1] == "a" || text[1] == "an" || text[1] == "the")
                    {
                        // Find Item in location, assign to variable
                        targetItem = location.Inventory.Fetch(text[2]);
                        return PickUpProcess(targetItem, location, text, p);
                    }
                    return errormsg;

                default:
                    return errormsg;
            }
		}

        private string PickUpProcess(Item targetItem, Location location, string[] text, Player p)
        {
            if (targetItem == null)
            {
                return string.Format("Can't find {0} in {1}", text[1], location.Name);
            }
            // Put item in player inventory
            p.Inventory.Put(targetItem);
            // Remove item from location inventory
            location.Inventory.Take(targetItem.FirstId);
            return string.Format("{0} had been added to your inventory", targetItem.Name);
        }
    }
}

