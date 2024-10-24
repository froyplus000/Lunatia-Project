
using LunatiaProject.LivingObject;

public class StoryManager
{
    // Fields
    private Dictionary<string, string> _storyData;
    private bool _setupDone;
    private bool _gateDone;
    private bool _entranceDone;
    private bool _centreDone;
    private bool _upperDone;
    private bool _lowerDone;
    private bool _licenseDone;
    private bool _potionDone;
    private bool _luminaraDone;


    // Property
    public Dictionary<string, string> StoryData
    {
        get { return _storyData; }
        set { _storyData = value; }
    }

    // Constructor
    public StoryManager(string filePath)
    {
        _storyData = new Dictionary<string, string>();
        LoadStoryFromFile(filePath);
        _setupDone = false;
        _gateDone = false;
        _entranceDone = false;
        _centreDone = false;
        _upperDone = false;
        _lowerDone = false;
        _licenseDone = false;
        _potionDone = false;
        _luminaraDone = false;
    }

    // Method to read the story from the file
    private void LoadStoryFromFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            string storyTrigger = null;
            string storyText = "";

            foreach (string line in lines)
            {
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    // If we encounter a new trigger section, store the previous trigger's story
                    if (storyTrigger != null && storyText.Length > 0)
                    {
                        _storyData[storyTrigger] = storyText.Trim();
                    }

                    // Set the new trigger
                    storyTrigger = line.Trim('[', ']');
                    storyText = "";
                }
                else
                {
                    // Collect the story text for the current location
                    storyText += line + "\n";
                }
            }

            // Add the final story text for the last location
            if (storyTrigger != null && storyText.Length > 0)
            {
                _storyData[storyTrigger] = storyText.Trim();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading story file: " + ex.Message);
        }
    }

    public void GetStory(string trigger)
    {
        if (_storyData.ContainsKey(trigger))
        {
            string story = _storyData[trigger];

            // Split the story by line breaks to display line by line
            string[] lines = story.Split('\n');

            foreach (var line in lines)
            {
                Console.WriteLine("\n" + line);  // Display the line
                Thread.Sleep(700);  // Delay time before next line of story
            }
        }
        else
        {
            Console.WriteLine("Story not found for this location.");
        }
    }

    public void CheckStory(Player player)
    {

        if (player.Location.Name == "Lunatia Forest" && _setupDone == false)
        {
            GetStory("Setup");
            _setupDone = true;
        }

        if (player.Location.Name == "Lunatia City Front Gate" && _gateDone == false)
        {
            GetStory("Lunatia City Front Gate");
            _gateDone = true;
        }

        if (player.Location.Name == "Lunatia City Entrance" && _entranceDone == false)
        {
            GetStory("Lunatia City Entrance");
            _entranceDone = true;
        }

        if (player.Location.Name == "Lunatia City Centre" && _centreDone == false)
        {
            GetStory("Lunatia City Centre");
            _centreDone = true;
        }

        if (player.Location.Name == "Lunatia Forest Upper" && _upperDone == false)
        {
            GetStory("Lunatia Forest Upper");
            _upperDone = true;
        }

        if (player.Location.Name == "Lunatia Forest Lower" && _lowerDone == false)
        {
            GetStory("Lunatia Forest Lower");
            _lowerDone = true;
        }

        if (player.Location.Name == "Luminara City of Light" && _luminaraDone == false)
        {
            GetStory("Luminara City of Light");
            _luminaraDone = true;
        }

        if (player.Inventory.HasItem("license") && _licenseDone == false)
        {
            GetStory("Obtained License");
            _licenseDone = true;
        }

        if (player.Inventory.HasItem("clearpotion") && _potionDone == false)
        {
            GetStory("Obtained Clear Potion");
            _potionDone = true;
        }
    }
}