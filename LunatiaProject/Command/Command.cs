using System;
using LunatiaProject.Core;
using LunatiaProject.LivingObject;


namespace LunatiaProject.Command
{
    public abstract class Command : IdentifiableObject
    {
        public Command(string[] ids) : base(ids) { }
        public abstract string Execute(Player p, string[] text);
    }
}
