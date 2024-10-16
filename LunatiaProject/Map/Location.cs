using System;
using System.Linq; 
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
        private List<GatherableObject> _gatherables;
        

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
                        return string.Format("\tThe only path is {0} in {1} direction\n", _paths[0].Name, _paths[0].FirstId);
                    default:
                        for (int i = 0; i < _paths.Count; i++)
                        {
                            list += string.Format("\t{0}. {1} in <{2}>\n", i + 1, _paths[i].Name, _paths[i].FirstId);
                        }
                        return list;
                }
            }
        }

        public string GatherableList
        {
            get
            {
                // Group gatherables by their Name
                var groupedGatherables = _gatherables
                    .GroupBy(gatherable => gatherable.Name)
                    .Select(gatherableGroup => new
                    {
                        Name = gatherableGroup.Key, // The Name of the gatherable object
                        Count = gatherableGroup.Count(), // The count of gatherables with this Name
                        // also getting their FullDescription and FirstId
                        gatherableGroup.First().FullDescription,
                        gatherableGroup.First().FirstId
                    });

                // Build the formatted string for the gatherable list
                string gatherableList = "";

                foreach (var gatherableGroup in groupedGatherables)
                {
                    gatherableList += string.Format("\t{0} x {1}, {2}. ({3})\n", gatherableGroup.Count, gatherableGroup.Name, gatherableGroup.FullDescription, gatherableGroup.FirstId);
                }

                return gatherableList;
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
                locationDescription += "List of Gatherables:\n";
                locationDescription += GatherableList;
                return locationDescription;

            }
        }
        // Constructor
        public Location(string[] ids, string name, string description) : base(ids, name, description)
        {
            _inventory = new Inventory();
            _paths = new List<Path>();
            _gatherables = new List<GatherableObject>();
        }

        // Methods

        // Add
        public void AddPath(Path path)
        {
            _paths.Add(path);
        }

        public void AddGatherable(GatherableObject gatherable)
        {
            _gatherables.Add(gatherable);
        }

        // Remove
        public void RemoveGatherable(GatherableObject gatherable)
        {
            _gatherables.Remove(gatherable);
        }

        // Locate
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

        public GatherableObject LocateGatherable(string id)
        {
            foreach (GatherableObject gatherable in _gatherables)
            {
                if (gatherable.AreYou(id))
                {
                    return gatherable;
                }
            }
            return null;
        }

    }
}

