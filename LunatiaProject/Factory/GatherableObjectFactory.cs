using System;
using LunatiaProject.Interfaces;
using LunatiaProject.Core;
using LunatiaProject.Enum;

namespace LunatiaProject.Factory
{
	public class GatherableObjectFactory : IGatherableObjectFactory
	{
        // Fields
        private Random _random;

        // Constrcutor
        public GatherableObjectFactory()
		{
            _random = new Random();
        }

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
                            new GatherableObject(new string[] { "tree" }, "Tree", "A normal tree that gives you wood", randomResourceAmount, ResourceType.Wood)
                        );
                        break;
                    case "rock":
                        gatherableObjects.Add(
                            new GatherableObject(new string[] { "rock" }, "Rock", "A normal rock that gives you stone", randomResourceAmount, ResourceType.Stone)
                        );
                        break;
                    case "grass":
                        gatherableObjects.Add(
                            new GatherableObject(new string[] { "grass" }, "Grass", "A normal grass that gives you fiber", randomResourceAmount, ResourceType.Fiber)
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

