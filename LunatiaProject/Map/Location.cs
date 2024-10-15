using System;
using LunatiaProject.Core;
using LunatiaProject.Interfaces;
using LunatiaProject.ItemAndInventory;


namespace LunatiaProject.Map
{
	public class Location : GameObject, IHaveInventory
	{
        // Fields
        private Inventory _inventory;
        private List<Path> _paths;

        // Properties
        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public string PathList
        {
            get
            {
                string list = string.Empty;

                switch (_paths.Count)
                {
                    case 1:
                        return string.Format("\tThe only path is {0} in {1} direction", _paths[0].Name, _paths[0].FirstId);
                    default:
                        for (int i = 0; i < _paths.Count; i++)
                        {
                            list += string.Format("\t{0}. {1} in <{2}>\n", i + 1, _paths[i].Name, _paths[i].FirstId);
                        }
                        return list;
                }
            }
        }

        public override string FullDescription
        {
            get
            {
                string locationDescription = string.Format("{0} {1}\n", Name, base.FullDescription);

                locationDescription += "Location contains:\n";
                locationDescription += Inventory.ItemList;
                locationDescription += "Exist Paths:\n";
                locationDescription += PathList;
                return locationDescription;

            }
        }
        // Constructor
        public Location(string[] ids, string name, string description) : base(ids, name, description)
        {
            _inventory = new Inventory();
            _paths = new List<Path>();
        }

        // Methods

        public void AddPath(Path path)
        {
            _paths.Add(path);
        }

        public GameObject Locate(string id)
        {
            return Inventory.Fetch(id);
        }

        public Path LocatePath(string id)
        {
            foreach (Path path in _paths)
            {
                if (path.AreYou(id))
                {
                    return path;
                }
            }
            return null;
        }

    }
}

