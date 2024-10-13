﻿using Swin_Adventure_Iteration_8;
using Path = Swin_Adventure_Iteration_8.Path; // Needed this line to able to use Path from our program, Path name is conflict with System.IO.Path

namespace TestIdentifiableObject;

public class Iteration_7_Test
{
    private Player player;
    private Location myroom;
    private Location livingroom;
    private Location kitchen;

    private Item key;
    private Item tv;
    private Item knife;

    private LookCommand lookCommand;
    private MoveCommand moveCommand;


    private Path myroom2livingroom;
    private Path livingroom2myroom;

    private Path myroom2kitchen;
    private Path kitchen2myroom;

    [SetUp]
    public void Setup()
    {
        // location and items
        myroom = new Location(new string[] { "myroom" }, "My Room", "Player's personal room");
        key = new Item(new string[] { "key" }, "Key", "Key to unlock kitchen door");
        livingroom = new Location(new string[] { "livingroom" }, "Living Room", "Comfortable Living Room");
        tv = new Item(new string[] { "tv" }, "Television", "55 inch smart television");
        livingroom.Inventory.Put(tv);
        kitchen = new Location(new string[] { "kitchen" }, "Kitchen Room", "Modern style Kitchen");
        knife = new Item(new string[] { "knife" }, "Japanese Knife", "High quality japanese knife");
        kitchen.Inventory.Put(knife);

        myroom2livingroom = new Path(new string[] { "north" }, "to living room", "Walking path to living room in North direction", myroom, livingroom);
        livingroom2myroom = new Path(new string[] { "south" }, "to my room", "Walking path to my room in South direction", livingroom, myroom);

        myroom2kitchen = new Path(new string[] { "west" }, "to kitchen", "Walking path to kitchen room in West direction", myroom, kitchen, key); // Add key to set this Path to lock and what key to unlock
        kitchen2myroom = new Path(new string[] { "east" }, "to my room", "Walking path to my room in East direction", kitchen, myroom);

        myroom.AddPath(myroom2livingroom);
        myroom.AddPath(myroom2kitchen);
        livingroom.AddPath(livingroom2myroom);
        kitchen.AddPath(kitchen2myroom);

        player = new Player("Folk", "ComSci Student");
        player.Location = myroom;
        //player.Inventory.Put(key);

        lookCommand = new LookCommand();
        moveCommand = new MoveCommand();
    }

    // Tests

    [Test]
    public void TestPlayerMovedToDestination()
    {
        moveCommand.Execute(player, new string[] { "move", "north" });
        Assert.That(livingroom, Is.EqualTo(player.Location));
    }
    [Test]
    public void TestGetPathFromLocation()
    {
        Path expectedPath = myroom2livingroom;
        Path actualPath = myroom.LocatePath("north");
        Assert.That(actualPath, Is.EqualTo(expectedPath));
    }

    [Test]
    public void TestValidPath()
    {
        string result = moveCommand.Execute(player, new string[] { "move", "north" });
        string expected = string.Format("You have moved to : {0}", player.Location.Name);
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void TestInvalidPath()
    {
        string result = moveCommand.Execute(player, new string[] { "move", "invalid_direction" });
        string expected = "Path not exist. You stay at the same place";
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void TestPathIsLocked() // No Key in Player's Inventory, so can't move
    {
        string result = moveCommand.Execute(player, new string[] { "move", "west" });
        string expected = "The path is locked, need key to unlock the door";
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void TestPathUnlockWithKey()
    {
        player.Inventory.Put(key); // Add Key to Player's Inventory
        string result = moveCommand.Execute(player, new string[] { "move", "west" });
        string expected = string.Format("You have moved to : {0}", player.Location.Name);
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void TestNoWhereToMove()
    {
        string result = moveCommand.Execute(player, new string[] { "move"});
        string expected = "Where do you want to move to?";
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void TestCommandLenghtOf2()
    {
        string result = moveCommand.Execute(player, new string[] { "move", "north" });
        string expected = string.Format("You have moved to : {0}", player.Location.Name);
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void TestCommandLenghtOf3()
    {
        string result = moveCommand.Execute(player, new string[] { "move", "to", "north" });
        string expected = string.Format("You have moved to : {0}", player.Location.Name);
        Assert.That(expected, Is.EqualTo(result));
    }

}
