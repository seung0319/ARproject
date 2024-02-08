using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

// Summaray : ���� GPS ��� ����ϱ� ���� ��ũ��Ʈ

public class GPSManager : MonoBehaviour
{
    public static GPSManager Instance { set; get; }

    public float latitude;
    public float longitude;

    public TextMeshProUGUI LatitudeText; // TextMeshProUGUI�� �����ϱ� ���� ����
    public TextMeshProUGUI LongitudeText; // TextMeshProUGUI�� �����ϱ� ���� ����
    public TextMeshProUGUI StatusText; // TextMeshProUGUI�� �����ϱ� ���� ����

    POIdata data = new POIdata("�繰������", "���ƿ�.", 0,0,0,0);
    List<POIdata> POIlist = new List<POIdata>();
    private void Start()
    {
        Instance = this;
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        Input.location.Start(1, 1); // ������Ʈ �ֱ⸦ 1�ʷ�, �ּ� �Ÿ��� 1�� ����
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        yield break;
    }

    private void Update()
    {
        StatusText.text = "Status : " + Input.location.status;

        if (Input.location.status == LocationServiceStatus.Running)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            LatitudeText.text = "Latitude: " + latitude;
            LongitudeText.text = "Longitude: " + longitude;
        }
    }
}
