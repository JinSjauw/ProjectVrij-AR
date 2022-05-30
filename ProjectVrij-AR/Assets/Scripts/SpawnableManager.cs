using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hitList = new List<ARRaycastHit>();
    List<GameObject> spawnedList;
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
        PlaceObject();
    }

    void PlaceObject() 
    {
        if (Input.touchCount == 0)
            return;

        //Tapping
        if(Input.touchCount == 1) 
        {
            if (raycastManager.Raycast(Input.GetTouch(0).position, hitList))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit))
                    {
                        if (hit.collider.gameObject.tag == "Spawnable")
                        {
                            spawnedObject = hit.collider.gameObject;
                        }
                        else
                        {
                            GameObject gameObject = Instantiate(spawnablePrefab, hitList[0].pose.position, Quaternion.identity);
                            spawnedList.Add(gameObject);
                        }
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
                    {
                        spawnedObject.transform.position = hitList[0].pose.position;
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
            Debug.Log("PINCHING");
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Scale(difference * 0.001f);

        }
    }
    void Scale(float scaling)
    {

        foreach (GameObject gameObject in spawnedList)
        {
            gameObject.transform.localScale *= scaling;
        }

    }

    public List<GameObject> getSpawnedList() 
    {
        return this.spawnedList;
    }

}
