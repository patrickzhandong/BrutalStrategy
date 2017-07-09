using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private const int MAP_SELECT_ELEMENT_HEIGHT = 30;

    // Start menu asset references
    public GameObject startPanel;

    // Main menu asset references
    public GameObject menuPanel;
    public GameObject displayPanel;

    // Map select main asset references
    public GameObject mapSelectPanel;
    public GameObject mapSelectLoadLabel;
    public GameObject mapSelectElement;

    public void onStart()
    {
        menuPanel.SetActive(true);
        displayPanel.SetActive(true);
        startPanel.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void onDirectMode()
    {
        mapSelectPanel.SetActive(true);
        menuPanel.SetActive(false);
        populateMapSelect();
    }

    public void onMapSelectBack()
    {
        menuPanel.SetActive(true);
        mapSelectPanel.SetActive(false);
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void populateMapSelect()
    {
        mapSelectLoadLabel.SetActive(true);
        MapManager.getMapManager().initialize();
        mapSelectLoadLabel.SetActive(false);

        RectTransform rowRectTransform = mapSelectElement.GetComponent<RectTransform>();
        RectTransform containerRectTransform = mapSelectPanel.GetComponent<RectTransform>();

        int numElements = MapManager.getMapManager().getMapCount();
        float scrollHeight = numElements * MAP_SELECT_ELEMENT_HEIGHT;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

        for (int i = 0; i < numElements; i++)
        {
            GameObject element = Instantiate(mapSelectElement) as GameObject;
            element.name = "MapSelectElement" + i;
            element.transform.SetParent(mapSelectPanel.transform, false);

            RectTransform rectTransform = element.GetComponent<RectTransform>();

            float x = -containerRectTransform.rect.width / 2;
            float y = containerRectTransform.rect.height / 2 - (MAP_SELECT_ELEMENT_HEIGHT * i);
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x;
            y = rectTransform.offsetMin.y + MAP_SELECT_ELEMENT_HEIGHT;
            rectTransform.offsetMax = new Vector2(x, y);
        }
    }

}
