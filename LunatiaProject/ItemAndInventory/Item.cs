﻿using System;
using LunatiaProject.Core;
namespace LunatiaProject.ItemAndInventory
{
	public class Item : GameObject
	{
        // Constructor
        public Item(string[] ids, string name, string desc) : base(ids, name, desc)
        {
        }
    }
}

