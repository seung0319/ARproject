using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// AR ray 를 발사하여 Plane에 닿으면 Indicator를 띄워준다! (Indicator Prefab 필요)
[RequireComponent(typeof(ARRaycastManager))]
public class PlaneDetection : MonoBehaviour
{
    [SerializeField] Transform indicator;
    [SerializeField] GameObject placePrefab;
    ARRaycastManager raycastManager;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Screen의 중심에서 ARRay를 전방으로 발사한다.
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        //RaycastHit hitinfo
        List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();

        if (raycastManager.Raycast(screenCenter, hitInfo, TrackableType.Planes))
        {
            indicator.position = hitInfo[0].pose.position;
            indicator.rotation = hitInfo[0].pose.rotation;
            indicator.gameObject.SetActive(true);
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (raycastManager.Raycast(touchPosition, hitInfo, TrackableType.Planes))
            {
                Pose hitPose = hitInfo[0].pose;

                Instantiate(placePrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}
