﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class BombermanTests
{
    private GameObject game; // Stores Instance of Entire Game
    private Player[] players;

    // Method for getting reference to player by index
    public Player GetPlayer(int index)
    {    
        // Loops through all players from SetUp function
        foreach (var player in players)
        {
            // Compares the playerNumber with given Index
            if(player.playerNumber == index)
            {
                // Returns that player
                return player;
            }
        }
        // All else fails, return null
        return null;
    }

    [SetUp]
    public void SetUp()
    {
        GameObject gamePrefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(gamePrefab);
        players = Object.FindObjectsOfType<Player>();
    }

    // >> TESTS GO HERE <<
    [UnityTest]
    public IEnumerator PlayerDropsBomb()
    {
        // Get the First Player
        Player player1 = GetPlayer(1);

        // Simulate Bomb Dropping
        player1.DropBomb();

        // Wait for the last frame
        yield return new WaitForEndOfFrame();

        // Check if Bomb Exists in the Scene
        Bomb bomb = Object.FindObjectOfType<Bomb>();

        // Bomb is not null
        Assert.IsTrue(bomb != null, "The Bomb didn't spawn");

        // Pauses the Editor
        Debug.Break();
    }

    [UnityTest]
    public IEnumerator PlayerMovement()
    {
        Player player1 = GetPlayer(1);

        Vector3 oldPosition = player1.transform.position;

        player1.Move(true, false, false, false);

        yield return new WaitForEndOfFrame();

        Vector3 newPosition = player1.transform.position;

        Assert.IsTrue(oldPosition != newPosition);
    }

    [TearDown]
    public void TearDown()
    {
        // Remove the Game from the Scene
        Object.Destroy(game);
    }
}
