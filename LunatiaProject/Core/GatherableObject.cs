using System;
using LunatiaProject.Interfaces;
using LunatiaProject.Enum;
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
        public bool IsDepleted()
        {
            return _resourceAmount <= 0; // Check if the object has any resources left
        }
    }
}