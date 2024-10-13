using System;
namespace Swin_Adventure_Iteration_8
{
	public class IdentifiableObject
	{
        // Fields
        private List<string> _identifiers;

        // Property
        public string FirstId
        {
            // Fix from 2.4P - If there are no identifiers in the list, assign "" to the first ID
            get { return _identifiers.Count > 0 ? _identifiers.First() : ""; } // Read-only
        }

        // Constructor
        public IdentifiableObject(string[] identifiers)
		{
            // Initialise with Lowercase String reguardless of user input using ForEach loop
            _identifiers = new List<string>();
            foreach (string id in identifiers)
            {
                AddIdentifier(id);
            }

        }

        // Methods
        public bool AreYou(string identifier)
        {
            // Method to check if the list contains any of string input in the method
            return _identifiers.Contains(identifier.ToLower());
        }
        


        public void AddIdentifier(string identifier)
        {
            // Method that change new string input to lowercase and add it to a list
            _identifiers.Add(identifier.ToLower());
        }

        public void PrivilegeEscalation(string pin)
        {
            // if input Pin match last 4 digits of studentID, will replace firstID in _identifiers with tutorialID "25"
            string studentId = "103883220";
            string lastFourDigits = studentId.Substring(studentId.Length - 4);
            if (pin == lastFourDigits)
            {
                _identifiers[0] = "25";
            }
        }

    }
}

