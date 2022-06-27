using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] SpawnableManager SpawnableManager;
    [SerializeField] GameObject blurPlane;
    [SerializeField] GameObject InputPanelUI;
    [SerializeField] GameObject reticle;

    bool hasSpawned = false;
    List<ARRaycastHit> hitList = new List<ARRaycastHit>();
    public TextMeshProUGUI debugLog;
    public TextMeshProUGUI debugLog2;
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
                //debugLog.text = "TOUCH";
                if (Input.GetTouch(0).phase == TouchPhase.Began && SpawnableManager.canSpawn)
                {
                    //debugLog.text = "PLACING OBJECT";
                    SpawnableManager.PlaceObject(hitList, reticle.transform.position);
                    reticle.SetActive(false);
                }
            }
            
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject, 10f, mask))
                {   
                    Interactable interactable = hitObject.collider.GetComponent<Interactable>();
                    if(hitObject.collider.tag == "Lock" && interactable != null && interactable.isActive())
                    {
                        InputPanelUI.SetActive(true);
                        InputPanelUI.GetComponent<InputPanel>().setInteractable(interactable);
                    }   
                }
            }
        }

        //Pinching
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            //SpawnableManager.Scale(difference * 0.05f + 1f);
        }
    }

    void CheckForPlanes() 
    {
        Vector3 ray = arCamera.ViewportToScreenPoint(new Vector3(0.5f,0.5f,0.0f));
        if (raycastManager.Raycast(ray, hitList, trackableTypes:TrackableType.PlaneWithinPolygon))
        {
            
            var hitPose = hitList[0].pose;

            if (hasSpawned == false)
            {
                reticle = Instantiate(reticle, hitPose.position, hitPose.rotation);
                //debugLog2.text = "I Spawned";
                hasSpawned = true;
            }
            else
            {
                reticle.transform.position = hitPose.position;
                //debugLog2.text = reticle.transform.position.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        CheckForPlanes();
        debugLog.text = "AR Cam position: " + arCamera.transform.position;
        
    }

    private void OnTriggerEnter(Collider other) {
        debugLog2.text = "Collider: " + other.name;
    }
}
