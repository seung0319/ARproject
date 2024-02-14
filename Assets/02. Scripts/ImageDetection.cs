using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Image Library의 각 이미지에 맞는 3d오브젝트를 Resources폴더에서 불러와 생성
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageDetection : MonoBehaviour
{
    ARTrackedImageManager ImageManager;
    void Awake()
    {
        ImageManager = GetComponent<ARTrackedImageManager>();
        ImageManager.trackedImagesChanged += OnImaageTrackedEvent;
    }
    void OnImaageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        foreach(ARTrackedImage trackedImage in arg.added)
        {
            string imagename = trackedImage.referenceImage.name;
            Debug.Log(imagename);
            GameObject prefab = Resources.Load<GameObject>(imagename);

            if(prefab != null)
            {
                GameObject obj = Instantiate(prefab,trackedImage.transform.position, trackedImage.transform.rotation);
                obj.transform.SetParent(trackedImage.transform);
            }
        }

        foreach(ARTrackedImage trackedImage in arg.updated)
        {
            if (trackedImage.transform.childCount >0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        foreach(ARTrackedImage trackedImage in arg.removed)
        {
            if(trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        ImageManager.trackedImagesChanged -= OnImaageTrackedEvent;
    }
}
