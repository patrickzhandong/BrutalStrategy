using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class MapManager {

    private string READ_PATH = Path.Combine(Application.dataPath, "Data\\Maps");

    // Singleton instance
    private static MapManager instance;
    // Global map list
    private static List<MapData> mapList;

    public static MapManager getMapManager()
    {
        if (instance == null) instance = new MapManager();
        return instance;
    }

    public bool initialize()
    {
        formatMapList();
        return loadMapData(READ_PATH);
    }

    private void formatMapList()
    {
        mapList = new List<MapData>();
    }

    private bool loadMapData(string dirPath)
    {
        Debug.Log("Beginning data load.");
        XmlSerializer serializer = new XmlSerializer(typeof(MapData));
        FileStream input;

        string[] files = Directory.GetFiles(dirPath, "*.xml");
        for (int i = 0; i < files.Length; i++)
        {
            try
            {
                Debug.Log(files[i]);
                input = new FileStream(files[i], FileMode.Open);
                mapList.Add(serializer.Deserialize(input) as MapData);
                Debug.Log(mapList[0].getWidth());
                input.Close();
            }
            catch (System.Exception e)
            {
                return false;
            } 
        }

        Debug.Log("Data loaded successfully.");
        return true;
    }

    public int getMapCount()
    {
        return mapList.Count;
    }

    public MapData getMapByIndex(int i)
    {
        try
        {
            return mapList[i];
        }
        catch (System.IndexOutOfRangeException e)
        {
            return null;
        }
    }
}
