using System.Numerics;
using Swin_Adventure_Iteration_8;
namespace TestIdentifiableObject;

public class Iteration_4_Test
{
    private Player player;
    private Bag bag;
    private Item gem;
    private LookCommand lookCommand;

    // Fixed from iteration 6
    private Location forest; 

    [SetUp]
    public void Setup()
    {
        player = new Player("Folk", "Newbie Adventurer");
        bag = new Bag(new string[] { "bag" }, "leather bag", "A sturdy leather bag for carrying items");
        gem = new Item(new string[] { "gem" }, "Gem", "Shiny Blue Gem");
        lookCommand = new LookCommand();

        // Fixed from iteration 6
        forest = new Location(new string[] { "forest" }, "Forest", "A dense forest");
        player.Location = forest;
    }

    // Tests

    [Test]
    public void TestLookAtMe()
    {
        string[] command = { "look", "at", "inventory" };
        string result = lookCommand.Execute(player, command);
        Assert.That(result, Is.EqualTo(player.FullDescription));
    }

    [Test]
    public void TestLookAtGem()
    {
        player.Inventory.Put(gem);
        string[] command = { "look", "at", "gem" };
        string result = lookCommand.Execute(player, command);
        Assert.That(result, Is.EqualTo(gem.FullDescription));
    }
    [Test]
    public void TestLookAtUnk()
    {
        string[] command = { "look", "at", "gem" };
        string result = lookCommand.Execute(player, command);
        Assert.That(result, Is.EqualTo("I can't find the gem"));
    }
    [Test]
    public void TestLookAtGemInMe()
    {
        player.Inventory.Put(gem);
        string[] command = { "look", "at", "gem", "in", "inventory" };
        string result = lookCommand.Execute(player, command);
        Assert.That(result, Is.EqualTo(gem.FullDescription));
    }
    [Test]
    public void TestLookAtGemInBag()
    {
        bag.Inventory.Put(gem);
        player.Inventory.Put(bag);
        string[] command = { "look", "at", "gem", "in", "bag" };
        string result = lookCommand.Execute(player, command);
        Assert.That(result, Is.EqualTo(gem.FullDescription));
    }
    [Test]
    public void TestLookAtGemInNoBag()
    {
        string[] command = { "look", "at", "gem", "in", "bag" };
        string result = lookCommand.Execute(player, command);
        Assert.That(result, Is.EqualTo("I can't find the bag"));
    }
    [Test]
    public void TestLookAtNoGemInBag()
    {
        player.Inventory.Put(bag);
        string[] command = { "look", "at", "gem", "in", "bag" };
        string result = lookCommand.Execute(player, command);
        Assert.That(result, Is.EqualTo("I can't find the gem"));
    }
    [Test]
    public void TestInvalidLook() // To test all errors conditions
    {
        // Return error message when input is shorter than 3 words
        string[] outOfLenght = { "look", "around" };
        string result1 = lookCommand.Execute(player, outOfLenght);
        Assert.That(result1, Is.EqualTo("I don't know how to look like that"));

        string[] hello = { "hello", "103883220" };
        string result2 = lookCommand.Execute(player, hello);
        Assert.That(result2, Is.EqualTo("I don't know how to look like that"));

        // Return error message when first word isn't "look"
        string[] hi = { "hi","at", "103883220" };
        string result3 = lookCommand.Execute(player, hi);
        Assert.That(result3, Is.EqualTo("Error in look input"));

        // Return error message when second word isn't "at"
        string[] commandNoAt = { "look", "for", "gem"};
        string result4 = lookCommand.Execute(player, commandNoAt);
        Assert.That(result4, Is.EqualTo("What do you want to look at?"));

        // Return error message when forth word isn't "in"
        string[] commandNoIn = { "look", "at", "gem", "of", "bag" };
        string result5 = lookCommand.Execute(player, commandNoIn);
        Assert.That(result5, Is.EqualTo("What do you want to look in?"));

        // Return error message when second word isn't "at"
        string[] lookAtFolk = { "look", "at", "folk" };
        string result6 = lookCommand.Execute(player, lookAtFolk);
        Assert.That(result6, Is.EqualTo("I can't find the folk"));

    }
}
