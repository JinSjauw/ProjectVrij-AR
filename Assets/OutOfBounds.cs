using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] GameObject VoidZoneUp;
    [SerializeField] GameObject VoidZoneDown;
    BlurCamera CameraBlur;

    // Start is called before the first frame update
    void Start()
    {
        CameraBlur = GameObject.Find("CameraBlur").GetComponent<BlurCamera>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "MainCamera")
        {
            VoidZoneUp.SetActive(true);
            VoidZoneDown.SetActive(true);
        
            CameraBlur.Blur(true);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "MainCamera")
        {
            VoidZoneUp.SetActive(false);
            VoidZoneDown.SetActive(false);

            CameraBlur.Blur(false);
        }
    }
}
