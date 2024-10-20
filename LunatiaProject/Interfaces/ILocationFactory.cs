using System;
using LunatiaProject.Map;

namespace LunatiaProject.Factory
{
	public class ILocationFactory
	{
        public interface IItemFactory
        {
            Location CreateLocations(string id, string name, string desc);
        }
    }
}

