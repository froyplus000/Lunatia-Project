using System;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.LivingObject;
using LunatiaProject.Map;

namespace LunatiaProject.Command
{
	public class DropCommand : Command
	{
        // Construtor
        public DropCommand() : base(new string[] { "drop", "take" }) { }

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
                //case (1):
                //    return errormsg;
                case (2):

                    // "pick a", "collect an", "put the", "pick up"
                    if (text.Last() == "a" | text.Last() == "an" | text.Last() == "the")
                    {
                        return errormsg; // Return default error message
                    }

                    // Find Item in location, assign to variable
                    targetItem = p.Inventory.Fetch(text[1]);
                    return DropProcess(targetItem, location, text, p);
                case (3):

                    if (text[1] == "a" || text[1] == "an" || text[1] == "the")
                    {
                        // Find Item in location, assign to variable
                        targetItem = p.Inventory.Fetch(text[2]);
                        return DropProcess(targetItem, location, text, p);
                    }
                    return errormsg;

                default:
                    return errormsg;
            }
        }

        private string DropProcess(Item targetItem, Location location, string[] text, Player p)
        {
            if (targetItem == null)
            {
                return string.Format("Can't find {0} in {1}", text[1], location.Name);
            }
            // Put item in player inventory
            p.Inventory.Take(targetItem.FirstId);
            // Remove item from location inventory
            location.Inventory.Put(targetItem);
            return string.Format("{0} had been removed from your inventory", targetItem.Name);
        }

    }
}

