using LunatiaProject.ItemAndInventory;
using LunatiaProject.Factory;


namespace LunatiaProjectTest;

public class Recipe_Test
{
    private RecipeFactory recipeFactory;
    private RecipeBook recipeBook;
    private Recipe recipe;
    private List<Recipe> recipes;
    private string filepath;

    [SetUp]
    public void Setup()
    {
        filepath = "../../../Data/RecipesTestData.json";
        recipeFactory = new RecipeFactory(filepath);
        recipeBook = RecipeBook.Instance;
        recipes = new List<Recipe>();

        recipes = recipeFactory.CreateRecipe();
        recipeBook.AddAllRecipe(recipes);
    }

    // Tests

    [Test]
    public void TestLocateRecipeByRecipeId()
    {
        recipe = recipeBook.Locate("woodensword-r");
        Assert.That(recipe, Is.Not.Null);
    }
    [Test]
    public void TestGetRecipeByRecipeName()
    {
        string result = recipeBook.GetRecipeByName("Wooden Sword Recipe");
        string expected = "Requires:\n\t2 x wood\n\t1 x fiber\nTo Craft : Wooden Sword\n";
        Assert.That(result, Is.EqualTo(expected));
    }
    [Test]
    public void TestGetRecipeByItemName()
    {
        string result = recipeBook.GetRecipeByName("Wooden Sword");
        string expected = "Requires:\n\t2 x wood\n\t1 x fiber\nTo Craft : Wooden Sword\n";
        Assert.That(result, Is.EqualTo(expected));
    }
    [Test]
    public void TestGetRecipeByRecipeId()
    {
        string result = recipeBook.GetRecipeByName("woodensword-r");
        string expected = "Requires:\n\t2 x wood\n\t1 x fiber\nTo Craft : Wooden Sword\n";
        Assert.That(result, Is.EqualTo(expected));
    }
}
