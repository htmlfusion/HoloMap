using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using System.Collections.Generic;

using UnityEngine.VR.WSA;
using UnityEngine.VR.WSA.Persistence;

public class MappingManager : Singleton<MappingManager> {

    GameObject worldAnchorsDir;
    GameObject miniWorldAnchorsDir;
    GameObject matchingMiniAnchor;
    GameObject marker;

    List<GameObject> anchors = new List<GameObject>();
    List<GameObject> miniAnchors= new List<GameObject>();
    bool anchorStoreReady = false;

	// Use this for initialization
	void Start () {
        worldAnchorsDir = GameObject.Find("WorldAnchors");
        miniWorldAnchorsDir = GameObject.Find("MiniAnchors");
        marker = GameObject.Find("Marker");
	}
	
	// Update is called once per frame
	void Update () {
        var userPosition = Camera.main.transform.position;

        if (anchorStoreReady)
        {
            var closestAnchor = GetClosestAnchor(anchors, userPosition);

            var relPostion = closestAnchor.position - userPosition;
            //var heading = testRoomAnchor.transform.position - userPosition;
            //var relPosition = roomAnchorTest.transform.position - userPosition;

            //var distanceToAnchor = relPosition.magnitude;

            //Debug.Log("here is the distance to anchor: + " + distanceToAnchor);

            UpdateMarker(relPostion, closestAnchor.name);
        }
    }

    public void AnchorsReady()
    {
        foreach (Transform child in worldAnchorsDir.transform)
        {
            var go = child.transform.gameObject;
            anchors.Add(go);
        }
        foreach (Transform miniChild in miniWorldAnchorsDir.transform)
        {
            var miniGo = miniChild.transform.gameObject;
            miniAnchors.Add(miniGo);
        }
        anchorStoreReady = true;
    }

    Transform GetClosestAnchor (List<GameObject> anchorList, Vector3 userPos)
    {
        Transform nearestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = userPos;

        foreach(GameObject possibleTarget in anchorList)
        {
            Vector3 directionToTarget = possibleTarget.transform.position - userPos;
            float disSqrToTarget = directionToTarget.sqrMagnitude;
            if (disSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = disSqrToTarget;
                nearestTarget = possibleTarget.transform;
            }
        }
        Debug.Log("the nearest anchor is: " + nearestTarget.name);
        return nearestTarget;
    }

    void UpdateMarker(Vector3 relPos, string miniAnchorName)
    {
        var scaledDistance = relPos * 0.03125f;
        Debug.Log("scaled dis: " + scaledDistance);
        foreach (GameObject mini in miniAnchors)
        {
            if (mini.name == miniAnchorName)
            {
                matchingMiniAnchor = mini;
            }
        }
       var scaledPos = matchingMiniAnchor.transform.position - scaledDistance;

        marker.transform.position = new Vector3(scaledPos.x, scaledPos.y, scaledPos.z);
    }
}
