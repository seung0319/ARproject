using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


//ARRay를 발사해 palane에 닿으면 Indicator 띄워줌

[RequireComponent(typeof(ARRaycastManager))]
public class PlayingDetection : MonoBehaviour
{
    [SerializeField] Transform indicator;
    ARRaycastManager raycastManager;
    Transform spawnObj;
    float zoomSpeed = 0.1f;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>(); //없으면 null뜸
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void CastRayByScreenTouch() //이동
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
        //Screen 중심에서 ARRay를 전방으로 발사
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        //RaycastHit hitinfo;
        List<ARRaycastHit> hitInfo = new List<ARRaycastHit>(); //리스트로 반환되서 List 사용
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
        //스크린에 두개의 손가락 터치가 있는지 확인
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            //두 터치 간의 현재위치와 이전 위치를 기반으로 거리를 계산
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            //거리 차이를 계산해 핀치 숨 크기 결정
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            //오브젝트의 스케일 조절
            //핀치해 화면을 벌리 때 오브젝트를 확대하고 핀치를 모을 때 오브젝트 축소
            spawnObj.localScale -= Vector3.one * deltaMagnitudeDiff * zoomSpeed;

            //스케일의 최소값과 최대값 제한(옵션)
            spawnObj.localScale = new Vector3
                (Mathf.Clamp(spawnObj.localScale.x, 0.1f, 5f),
                Mathf.Clamp(spawnObj.localScale.y, 0.1f, 5f),
                Mathf.Clamp(spawnObj.localScale.z, 0.1f, 5f)
                );
        }
    }
}
