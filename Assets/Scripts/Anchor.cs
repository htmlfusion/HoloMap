using UnityEngine;
using System.Collections;

using UnityEngine.VR.WSA;
using UnityEngine.VR.WSA.Persistence;

public class Anchor : MonoBehaviour {

    GameObject appManager;
    AnchorStoreService anchorService;

    bool anchorStoreLoaded = false;
    bool isPlacing = false;

	// Use this for initialization
	void Start () {
        appManager = GameObject.Find("AppManager");
        anchorService = appManager.GetComponent<AnchorStoreService>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AnchorStoreReady()
    {
        anchorStoreLoaded = true;
    }

    public void RemoveAnchor()
    {
        Debug.Log("Remove Anchor Called");
        if (gameObject.GetComponent<WorldAnchor>())
        {
            Debug.Log("removing world anchor on " + gameObject.name);
            DestroyImmediate(gameObject.GetComponent<WorldAnchor>());
        }
    }

    public void SetAnchor()
    {
        if (!gameObject.GetComponent<WorldAnchor>())
        {
            Debug.Log("adding world anchor on " + gameObject.name);
            var anchor = gameObject.AddComponent<WorldAnchor>();

            if (anchorStoreLoaded)
            {
                anchorService.SaveAnchorLocation(gameObject.name, anchor);
            }
            else
            {
                Debug.Log("The WorldAnchorStore has not loaded yet. Anchor not saved!");
            }
        }
        Debug.Log("Setting new room position");
        GameObject.Find("Room").transform.position = transform.position; 
    }

    void OnSelect()
    {
        Debug.Log("this was on selected!");
        isPlacing = !isPlacing;

        if (isPlacing)
        {
            RemoveAnchor();
        }
        else
        {
            SetAnchor();
        }
    }
}
