using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameFollow : MonoBehaviour
{

    public Vector3 pos;

    public GameObject robot;
    public Camera camera;
    private Vector3 roboPos;
    private RectTransform rt;
    private RectTransform canvasRT;
    private Vector3 roboScreenPos;

    // Use this for initialization
    void Start()
    {
        roboPos = robot.transform.position;
        //robot.transform.TransformPoint(roboPos)
        rt = GetComponent<RectTransform>();
        canvasRT = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        roboScreenPos = camera.WorldToViewportPoint(roboPos);
        rt.anchorMax = roboScreenPos + (new Vector3(0.5f, 0.5f, 0.5f));
        rt.anchorMin = roboScreenPos + (new Vector3(0.5f, 0.5f, 0.5f)); ;
    }

    // Update is called once per frame
    void Update()
    {
        roboPos = robot.transform.position;
        roboScreenPos = camera.WorldToViewportPoint(roboPos);
        rt.anchorMax = roboScreenPos + (new Vector3(0.5f, 0.5f, 0.5f)); ;
        rt.anchorMin = roboScreenPos + (new Vector3(0.5f, 0.5f, 0.5f)); ;
    }
}