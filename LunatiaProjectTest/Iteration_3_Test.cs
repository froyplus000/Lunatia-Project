using System.Numerics;
using LunatiaProject.Core;
using LunatiaProject.ItemAndInventory;

namespace LunatiaProjectTest;

public class Iteration_3_Test
{

    private Bag bag;
    private Item potion;
    private Bag smallBag;
    private Item key;

    [SetUp]
    public void Setup()
    {
        bag = new Bag(new string[] { "bag" }, "leather bag", "A sturdy leather bag for carrying items");
        potion = new Item(new string[] { "potion" }, "Health Potion", "Restores health when consumed");
        key = new Item(new string[] { "key" }, "Golden Key", "Unlocks mysterious doors");
        smallBag = new Bag(new string[] { "smallbag" }, "small bag", "A smaller bag to store little things");
    }

    // Tests
    [Test]
    public void TestBagLocatesItems()
    {
        bag.Inventory.Put(potion);
        GameObject locatedItem = bag.Locate("potion");
        Assert.That(locatedItem, Is.EqualTo(potion));
    }
    [Test]
    public void TestBagLocatesSelf()
    {
        GameObject locatedItem = bag.Locate("bag");
        Assert.That(locatedItem, Is.EqualTo(bag));
    }
    [Test]
    public void TestBagLocatesNothing()
    {
        GameObject locatedItem = bag.Locate("sword");
        Assert.That(locatedItem, Is.Null);
    }
    [Test]
    public void TestBagFullDescirption()
    {
        bag.Inventory.Put(potion);
        bag.Inventory.Put(key);
        string expectedDescription = string.Format("In the {0} you can see:\n\t1 x {1}\n\t1 x {2}\n", bag.Name, potion.ShortDescription, key.ShortDescription);
        Assert.That(bag.FullDescription, Is.EqualTo(expectedDescription));
    }
    [Test]
    public void TestBagInBag()
    {
        bag.Inventory.Put(smallBag);
        GameObject locatedBag = bag.Locate("smallbag");
        Assert.That(locatedBag, Is.EqualTo(smallBag)); // Bag can locate smaller bag

        bag.Inventory.Put(potion);
        GameObject locatedItem = bag.Locate("potion");
        Assert.That(locatedItem, Is.EqualTo(potion)); // Bag can locate item in bag

        smallBag.Inventory.Put(key); // Put key in smaller bag
        GameObject locateKey = bag.Locate("key");
        Assert.That(locateKey, Is.Null); // Bag can't locate Key, so it return Null
    }
    [Test]
    public void TestBagPrivilegedItem()
    {
        bag.Inventory.Put(smallBag);
        key.PrivilegeEscalation("3220"); // Make key Privilege item
        smallBag.Inventory.Put(key);

        Assert.That(key.FirstId, Is.EqualTo("25")); // Make sure Key is Privilege item
        Assert.That(smallBag.Inventory.HasItem("25"), Is.True); // Make sure that Small bag has Privilege Item

        GameObject locatePrivilege = bag.Locate("25");
        Assert.That(locatePrivilege, Is.Null); // Bag can't locate Privilege item, so it return Null
    }
}
