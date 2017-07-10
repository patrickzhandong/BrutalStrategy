using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    // Editor modifiable properties
    public GameObject[] floorTiles;
    public GameObject unitPrefab;
    public Camera globalCamera;
    public GameObject uiHolder;
    public GameObject healthBarPrefab;
    public GameObject nameLabelPrefab;

    // Map properties
    private int width;
    private int height;
    private Transform boardHolder;
    private List<GameObject> tiles = new List<GameObject>();
    private List<GameObject> units = new List<GameObject>();

    void BoardSetup(MapData map)
    {   
        if (map == null)
        {
            throw new System.ArgumentException("Cannot set up null map.", "map");
        }
        width = map.getWidth();
        height = map.getHeight();
        boardHolder = new GameObject("Board").transform;
        boardHolder.transform.SetParent(transform);
        Debug.Log("Drawing map from file.");
        tiles.Clear();
        Debug.Log("Populating tiles... ");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {         
                GameObject instance = Instantiate(floorTiles[map.getTileID(x, y)], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                instance.GetComponent<Tile>().setX(x);
                instance.GetComponent<Tile>().setY(y);
                tiles.Add(instance);
            }
        }
        Debug.Log("Tiles complete.");
        int unitCount = map.getNumUnits();
        Debug.Log("Populating units... ");
        for (int i = 0; i < unitCount; i++)
        {
            int x = map.getUnitX(i);
            int y = map.getUnitY(i);
            GameObject unit = Instantiate(unitPrefab, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
            unit.transform.SetParent(boardHolder);
            unit.name = "Player" + (i + 1);
            unit.GetComponent<playerController>().setFaction(map.getUnitFaction(i));
            unit.GetComponent<playerController>().setType(map.getUnitType(i));        

            healthBarPrefab.GetComponent<NameFollow>().robot = unit;
            healthBarPrefab.GetComponent<NameFollow>().camera = globalCamera;
            GameObject health = Instantiate(healthBarPrefab, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
            health.transform.SetParent(uiHolder.transform);
            unit.GetComponent<playerController>().setHealthBar(health);

            nameLabelPrefab.GetComponent<NameFollow>().robot = unit;
            nameLabelPrefab.GetComponent<NameFollow>().camera = globalCamera;
            GameObject label = Instantiate(nameLabelPrefab, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
            label.transform.SetParent(uiHolder.transform);
            unit.GetComponent<playerController>().setNameLabel(label);
            unit.GetComponent<playerController>().updateNickname("Player" + (i + 1));

            // Assign the unit to its corresponding tile.
            tiles[x + (y * width)].GetComponent<Tile>().setUnit(unit);

            units.Add(unit);
        }
        Debug.Log("Units complete.");
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(MapData map = null)
    {
        BoardSetup(map);
    }

    public bool isTilePassable(int x, int y)
    {
        return tiles[x + (y * width)].GetComponent<Tile>().passable;
    }

    public bool tileHasUnit(int x, int y)
    {
        return tiles[x + (y * width)].GetComponent<Tile>().getUnit() != null;
    }

    public GameObject getTileUnit(int x, int y)
    {
        return tiles[x + (y * width)].GetComponent<Tile>().getUnit();
    }

    public void setTileUnit(int x, int y, GameObject unit)
    {
        tiles[x + (y * width)].GetComponent<Tile>().setUnit(unit);
    }
}

