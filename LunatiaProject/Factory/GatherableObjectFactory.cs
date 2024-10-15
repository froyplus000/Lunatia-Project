using System;
using LunatiaProject.Interfaces;
using LunatiaProject.Core;

namespace LunatiaProject.Factory
{
	public class GatherableObjectFactory : IGatherableObjectFactory
	{
        // Fields
        //private int _resourceAmount;
        private Random _random;

        // Properties
        //public int ResourceAmount
        //{
        //    get { return _resourceAmount; }
        //    set { value = _resourceAmount; }
        //}

        // Constrcutor
        public GatherableObjectFactory()
		{
            //Random rnd = new Random();
            //int randomNumberInRange = rnd.Next(2, 6);
            //ResourceAmount = randomNumberInRange;

            _random = new Random();
        }

        // Method
        //public void CreateGatherableObject(string type, int amount)
        //{
        //    switch (type.ToLower())
        //    {
        //        case "tree":
        //            new GatherableObject(new string[] {"tree"}, "Tree", "A normal tree thats give you woods", _resourceAmount);
        //            break;

        //    }  
        //}

        public List<GatherableObject> CreateGatherableObject(string type, int amount)
        {
            List<GatherableObject> gatherableObjects = new List<GatherableObject>();

            for (int i = 0; i < amount; i++)
            {
                int randomResourceAmount = _random.Next(2, 6); // Random amount between 2 and 6

                switch (type.ToLower())
                {
                    case "tree":
                        gatherableObjects.Add(
                            new GatherableObject(new string[] { "tree" }, "Tree", "A normal tree that gives you wood", randomResourceAmount)
                        );
                        break;

                    // You can add more case statements for different gatherable objects here, such as "rock", "bush", etc.
                    default:
                        throw new ArgumentException(string.Format("Unknown gatherable object type: {0}", type));
                }
            }

            return gatherableObjects; // Return the list of created objects
        }

    }
}

