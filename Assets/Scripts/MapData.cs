using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("MapData")]
public class MapData {

    public struct UnitData
    { 
        public enum unitType {MARKSMAN, GRENADIER, FENCER};
        public int x;
        public int y;
        public int faction;
        public unitType type; 
    };
    
    public string name;
    
    public int width;
    
    public int height;

    public int factions;
    
    [XmlArray("tiles")]
    [XmlArrayItem("int")]
    public List<int> tiles;

    [XmlArray("units")]
    [XmlArrayItem("UnitData")]
    public List<UnitData> units;

    public string getName()
    {
        return name;
    }

    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }

    public int getNumUnits()
    {
        return units.Count;
    }

    public int getTileID(int x, int y)
    {
        try
        {
            return tiles[x + (y * width)];
        }
        catch (System.IndexOutOfRangeException e)
        {
            return -1;
        }
    }

    public int getUnitX(int index)
    {
        return units[index].x;
    }

    public int getUnitY(int index)
    {
        return units[index].y;
    }

    public int getUnitFaction(int index)
    {
        return units[index].faction;
    }

    public UnitData.unitType getUnitType(int index)
    {
        return units[index].type;
    }
}


