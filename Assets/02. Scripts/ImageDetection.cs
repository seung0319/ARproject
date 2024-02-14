using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageDetection : MonoBehaviour
{
    ARTrackedImageManager imageManager;

    private void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        imageManager.trackedImagesChanged += OnImageTrackedEvent;
    }
    
    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        foreach(ARTrackedImage trackedImage in arg.added)
        {
            Debug.Log(trackedImage.name + " Added");
            string imageName = trackedImage.referenceImage.name;
            Debug.Log(imageName);

            GameObject prefab = Resources.Load<GameObject>(imageName);
            if(prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.transform.SetParent(trackedImage.transform);
            }
        }

        foreach(ARTrackedImage trackedImage in arg.updated)
        {
            Debug.Log(trackedImage.name + " Updated");
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                //trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        foreach(ARTrackedImage trackedImage in arg.removed)
        {
            Debug.Log(trackedImage.name + " Removed");
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }
}
