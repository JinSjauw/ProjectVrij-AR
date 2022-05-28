using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeAblePrefabs;

    private Dictionary<string, GameObject> spawnedPrefab = new Dictionary<string, GameObject>();
    private ARTrackedImageManager imageManager;

    private void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        foreach (GameObject prefab in placeAblePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.AddComponent<ARAnchor>();
            spawnedPrefab.Add(newPrefab.name, newPrefab);
            newPrefab.SetActive(false);
        }
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
        foreach (ARTrackedImage trackedImage in args.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in args.removed)
        {
            spawnedPrefab[trackedImage.name].SetActive(false);
        }

    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position; 

        GameObject prefab = spawnedPrefab[name];
        prefab.transform.position = position;
        prefab.SetActive(true);

        foreach(GameObject go in spawnedPrefab.Values) 
        {
            if (go.name != name) 
            {
                go.SetActive(false);    
            
            }           
        }
    }
}
