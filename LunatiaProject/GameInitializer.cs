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

		private StoryManager _storyManager;


		// Constructor
		public GameInitializer(Player player, StoryManager storyManager)
		{
			_player = player;
			_storyManager = storyManager;
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
			Location cityGate = _locationFactory.CreateLocations("lunatiaCityGate", "Lunatia City Front Gate", "a Front Gate to enter Lunatia City");
			Location cityEntrance = _locationFactory.CreateLocations("lunatiaCityEntrance", "Lunatia City Entrance", "an entrance of main city in Lunatia region");
			Location cityCentre = _locationFactory.CreateLocations("lunatiaCityCentre", "Lunatia City Centre", "a centre of Lunatia City");
            Location forest = _locationFactory.CreateLocations("lunatiaForest", "Lunatia Forest", "a myterious forest located in Lunatia region");
            Location forestUpper = _locationFactory.CreateLocations("lunatiaForestUpper", "Lunatia Forest Upper", "an Upper level of Lunatia Forest. Who knows what's lies ahead...");
            Location forestLower = _locationFactory.CreateLocations("lunatiaForestLower", "Lunatia Forest Lower", "an Lower level of Lunatia Forest. Who knows what's lies ahead...");

			
			// Key Item
            Item cityEntranceLicense = _itemFactory.CreateItem("license", "Lunatia City Entrance License", "a license required to enter the City of Lunatia region");
            Item ladder = _itemFactory.CreateItem("ladder", "Ladder", "a ladder, can be used to travel up the hill");
            Item strongrope = _itemFactory.CreateItem("strongrope", "Strong Rope", "a strong rope, can be used to travel down the hill");

			// Story Item
			Item note = _itemFactory.CreateItem("note", "The truth lies beneath.", "To see the city as it truly is, offer the passage and purity it requires. Drop what grants entry and clears the way into the heart of Lunatia.");
		

			// City Gate
            Map.Path citygate2forest = _pathFactory.CreatePath("west", "Lunatia Forest", "a path from Lunatia City Front Gate to Lunatia Forest", cityGate, forest);
            Map.Path citygate2entrance = _pathFactory.CreatePath("east", "Lunatia City Entrance", "a path from Lunatia City to Lunatia City Entrance", cityGate, cityEntrance, cityEntranceLicense);
			cityGate.AddPath(citygate2forest);
			cityGate.AddPath(citygate2entrance);

			// City Entrance
            Map.Path cityentrance2gate = _pathFactory.CreatePath("west", "Lunatia City Front Gate", "a path from Lunatia City Entrance to Lunatia City Front Gate", cityEntrance, cityGate);
            Map.Path cityentrance2centre = _pathFactory.CreatePath("east", "Lunatia City Centre", "a path from Lunatia City Entrance to City Centre", cityEntrance, cityCentre);

            cityEntrance.AddPath(cityentrance2gate);
            cityEntrance.AddPath(cityentrance2centre);

			// City Centre
            Map.Path citycentre2entrance = _pathFactory.CreatePath("west", "Lunatia City Entrance", "a path from Lunatia City Centre to City Entrance", cityCentre, cityEntrance);

            cityCentre.AddPath(citycentre2entrance);

			// Forest

            Map.Path forest2citygate = _pathFactory.CreatePath("east", "Lunatia City Front Gate", "a path from Lunatia Forest to a Lunatia City Front Gate", forest, cityGate);
            Map.Path forest2upper = _pathFactory.CreatePath("uphill", "Lunatia Forest Upper", "a path from Lunatia Forest to Lunatia Forest Upper", forest, forestUpper, ladder);
            Map.Path forest2lower = _pathFactory.CreatePath("downhill", "Lunatia Forest Lower", "a path from Lunatia Forest to Lunatia Forest Lower", forest, forestLower, strongrope);

			forest.AddPath(forest2citygate);
			forest.AddPath(forest2upper);
			forest.AddPath(forest2lower);

			forest.AddAllGatherable(gatherableObjects);

			// Forest Upper
            Map.Path upper2forest = _pathFactory.CreatePath("downhill", "Lunatia Forest", "a path from Lunatia Forest Upper to Lunatia Forest", forestUpper, forest, ladder);

			forestUpper.AddPath(upper2forest);

			forestUpper.Inventory.Put(cityEntranceLicense);

			// Forest Lower
            Map.Path lower2forest = _pathFactory.CreatePath("uphill", "Lunatia Forest", "a path from Lunatia Forest Lower to Lunatia Forest", forestLower, forest, strongrope);

			forestLower.AddPath(lower2forest);
			forestLower.Inventory.PutMultipleItems(items);
			forestLower.Inventory.Put(note);

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

		private List<Item> StarterItem() // Create Ingredient items from file
		{
			_itemFactory = new ItemFactory("../../../Data/FlowersData.json");
			List<Item> items = _itemFactory.CreateItemsFromFile();
			return items;
        }

		private void StarterStory()
		{
			_storyManager.CheckStory(_player);
        }
    }
}

