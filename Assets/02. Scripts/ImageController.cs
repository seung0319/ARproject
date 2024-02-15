using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public float panSpeed = 0.1f;
    public float zoomSpeed = 0.5f;
    public RectTransform canvas;  // Canvas�� RectTransform
    public Vector2 minSize;  // �ּ� �̹��� ũ��
    public Vector2 maxSize;  // �ִ� �̹��� ũ��

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

                // �̹��� ��ġ�� ȭ�� ���� �ִ��� Ȯ���ϰ�, �ʿ��ϴٸ� ����
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
                    // �� �հ����� �߽����� ���� ��ġ ��ȭ�� ���
                    Vector2 oldCenterPoint = (oldTouchPositions[0] + oldTouchPositions[1]) / 2;
                    Vector2 newCenterPoint = (newTouchPositions[0] + newTouchPositions[1]) / 2;
                    Vector2 centerPointDelta = newCenterPoint - oldCenterPoint;

                    // �̹��� ũ�� ����
                    Vector2 size = rectTransform.sizeDelta * (newTouchDistance / oldTouchDistance);

                    // �̹��� ũ�Ⱑ ������ ���� ���� �ִ��� Ȯ���ϰ�, �ʿ��ϴٸ� ����
                    size.x = Mathf.Clamp(size.x, minSize.x, maxSize.x);
                    size.y = Mathf.Clamp(size.y, minSize.y, maxSize.y);

                    // �̹��� ��ġ ����
                    // �̹��� ��ġ�� ȭ���� 70% ���� ���� �ִ��� Ȯ���ϰ�, �ʿ��ϴٸ� ����
                    Vector2 pos = rectTransform.anchoredPosition;
                    Vector2 min = -0.5f * (rectTransform.sizeDelta - canvas.sizeDelta) * 0.7f;  // 70% �������� ����
                    Vector2 max = 0.5f * (rectTransform.sizeDelta - canvas.sizeDelta) * 0.7f;  // 70% �������� ����
                    pos.x = Mathf.Clamp(pos.x, min.x, max.x);
                    pos.y = Mathf.Clamp(pos.y, min.y, max.y);

                    // ����� �̹��� ũ��� ��ġ�� ����
                    rectTransform.sizeDelta = size;
                    rectTransform.anchoredPosition = pos;

                    // ���� ��ġ ��ġ�� ����, �Ÿ��� ������Ʈ
                    oldTouchPositions = new Vector2[] { newTouchPositions[0], newTouchPositions[1] };
                    oldTouchVector = newTouchVector;
                    oldTouchDistance = newTouchDistance;

                }
            }
        }
    }
}
