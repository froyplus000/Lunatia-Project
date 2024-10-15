using System;
using LunatiaProject.Core;
namespace LunatiaProject.Interfaces
{
	public interface IHaveInventory
	{
        // Any class that implements IHaveInventory must have readonly Name property
        string Name { get; }
        // Any class that implements IHaveInventory must have a Locate Method
        GameObject Locate(string id);
    }
}

