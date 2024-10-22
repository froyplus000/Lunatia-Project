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

            string input = "";
            do
            {
                Console.WriteLine("\nEnter a command:");
                input = Console.ReadLine().ToLower();
                // split each word in sentence and add in to list of string

            } while (input == "");
             string[] commandParts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // DEMO
            if (commandParts[0] == "demo")
            {
                // List of demo input string
                string[] demoInputs = new string[] {
                    "help",
                    "look at me",
                    "look at here",
                    "move uphill",
                    "look at uphill",
                    "look at east",
                    "look at downhill",
                    "go east",
                    "look at here",
                    "go east",
                    "look at east",
                    //"",
                    //"",
                    //"",
                    "exit"
                };
                // Loop for to progress each string in the list
                for (int i = 0; i < demoInputs.Length; i++)
                {
                    // If the input is "quit" means the demo had ended.
                    if (demoInputs[i] == "exit")
                    {
                        Console.WriteLine("\nDemo Input : " + demoInputs[i]);
                        Console.WriteLine("\nThe Demo has Ended. Press any key to close a program");
                        Console.ReadKey();
                        return;
                    }
                    // Process the command in the list
                    string[] demoCommardParts = demoInputs[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("\nDemo Input : " + demoInputs[i]);
                    Console.WriteLine(commandProcessor.Execute(player, demoCommardParts));
                    Console.ReadKey(); // Press any key to perform next command
                }
                
            }
            // NORMAL
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
