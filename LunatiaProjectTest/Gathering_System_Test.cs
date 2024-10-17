using LunatiaProject.LivingObject;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.Command;
using LunatiaProject.Map;
using LunatiaProject.Core;
using LunatiaProject.Factory;


namespace LunatiaProjectTest;

public class Gathering_System_Test
{
    private Player player;
    private Location forest;

    private Item potion;
    private Item phone;
    private Item knife;

    private Command commandProcessor;

    private GatherableObjectFactory gatherableObjectFactory;
    List<GatherableObject> gatherableObjects;



    [SetUp]
    public void Setup()
    {
        // location and items

        forest = new Location(new string[] { "forest" }, "Starter Forest", "a forest that player spawn in, this contain items and gatherable objects");
        phone = new Item(new string[] { "phone" }, "iPhone", "iPhone 15 Pro Max");
        knife = new Item(new string[] { "knife" }, "Japanese Knife", "High quality japanese knife");
        potion = new Item(new string[] { "potion" }, "Health Potion", "Restores health when consumed");
        forest.Inventory.Put(knife);
        forest.Inventory.Put(potion);

        player = new Player("Folk", "a Newbie Adventurer");
        player.Inventory.Put(phone);
        player.Location = forest;

        commandProcessor = new CommandProcessor();

        GatherableObjectFactory gatherableObjectFactory = new GatherableObjectFactory();
        // Create Gatherable Objects and add to a list of Gatherables variable
        gatherableObjects = gatherableObjectFactory.CreateGatherableObject("Tree", 1);
        gatherableObjects.AddRange(gatherableObjectFactory.CreateGatherableObject("Rock", 2));
        gatherableObjects.AddRange(gatherableObjectFactory.CreateGatherableObject("Grass", 2));
        // Add all gatherable object to location using this method
        forest.AddAllGatherable(gatherableObjects);
    }

    // Tests

    // Creating Gatherable Object using Factory
    [Test]
    public void TestCreateGatherableObjectUsingFactory()
    {
        Assert.That(forest.LocateGatherable("tree"), Is.Not.Null);
        Assert.That(forest.LocateGatherable("rock"), Is.Not.Null);
        Assert.That(forest.LocateGatherable("grass"), Is.Not.Null);
    }

    [Test]
    public void TestLocateNonExistingGatherableObject()
    {
        Assert.That(forest.LocateGatherable("bush"), Is.Null);
        string result = commandProcessor.Execute(player, new string[] { "chop", "bush" });
        string expected = "bush doesn't exist. Try looking what you can chop here";
        Assert.That(result, Is.EqualTo(expected));

    }
    [Test]
    public void TestGatherCommand()
    {
        commandProcessor.Execute(player, new string[] { "chop", "tree" });
        Assert.That(player.Inventory.HasItem("wood"), Is.True);
        Assert.That(forest.LocateGatherable("tree"), Is.Null);
    }

    [Test]
    public void TestItemAddedToPlayerInventory()
    {
        bool playerBefore = player.Inventory.HasItem("wood");
        commandProcessor.Execute(player, new string[] { "chop", "tree" });
        bool playerAfter = player.Inventory.HasItem("wood");

        Assert.That(playerBefore, Is.False);
        Assert.That(playerAfter, Is.True);
    }

    [Test]
    public void TestGatherableDepletion()
    {
        // Mine rock two times, so no more rock exist in the location
        commandProcessor.Execute(player, new string[] { "mine", "rock" });
        commandProcessor.Execute(player, new string[] { "mine", "rock" });
        Assert.That(forest.LocateGatherable("rock"), Is.Null);
    }
    [Test]
    public void TestChopOnlyWorkForTree()
    {
        // Mine Tree
        string mineTree = commandProcessor.Execute(player, new string[] { "mine", "tree" });
        string expectMineTree = "You can't mine a tree. Try mine other thing or use other word";
        Assert.That(mineTree, Is.EqualTo(expectMineTree));
        // Break Tree
        string breakTree = commandProcessor.Execute(player, new string[] { "break", "tree" });
        string expectBreakTree = "You can't break a tree. Try break other thing or use other word";
        Assert.That(breakTree, Is.EqualTo(expectBreakTree));
        // Cut Tree
        string cutTree = commandProcessor.Execute(player, new string[] { "cut", "tree" });
        string expectCutTree = "You can't cut a tree. Try cut other thing or use other word";
        Assert.That(cutTree, Is.EqualTo(expectCutTree));
        // Harvest Tree
        string harvestTree = commandProcessor.Execute(player, new string[] { "harvest", "tree" });
        string expectHarvestTree = "You can't harvest a tree. Try harvest other thing or use other word";
        Assert.That(harvestTree, Is.EqualTo(expectHarvestTree));
        // Chop tree
        commandProcessor.Execute(player, new string[] { "chop", "tree" });
        Assert.That(player.Inventory.HasItem("wood"), Is.True);
        Assert.That(forest.LocateGatherable("tree"), Is.Null);
    }

