using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LunatiaProject.Map;
using LunatiaProject.LivingObject;


namespace LunatiaProject.Command
{
	public class MoveCommand : Command
	{
        // Fields
        private string _moveDirection;
        private Map.Path _path; 

		// Constructor
        public MoveCommand() : base(new string[] { "move", "go" }) { }

        // Method
        public override string Execute(Player p, string[] text)
        {
            // Default Error Message
            string errorMsg = "Path not exist. You stay at the same place";

            // Condition checking which path player want to move to
            switch (text.Length)
            {
                case 1:
                    return "Where do you want to move to?";

                case 2:
                    _moveDirection = text[1].ToLower();
                    break;

                case 3:
                    _moveDirection = text[2].ToLower();
                    break;

                default:
                    return errorMsg;
            }

            // Assign selected path to _path
            _path = p.Location.LocatePath(_moveDirection);

            // If _path is not null, mean player can locate this path and store in this field successfully
            if (_path != null)
            {
                if (_path.IsLocked)
                {
                    // Check if player hold a key item of that path or not
                    if (p.Inventory.HasItem(_path.Key.FirstId))
                    {
                        return MovePlayer(p, _path);
                    }
                    return "You can't travel through this path yet. What item could help you get through this path?";
                }
                return MovePlayer(p, _path);
            }
            return errorMsg;
        }

        public string MovePlayer(Player p, Map.Path path)
        {
            p.Move(path);
            return string.Format("You have moved to : {0}", p.Location.Name);
        }
   

    }
}

