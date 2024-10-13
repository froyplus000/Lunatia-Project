using System;
namespace Swin_Adventure_Iteration_8
{
    public abstract class Command : IdentifiableObject
    {
        public Command(string[] ids) : base(ids) { }
        public abstract string Execute(Player p, string[] text);
    }
}
