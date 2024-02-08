using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompassManager : MonoBehaviour
{
    public TextMeshProUGUI compassText; // TextMeshPro 컴포넌트를 참조하는 변수
    public Image compassImage;

    void Start()
    {
        Input.compass.enabled = true; // 나침반 활성화
    }

    int roundedHeading;
    float smoothness = 0.05f;
    void Update()
    {
        roundedHeading = Mathf.RoundToInt(Input.compass.trueHeading);
        compassText.text = "Compass: " + roundedHeading.ToString(); // 나침반 값 출력
        //compassImage.transform.rotation = Quaternion.Euler(0, 0, -roundedHeading);
        
        // 너무 흔들리기때문에 보간으로 부드럽게 만들어줌
        float angle = Mathf.LerpAngle(compassImage.transform.eulerAngles.z, -roundedHeading, smoothness);
        compassImage.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
