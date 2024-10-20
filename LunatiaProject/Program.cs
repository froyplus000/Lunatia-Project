using LunatiaProject.Command;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.LivingObject;
using LunatiaProject.Map;
using LunatiaProject.Factory;
using LunatiaProject.Core;



namespace LunatiaProject;

class Program
{
    static void Main(string[] args)
    {
        // Get player's name and description from user input
        Console.WriteLine("Enter player's name:");
        string playerName = Console.ReadLine();
        Console.WriteLine("Enter player's description:");
        string playerDescription = Console.ReadLine();

        // Create Player object with these Inputs
        Player player = new Player(playerName, playerDescription);

        // SETUP

        GameInitializer gameInitializer = new GameInitializer(player);
        gameInitializer.StartGame();

        Command.Command commandProcessor = new CommandProcessor();

        // Loop reading command, This will continuous asking player for command input
        while (true)
        {
            Console.WriteLine("\nEnter a command:");
            string input = Console.ReadLine().ToLower();
            // split each word in sentence and add in to list of string
            string[] commandParts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (commandParts[0] != "quit")
            {
                Console.WriteLine(commandProcessor.Execute(player, commandParts));
            }
            else
            {
                Console.WriteLine("\nThank you for Playing. Press any key to close a program");
                Console.ReadKey();
                break;
            }
        }
    }
}
