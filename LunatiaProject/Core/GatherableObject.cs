using System;
using LunatiaProject.Interfaces;

namespace LunatiaProject.Core
{
    public class GatherableObject : GameObject, IGatherable
    {
        // Fields
        private int _resourceAmount;

        // Properties
        public int ResourceAmount
        {
            get { return _resourceAmount; }
        }

        // Constructor
        public GatherableObject(string[] ids, string name, string desc, int resourceAmount)
            : base(ids, name, desc)
        {
            _resourceAmount = resourceAmount; // Initialize the resource amount
        }

        // Method
        public bool IsDepleted()
        {
            return _resourceAmount <= 0; // Check if the object has any resources left
        }
    }
}