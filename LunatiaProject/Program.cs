using LunatiaProject.Command;
using LunatiaProject.LivingObject;
using LunatiaProject.Map;
using LunatiaProject.Factory;
using LunatiaProject.ItemAndInventory;



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
        StoryManager storyManager = new StoryManager("../../../Data/Story.txt");
        GameInitializer gameInitializer = new GameInitializer(player, storyManager);
        Location luminara = locationFactory.CreateLocations("luminara", "Luminara City of Light", "A radiant city hidden from ordinary sight. Only those who follow the ancient rituals may witness its splendor, bathed in eternal light and mystery.");
        gameInitializer.StartGame();

        // THIS IS WHERE TEST SINGLETON RECIPEBOOK FAILED IN THE INTERVIEW
        RecipeBook recipebook = RecipeBook.Instance;
        // Load all recipe from file
        recipebook = RecipeBook.Instance;
        string recipeFilePath = "../../../Data/RecipesData.json";
        RecipeFactory recipeFactory = new RecipeFactory(recipeFilePath);
        List<Recipe> allRecipes = recipeFactory.CreateRecipe();
        // Add all to RecipeBook
        recipebook.AddAllRecipe(allRecipes);
        // Put recipeBook into player inventory
        player.Inventory.Put(recipebook);

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
                    "look at uphill", // See path to upper, ladder required
                    "move uphill", // can't travel message 
                    "look at east", // look east to see path that not lock
                    "move east",
                    "look at here",
                    "look at east", // can see that entrance requires License
                    "move west", // Forest again
                    "look at recipebook", // Recipe book 
                    "look at ladder-r", // ladder recipe
                    "gather tree", // gathering command demo
                    "gather rock",
                    "gather grass",
                    "chop tree",
                    "mine rock",
                    "break rock",
                    "cut grass",
                    "harvest grass",
                    "harvest tree",
                    "chop rock",
                    "break grass",
                    "look at me", // Player inventory
                    "look at ladder-r",
                    "craft rope-r", // Crafting
                    "craft rope",
                    "craft Rope",
                    "craft Ladder",
                    "go uphill",
                    "look around",
                    "look at license in lunatiaForestUpper", // Look at item in location
                    "collect license",
                    "go downhill",
                    "look at downhill",
                    "look at me",
                    "craft Rope",
                    "craft Strong Rope",
                    "go downhill",
                    "look around",
                    "look at note in lunatiaForestLower",
                    "collect note",
                    "collect lionragweed", // Collect flowers
                    "collect goldenglow",
                    "collect glons",
                    "look at lionragweed", // Look at flowers
                    "look at goldenglow",
                    "look at glons",
                    "look at clearpotion-r",
                    "craft Clear Potion", // Craft potion
                    "go uphill",
                    "go east",
                    "go east",
                    "look around",
                    "go east", // City Centre
                    "look around",
                    "drop license",
                    "drop clearpotion", // Game Completed
                    "exit"
                };
                // Loop for to progress each string in the list
                for (int i = 0; i < demoInputs.Length; i++)
                {

                    // If the input is "quit" means the demo had ended.
                    if (demoInputs[i] == "exit")
                    {
                        //Console.WriteLine("\nDemo Input : " + demoInputs[i]);
                        Console.WriteLine(string.Format("\n[ {0} ]\n", demoInputs[i]));
                        Console.WriteLine("\nThe Demo has Ended. Press any key to close a program");
                        Console.ReadKey();
                        return;
                    }

                    // Process the command in the list
                    string[] demoCommardParts = demoInputs[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine(string.Format("\n[ {0} ]\n", demoInputs[i]));
                    Console.WriteLine(commandProcessor.Execute(player, demoCommardParts));
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
