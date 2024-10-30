using System;
using LunatiaProject.LivingObject;

namespace LunatiaProject.Command
{
	public class HelpCommand : Command
	{
        // Constructor
        public HelpCommand() : base(new string[] { "help" }) { }

        // Method
        public override string Execute(Player p, string[] text)
        {
            //string helpInfo = "Commands:\n";
            string helpInfo = "";
            // LookCommand
            helpInfo += "\n\tLook Command: you can look at many thing in the game.\n";
            helpInfo += "\t\tlook at me -> See your Inventory\n";
            helpInfo += "\t\tlook at here -> See your Surrounding\n";
            helpInfo += "\t\tlook at east -> See Path detail exist in your location.\n";
            helpInfo += "\t\tlook at recipebook -> See all Recipe in RecipeBook.\n";
            helpInfo += "\t\tlook at rope-r -> See specific Recipe in RecipeBook.\n";
            // MoveCommand
            helpInfo += "\n\tMove Command: you can move to location with exist path.\n";
            helpInfo += "\t\tmove uphill -> Move you to a new location based on exist path ID.\n";
            helpInfo += "\t\tgo downhill -> You can use go keyword as well.\n";
            // PickUpCommand
            helpInfo += "\n\tPick up Command: you can pick up item exist in your location.\n";
            helpInfo += "\t\tpick up flower -> Pick up exist item based on an item ID.\n";
            helpInfo += "\t\tcollect flower -> You can use collect keyword as well.\n";
            // DropCommand
            helpInfo += "\n\tDrop Command: you can drop item exist in your inventory.\n";
            helpInfo += "\t\tdrop sword -> drop an item based on an item ID.\n";
            // GatherCommand
            helpInfo += "\n\tGather Command: you can gather resources from exist gatherable object in the location.\n";
            helpInfo += "\t\tgather tree -> Gather any gatherable object with based on its ID.\n";
            helpInfo += "\t\tchop tree -> Chop can only be used for Tree\n";
            helpInfo += "\t\tmine rock -> Mine can only be used for Rock\n";
            helpInfo += "\t\tcut grass -> Cut can only be used for Grass\n";
            // CraftCommand
            helpInfo += "\n\tCraft Command: you can craft item from of exist recipe in recipe book.\n";
            helpInfo += "\t\tcraft strong rope -> Craft item based on its name\n";
            helpInfo += "\t\tcraft strongrope -> Craft item based on its ID\n";
            helpInfo += "\t\tcraft strongrope-r -> Craft item based on its Recipe ID\n";
            helpInfo += "\t\tNotes: You need required ingredients to craft that item.\n";
            return helpInfo;
        }
    }
}

