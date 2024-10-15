using System;
using LunatiaProject.Core;
using LunatiaProject.ItemAndInventory;

namespace LunatiaProject.Map
{
	public class Path : GameObject
	{
        // Fields
        private Location _from;
        private Location _destination;
        private bool _isLocked;
        private Item _key;

        // Property
        public Location From
        {
            get { return _from; }
        }
        public Location Destination
        {
            get { return _destination; }
        }
        public Item Key
        {
            get { return _key; }
        }
        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        // Constructors
        public Path(string[] ids, string name, string description, Location from, Location destination) : base(ids, name, description)
        {
            _from = from;
            _destination = destination;
            IsLocked = false;
        }

        public Path(string[] ids, string name, string description, Location from, Location destination, Item key) : base(ids, name, description)
        {
            _from = from;
            _destination = destination;
            _key = key;
            IsLocked = true;
        }
    }
}

