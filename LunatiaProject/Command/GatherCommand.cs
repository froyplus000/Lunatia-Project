using System;
using LunatiaProject.Core;
using LunatiaProject.Map;

using LunatiaProject.LivingObject;

namespace LunatiaProject.Command
{
	public class GatherCommand : Command
	{
        // Fields

        // Constructor
        public GatherCommand() : base(new string[] { "gather" }) { }

        // Method
        public override string Execute(Player p, string[] text)
        {
            string errormsg = "What do you want to gather?";
            int textLength = text.Length;
            for (int i = 0; i < textLength; i++)  // Ensure all input will be in Lowercase
            {
                text[i] = text[i].ToLower();
            }
            Location location = p.Location;
            GatherableObject gatherableObject = null;

            // Get id of chosen objec to gather from user input
            switch (textLength)
            {
                case (1):
                    return errormsg;
                case (2):
                    // Locate object that exist in the location
                    gatherableObject = location.LocateGatherable(text[1]);
                    break;
            }

            if (gatherableObject != null)
            {

                p.Gather(gatherableObject);
                location.RemoveGatherable(gatherableObject);
                return string.Format("You gained {0} of {1} to your inventory", gatherableObject.ResourceAmount, gatherableObject.ResourceType.ToString());
            }
            return errormsg;



        }
    }
}

