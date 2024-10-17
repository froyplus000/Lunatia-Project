using System;
using LunatiaProject.Core;
namespace LunatiaProject.Interfaces
{
	public interface IGatherableObjectFactory
	{
		List<GatherableObject> CreateGatherableObject(string type, int amount);
	}
}

