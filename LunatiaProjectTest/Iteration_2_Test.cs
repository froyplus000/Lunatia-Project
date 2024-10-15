using System.Numerics;
using LunatiaProject.Core;
using LunatiaProject.LivingObject;
using LunatiaProject.ItemAndInventory;

namespace LunatiaProjectTest;

public class Iteration_2_Test
{
    private Player p;
    private Inventory pi;
    private Item sword;
    private Item shield;

    [SetUp]
    public void Setup()
    {
        p = new Player("Fred", "Rank A Adventurer");
        pi = p.Inventory;
        sword = new Item(new string[] { "sword" }, "Sword", "Training Sword");
        shield = new Item(new string[] { "shield" }, "Shield", "Protective Shield");
    }

    // Item Unit Tests
    [Test]
    public void TestIdentifiable()
    {
        Assert.That(sword.AreYou("sword"));
    }
    [Test]
    public void TestShortDescription()
    {
        Assert.That(sword.ShortDescription, Is.EqualTo("a Sword (sword)"));
    }
    [Test]
    public void TestFullDescription()
    {
        Assert.That(sword.FullDescription, Is.EqualTo("Training Sword"));
    }
    [Test]
    public void TestPrivilegeEscalation()
    {
        string beforePE = sword.FirstId; // first id = "sword"
        string expectedID = "25";
        sword.PrivilegeEscalation("3220"); // First id should now be = 25
        string afterPE = sword.FirstId;
        Assert.That(afterPE, Is.EqualTo(expectedID));
        Assert.That(afterPE, Is.Not.EqualTo(beforePE));
    }

    // Inventory Tests
    [Test]
    public void TestFindItem()
    {
        pi.Put(sword);
        Item foundItem = pi.Fetch("sword");
        Assert.That(foundItem, Is.Not.Null);
        Assert.That(foundItem.AreYou("sword"), Is.True);
    }
    [Test]
    public void TestNoItemFind()
    {
        pi.Put(sword);
        Item foundItem = pi.Fetch("club");
        Assert.That(foundItem, Is.Null);
    }
    [Test]
    public void TestFetchItem()
    {
        pi.Put(sword);
        Item fetchedItem = pi.Fetch("sword");
        Assert.That(fetchedItem.FirstId, Is.EqualTo("sword"));
        Assert.That(pi.HasItem("sword"), Is.True);
    }
    [Test]
    public void TestTakeItem()
    {
        pi.Put(sword);
        Item fetchedItem = pi.Fetch("sword");
        Assert.That(fetchedItem.FirstId, Is.EqualTo("sword"));
        pi.Take("sword");
        Assert.That(pi.HasItem("sword"), Is.False);
    }
    [Test]
    public void TestItemList()
    {
        pi.Put(sword);
        pi.Put(shield);
        string itemList = pi.ItemList;
        string expectedItemList = "\ta Sword (sword)\n\ta Shield (shield)\n";
        Assert.That(itemList, Is.EqualTo(expectedItemList));
    }

    // Player Tests
    [Test]
    public void TestPlayerIdentifiable()
    {
        Assert.That(p.AreYou("me"), Is.True);
        Assert.That(p.AreYou("inventory"), Is.True);
    }
    [Test]
    public void TestPlayerLocateItem()
    {
        pi.Put(sword);
        GameObject locatedObject = p.Locate("sword"); // Player can locate Sword
        Assert.That(locatedObject.FirstId, Is.EqualTo("sword")); // Return object is actually Sword
        Assert.That(pi.HasItem("sword"), Is.True); //  Player Inventory still have Sword
    }
    [Test]
    public void TestPlayerLocateSelf()
    {
        GameObject locatedplayerMe = p.Locate("me"); // Locate with 'me'
        GameObject locatedplayerInv = p.Locate("inventory"); // Locate with 'inventory'
        Assert.That(locatedplayerMe.Name, Is.EqualTo("Fred")); // Located player is Fred
        Assert.That(locatedplayerInv.Name, Is.EqualTo("Fred")); // Located player is Fred
        Assert.That(p, Is.EqualTo(locatedplayerMe));
        Assert.That(p, Is.EqualTo(locatedplayerInv));
    }
    [Test]
    public void TestPlayerLocateNothing()
    {
        GameObject locatedplayer = p.Locate("John");
        Assert.That(locatedplayer, Is.Null);
    }
    [Test]
    public void TestPlayerFullDescription()
    {
        pi.Put(sword);
        pi.Put(shield);
        string fullDescription = p.FullDescription;
        string expectedDescription = "You are Fred Rank A Adventurer\nYou are carrying\n\ta Sword (sword)\n\ta Shield (shield)\n";
        Assert.That(fullDescription, Is.EqualTo(expectedDescription));
    }
}
