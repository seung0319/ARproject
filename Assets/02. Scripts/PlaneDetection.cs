using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// AR ray �� �߻��Ͽ� Plane�� ������ Indicator�� ����ش�! (Indicator Prefab �ʿ�)
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

        ////Screen�� �߽ɿ��� ARRay�� �������� �߻��Ѵ�.
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

        // ��ġ�Ѱ� ������Ʈ ����
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

        // XR Environment ȯ�濡�� ������Ʈ ����
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

        // ARȯ�濡�� ������Ʈ ������ ���� (�հ��� �ΰ�)
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // �� ��ġ ���� ���� ��ġ�� ���� ��ġ�� ������� �Ÿ��� ���
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position -touchOne.position).magnitude;

            // �Ÿ� ���̸� ����Ͽ� ��ġ �� ũ�⸦ ����
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ������Ʈ ������ ����
            spawnedObject.transform.localScale -= Vector3.one * deltaMagnitudeDiff * zoomSpeed;
            
            // ������ �ּҰ�, �ִ밪 ����
            spawnedObject.transform.localScale = new Vector3(
                Mathf.Clamp(spawnedObject.transform.localScale.x, 0.1f, 5f),
                Mathf.Clamp(spawnedObject.transform.localScale.y, 0.1f, 5f),
                Mathf.Clamp(spawnedObject.transform.localScale.z, 0.1f, 5f)
            );
 
        }

        // XR Environment ȯ�濡�� ������Ʈ ������ ���� (���콺 ��)
        if (Input.mouseScrollDelta.y != 0)
        {
            float scroll = Input.mouseScrollDelta.y; // ���콺 ��ũ�� ��ȭ�� (+, -)
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
