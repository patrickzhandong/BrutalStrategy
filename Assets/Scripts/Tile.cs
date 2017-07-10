using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool passable;

    private int x, y;
    private GameObject unit = null;

    public int getX()
    {
        return x;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public int getY()
    {
        return y;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public bool hasUnit()
    {
        return unit != null;
    }

    public GameObject getUnit()
    {
        return unit;
    }

    public void setUnit(GameObject unit)
    {
        this.unit = unit;
    }

}
