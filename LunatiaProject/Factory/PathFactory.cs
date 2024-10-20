using System;
using LunatiaProject.ItemAndInventory;
using LunatiaProject.Map;
using LunatiaProject.Interfaces;

namespace LunatiaProject.Factory
{
	public class PathFactory : IPathFactory
	{
      
        // Constructor
        public PathFactory()
        {
            
        }

        // Method
        public Map.Path CreatePath(string id, string name, string desc, Location from, Location destination, Item key = null)
        {
            if (key == null)
            {
                return new Map.Path(new string[] { id }, name, desc, from, destination);

            }
            else
            {
                return new Map.Path(new string[] { id }, name, desc, from, destination, key);

            }
        }
    }
}

