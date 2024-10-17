using System;
using LunatiaProject.Interfaces;
using LunatiaProject.Enum;
using LunatiaProject.ItemAndInventory;
using System.Security.AccessControl;

namespace LunatiaProject.Core
{
    public class GatherableObject : GameObject, IGatherable
    {
        // Fields
        private int _resourceAmount;
        private Enum.ResourceType _resourceType;

        // Properties
        public int ResourceAmount
        {
            get { return _resourceAmount; }
        }
        public Enum.ResourceType ResourceType
        {
            get { return _resourceType; }
        }

        // Constructor
        public GatherableObject(string[] ids, string name, string desc, int resourceAmount, Enum.ResourceType resourceType)
            : base(ids, name, desc)
        {
            _resourceAmount = resourceAmount; // Initialize the resource amount
            _resourceType = resourceType;
        }

        // Method

        public Item CreateItem()
        {
            // Convert ResourceType to string to use as an ID
            string resourceId = ResourceType.ToString();

            // Determine the item based on the ResourceType
            switch (this.FirstId)
            {
                case "tree":
                    return new Item(new string[] { resourceId }, "Wood", "A piece of wood.");
                case "rock":
                    return new Item(new string[] { resourceId }, "Stone", "A piece of stone.");
                case "grass":
                    return new Item(new string[] { resourceId }, "Fiber", "A piece of stone.");
                default:
                    throw new ArgumentException("Unknown resource type.");
            }
        }

    }
}