using System;
namespace Swin_Adventure_Iteration_8
{
	public class Player : GameObject, IHaveInventory
	{
		// Fields
		private Inventory _inventory;
        private Location _location;

        // Properties
        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public override string FullDescription
		{
			get
			{
				string playerDescription = string.Format("You are {0} {1}\n", Name, base.FullDescription);

                playerDescription += "You are carrying\n";
                playerDescription += Inventory.ItemList;
                return playerDescription;
			}
		}

        // Constructor
        public Player(string name, string desc) : base (new string[] {"me", "inventory"}, name, desc)
		{
			_inventory = new Inventory();

		}

		// Methods
		public GameObject Locate(string id)
		{
			id = id.ToLower();
			if (AreYou(id) || Name == id)
			{
				return this;
			}
			else if (Inventory.HasItem(id))
			{
				return Inventory.Fetch(id);
			}
            else
			{
				return null;
			}
		}

        public void Move(Path path)
        {
            if (path.Destination != null)
            {
                Location = path.Destination;
            }
        }
    }
}

