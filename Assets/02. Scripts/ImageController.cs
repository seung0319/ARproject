using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public float panSpeed = 0.1f;
    public float zoomSpeed = 0.5f;
    public RectTransform canvas;  // Canvas의 RectTransform
    public Vector2 minSize;  // 최소 이미지 크기
    public Vector2 maxSize;  // 최대 이미지 크기

    private RectTransform rectTransform;
    private Vector2[] oldTouchPositions;
    private Vector2 oldTouchVector;
    private float oldTouchDistance;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            oldTouchPositions = new Vector2[0];
        }
        else if (Input.touchCount == 1)
        {
            if (oldTouchPositions.Length != 1)
            {
                oldTouchPositions = new Vector2[] { Input.GetTouch(0).position };
            }
            else
            {
                Vector2 newTouchPosition = Input.GetTouch(0).position;
                Vector2 direction = oldTouchPositions[0] - newTouchPosition;

                rectTransform.anchoredPosition -= direction * panSpeed;

                oldTouchPositions[0] = newTouchPosition;

                // 이미지 위치가 화면 내에 있는지 확인하고, 필요하다면 보정
                Vector2 pos = rectTransform.anchoredPosition;
                Vector2 min = -0.5f * (rectTransform.sizeDelta - canvas.sizeDelta);
                Vector2 max = 0.5f * (rectTransform.sizeDelta - canvas.sizeDelta);
                pos.x = Mathf.Clamp(pos.x, min.x, max.x);
                pos.y = Mathf.Clamp(pos.y, min.y, max.y);
                rectTransform.anchoredPosition = pos;
            }
        }
        else
        {
            if (oldTouchPositions.Length != 2)
            {
                oldTouchPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                oldTouchVector = oldTouchPositions[1] - oldTouchPositions[0];
                oldTouchDistance = oldTouchVector.magnitude;
            }
            else
            {
                Vector2[] newTouchPositions = {
        Input.GetTouch(0).position,
        Input.GetTouch(1).position
    };
                Vector2 newTouchVector = newTouchPositions[1] - newTouchPositions[0];
                float newTouchDistance = newTouchVector.magnitude;

                if (oldTouchPositions.Length != 2)
                {
                    oldTouchPositions = new Vector2[] { newTouchPositions[0], newTouchPositions[1] };
                    oldTouchVector = newTouchVector;
                    oldTouchDistance = newTouchDistance;
                }
                else
                {
                    // 두 손가락의 중심점에 대한 위치 변화를 계산
                    Vector2 oldCenterPoint = (oldTouchPositions[0] + oldTouchPositions[1]) / 2;
                    Vector2 newCenterPoint = (newTouchPositions[0] + newTouchPositions[1]) / 2;
                    Vector2 centerPointDelta = newCenterPoint - oldCenterPoint;

                    // 이미지 크기 변경
                    Vector2 size = rectTransform.sizeDelta * (newTouchDistance / oldTouchDistance);

                    // 이미지 크기가 지정된 범위 내에 있는지 확인하고, 필요하다면 보정
                    size.x = Mathf.Clamp(size.x, minSize.x, maxSize.x);
                    size.y = Mathf.Clamp(size.y, minSize.y, maxSize.y);

                    // 이미지 위치 변경
                    // 이미지 위치가 화면의 70% 영역 내에 있는지 확인하고, 필요하다면 보정
                    Vector2 pos = rectTransform.anchoredPosition;
                    Vector2 min = -0.5f * (rectTransform.sizeDelta - canvas.sizeDelta) * 0.7f;  // 70% 영역으로 제한
                    Vector2 max = 0.5f * (rectTransform.sizeDelta - canvas.sizeDelta) * 0.7f;  // 70% 영역으로 제한
                    pos.x = Mathf.Clamp(pos.x, min.x, max.x);
                    pos.y = Mathf.Clamp(pos.y, min.y, max.y);

                    // 변경된 이미지 크기와 위치를 적용
                    rectTransform.sizeDelta = size;
                    rectTransform.anchoredPosition = pos;

                    // 이전 터치 위치와 벡터, 거리를 업데이트
                    oldTouchPositions = new Vector2[] { newTouchPositions[0], newTouchPositions[1] };
                    oldTouchVector = newTouchVector;
                    oldTouchDistance = newTouchDistance;

                }
            }
        }
    }
}
