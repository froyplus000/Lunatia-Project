﻿using System;
using LunatiaProject.LivingObject;

namespace LunatiaProject.Command
{
	public class CommandProcessor : Command
	{
		// Fields
        private List<Command> _commands;

		// Constructor
        public CommandProcessor() : base(new string[] { "command" })
        {
            _commands = new List<Command>();
            _commands.Add(new LookCommand());
            _commands.Add(new MoveCommand());
            _commands.Add(new GatherCommand());
            _commands.Add(new PickUpCommand());
            _commands.Add(new DropCommand());
            _commands.Add(new CraftCommand());
            _commands.Add(new HelpCommand());

        }

        // Method
        public override string Execute(Player p, string[] text)
        {
            foreach (Command command in _commands)
            {
                if (command.AreYou(text[0]))
                {
                    return command.Execute(p, text);
                }
            }
            return "Error Command Input, Please enter your command again.";
        }
    }
}

