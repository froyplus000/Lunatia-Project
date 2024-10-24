using LunatiaProject.LivingObject;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.Command;
using LunatiaProject.Map;

namespace LunatiaProjectTest;

public class Iteration_8_Test
{
    private Player player;
    private Location myroom;
    private Location livingroom;
    private Location kitchen;

    private Bag bag;

    private Item potion;
    private Item key;
    private Item phone;
    private Item tv;
    private Item knife;

    private Command commandProcessor;


    private LunatiaProject.Map.Path myroom2livingroom;
    private LunatiaProject.Map.Path livingroom2myroom;

    private LunatiaProject.Map.Path myroom2kitchen;
    private LunatiaProject.Map.Path kitchen2myroom;

    [SetUp]
    public void Setup()
    {
        // location and items
        key = new Item(new string[] { "key" }, "Key", "Key to unlock kitchen door");
        myroom = new Location(new string[] { "myroom" }, "My Room", "Player's personal room");
        phone = new Item(new string[] { "phone" }, "iPhone", "iPhone 15 Pro Max");
        myroom.Inventory.Put(phone);
        livingroom = new Location(new string[] { "livingroom" }, "Living Room", "Comfortable Living Room");
        tv = new Item(new string[] { "tv" }, "Television", "55 inch smart television");
        livingroom.Inventory.Put(tv);
        kitchen = new Location(new string[] { "kitchen" }, "Kitchen Room", "Modern style Kitchen");
        knife = new Item(new string[] { "knife" }, "Japanese Knife", "High quality japanese knife");
        kitchen.Inventory.Put(knife);
        bag = new Bag(new string[] { "bag" }, "leather bag", "A sturdy leather bag for carrying items");
        potion = new Item(new string[] { "potion" }, "Health Potion", "Restores health when consumed");
        bag.Inventory.Put(potion);

        myroom2livingroom = new LunatiaProject.Map.Path(new string[] { "north"}, "to living room", "Walking path to living room in North direction", myroom, livingroom);
        livingroom2myroom = new LunatiaProject.Map.Path(new string[] { "south" }, "to my room", "Walking path to my room in South direction", livingroom, myroom);

        myroom2kitchen = new LunatiaProject.Map.Path(new string[] { "west" }, "to kitchen", "Walking path to kitchen room in West direction", myroom, kitchen, key); // Add key to set this Path to lock and what key to unlock
        kitchen2myroom = new LunatiaProject.Map.Path(new string[] { "east" }, "to my room", "Walking path to my room in East direction", kitchen, myroom);

        myroom.AddPath(myroom2livingroom);
        myroom.AddPath(myroom2kitchen);
        livingroom.AddPath(livingroom2myroom);
        kitchen.AddPath(kitchen2myroom);

        player = new Player("Folk", "ComSci Student");
        player.Location = myroom;
        player.Inventory.Put(key);

        commandProcessor = new CommandProcessor();
    }

    // Tests

    [Test]
    public void TestCommandProcessorExecution_MoveCommandWithMove()
    {
        commandProcessor.Execute(player, new string[] { "move", "north" });
        Assert.That(livingroom, Is.EqualTo(player.Location));
    }
    [Test]
    public void TestCommandProcessorExecution_MoveCommandWithGo()
    {
        commandProcessor.Execute(player, new string[] { "go", "north" });
        Assert.That(livingroom, Is.EqualTo(player.Location));
    }
    [Test]
    public void TestCommandProcessorExecution_MoveCommandWithLenght3()
    {
        commandProcessor.Execute(player, new string[] { "go", "to", "north" });
        Assert.That(livingroom, Is.EqualTo(player.Location));
    }
    [Test]
    public void TestCommandProcessorExecution_MoveCommand_MultipleTimes()
    {
        commandProcessor.Execute(player, new string[] { "move", "to", "north" });
        Assert.That(livingroom, Is.EqualTo(player.Location));
        commandProcessor.Execute(player, new string[] { "go", "south" });
        Assert.That(myroom, Is.EqualTo(player.Location));

        commandProcessor.Execute(player, new string[] { "move", "west" });
        Assert.That(kitchen, Is.EqualTo(player.Location));
        commandProcessor.Execute(player, new string[] { "go", "to", "east" });
        Assert.That(myroom, Is.EqualTo(player.Location));
    }
    [Test]
    public void TestCommandProcessorExecution_LookCommand_AtMe()
    {
        string[] command = { "look", "at", "me" };
        string result = commandProcessor.Execute(player, command);
        Assert.That(result, Is.EqualTo(player.FullDescription));
    }
    [Test]
    public void TestCommandProcessorExecution_LookCommand_AtKey()
    {
        player.Inventory.Put(key);
        string[] command = { "look", "at", "key" };
        string result = commandProcessor.Execute(player, command);
        Assert.That(result, Is.EqualTo(key.FullDescription));
    }
    [Test]
    public void TestCommandProcessorExecution_LookCommand_AtItemInLocation()
    {
        player.Inventory.Put(key);
        string[] command = { "look", "at", "phone", "in", "myroom" };
        string result = commandProcessor.Execute(player, command);
        Assert.That(result, Is.EqualTo(phone.FullDescription));
    }
    [Test]
    public void TestCommandProcessorExecution_LookCommand_AtItemInBag()
    {
        player.Inventory.Put(bag);
        string[] command = { "look", "at", "potion", "in", "bag" };
        string result = commandProcessor.Execute(player, command);
        Assert.That(result, Is.EqualTo(potion.FullDescription));
    }
    [Test]
    public void TestCommandProcessorExecution_LookCommand_AtNoGemInBag()
    {
        player.Inventory.Put(bag);
        string[] command = { "look", "at", "gem", "in", "bag" };
        string result = commandProcessor.Execute(player, command);
        Assert.That(result, Is.EqualTo("I can't find the gem"));
    }
    [Test]
    public void TestCommandProcessorExecution_LookCommand_InvalidLook() // To test all errors conditions
    {
        // Return error message when input is shorter than 3 words
        string[] outOfLenght = { "look", "where" };
        string result1 = commandProcessor.Execute(player, outOfLenght);
        Assert.That(result1, Is.EqualTo("I don't know how to look like that"));

        string[] hello = { "hello", "103883220" };
        string result2 = commandProcessor.Execute(player, hello);
        Assert.That(result2, Is.EqualTo("Error Command Input, Please enter your command again."));

        // Return error message when first word isn't "look"
        string[] hi = { "hi", "at", "103883220" };
        string result3 = commandProcessor.Execute(player, hi);
        Assert.That(result3, Is.EqualTo("Error Command Input, Please enter your command again."));

        // Return error message when second word isn't "at"
        string[] commandNoAt = { "look", "for", "gem" };
        string result4 = commandProcessor.Execute(player, commandNoAt);
        Assert.That(result4, Is.EqualTo("What do you want to look at?"));

        // Return error message when forth word isn't "in"
        string[] commandNoIn = { "look", "at", "gem", "of", "bag" };
        string result5 = commandProcessor.Execute(player, commandNoIn);
        Assert.That(result5, Is.EqualTo("What do you want to look in?"));

        // Return error message when second word isn't "at"
        string[] lookAtFolk = { "look", "at", "folk" };
        string result6 = commandProcessor.Execute(player, lookAtFolk);
        Assert.That(result6, Is.EqualTo("I can't find the folk"));

    }
}
