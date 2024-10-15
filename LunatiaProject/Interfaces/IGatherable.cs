using System;
namespace LunatiaProject.Interfaces
{
	public interface IGatherable
	{
		int ResourceAmount { get; }
		bool IsDepleted();
	}
}

