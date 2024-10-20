using System;
using System.Text.Json;
using LunatiaProject.Interfaces;
using LunatiaProject.ItemAndInventory;
namespace LunatiaProject.Factory
{

	public class ItemFactory : IItemFactory
	{
        // Field
        private string _itemFilePath;

        // Property
        public string ItemFilePath
        {
            get { return _itemFilePath; }
        }

        // Construtor
        public ItemFactory(string itemFilePath)
        {
            _itemFilePath = itemFilePath;
        }

        // Method

        public Item CreateItem(string id, string name, string desc)
        {
            return new Item(new string[] {id}, name, desc);
        }

        public List<Item> CreateItems(string id, string name, string desc, int amount)
		{
			List<Item> items = new List<Item>();
			for (int i = 0; i < amount; i++)
			{
				Item item = new Item(new string[] {id}, name, desc);
				items.Add(item);
			}
			return items;
        }

        public List<Item> CreateItemsFromFile()
        {
            List<Item> items = new List<Item>();

            try
            {
                // Read the JSON file as a string
                string jsonString = File.ReadAllText(ItemFilePath);

                // Deserialize the JSON data into a list of RecipeJson objects
                var itemJsonList = JsonSerializer.Deserialize<List<ItemJson>>(jsonString);

                // Convert each RecipeJSON entry into a Recipe object
                foreach (var itemJson in itemJsonList)
                {
                    Item item = new Item(
                        itemJson.Id,
                        itemJson.Name,
                        itemJson.Desc
                    );
                    items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading items: " + ex.Message);
            }
            return items;
        }

        // Method For working with JSON
        private class ItemJson
        {
            public string[] Id { get; set; }  // Matches the "id" in JSON (array of strings)
            public string Name { get; set; }   // Matches the "name" in JSON
            public string Desc { get; set; }   // Matches the "desc" in JSON

        }
    }
}

