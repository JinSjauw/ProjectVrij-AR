using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class SpawnableManager : MonoBehaviour
{

    public TextMeshProUGUI debugLog;
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARAnchorManager anchorManager;

    List<ARRaycastHit> hitList = new List<ARRaycastHit>();
    List<GameObject> spawnedList = new List<GameObject>();
    [SerializeField]
    GameObject spawnablePrefab;

    Camera arCam;
    GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        arCam = FindObjectOfType<Camera>();
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void PlaceObject(ARAnchor roomAnchor) 
    {
        GameObject gameObject = Instantiate(spawnablePrefab, hitList[0].pose.position, Quaternion.identity);
        if (gameObject.GetComponent<ARAnchor>() == null) 
        {
            gameObject.transform.SetParent(roomAnchor.transform);
        }

        spawnedList.Add(gameObject);
        debugLog.text = "SPAWNING";
    }
    void Scale(float scaling)
    {
        debugLog.text = "SCALING" + spawnedList.Count.ToString();
        foreach (GameObject gameObject in spawnedList)
        {
            debugLog.text = "scaling: " + scaling;
            gameObject.transform.localScale *= scaling;
        }

    }

    void HandleInput() 
    {
        if (Input.touchCount == 0)
            return;

        //Tapping
        if (Input.touchCount == 1)
        {
            debugLog.text = "TAPPING";
            if (raycastManager.Raycast(Input.GetTouch(0).position, hitList))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit))
                    {
                        if (spawnedList.Count < 1) 
                        {
                            ARAnchor roomAnchor = anchorManager.AttachAnchor(hitList[0].trackable.GetComponent<ARPlane>(), hitList[0].pose);
                            debugLog.text = roomAnchor.ToString() + "ROOMANCHOR";
                            if (roomAnchor != null) 
                            {
                                PlaceObject(roomAnchor);
                            }
                        }
                        
                    }
          
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        spawnedObject = null;
                    }
                }
            }
        }

        //Pinching
        if (Input.touchCount == 2)
        {
            debugLog.text = "PINCHING";
            Debug.Log("PINCHING");
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Scale(difference * 0.05f + 1f);
        }
    }

    public List<GameObject> getSpawnedList() 
    {
        return this.spawnedList;
    }

}
