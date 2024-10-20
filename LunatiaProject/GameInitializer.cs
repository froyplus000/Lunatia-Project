using System;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.Map;
using LunatiaProject.LivingObject;
using LunatiaProject.Factory;
using LunatiaProject.Core;

namespace LunatiaProject
{
	public class GameInitializer
	{
        // Fields
        private LocationFactory _locationFactory;
		private PathFactory _pathFactory;
		private GatherableObjectFactory _gatherableObjectFactory;

		private RecipeBook _recipeBook;
		private RecipeFactory _recipeFactory;

		private ItemFactory _itemFactory;

		private Player _player;

		public GameInitializer(Player player)
		{
			_player = player;
		}

		// Method

		public void StartGame()
		{
            StarterLocationAndPath();
			StarterRecipe();
			StarterStory();
        }

		private void StarterLocationAndPath()
		{
            _locationFactory = new LocationFactory();
            _pathFactory = new PathFactory();
            List<GatherableObject> gatherableObjects = StarterGatherableObjects();
			List<Item> items = StarterItem();
			// Locations Creation
			Location city = _locationFactory.CreateLocations("lunatiaCity", "Lunatia City", "a main city of Lunatia region");
            Location forest = _locationFactory.CreateLocations("lunatiaForest", "Lunatia Forest", "a myterious forest located in Lunatia region");
            Location forestUpper = _locationFactory.CreateLocations("lunatiaForestUpper", "Lunatia Forest Upper", "an Upper level of Lunatia Forest. Who knows what's lies ahead...");
			// Key Item
            Item cityEntranceLicense = _itemFactory.CreateItem("license", "Lunatia City Entrance License", "a license required to enter the City of Lunatia region");
            Item rope = _itemFactory.CreateItem("rope", "Rope", "a Rope. What can you use this for?");

			// City
            Map.Path city2forest = _pathFactory.CreatePath("west", "Lunatia Forest", "a path from Lunatia City to Lunatia Forest", city, forest);

			city.AddPath(city2forest);

			// Forest

            Map.Path forest2city = _pathFactory.CreatePath("east", "Lunatia City", "a path from Lunatia Forest to Lunatia City", forest, city, cityEntranceLicense);
            Map.Path forest2upper = _pathFactory.CreatePath("uphill", "Lunatia Forest Upper", "a path from Lunatia Forest to Lunatia Forest Upper", forest, forestUpper, rope);

			forest.AddPath(forest2city);
			forest.AddPath(forest2upper);

			forest.AddAllGatherable(gatherableObjects);

			// Forest Upper
            Map.Path upper2forest = _pathFactory.CreatePath("downhill", "Lunatia Forest", "a path from Lunatia Forest Upper to Lunatia Forest", forestUpper, forest, rope);

			forestUpper.AddPath(upper2forest);
			forestUpper.AddAllGatherable(gatherableObjects);
			forestUpper.Inventory.PutMultipleItems(items);

			forestUpper.Inventory.Put(cityEntranceLicense);

			// Player Starting location is Forest
			_player.Location = forest;
        }

        private List<GatherableObject> StarterGatherableObjects()
        {
            _gatherableObjectFactory = new GatherableObjectFactory();
            List<GatherableObject> gatherableObjects = _gatherableObjectFactory.CreateGatherableObject("Tree", 60);
            gatherableObjects.AddRange(_gatherableObjectFactory.CreateGatherableObject("Rock", 40)); // AddRange to add multiple object to this list
            gatherableObjects.AddRange(_gatherableObjectFactory.CreateGatherableObject("Grass", 100));
			return gatherableObjects;
        }

        private void StarterRecipe()
		{
			// Load all recipe from file
            _recipeBook = RecipeBook.Instance;
			string recipeFilePath = "../../../Data/RecipesData.json";
            _recipeFactory = new RecipeFactory(recipeFilePath);
			List<Recipe> allRecipes = _recipeFactory.CreateRecipe();
			// Add all to RecipeBook
            _recipeBook.AddAllRecipe(allRecipes);
			// Put recipeBook into player inventory
			_player.Inventory.Put(_recipeBook);
		}

		private List<Item> StarterItem() // For multiple Items creation
		{
			_itemFactory = new ItemFactory("../../../Data/ItemsData.json");
			List<Item> items = _itemFactory.CreateItemsFromFile();
			return items;
        }

		private void StarterStory()
		{
            Console.WriteLine("\nYou woke up in the middle of no where.\n");
			Thread.Sleep(1500); // delay 1.5 sec
            Console.WriteLine("You can see Tree, Rock and Grass.\n");
            Thread.Sleep(1500);
            Console.WriteLine("You now realised that you are in the some kind of forest.\n");
            Thread.Sleep(1500);
            Console.WriteLine("What can you do now?");
            Thread.Sleep(1500);
        }
		
    }
}

