using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompassManager : MonoBehaviour
{
    public TextMeshProUGUI compassText; // TextMeshPro ������Ʈ�� �����ϴ� ����

    void Start()
    {
        Input.compass.enabled = true; // ��ħ�� Ȱ��ȭ
    }

    void Update()
    {
        compassText.text = "Compass: " + Input.compass.trueHeading.ToString(); // ��ħ�� �� ���
    }
}
