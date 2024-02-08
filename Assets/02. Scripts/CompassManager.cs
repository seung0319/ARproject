using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompassManager : MonoBehaviour
{
    public TextMeshProUGUI compassText; // TextMeshPro ������Ʈ�� �����ϴ� ����
    public Image compassImage;

    void Start()
    {
        Input.compass.enabled = true; // ��ħ�� Ȱ��ȭ
    }

    int roundedHeading;
    float smoothness = 0.05f;
    void Update()
    {
        roundedHeading = Mathf.RoundToInt(Input.compass.trueHeading);
        compassText.text = "Compass: " + roundedHeading.ToString(); // ��ħ�� �� ���
        //compassImage.transform.rotation = Quaternion.Euler(0, 0, -roundedHeading);
        
        // �ʹ� ��鸮�⶧���� �������� �ε巴�� �������
        float angle = Mathf.LerpAngle(compassImage.transform.eulerAngles.z, -roundedHeading, smoothness);
        compassImage.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
