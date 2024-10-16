﻿using System;
using LunatiaProject.Core;
using LunatiaProject.Interfaces;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.Map;


namespace LunatiaProject.LivingObject
{
	public class Player : GameObject, IHaveInventory
	{
		// Fields
		private Inventory _inventory;
        private Location _location;

        // Properties
        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public override string FullDescription
		{
			get
			{
				string playerDescription = string.Format("You are {0} {1}\n", Name, base.FullDescription);

                playerDescription += "You are carrying\n";
                playerDescription += Inventory.ItemList;
                return playerDescription;
			}
		}

        // Constructor
        public Player(string name, string desc) : base (new string[] {"me", "inventory"}, name, desc)
		{
			_inventory = new Inventory();

		}

		// Methods
		public GameObject Locate(string id)
		{
			id = id.ToLower();
			if (AreYou(id) || Name == id)
			{
				return this;
			}
			else if (Inventory.HasItem(id))
			{
				return Inventory.Fetch(id);
			}
            else
			{
				return null;
			}
		}

        public void Move(Map.Path path)
        {
            if (path.Destination != null)
            {
                Location = path.Destination;
            }
        }

		public void Gather(GatherableObject gatherableObject)
		{
			// Add item based multiple time based on resource amount
            for (int i = 0; i < gatherableObject.ResourceAmount; i++)
			{
				// Create Item and add to player inventory
				Inventory.Put(gatherableObject.CreateItem());
			}
        }

    }
}

