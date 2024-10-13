using System;
namespace Swin_Adventure_Iteration_8
{
	public interface IHaveInventory
	{
        // Any class that implements IHaveInventory must have readonly Name property
        string Name { get; }
        // Any class that implements IHaveInventory must have a Locate Method
        GameObject Locate(string id);
    }
}

