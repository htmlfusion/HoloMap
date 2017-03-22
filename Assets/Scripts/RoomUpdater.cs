using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;

public class RoomUpdater : MonoBehaviour, IInputClickHandler
{
    private bool isMoving = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnInputClicked(InputEventData eventData)
    {
        if(isMoving)
        {
            Transform anchors = GameObject.Find("WorldAnchors").transform;
            Vector3 anchor1Pos = anchors.GetChild(0).position;
            Vector3 anchor2Pos = anchors.GetChild(1).position;
            Vector3 anchor3Pos = anchors.GetChild(2).position;
            Vector3 midPoint = (anchor1Pos + anchor2Pos) / 2;

            GameObject room = GameObject.Find("Room");
            room.transform.position = midPoint;


            //Vector3 bottomCorner = new Vector3(anchor1Pos.x, anchor2Pos.y, anchor2Pos.z);
            Vector3 newForward = (anchor1Pos + anchor3Pos) / 2;
            //GameObject.Find("PointVisualizer").transform.position = bottomCorner;
            GameObject.Find("PointVisualizer").transform.position = newForward;

            //room.transform.LookAt((bottomCorner + anchor1Pos)/2);
            room.transform.LookAt(newForward);
        }
        isMoving = !isMoving;
    }
    
}
