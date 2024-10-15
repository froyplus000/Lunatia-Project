using System;
namespace LunatiaProject.Core
{
	public abstract class GameObject : IdentifiableObject
	{
		// Fields
		private string _description;
		private string _name;

		// Properties
		public string Name
		{
			get { return _name; }
		}
		public string ShortDescription
		{
            get { return string.Format("a {0} ({1})", _name, FirstId); }
        }
		public virtual string FullDescription
		{
			get { return _description; }
		}


		// Contructor
		public GameObject(string[] ids, string name, string desc) : base(ids)
        {
			_name = name;
			_description = desc;
		}
	}
}

