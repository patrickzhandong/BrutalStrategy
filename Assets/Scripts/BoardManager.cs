using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    // Editor modifiable properties
    public GameObject[] floorTiles;
    public GameObject[] team1Players;
    public GameObject[] team2Players;

    // Map properties
    private Transform boardHolder;
    private List<GameObject> tiles = new List<GameObject>();

    void BoardSetup(MapData map)
    {
        boardHolder = new GameObject("Board").transform;
        if (map == null)
        {
            throw new System.ArgumentException("Cannot set up null map.", "map");
        }
        else
        {
            Debug.Log("Drawing map from file.");
            tiles.Clear();
            for (int x = 0; x < map.getWidth(); x++)
            {
                for (int y = 0; y < map.getHeight(); y++)
                {         
                    GameObject instance = Instantiate(floorTiles[map.getTileID(x, y)], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    tiles.Add(instance);
                }
            }
        }
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(MapData map = null)
    {
        BoardSetup(map);
        Console.Write(team1Players.Length);

    }
}

