using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float zoomSpeed = 5f;
    public RectTransform imageRect; // Raw Image�� RectTransform

    private Vector2[] oldTouchPositions;
    private Vector2 oldTouchVector;
    private float oldTouchDistance;

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

                // Translate camera
                transform.Translate((Vector3)(
                    oldTouchPositions[0] - newTouchPosition) * panSpeed * Time.deltaTime);

                oldTouchPositions[0] = newTouchPosition;
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
                Vector2 screen = new Vector2(Screen.width, Screen.height);

                Vector2[] newTouchPositions = {
                    Input.GetTouch(0).position,
                    Input.GetTouch(1).position
                };
                Vector2 newTouchVector = newTouchPositions[1] - newTouchPositions[0];
                float newTouchDistance = newTouchVector.magnitude;

                // Translate camera
                transform.Translate((Vector3)(
                    (oldTouchPositions[0] + oldTouchPositions[1] - screen) / 2 - (newTouchPositions[0] + newTouchPositions[1]) / 2)
                    * panSpeed * Time.deltaTime);

                // Zoom camera
                Camera.main.fieldOfView += (oldTouchDistance - newTouchDistance) * zoomSpeed * Time.deltaTime;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 30, 90);


                oldTouchPositions = newTouchPositions;
                oldTouchVector = newTouchVector;
                oldTouchDistance = newTouchDistance;
            }
        }
        // ī�޶� ��ġ ������Ʈ �Ŀ�
        Vector3 pos = transform.position;
        Vector3 min = imageRect.rect.min - imageRect.rect.center;
        Vector3 max = imageRect.rect.max - imageRect.rect.center;

        // ī�޶� ��ġ�� �̹��� ��� ���� �ִ��� Ȯ���ϰ�, �ʿ��ϴٸ� ����
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
}