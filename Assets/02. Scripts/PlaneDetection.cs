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

    public float zoomSpeed = 0.5f;
    private Vector2[] oldTouchPositions;
    private Vector2 oldTouchVector;
    private float oldTouchDistance;

    private GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hitinfo
        List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();

        ////Screen의 중심에서 ARRay를 전방으로 발사한다.
        //Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        //if (raycastManager.Raycast(screenCenter, hitInfo, TrackableType.Planes))
        //{
        //    indicator.position = hitInfo[0].pose.position;
        //    indicator.rotation = hitInfo[0].pose.rotation;
        //    indicator.gameObject.SetActive(true);
        //}
        //else
        //{
        //    indicator.gameObject.SetActive(false);
        //}

        // 터치한곳 오브젝트 스폰
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (raycastManager.Raycast(touchPosition, hitInfo, TrackableType.Planes))
            {
                Pose hitPose = hitInfo[0].pose;

                if(spawnedObject == null)
                {
                    spawnedObject = Instantiate(placePrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                    spawnedObject.transform.rotation = hitPose.rotation;
                }
            }
        }

        // XR Environment 환경에서 오브젝트 스폰
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (raycastManager.Raycast(mousePos, hitInfo, TrackableType.Planes))
            {
                Pose hitPose = hitInfo[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(placePrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                    spawnedObject.transform.rotation = hitPose.rotation;
                }
            }
        }

        // AR환경에서 오브젝트 스케일 조정 (손가락 두개)
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 두 터치 간의 현재 위치와 이전 위치를 기반으로 거리를 계산
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position -touchOne.position).magnitude;

            // 거리 차이를 계산하여 핀치 줌 크기를 결정
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 오브젝트 스케일 조정
            spawnedObject.transform.localScale -= Vector3.one * deltaMagnitudeDiff * zoomSpeed;
            
            // 스케일 최소값, 최대값 제한
            spawnedObject.transform.localScale = new Vector3(
                Mathf.Clamp(spawnedObject.transform.localScale.x, 0.1f, 5f),
                Mathf.Clamp(spawnedObject.transform.localScale.y, 0.1f, 5f),
                Mathf.Clamp(spawnedObject.transform.localScale.z, 0.1f, 5f)
            );
 
        }

        // XR Environment 환경에서 오브젝트 스케일 조정 (마우스 휠)
        if (Input.mouseScrollDelta.y != 0)
        {
            float scroll = Input.mouseScrollDelta.y; // 마우스 스크롤 변화량 (+, -)
            if (spawnedObject != null)
            {
                spawnedObject.transform.localScale += Vector3.one * scroll * zoomSpeed;
                spawnedObject.transform.localScale = new Vector3(
                    Mathf.Clamp(spawnedObject.transform.localScale.x, 0.1f, 5f),
                    Mathf.Clamp(spawnedObject.transform.localScale.y, 0.1f, 5f),
                    Mathf.Clamp(spawnedObject.transform.localScale.z, 0.1f, 5f)
                    );
            }
        }
    }
}