    [Test]
    public void TestMineAndBreakOnlyWorkForRock()
    {
        // Chop Rock
        string chopRock = commandProcessor.Execute(player, new string[] { "chop", "rock" });
        string expectChopRock = "You can't chop a rock. Try chop other thing or use other word";
        Assert.That(chopRock, Is.EqualTo(expectChopRock));

        // Cut Rock
        string cutRock = commandProcessor.Execute(player, new string[] { "cut", "rock" });
        string expectCutRock = "You can't cut a rock. Try cut other thing or use other word";
        Assert.That(cutRock, Is.EqualTo(expectCutRock));

        // Harvest Rock
        string harvestRock = commandProcessor.Execute(player, new string[] { "harvest", "rock" });
        string expectHarvestRock = "You can't harvest a rock. Try harvest other thing or use other word";
        Assert.That(harvestRock, Is.EqualTo(expectHarvestRock));

        // Mine Rock
        commandProcessor.Execute(player, new string[] { "mine", "rock" });
        Assert.That(player.Inventory.HasItem("stone"), Is.True); // Player have stone in inventory, gather successful

        // Break Rock
        commandProcessor.Execute(player, new string[] { "break", "rock" });
        Assert.That(forest.LocateGatherable("rock"), Is.Null); // No more rock in the location, gather successful
    }

    [Test]
    public void TestCutAndHarvestOnlyWorkForGrass()
    {
        // Chop Grass
        string chopGrass = commandProcessor.Execute(player, new string[] { "chop", "grass" });
        string expectChopGrass = "You can't chop a grass. Try chop other thing or use other word";
        Assert.That(chopGrass, Is.EqualTo(expectChopGrass));

        // Mine Grass
        string mineGrass = commandProcessor.Execute(player, new string[] { "mine", "grass" });
        string expectMineGrass = "You can't mine a grass. Try mine other thing or use other word";
        Assert.That(mineGrass, Is.EqualTo(expectMineGrass));

        // Break Grass
        string breakGrass = commandProcessor.Execute(player, new string[] { "break", "grass" });
        string expectBreakGrass = "You can't break a grass. Try break other thing or use other word";
        Assert.That(breakGrass, Is.EqualTo(expectBreakGrass));

        // Cut Grass
        commandProcessor.Execute(player, new string[] { "cut", "grass" });
        Assert.That(player.Inventory.HasItem("fiber"), Is.True);

        // Harvest Grass
        commandProcessor.Execute(player, new string[] { "harvest", "grass" });
        Assert.That(forest.LocateGatherable("grass"), Is.Null);
    }

    [Test]
    public void TestGatherWorkForAll()
    {
        commandProcessor.Execute(player, new string[] { "gather", "tree" });
        commandProcessor.Execute(player, new string[] { "gather", "rock" });
        commandProcessor.Execute(player, new string[] { "gather", "rock" });
        commandProcessor.Execute(player, new string[] { "gather", "grass" });
        commandProcessor.Execute(player, new string[] { "gather", "grass" });

        Assert.That(player.Inventory.HasItem("wood"), Is.True);
        Assert.That(player.Inventory.HasItem("stone"), Is.True);
        Assert.That(player.Inventory.HasItem("fiber"), Is.True);

        Assert.That(forest.LocateGatherable("tree"), Is.Null);
        Assert.That(forest.LocateGatherable("rock"), Is.Null);
        Assert.That(forest.LocateGatherable("grass"), Is.Null);
    }
}
