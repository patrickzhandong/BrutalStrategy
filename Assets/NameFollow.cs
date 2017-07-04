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

        rt = GetComponent<RectTransform>();
        canvasRT = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        roboScreenPos = camera.WorldToViewportPoint(robot.transform.TransformPoint(roboPos));
        rt.anchorMax = roboScreenPos;
        rt.anchorMin = roboScreenPos;
    }

    // Update is called once per frame
    void Update()
    {
        roboScreenPos = camera.WorldToViewportPoint(robot.transform.TransformPoint(roboPos));
        rt.anchorMax = roboScreenPos;
        rt.anchorMin = roboScreenPos;
    }
}