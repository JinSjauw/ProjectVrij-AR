using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    private ARTrackedImageManager imageManager;

    private void Awake()
    {
        imageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args) 
    {
        foreach (var trackedImage in args.added) 
        {
            Debug.Log(trackedImage.name);
        
        }
    
    }
}
