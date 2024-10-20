using LunatiaProject.LivingObject;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.Command;
using LunatiaProject.Map;
using LunatiaProject.Core;
using LunatiaProject.Factory;
using LunatiaProject.Interfaces;


namespace LunatiaProjectTest;

public class Crafting_System_Test
{

    private RecipeFactory recipeFactory;
    private RecipeBook recipeBook;
    private List<Recipe> recipes;
    private string filepath;

    private Player player;
    private Location forest;

    private Item potion;
    private Item phone;
    private Item knife;

    ItemFactory itemFactory;
    List<Item> items;

    private Command commandProcessor;

    [SetUp]
    public void Setup()
    {
        // Recipes
        filepath = "../../../Data/RecipesTestData.json";
        recipeFactory = new RecipeFactory(filepath);
        recipeBook = RecipeBook.Instance;
        recipes = new List<Recipe>();

        recipes = recipeFactory.CreateRecipe();
        recipeBook.AddAllRecipe(recipes);

        // Location and Items

        itemFactory = new ItemFactory("../../../Data/ItemsData.json");
        items = itemFactory.CreateItems("wood", "Wood", "A piece of wood.", 2);
        //items.AddRange(itemFactory.CreateItems("fiber", "Fiber", "A piece of stone.", 4));
        items.AddRange(itemFactory.CreateItems("fiber", "Fiber", "A piece of fiber.", 1));

        forest = new Location(new string[] { "forest" }, "Starter Forest", "a forest that player spawn in, this contain items and gatherable objects");
        phone = new Item(new string[] { "phone" }, "iPhone", "iPhone 15 Pro Max");
        knife = new Item(new string[] { "knife" }, "Japanese Knife", "High quality japanese knife");
        potion = new Item(new string[] { "potion" }, "Health Potion", "Restores health when consumed");
        forest.Inventory.Put(knife);
        forest.Inventory.Put(potion);

        player = new Player("Folk", "a Newbie Adventurer");
        player.Inventory.Put(phone);
        player.Inventory.Put(recipeBook);
        player.Location = forest;

        commandProcessor = new CommandProcessor();
    }

    // Tests

    // TestCraftWithItemName
    [Test]
    public void TestCraftWithItemName()
    {
        // Put resources in Player inventory
        player.Inventory.PutMultipleItems(items);
        string result = commandProcessor.Execute(player, new string[] { "craft", "Wooden", "Sword" });
        Assert.That(player.Inventory.HasItem("woodensword"), Is.True);

    }

    // TestCraftWithItemId
    [Test]
    public void TestCraftWithItemId()
    {
        // Put resources in Player inventory
        player.Inventory.PutMultipleItems(items);
        string result = commandProcessor.Execute(player, new string[] { "craft", "woodensword" });
        Assert.That(player.Inventory.HasItem("woodensword"), Is.True);

    }
    // TestCraftWithRecipeId
    [Test]
    public void TestCraftWithRecipeId()
    {
        // Put resources in Player inventory
        player.Inventory.PutMultipleItems(items);
        string result = commandProcessor.Execute(player, new string[] { "craft", "woodensword-r" });
        Assert.That(player.Inventory.HasItem("woodensword"), Is.True);
    }
    // TestCraftErrorMessage
    [Test]
    public void TestCraftErrorMessage()
    {
        // Put resources in Player inventory
        player.Inventory.PutMultipleItems(items);
        string result1 = commandProcessor.Execute(player, new string[] { "craft", "a" });
        string result2 = commandProcessor.Execute(player, new string[] { "craft", "the" });
        string result3 = commandProcessor.Execute(player, new string[] { "craft", "that" });
        string expected = "What do you want to craft?";
        Assert.That(result1, Is.EqualTo(expected));
        Assert.That(result2, Is.EqualTo(expected));
        Assert.That(result3, Is.EqualTo(expected));
    }
    // TestCraftSuccessMessage
    [Test]
    public void TestCraftSuccessMessage()
    {
        // Put resources in Player inventory
        player.Inventory.PutMultipleItems(items);
        string result = commandProcessor.Execute(player, new string[] { "craft", "woodensword" });
        string expected = "Wooden Sword is crafted and added to your inventory";
        Assert.That(result, Is.EqualTo(expected));
    }
    // TestNotEnoughIngredientMessage
    [Test]
    public void TestNotEnoughIngredientMessage()
    {
        // Put resources in Player inventory
        //player.Inventory.PutMultipleItems(items);
        string result = commandProcessor.Execute(player, new string[] { "craft", "woodensword" });
        string expected = "You need 2 x wood to craft Wooden Sword";
        Assert.That(result, Is.EqualTo(expected));
    }
}
