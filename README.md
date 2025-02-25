# Lunatia-Project
This is a Custom Program created for Object Oriented Programming unit (COS20007) to achieve Distinction Grade, which I extends from an exist Swin-Adventure text based RPG game. Lunatia Project is a Console Based RPG game developed with C# language aiming to demonstrate an understanding of OOP concept, Design patterns and Unit Testing with NUnit.

[Watch Design Report Presentation](https://youtu.be/jLiCxVLYoFg) <br>
[Watch Console Demo](https://youtu.be/4FHm9u6Tfo8)

## Design Patterns used in this project
There are three design patterns this project extends from the original Swin-Adventure: Factory, Singleton and Facade pattern. These three-design pattern provide solution to feature this project needed, it helps me to design my project to be more maintainable, well structure and easy to use. I also extends an Command Pattern with more command class like GatherCommand, CraftCommand, PickUpCommand, DropCommand, HelpCommand. As well as make in improvement for existing commands like LookCommand, MoveCommand and CommandProcessor to match features implemented in this project.
### Factory Pattern
The Factory Pattern handled everything about creating object, this including creation of Item, Recipe, Gatherable Object, Location and Path. With these factory class for different type of object, I’m able to add method to create multiple objects at once or even create objects from an external data file like JSON and text file. This helps the creation process of the program to be more organize and improves readability.
### Singleton Pattern
The Singleton Pattern is used to handle every recipe object in one place, which is a recipe book object. It also used to make sure there could be only one recipe book exist in the game. Recipe book object handle everything related to Recipe object, including getting a list of recipes and locating a recipe. Without this object player wouldn’t be able to do these things.
### Facade Pattern
The Facade Pattern had been used for mainly setting up environment for the game. I call methods from many classes including factory classes to make sure the game had been correctly setup as intended. It also calls methods from Story Manager class to output story to player at the start of the game.
### Command Pattern
The Command Pattern had been used to handle different command that player will input to the game. This includes LookCommand, MoveCommand, GatherCommand, CraftCommand, PickUpCommand, DropCommand, HelpCommand and CommandProcessor. These act as a main feature for user interaction with the game my thier inputs.
