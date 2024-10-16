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

        // Create locations and items
        Location myroom = new Location(new string[] { "myroom" }, "My Room", "Player's personal room");
        Item phone = new Item(new string[] { "phone" }, "iPhone", "iPhone 15 Pro Max");
        Item key = new Item(new string[] { "key" }, "Key", "Key to unlock kitchen door");
        myroom.Inventory.Put(phone);

        Location livingroom = new Location(new string[] { "livingroom" }, "Living Room", "Comfortable Living Room");
        Item tv = new Item(new string[] { "tv" }, "Television", "55 inch smart television");
        Item sofa = new Item(new string[] { "sofa" }, "Sofa", "Comfortable big Sofa");
        livingroom.Inventory.Put(tv);
        livingroom.Inventory.Put(sofa);

        Location kitchen = new Location(new string[] { "kitchen" }, "Kitchen Room", "Modern style Kitchen");
        Item knife = new Item(new string[] { "knife" }, "Japanese Knife", "High quality japanese knife");
        Item salt = new Item(new string[] { "salt" }, "Salt", "Normal Salt");
        kitchen.Inventory.Put(knife);
        kitchen.Inventory.Put(salt);

        Item airpod = new Item(new string[] { "airpod" }, "Airpod Pro", "2nd Generation AirPod Pro");

        // Test Gatherable Factory
        GatherableObjectFactory gatherableObjectFactory = new GatherableObjectFactory();
        List<GatherableObject> gatherableObjects = gatherableObjectFactory.CreateGatherableObject("Tree", 3);
        gatherableObjects.AddRange(gatherableObjectFactory.CreateGatherableObject("ROCK", 3)); // AddRange to add multiple object to this list
        gatherableObjects.AddRange(gatherableObjectFactory.CreateGatherableObject("GraSS", 3));
        foreach (GatherableObject gatherable in gatherableObjects)
        {
            livingroom.AddGatherable(gatherable);
        }

        // Create Paths
        Map.Path myroom2livingroom = new Map.Path(new string[] { "north" }, "Living room", "Walking path to living room in North direction", myroom, livingroom);
        Map.Path livingroom2myroom = new Map.Path(new string[] { "south" }, "My room", "Walking path to my room in South direction", livingroom, myroom);

        Map.Path myroom2kitchen = new Map.Path(new string[] { "west" }, "Kitchen", "Walking path to kitchen room in West direction", myroom, kitchen, key);
        Map.Path kitchen2myroom = new Map.Path(new string[] { "east" }, "My room", "Walking path to my room in East direction", kitchen, myroom);

        myroom.AddPath(myroom2livingroom);
        myroom.AddPath(myroom2kitchen);
        livingroom.AddPath(livingroom2myroom);
        kitchen.AddPath(kitchen2myroom);

        player.Location = myroom;
        player.Inventory.Put(airpod);
        player.Inventory.Put(key);

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
