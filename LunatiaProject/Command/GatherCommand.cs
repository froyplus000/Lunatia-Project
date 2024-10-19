using System;
using LunatiaProject.Core;
using LunatiaProject.Map;
using LunatiaProject.LivingObject;
using LunatiaProject.ItemAndInventory;
using static System.Net.Mime.MediaTypeNames;

namespace LunatiaProject.Command
{
	public class GatherCommand : Command
	{
        // Constructor - Support many input cases
        public GatherCommand() : base(new string[] { "gather", "chop", "mine", "break", "cut", "harvest"}) { }

        // Methods
        public override string Execute(Player p, string[] text)
        {
            int textLength = text.Length; // Text length
            for (int i = 0; i < textLength; i++)  // Ensure all input will be in Lowercase
            {
                text[i] = text[i].ToLower();
            }

            // Assign that we'll be working with Player current location
            Location location = p.Location; 
            // If object doesn't exist in the location, this will be null. Using for error handling
            GatherableObject gatherableObject = location.LocateGatherable(text.Last());
            // Default error message
            string errormsg = string.Format("What do you want to {0}?", text[0]);
            
            switch (textLength) // Switch case for readability
            {
                case (1):
                    return errormsg; 
                case (2):
                    // Check if the second word is these word
                    if (text.Last() == "a" | text.Last() == "an" | text.Last() == "the" | text.Last() == "from")
                    {
                        return errormsg;
                    }

                    // Perform Checking
                    if (gatherableObject == null)
                    {
                        return ObjectNotExistError(text);
                    }
                    if (CheckNotMatch(text))
                    {
                        return NotMatchError(text);
                    }
                    break;
                default:
                    // Perform Checking
                    if (gatherableObject == null)
                    {
                        return ObjectNotExistError(text);
                    }
                    if (CheckNotMatch(text))
                    {
                        return NotMatchError(text);
                    }
                    break;
            }
            // After passing all condition, Gather and Remove object from location
            p.Gather(gatherableObject);
            location.RemoveGatherable(gatherableObject);
            return string.Format("You gained {0} of {1} to your inventory", gatherableObject.ResourceAmount, gatherableObject.ResourceType.ToString());
        }

        // Returning Error message Methods

        // Avoid Duplication of returning Not Match Error message
        private string NotMatchError(string[] text)
        {
            return string.Format("You can't {0} a {1}. Try {0} other thing or use other word", text[0], text.Last());
        }

        // Avoid Duplication of returning Object doesn't exist in this location message
        private string ObjectNotExistError(string[] text)
        {
            return string.Format("{0} doesn't exist. Try looking what you can {1} here", text.Last(), text[0]);
        }

        // Conditions Method

        private bool CheckNotMatch(string[] text) // Avoid Duplication
        {
            // Chop only work for Tree
            if (text[0] == "chop" && text.Last() != "tree")
            {
                return true;
            }

            // Mine and Break only work for Rock
            if ((text[0] == "mine" || text[0] == "break") && text.Last() != "rock")
            {
                return true;
            }

            // Cut and Harvest only work for Rock
            if ((text[0] == "cut" || text[0] == "harvest") && text.Last() != "grass")
            {
                return true;
            }
            return false;
        }
    }
}

