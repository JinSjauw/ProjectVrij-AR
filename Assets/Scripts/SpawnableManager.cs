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

    //List<ARRaycastHit> hitList = new List<ARRaycastHit>();
    List<GameObject> spawnedList = new List<GameObject>();
    [SerializeField] GameObject spawnablePrefab;
    [SerializeField] GameObject GateLock;
    [SerializeField] Button exitButton;

    //Camera arCam;
    GameObject spawnedObject;
    public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        //canSpawn = true;
        //arCam = FindObjectOfType<Camera>();
        PushTheButton.ButtonPressed += AddDigit;
    }
    
    // Update is called once per frame
    void Update()
    {
        //HandleInput();
        if (numbers.text == "TINA" && numbers.text.Length == 4)
        {
            //Open Door.
            numbers.text = "Correct";
            //numberLock.SetActive(false);
        }
        else if (numbers.text.Length >= 4) 
        {
            numbers.text = "";
        }
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
        debugLog.text = "SPAWNING";
    }

    public void Scale(float scaling)
    {
        debugLog.text = "SCALING" + spawnedList.Count.ToString();
        foreach (GameObject gameObject in spawnedList)
        {
            debugLog.text = "scaling: " + scaling;
            gameObject.transform.localScale *= scaling;
        }
    }

    private void AddDigit(string digit)
    {
        numbers.text += digit;
    }

    private void Dismiss()
    {
        GateLock.SetActive(false);
    } 


    // void HandleInput() 
    // {
    //     if (Input.touchCount == 0)
    //         return;

    //     //Tapping
    //     if (Input.touchCount == 1)
    //     {
    //         //debugLog.text = "TAPPING";
    //         if (raycastManager.Raycast(Input.GetTouch(0).position, hitList))
    //         {
    //             if (Input.GetTouch(0).phase == TouchPhase.Began)
    //             {
    //                 RaycastHit hit;
    //                 Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);
                    
    //                 if (spawnedObject == null)
    //                 {
    //                     if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit))
    //                     {
    //                         debugLog.text = hit.transform.name;
    //                         if (spawnedList.Count < 1)
    //                         {
    //                             ARAnchor roomAnchor = anchorManager.AttachAnchor(hitList[0].trackable.GetComponent<ARPlane>(), hitList[0].pose);
    //                             debugLog.text = roomAnchor.ToString() + "ROOMANCHOR";
    //                             if (roomAnchor != null)
    //                             {
    //                                 PlaceObject(roomAnchor);
    //                             }
    //                         }
    //                     }
    //                     if (Input.GetTouch(0).phase == TouchPhase.Ended)
    //                     {
    //                         spawnedObject = null;
    //                     }
    //                 }
    //                 if (Physics.Raycast(ray, out hit))
    //                 {
    //                     NumberLock aLock = hit.transform.GetComponent<NumberLock>();

    //                     if (aLock != null) 
    //                     {
    //                         GateLock.SetActive(true);
    //                         //debugLog.text = "NOT NULL";
    //                     }
    //                 }
    //             }
    //         }
    //     }

    //     //Pinching
    //     if (Input.touchCount == 2)
    //     {
    //         debugLog.text = "PINCHING";
    //         Debug.Log("PINCHING");
    //         Touch touchZero = Input.GetTouch(0);
    //         Touch touchOne = Input.GetTouch(1);

    //         Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
    //         Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

    //         float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
    //         float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

    //         float difference = currentMagnitude - prevMagnitude;

    //         Scale(difference * 0.05f + 1f);
    //     }
    // }

    public List<GameObject> getSpawnedList() 
    {
        return this.spawnedList;
    }

}
