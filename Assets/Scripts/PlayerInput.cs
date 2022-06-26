using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] SpawnableManager SpawnableManager;
    [SerializeField] GameObject blurPlane;

    List<ARRaycastHit> hitList = new List<ARRaycastHit>();

    public TextMeshProUGUI debugLog;

    RaycastHit hit;
    LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        hitList = new List<ARRaycastHit>();
        mask = LayerMask.GetMask("Interactable");
    }

     void HandleInput() 
    {
        if (Input.touchCount == 0)
            return;

        if (Input.touchCount == 1)
        {
            //Tapping

            //Spawning object
            if (raycastManager.Raycast(Input.GetTouch(0).position, hitList))
            {
                debugLog.text = "TOUCH";
                if (Input.GetTouch(0).phase == TouchPhase.Began && SpawnableManager.canSpawn)
                {
                    debugLog.text = "PLACING OBJECT";
                    SpawnableManager.PlaceObject(hitList);
                }
            }
            
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hitObject;
                debugLog.text = "TAPPING FOR INTERACTABLES";
                if(Physics.Raycast(ray, out hitObject, mask))
                {   
                    debugLog.text = "IN THE INTERACTABLE LAYER " + hitObject.collider.name;
                        debugLog.text = "LOCKKKKHEART";
                        
                        //blurPlane.SetActive(true);
                        //Get the "IInteractable" component from the object. Call the Interact() function
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
            SpawnableManager.Scale(difference * 0.05f + 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
}