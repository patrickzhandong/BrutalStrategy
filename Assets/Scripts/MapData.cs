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
        public unitType type; 
    };
    
    public string name;
    
    public int width;
    
    public int height;
    
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
}
