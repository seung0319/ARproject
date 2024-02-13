using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


//ARRay�� �߻��� palane�� ������ Indicator �����

[RequireComponent(typeof(ARRaycastManager))]
public class PlayingDetection : MonoBehaviour
{
    [SerializeField] Transform indicator;
    ARRaycastManager raycastManager;
    Transform spawnObj;
    float zoomSpeed = 0.1f;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>(); //������ null��
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void CastRayByScreenTouch() //�̵�
    {

        if (Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Vector2 touchPoint = touch.position;
                List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touchPoint, hitInfo, TrackableType.Planes)) 
                {
                    Pose hitPos = hitInfo[0].pose;
                    if (spawnObj != null)
                    {
                        /*spawnObj.transform.position= hitPos.position;
                        spawnObj.transform.rotation= hitPos.rotation;*/
                        Vector3 touchDeltaPosition = touch.deltaPosition;
                        spawnObj.Translate(new Vector3(touchDeltaPosition.x, 0, touchDeltaPosition.y));
                    }
                }
            }
            //Touch secondTouch = Input.GetTouch(1);
        }
    }

    private void CastRayFromScreenCenter()
    {
        //Screen �߽ɿ��� ARRay�� �������� �߻�
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        //RaycastHit hitinfo;
        List<ARRaycastHit> hitInfo = new List<ARRaycastHit>(); //����Ʈ�� ��ȯ�Ǽ� List ���
        if (raycastManager.Raycast(screenCenter, hitInfo, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            indicator.position = hitInfo[0].pose.position;
            indicator.rotation = hitInfo[0].pose.rotation;
            indicator.gameObject.SetActive(true);
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CastRayFromScreenCenter();
        CastRayByScreenTouch();
        ZoomInOutObjectByTouch();
    }

    private void ZoomInOutObjectByTouch()
    {
        //��ũ���� �ΰ��� �հ��� ��ġ�� �ִ��� Ȯ��
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            //�� ��ġ ���� ������ġ�� ���� ��ġ�� ������� �Ÿ��� ���
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            //�Ÿ� ���̸� ����� ��ġ �� ũ�� ����
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            //������Ʈ�� ������ ����
            //��ġ�� ȭ���� ���� �� ������Ʈ�� Ȯ���ϰ� ��ġ�� ���� �� ������Ʈ ���
            spawnObj.localScale -= Vector3.one * deltaMagnitudeDiff * zoomSpeed;

            //�������� �ּҰ��� �ִ밪 ����(�ɼ�)
            spawnObj.localScale = new Vector3
                (Mathf.Clamp(spawnObj.localScale.x, 0.1f, 5f),
                Mathf.Clamp(spawnObj.localScale.y, 0.1f, 5f),
                Mathf.Clamp(spawnObj.localScale.z, 0.1f, 5f)
                );
        }
    }
}
