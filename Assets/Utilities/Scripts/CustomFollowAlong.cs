using UnityEngine;
using System.Collections;

public class CustomFollowAlong : MonoBehaviour {

    Vector3 userPosition;
    Vector3 userGazeDirection;
    Vector3 mapLocation;

    public Vector3 offset = new Vector3(0.35f, -0.35f, 0.7f);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        userPosition = Camera.main.transform.position;
        userGazeDirection = Camera.main.transform.forward;
        mapLocation = userPosition + offset; 
    }

    void LateUpdate()
    {
        transform.position = mapLocation;
    }
}
