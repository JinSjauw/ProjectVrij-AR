using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;

public class SpawnableManager : MonoBehaviour
{

    public TextMeshProUGUI debugLog;
    public TextMeshProUGUI numbers;
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARAnchorManager anchorManager;
    List<GameObject> spawnedList = new List<GameObject>();
    [SerializeField] GameObject spawnablePrefab;
    //Camera arCam;
    GameObject spawnedObject;
    public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        //PushTheButton.ButtonPressed += AddDigit;
    }

    public void PlaceObject(List<ARRaycastHit> hitList) 
    {
        ARAnchor roomAnchor = anchorManager.AttachAnchor(hitList[0].trackable.GetComponent<ARPlane>(), hitList[0].pose);
        GameObject gameObject = Instantiate(spawnablePrefab, hitList[0].pose.position, Quaternion.identity);
        
        if (gameObject.GetComponent<ARAnchor>() == null) 
        {
            gameObject.transform.SetParent(roomAnchor.transform);
            canSpawn = false;
        }
        spawnedList.Add(gameObject);
        //debugLog.text = "SPAWNING";
    }

    public void Scale(float scaling)
    {
        //debugLog.text = "SCALING" + spawnedList.Count.ToString();
        foreach (GameObject gameObject in spawnedList)
        {
            //debugLog.text = "scaling: " + scaling;
            gameObject.transform.localScale *= scaling;
        }
    }

    public List<GameObject> getSpawnedList() 
    {
        return this.spawnedList;
    }

}
