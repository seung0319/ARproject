using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompassManager : MonoBehaviour
{
    public TextMeshProUGUI compassText; // TextMeshPro 컴포넌트를 참조하는 변수

    void Start()
    {
        Input.compass.enabled = true; // 나침반 활성화
    }

    void Update()
    {
        compassText.text = "Compass: " + Input.compass.trueHeading.ToString(); // 나침반 값 출력
    }
}
