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
        Console.WriteLine("-------------------------");
        Console.WriteLine("Welcome to Adventurer!");
        Console.WriteLine("-------------------------");
        // Get player's name and description from user input
        Console.WriteLine("\nEnter player's name:");
        string playerName = Console.ReadLine();
        Console.WriteLine("Enter player's description:");
        string playerDescription = Console.ReadLine();

        // Create Player object with these Inputs
        Player player = new Player(playerName, playerDescription);

        // SETUP
        LocationFactory locationFactory = new LocationFactory();
        StoryManager storyManager = new StoryManager("../../../Data/StoryNew.txt");
        GameInitializer gameInitializer = new GameInitializer(player, storyManager);
        Location luminara = locationFactory.CreateLocations("luminara", "Luminara City of Light", "A radiant city hidden from ordinary sight. Only those who follow the ancient rituals may witness its splendor, bathed in eternal light and mystery.");
        gameInitializer.StartGame();

        Command.Command commandProcessor = new CommandProcessor();

        // Loop reading command, This will continuous asking player for command input
        while (true)
        {
            // Completed the game check
            if (player.Location.Name == "Lunatia City Centre" && player.Location.Inventory.HasItem("license") && player.Location.Inventory.HasItem("clearpotion"))
            {
                player.Location = luminara;
                storyManager.CheckStory(player);
                Console.WriteLine("\nCongratulations, you had completed Lunatia Project Demo. Thank you for playing");
                Console.ReadKey();
                return;
            }
            storyManager.CheckStory(player);
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
