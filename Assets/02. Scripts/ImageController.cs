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
    }
}
