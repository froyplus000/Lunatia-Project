using System;
using LunatiaProject.Map;
namespace LunatiaProject.Factory
{
	public class LocationFactory
	{
		// Constructor
		public LocationFactory()
		{
		}

		// Method
		public Location CreateLocations(string id, string name, string desc)
		{
			return new Location(new string[] { id }, name, desc);
        }
    }
}

