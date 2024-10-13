using System.Numerics;
using Swin_Adventure_Iteration_8;
namespace TestIdentifiableObject;

public class Iteration_6_Test
{
    private Player player;
    private Bag bag;
    private Item tree;
    private Item rock;
    private LookCommand lookCommand;
    private Location forest;

    [SetUp]
    public void Setup()
    {
        player = new Player("Folk", "Newbie Adventurer");
        bag = new Bag(new string[] { "bag" }, "Bag", "A sturdy leather bag for carrying items");
        tree = new Item(new string[] { "tree" }, "Tree", "A tall oak tree");
        rock = new Item(new string[] { "rock" }, "Rock", "A large rock");
        lookCommand = new LookCommand();
        forest = new Location(new string[] { "forest" }, "Forest", "A dense forest");
        // Assign forest as a current location of player
        player.Location = forest;
        // Assign tree and rock to forest
        forest.Inventory.Put(tree);
        forest.Inventory.Put(rock);
    }

    // Tests

    [Test]
    public void TestForestLocateSelf() // Location can locate itself
    {
        Assert.That(forest.AreYou("forest"), Is.True);
    }

    [Test]
    public void TestForestLocateItem() // Location can locate item in it's inventory
    {
        GameObject foundTree = forest.Locate("tree");
        GameObject foundRock = forest.Locate("rock");
        Assert.That(foundTree.Name, Is.EqualTo(tree.Name));
        Assert.That(foundRock.Name, Is.EqualTo(rock.Name));
    }

    [Test]
    public void TestPlayerLocateItemInForest() // Player can locate item in their current location
    {
        string[] lookAtTreeInForest = { "Look", "at", "tree", "in", "forest" };
        string result = lookCommand.Execute(player, lookAtTreeInForest);
        Assert.That(result, Is.EqualTo(tree.FullDescription));
    }

    [Test]
    public void TestPlayerLookAtCurrentLocation() // Player can locate where they are
    {
        string[] lookAtHere = { "look", "at", "here" };
        string result = lookCommand.Execute(player, lookAtHere);
        string expected = player.Location.FullDescription;

        Assert.That(result, Is.EqualTo(expected));
    }

    // Fixed test to see if input from test file had been correctly handled
    [Test]
    public void TestCaseSensitiveInputHandling()
    {
        string[] lookAtRockInForest = { "Look", "aT", "RoCk", "IN", "FoReSt" };
        string result = lookCommand.Execute(player, lookAtRockInForest);
        Assert.That(result, Is.EqualTo(rock.FullDescription));
    }
}
