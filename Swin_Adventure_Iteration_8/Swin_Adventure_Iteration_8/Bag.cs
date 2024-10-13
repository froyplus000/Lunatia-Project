using System;
namespace Swin_Adventure_Iteration_8
{
	public class Bag : Item, IHaveInventory
	{

        // Fields
        private Inventory _inventory;

        // Properties
        public Inventory Inventory
        {
            get { return _inventory; }
        }
        public override string FullDescription
        {
            get
            {
                return string.Format("In the {0} you can see:\n", Name) + Inventory.ItemList;
            }
        }

        // Constructor
        public Bag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new Inventory();
        }

        // Methods

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            else
            {
                return _inventory.Fetch(id);
            }
        }
    }
}

