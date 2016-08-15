using UnityEngine;
using System.Collections;

using HoloToolkit.Unity;
using UnityEngine.VR.WSA;
using UnityEngine.VR.WSA.Persistence;

public class AnchorStoreService : Singleton<AnchorStoreService> {

    WorldAnchorStore anchorStore;
    WorldAnchor worldAnchor;

    GameObject worldAnchorsDir;
    bool storeLoaded;

	void Start () {
        // get WorldAnchors directory
        worldAnchorsDir = GameObject.Find("WorldAnchors");

        WorldAnchorStore.GetAsync(StoreLoaded);
	}

    // sets all found anchors when WAS has loaded
    void StoreLoaded(WorldAnchorStore store)
    {
        anchorStore = store;
        storeLoaded = true;

        foreach (Transform child in worldAnchorsDir.transform)
        {
            var go = child.transform.gameObject;

            var thisAnchor = store.Load(go.name, go);
            if (!thisAnchor)
            {
                Debug.Log("No saved anchors have been found");
            }
            else
            {
                Debug.Log("Anchor " + thisAnchor.name + " has been placed");
            }

            // notifies children the store is ready
            if (go.GetComponent<Anchor>())
            {
                go.GetComponent<Anchor>().AnchorStoreReady();
            }
        }
    }

    public void SaveAnchorLocation(string anchorName, WorldAnchor anchor)
    {
        Debug.Log("save anchor called");

        // ** attempting to save a key that already exists will fail, not overwrite! **
        // if key id exists in anchor store, delete first, then add new reference
        string[] ids = anchorStore.GetAllIds();
        foreach (string id in ids)
        {
            if (id == anchorName)
            {
                Debug.Log("anchor key already exists...deleting " + id);
                anchorStore.Delete(id);
            }
        }
        var savedAnchor = anchorStore.Save(anchorName, anchor);
    }
}
