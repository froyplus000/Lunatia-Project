using System;
using LunatiaProject.Map;
using LunatiaProject.ItemAndInventory;

namespace LunatiaProject.Interfaces
{
    public interface IPathFactory
    {
        Map.Path CreatePath(string id, string name, string desc, Location from, Location destination, Item key);
    }

}

